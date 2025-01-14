//using System.Windows.Forms;
using AEM;
using HtmlAgilityPack;
using iText.Kernel.Geom;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MbCore
{
    public static class BookExtensions
    {
        /// <summary>
        /// Loads page information for the current book instance. 
        /// If the book's pages collection is already populated, no action is taken. 
        /// Attempts to load page information from the database if available. 
        /// Otherwise, retrieves the information from the archive website and saves it to the database.
        /// </summary>
        /// <returns>
        /// True if the operation succeeds; otherwise, false.
        /// </returns>
        /// <remarks>
        /// This method is an extension of the <see cref="Book"/> class.       
        /// </remarks>        

        public static void Save(this Book book)
        {
            Trace.TraceInformation($"Saving book {book.RefId} {book.Title}");
            using var ctx = new MatrikelBrowserCTX();
            ctx.Update(book);
            ctx.SaveChanges();
        }

        public static bool LoadEvents(this Book book)
        {
            if (book.Events.Any()) return true; // only load events if necessary            

            using var ctx = new MatrikelBrowserCTX();

            ctx.Entry(book).Collection(b => b.Events).Load();


            return true;
        }

        public static void AddEvent(this Book book, Event evnt)
        {
            using MatrikelBrowserCTX ctx = new();
            book.Events.Add(evnt);
            ctx.Update(book);
            ctx.SaveChanges();
        }


        public static bool LoadPageInfo(this Book book)
        {
            if (book.Pages.Any()) return true; // only load pages if necessary            

            var archiveType = book.Parish.Archive.ArchiveType;

            bool ok = archiveType switch
            {
                ArchiveType.AEM => book.LoadPageInfoAEM().GetAwaiter().GetResult(),
                ArchiveType.MAT => book.LoadPageInfoMAT(),
                _ => false
            };

            if (!ok) Trace.TraceError("Page info load error");
            return ok;
        }


        private async static Task<bool> LoadPageInfoAEM(this Book book)
        {
            using var ctx = new MatrikelBrowserCTX();

            if (ctx.Pages.Any(p => p.Book == book))  // use pages from database if available
            {
                Trace.TraceInformation("book already has page infos");
                ctx.Entry(book).Collection(b => b.Pages).Load(); // -> load them from db
                return true;
            }

            // download the book info from the archive, and update database accordingly
            var infoURLTemplate = book.Parish.Archive.BookInfoUrl;
            var infoURL = infoURLTemplate.Replace("{BOOKID}", book.BookInfoLink);

            Trace.TraceInformation($"download page info for book {book.Title} ({book.Parish.Name}) from archive {book.Parish.Archive}");
            using HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.UserAgent.Clear();
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/131.0.0.0 Safari/537.36");

            //var x = httpClient.DefaultRequestHeaders;

            string bookInfoXML =  httpClient.GetStringAsync(infoURL).GetAwaiter().GetResult();


            if (!string.IsNullOrEmpty(bookInfoXML))
            {
                mets bookInfo = bookInfoXML.ParseXML<mets>() ?? new mets(); //see: https://de.wikipedia.org/wiki/Metadata_Encoding_%26_Transmission_Standard

                // read out start and end dates of the book
                var dates = bookInfo.dmdSec.mdWrap.xmlData.mods.originInfo.dateCreated;
                string? startString = dates.FirstOrDefault(d => d.point == "start")?.Value;
                string? endString = dates.FirstOrDefault(d => d.point == "end")?.Value;

                if (DateOnly.TryParse(startString, out DateOnly StartDate)) book.StartDate = StartDate;
                if (DateOnly.TryParse(endString, out DateOnly EndDate)) book.EndDate = EndDate;

                // read out info about the books pages
                List<string> pageLinks = bookInfo.fileSec.fileGrp.file.Select(p => p.FLocat.href).ToList();
                book.ImageLinkPrefix = FindLongestCommonPrefix(pageLinks);

                book.Pages.Clear(); //should be empty here bit clear anyway
                foreach (var pageInfo in pageLinks) // generate Page objects from the information given in bookInfo.xml
                {
                    book.Pages.Add(new Page
                    {
                        Book = book,
                        ImageId = pageInfo.Substring(book.ImageLinkPrefix.Length), // only store the non constant part to reduce database size
                    });
                };

                ctx.Update(book);
                ctx.SaveChanges();

                
            }
            return true;
        }

        private static bool LoadPageInfoMAT(this Book book)
        {
            using var ctx = new MatrikelBrowserCTX();

            if (ctx.Pages.Any(p => p.Book == book))  // do we already have pages in the database?
            {
                Trace.TraceInformation("book already has page info");
                ctx.Entry(book).Collection(b => b.Pages).Load(); // -> load them from db
                return true;
            }

            // construct the url where we can download information about the book and its pages
            string infoURLTemplate = book.Parish.Archive.BookInfoUrl;
            string infoURL = infoURLTemplate.Replace("{BOOKID}", book.BookInfoLink);
            Trace.TraceInformation($"download page info from archive {infoURL}");

            // download corresponding information and extract from contained script
            try
            {
                var htmlDoc = new HtmlWeb().Load(infoURL);
                var scriptContent = htmlDoc.DocumentNode.SelectSingleNode("//script[contains(text(), 'MatriculaDocView')]")?.InnerText.Split('\n');
                var imageRawData = scriptContent?.Where(s => s.Contains("files")).FirstOrDefault()?.Split(['[', ']'])[1];
                var rawPageInfo = imageRawData?.Split(',');
                if (rawPageInfo == null)
                {
                    Trace.TraceError("Parse Error in page info");
                    return false;
                }

                List<string> imageLinks = [];
                Regex reg = new Regex(@"/image/([A-Za-z0-9+]+)");
                foreach (var entry in rawPageInfo)
                {
                    var match = reg.Match(entry);
                    if (match.Success)
                    {
                        // Extract the Base64 part and decode
                        string base64 = match.Groups[1].Value;

                        var decodedBytes = WebEncoders.Base64UrlDecode(base64);
                        string imageLink = Encoding.UTF8.GetString(decodedBytes);
                        imageLinks.Add(imageLink);
                    }
                    else
                    {
                        Trace.TraceError("Base64 decoding error");
                        return false;
                    }
                }

                book.ImageLinkPrefix = FindLongestCommonPrefix(imageLinks); //store constant information in the book object 
                foreach (var imageLink in imageLinks) // generate Page objects from the information given in bookInfo.xml
                {
                    book.Pages.Add(new Page
                    {
                        Book = book,
                        ImageId = imageLink.Substring(book.ImageLinkPrefix.Length) // only store variable part in each page object. 
                    });
                };
            }
            catch (Exception ex)
            {
                Trace.TraceError($"error downloading / parsing book information");
                Trace.TraceError(ex.ToString());
                return false;
            }

            ctx.Update(book);
            ctx.SaveChanges();
            return true;
        }

        private static string FindLongestCommonPrefix(List<string> strings)
        {
            if (strings == null || strings.Count == 0)
                return string.Empty;

            // Start with the first string as a candidate prefix
            string prefix = strings[0];

            foreach (string str in strings)
            {
                // Compare prefix with the current string and reduce it as needed
                while (!str.StartsWith(prefix))
                {
                    prefix = prefix.Substring(0, prefix.Length - 1);
                    if (prefix == string.Empty) return string.Empty;
                }
            }

            return prefix;
        }
    }
}

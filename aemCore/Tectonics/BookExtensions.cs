using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
//using System.Windows.Forms;
using HtmlAgilityPack;
using iText.Commons.Utils;
using Microsoft.AspNetCore.WebUtilities;


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
        public static bool LoadPageInfo(this Book book)
        {
            if (book.Pages.Any()) return true; // only load pages if necessary

            bool ok = book.Parish.Archive.ArchiveType switch
            {
                ArchiveType.AEM => book.LoadPageInfoAEM(),
                ArchiveType.MAT => book.LoadPageInfoMAT(),
                _ => false
            };

            if (!ok)
            {
                Trace.TraceError("Page info load error");
            }
            return ok;
        }


        private static bool LoadPageInfoAEM(this Book book)
        {
            using var ctx = new MatrikelBrowserCTX();

            if (ctx.Pages.Any(p => p.Book == book))  // do we already have pages in the database?
            {
                Trace.TraceInformation("book already has page infos");
                ctx.Entry(book).Collection(b => b.Pages).Load(); // -> load them from db
                return true;
            }

            var infoURLTemplate = book.Parish.Archive.BookInfoUrl;
            var infoURL = infoURLTemplate.Replace("{BOOKID}", book.BookInfoLink);

            Trace.TraceInformation("download page infos from archive");
            using HttpClient httpClient = new HttpClient();
            string bookInfoXML = httpClient.GetStringAsync(infoURL).GetAwaiter().GetResult();

            if (!string.IsNullOrEmpty(bookInfoXML))
            {
                Mets bookInfo = bookInfoXML.ParseXML<Mets>() ?? new Mets(); //see: https://de.wikipedia.org/wiki/Metadata_Encoding_%26_Transmission_Standard

                List<string> pageLinks = bookInfo.fileSec.fileGrp.file.Select(p => p.FLocat.href).ToList();
                book.ImageLinkPrefix = FindLongestCommonPrefix(pageLinks);

                foreach (var pageInfo in pageLinks) // generate Page objects from the information given in bookInfo.xml
                {
                    book.Pages.Add(new Page
                    {
                        Book = book,
                        ImageId = pageInfo.Substring(book.ImageLinkPrefix.Length),
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

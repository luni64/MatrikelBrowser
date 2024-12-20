using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Xml;
using System.Xml.Serialization;

namespace AEM
{
    internal static class ParseHelpers
    {
        public static Stream ToStream(this string @this)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(@this);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public static T? ParseXML<T>(this string @this) where T : class
        {
            var reader = XmlReader.Create(@this.Trim().ToStream(), new XmlReaderSettings() { ConformanceLevel = ConformanceLevel.Document });
            return new XmlSerializer(typeof(T)).Deserialize(reader) as T;
        }


        public static string FindLongestCommonPrefix(List<string> strings)
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



        public static void LoadPageInfoAEM(this Book book)
        {
            using var ctx = new MatrikelBrowserCTX();

            if (ctx.Pages.Any(p => p.Book == book))  // do we already have pages in the database?
            {
                ctx.Entry(book).Collection(b => b.Pages).Load(); // -> load them
                return;
            }

            var infoURLTemplate = book.Parish.Archive.BookInfoUrl;
            var infoURL = infoURLTemplate.Replace("{BOOKID}", book.BookInfoLink);

            string bookInfoXML;
            if (!File.Exists(book.Title))  // Caching the bookInfo file during development. 
            {
                Trace.WriteLine("download bookInfoFile");
                HttpClient httpClient = new HttpClient();
                bookInfoXML = httpClient.GetStringAsync(infoURL).GetAwaiter().GetResult();
                File.WriteAllText(book.Title, bookInfoXML);
            }
            else
            {
                bookInfoXML = File.ReadAllText(book.Title);
            }


            if (!string.IsNullOrEmpty(bookInfoXML))
            {
                mets bookInfo = bookInfoXML.ParseXML<mets>() ?? new mets(); //see: https://de.wikipedia.org/wiki/Metadata_Encoding_%26_Transmission_Standard

                List<string> pageLinks = bookInfo.fileSec.fileGrp.file.Select(p => p.FLocat.href).ToList();
                book.PageLinkPrefix = FindLongestCommonPrefix(pageLinks);

                foreach (var pageInfo in pageLinks) // generate Page objects from the information given in bookInfo.xml
                {
                    book.Pages.Add(new Page
                    {
                        Book = book,
                        ImageLink = pageInfo.Substring(book.PageLinkPrefix.Length),
                    });                    
                };

                //ctx.Attach(b.dto);
                //ctx.Entry(b.dto).State = EntityState.Modified;
                ctx.Update(book);
                ctx.SaveChanges();
            }
        }
    }
}



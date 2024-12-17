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



        public static void GetAEMPages(this Book b)
        {
            using var ctx = new MatrikelBrowserCTX();

            if (ctx.Pages.Any(p => p.Book == b))  // do we have pages in db
            {
                ctx.Entry(b).Collection(b => b.Pages).Load(); // -> load them
                return;
            }

            var infoURLTemplate = b.Parish.Archive.BookInfoUrl;
            var infoURL = infoURLTemplate.Replace("{BOOKID}", b.BookInfoLink);

            string bookInfoXML;

            if (!File.Exists(b.Title))
            {
                Trace.WriteLine("download bookInfoFile");
                HttpClient httpClient = new HttpClient();
                bookInfoXML = httpClient.GetStringAsync(infoURL).GetAwaiter().GetResult();
                File.WriteAllText(b.Title, bookInfoXML);
            }
            else
            {
                bookInfoXML = File.ReadAllText(b.Title);
            }


            if (!string.IsNullOrEmpty(bookInfoXML))
            {
                mets bookInfo = bookInfoXML.ParseXML<mets>() ?? new mets(); //see: https://de.wikipedia.org/wiki/Metadata_Encoding_%26_Transmission_Standard

                List<string> pageLinks = bookInfo.fileSec.fileGrp.file.Select(p => p.FLocat.href).ToList();
                b.PageLinkPrefix = FindLongestCommonPrefix(pageLinks);

                foreach (var pageInfo in pageLinks) // generate Page objects from the information given in bookInfo.xml
                {
                    Page page = new Page
                    {
                        Book = b,
                        ImageLink = pageInfo.Substring(b.PageLinkPrefix.Length),
                    };
                    b.Pages.Add(page);
                };

                //ctx.Attach(b.dto);
                //ctx.Entry(b.dto).State = EntityState.Modified;
                ctx.Update(b);
                ctx.SaveChanges();
            }
        }
    }
}



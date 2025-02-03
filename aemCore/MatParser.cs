using AEM;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MbCore
{
    internal static class MatParser
    {
        public static List<Country> ParseCountries(string countryInfoURL)
        {
            var result = new List<Country>();

            try
            {
                string html;
                var cachedFilename = (countryInfoURL.Trim('/')).toSafeFilename(); // caching while developing
                if (!File.Exists(cachedFilename))
                {
                    Trace.TraceInformation($"Load Country Metadata from {countryInfoURL}");
                    using var client = new WebClient();
                    html = client.DownloadString(countryInfoURL);
                    File.WriteAllText(cachedFilename, html);
                }
                else
                {
                    Trace.TraceInformation($"Use cached country Metadata from {countryInfoURL}");
                    html = File.ReadAllText(cachedFilename);
                }

                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(html);


                var countryNodes = doc.DocumentNode.SelectNodes("//div[@class='list-group']/a");

                if (countryNodes != null)
                {
                    foreach (var node in countryNodes)
                    {
                        string fullText = node.InnerText.Trim();
                        string country = Regex.Replace(fullText, @"^\d+", "").Trim();
                        string href = node.GetAttributeValue("href", string.Empty).Trim('/').Substring(3);  // remove /de/ from href
                        result.Add(new Country
                        {
                            Name = country,
                            Breadcrumb = href
                        });
                    }
                }
            }
            catch { }
            return result;
        }
        public static List<Archive> ParseArchives(string archiveInfoURL)
        {
            var result = new List<Archive>();

            try
            {
                string html;
                var cachedFilename = (archiveInfoURL.Trim('/')).toSafeFilename(); // caching while developing
                if (!File.Exists(cachedFilename))
                {
                    var url = "https://data.matricula-online.eu/de/" + archiveInfoURL;
                    Trace.TraceInformation($"Load Archive Metadata from {url}");
                    using var client = new WebClient();
                    html = client.DownloadString(url);
                    File.WriteAllText(cachedFilename, html);
                }
                else
                {
                    html = File.ReadAllText(cachedFilename);
                    Trace.TraceInformation($"Use cached archive metadata from {cachedFilename}");
                }

                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(html);


                var archiveNodes = doc.DocumentNode.SelectNodes("//div[@class='list-group']/a");

                if (archiveNodes != null)
                {
                    foreach (var node in archiveNodes)
                    {
                        string fullText = node.InnerText.Trim();
                        string archive = Regex.Replace(fullText, @"^\d+", "").Trim();
                        string href = node.GetAttributeValue("href", string.Empty);

                        result.Add(new Archive
                        {
                            Name = archive,
                            ArchiveType = ArchiveType.MAT,
                            Breadcrumb = href.Substring(archiveInfoURL.Length + 4).Trim('/'),
                            ViewerUrl = "",
                        });
                    }
                }
            }
            catch { }
            return result;
        }
        public static List<Parish> ParseParishes(string parishInfoURL)
        {
            var result = new List<Parish>();

            try
            {
                string html;
                var cachedFilename = (parishInfoURL.Trim('/')).toSafeFilename(); // caching while developing
                if (!File.Exists(cachedFilename))
                {
                    var url = "https://data.matricula-online.eu/de/" + parishInfoURL;
                    Trace.TraceInformation($"Load Parish metadata from {parishInfoURL}");
                    using var client = new WebClient();
                    html = client.DownloadString(url);
                    File.WriteAllText(cachedFilename, html);
                }
                else
                {
                    Trace.TraceInformation($"Use cached parish metadata from {cachedFilename}");
                    html = File.ReadAllText(cachedFilename);
                }

                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(html);

                var parishNodes = doc.DocumentNode.SelectNodes("//div[@class='list-group']/a");

                if (parishNodes != null)
                {
                    foreach (var parishNode in parishNodes)
                    {
                        string fullText = parishNode.InnerText.Trim();
                        string parish = Regex.Replace(fullText, @"\s*&nbsp;\s*|\s*\n\s*", "").Trim();
                        string href = parishNode.GetAttributeValue("href", string.Empty);
                        string refId = href.Substring(parishInfoURL.Length + 4).Trim('/');

                        string place = "", church = "";
                        if (refId.Length == 5 && refId.StartsWith("CB"))
                        {
                            var parts = parish.Split('-');
                            if (parts.Length > 1)
                            {
                                place = parts[0];
                                church = parts[1];
                            }

                        }
                        else
                        {
                            var parts = parish.Split(',');
                            if (parts.Length > 1)
                            {
                                place = parts[0];
                                church = parts[1];
                            }
                        }




                        result.Add(new Parish
                        {
                            Name = parish,
                            Place = place,
                            Church = church,
                            Breadcrumb = refId,
                            RefId = refId,
                        });
                    }
                }
            }
            catch { }
            return result;
        }
        public static List<Book> ParseBooks(string bookInfoURL)
        {
            List<Book> result = [];
            var htmlDoc = GetBookInfo(bookInfoURL);
                      
            var pageNodes = htmlDoc.DocumentNode.SelectNodes("//ul[contains(@class, 'pagination')]//a"); // list of books is paginated if n > 50
            var cnt = 1;

            if (pageNodes != null)
            {
                foreach (var node in pageNodes)
                {
                    string href = node.GetAttributeValue("href", "");

                    // Extract page number using regex
                    Match match = Regex.Match(href, @"\?page=(\d+)");
                    if (match.Success)
                    {
                        int pageNum = int.Parse(match.Groups[1].Value);
                        cnt=Math.Max(cnt, pageNum);                        
                    }
                }
            }

            
            int pageNr = 1;
            while(true)
            {              
                var booksTable = htmlDoc.DocumentNode.SelectSingleNode("//table[@class='table table-bordered w-100']");
                if (booksTable == null) return result;
                var rows = booksTable.SelectNodes(".//tr"); // extract books

                foreach (var row in rows.Skip(1)) // skip header
                {
                    var cells = row.SelectNodes("td");
                    if (cells == null || cells.Count != 4) continue;

                    var REFID = cells[1].InnerText.Trim().Replace("\r", "").Replace("\n", " | ");
                    var title = cells[2].InnerText.Trim().Replace("\r", "").Replace("\n", " | ");
                    var date = cells[3].InnerText.Trim().Replace("\r", "").Replace("\n", " | ");

                    var years = Regex.Matches(date, @"\b\d{4}\b").Select(m => int.Parse(m.Value)).ToList();

                    int startYear = 0, endYear = 0;
                    if (years.Count > 0)
                    {
                        startYear = years.Min();
                        endYear = years.Max();
                    }

                    var linkNode = cells[0].SelectSingleNode(".//a[@href]");
                    var link = linkNode?.GetAttributeValue("href", string.Empty);
                    if (string.IsNullOrEmpty(link)) return result;

                    if (link.Length <= bookInfoURL.Length)
                    {
                        Trace.TraceInformation($"Book {title} skipped");
                        continue;  // book not accessible or other issue, just skip this book
                    }

                    var book = new Book
                    {
                        RefId = REFID,
                        BookType = title.toBookType(),
                        Title = $"{title} {date}",
                        Breadcrumb = link.Substring(bookInfoURL.Length + 4).Trim('/'),
                    };

                    book.StartDate = DateOnly.TryParse($"{startYear}-01-01", out DateOnly sy) ? sy : new DateOnly(1, 1, 1);
                    book.EndDate = DateOnly.TryParse($"{endYear}-01-01", out DateOnly ey) ? ey : new DateOnly(1, 1, 1);
                    result.Add(book);
                }

                if (pageNr == cnt) break;
                Task.Delay(232).GetAwaiter().GetResult();
                
                pageNr++;
                htmlDoc = GetBookInfo(bookInfoURL + $"/?page={pageNr}");
            }
            return result;
        }

        internal static HtmlDocument GetBookInfo(string bookInfoURL)
        {
            string html;

            var cachedFilename = bookInfoURL.Trim('/').toSafeFilename(); // caching while developing
            if (!File.Exists(cachedFilename))
            {
                Trace.TraceInformation($"Load Parish metadata from {bookInfoURL}");
                var url = "https://data.matricula-online.eu/de/" + bookInfoURL;
                using var client = new WebClient();
                html = client.DownloadString(url);
                File.WriteAllText(cachedFilename, html);
            }
            else
            {
                Trace.TraceInformation($"Use cached book metadata from {cachedFilename}");
                html = File.ReadAllText(cachedFilename);
            }

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            return htmlDoc;
        }
    }
}





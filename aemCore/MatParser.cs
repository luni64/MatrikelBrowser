using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

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
                    using var client = new WebClient();
                    html = client.DownloadString(countryInfoURL);
                    File.WriteAllText(cachedFilename, html);
                }
                else html = File.ReadAllText(cachedFilename);

                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(html);


                var countryNodes = doc.DocumentNode.SelectNodes("//div[@class='list-group']/a");

                if (countryNodes != null)
                {
                    foreach (var node in countryNodes)
                    {
                        string fullText = node.InnerText.Trim();
                        string country = Regex.Replace(fullText, @"^\d+", "").Trim();
                        string href = node.GetAttributeValue("href", string.Empty);
                        result.Add(new Country
                        {
                            Name = country,
                            InfoLink = "https://data.matricula-online.eu" + href,
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
                    using var client = new WebClient();
                    html = client.DownloadString(archiveInfoURL);
                    File.WriteAllText(cachedFilename, html);
                }
                else html = File.ReadAllText(cachedFilename);

                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(html);


                var countryNodes = doc.DocumentNode.SelectNodes("//div[@class='list-group']/a");

                if (countryNodes != null)
                {
                    foreach (var node in countryNodes)
                    {
                        string fullText = node.InnerText.Trim();
                        string archive = Regex.Replace(fullText, @"^\d+", "").Trim();
                        string href = node.GetAttributeValue("href", string.Empty);

                        result.Add(new Archive
                        {
                            Name = archive,
                            ArchiveType = ArchiveType.MAT,
                            BookInfoUrl = "https://data.matricula-online.eu" + href,
                            Country = null,
                            ViewerUrl = "asfasdf",

                        });
                    }
                }
            }
            catch { }
            return result;
        }
        public static List<Parish> ParseParishes(string archiveInfoURL)
        {
            var result = new List<Parish>();

            try
            {
                string html;
                var cachedFilename = (archiveInfoURL.Trim('/')).toSafeFilename(); // caching while developing
                if (!File.Exists(cachedFilename))
                {
                    using var client = new WebClient();
                    html = client.DownloadString(archiveInfoURL);
                    File.WriteAllText(cachedFilename, html);
                }
                else html = File.ReadAllText(cachedFilename);

                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(html);


                var countryNodes = doc.DocumentNode.SelectNodes("//div[@class='list-group']/a");

                if (countryNodes != null)
                {
                    foreach (var node in countryNodes)
                    {
                        string fullText = node.InnerText.Trim();
                        string parish = Regex.Replace(fullText, @"\s*&nbsp;\s*|\s*\n\s*", "").Trim();
                        string href = node.GetAttributeValue("href", string.Empty);

                        result.Add(new Parish
                        {
                            Name = parish,
                            Place = parish,
                            BookBaseUrl = href,
                            Archive = null,
                        });
                    }
                }
            }
            catch { }
            return result;
        }
        public static MatParishInfo ParseBooks(string parishInfoURL)
        {
            var result = new MatParishInfo();

            string html;

            var cachedFilename = parishInfoURL.Trim('/').toSafeFilename(); // caching while developing
            if (!File.Exists(cachedFilename))
            {
                using var client = new WebClient();
                html = client.DownloadString(parishInfoURL);
                File.WriteAllText(cachedFilename, html);
            }
            else html = File.ReadAllText(cachedFilename);

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var matrikelCountNode = htmlDoc.DocumentNode.SelectSingleNode("//h3[@id='register-header']");
            var smallNode = matrikelCountNode.SelectSingleNode(".//small");
            string smallText = smallNode.InnerText;
            var matches = Regex.Matches(smallText, @"\b-?\d+\b");
            int total = 0; int loaded = 0;
            //int total = int.ParseBooks(matches[0].Value);
            //int loaded = int.ParseBooks(matches[1].Value);

            var breadCrumbs = htmlDoc.DocumentNode.SelectSingleNode("//ol[@class='breadcrumb']");
            if (breadCrumbs == null) return result;

            var breadcrumbItems = breadCrumbs.SelectNodes(".//li");
            if (breadcrumbItems == null) return result;

            var booksTable = htmlDoc.DocumentNode.SelectSingleNode("//table[@class='table table-bordered w-100']");
            if (booksTable == null) return result;
            var rows = booksTable.SelectNodes(".//tr"); // extract books

            // use the fist book to look up the base url
            var c = rows[1].SelectNodes("td").FirstOrDefault();
            if (c == null) return result;
            var path = Path.GetDirectoryName(c.SelectSingleNode(".//a[@href]").GetAttributeValue("href", string.Empty).Trim('/'))?.Replace('\\', '/');
            if (path == null) return result;

            result.CountryName = breadcrumbItems[1].InnerText.Trim();
            result.ArchiveName = breadcrumbItems[2].InnerText.Trim();
            result.ParishName = breadcrumbItems[3].InnerText.Trim();
            result.BookBaseUrl = "https://data.matricula-online.eu/" + path;
            result.totalNrOfBooks = total;
            result.loadedNrOfBooks = loaded;

            foreach (var row in rows.Skip(1)) // skip header
            {
                var cells = row.SelectNodes("td");
                if (cells == null || cells.Count != 4) continue;

                var REFID = cells[1].InnerText.Trim().Replace("\r", "").Replace("\n", " | ");
                var title = cells[2].InnerText.Trim().Replace("\r", "").Replace("\n", " | ");
                var date = cells[3].InnerText.Trim().Replace("\r", "").Replace("\n", " | ");

                var years = Regex.Matches(date, @"\b\d{4}\b").Select(m => int.Parse(m.Value)).ToList();

                string startYear = "", endYear = "";
                if (years.Count > 0)
                {
                    startYear = years.Min().ToString();
                    endYear = years.Max().ToString();
                }

                var linkNode = cells[0].SelectSingleNode(".//a[@href]");
                var link = linkNode?.GetAttributeValue("href", string.Empty);
                if (string.IsNullOrEmpty(link)) return result;

                result.bookInfos.Add(new MatrikulaBookInfo
                {
                    BookREFID = REFID,
                    Type = title.toBookType(),
                    StartYear = startYear,
                    EndYear = endYear,
                    Title = $"{title} {date}",
                    InfoUrl = link,
                }); ;
            }

            result.isOK = true;
            return result;
        }

    }
}





using HtmlAgilityPack;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace MbCore
{
    internal class MatParishParser
    {
        public MatrikulaParishInfo Parse(Uri parishInfoURL)
        {
            var result = new MatrikulaParishInfo();

            string html;

            var cachedFilename = (parishInfoURL.LocalPath.Trim('/') + parishInfoURL.Query).toSafeFilename(); // caching while developing
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
            //int total = int.Parse(matches[0].Value);
            //int loaded = int.Parse(matches[1].Value);

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





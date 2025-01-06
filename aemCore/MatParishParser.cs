using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace MbCore
{
    internal class MatParishParser
    {
        public string REFID { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string ArchiveName { get; set; } = string.Empty;
        public string ParishName { get; set; } = string.Empty;
        public string Church { get; set; } = string.Empty;
        public string BookBaseUrl { get; set; } = string.Empty;
        public IList<MatrikulaBookInfo> bookInfos { get; set; } = [];
        //public string BookInfoUrl { get; set; }= string.Empty;


        public Parish? Parse(Uri infoURL)
        {
            // caching while developing
            var filename = (infoURL.LocalPath.Trim('/') + infoURL.Query).toSafeFilename();

            string html;
            if (!File.Exists(filename))
            {
                using var client = new WebClient();
                html = client.DownloadString(infoURL);
                File.WriteAllText(filename, html);
            }
            else
                html = File.ReadAllText(filename);

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var breadCrumbs = htmlDoc.DocumentNode.SelectSingleNode("//ol[@class='breadcrumb']");
            if (breadCrumbs == null) return null;

            var breadcrumbItems = breadCrumbs.SelectNodes(".//li");
            if (breadcrumbItems == null) return null;

            var CountryName = breadcrumbItems[1].InnerText.Trim();
            var DioceseName = breadcrumbItems[2].InnerText.Trim();
            var ParishName = breadcrumbItems[3].InnerText.Trim();

            using var ctx = new MatrikelBrowserCTX();

            var country = ctx.Countries.FirstOrDefault(c => c.Name == CountryName) ?? new MbCore.Country { Name = CountryName };
            var archive = ctx.Archives.FirstOrDefault(a => a.Name == DioceseName && a.Country == country) ?? new Archive { Name = DioceseName, Country = country, ArchiveType = ArchiveType.MAT, ViewerUrl = "", BookInfoUrl = "https://data.matricula-online.eu/{BOOKID}", };

            var booksTable = htmlDoc.DocumentNode.SelectSingleNode("//table[@class='table table-bordered w-100']");
            if (booksTable == null) return null;
            var rows = booksTable.SelectNodes(".//tr"); // extract books

            // use the fist book to look up the base url
            var c = rows[1].SelectNodes("td").FirstOrDefault();
            if(c == null) return null;
            var path = Path.GetDirectoryName(c.SelectSingleNode(".//a[@href]").GetAttributeValue("href", string.Empty).Trim('/'))?.Replace('\\', '/');
            if (path == null) return null;

            var parish = new Parish
            {
                Name = ParishName,
                Place = ParishName,
                Church = Church,
                Archive = archive,
                BookBaseUrl = "https://data.matricula-online.eu/" + path
            };

            foreach (var row in rows.Skip(1)) // skip header
            {
                var cells = row.SelectNodes("td");
                if (cells == null || cells.Count != 4) continue;

                var REFID = cells[1].InnerText.Trim().Replace("\r", "").Replace("\n", " | "); ;
                var matrikeltyp = cells[2].InnerText.Trim().Replace("\r", "").Replace("\n", " | "); ;
                var datum = cells[3].InnerText.Trim().Replace("\r", "").Replace("\n", " | "); ;

                var linkNode = cells[0].SelectSingleNode(".//a[@href]");
                var link = linkNode?.GetAttributeValue("href", string.Empty);
                if (string.IsNullOrEmpty(link)) return null;

                parish.Books.Add(new Book
                {
                    RefId = REFID,
                    BookType = matrikeltyp.toBookType(),
                    Title = $"{matrikeltyp} ({datum})",
                    BookInfoLink = link,
                });
            }

            return parish;
        }

        //public void UpdateDB(MatrikelBrowserCTX ctx)
        //{
        //    var country = ctx.Countries.FirstOrDefault(c => c.Name == Country);
        //    if (country == null)
        //    {
        //        country = new Country
        //        {
        //            Name = Country
        //        };
        //        ctx.Countries.Add(country);
        //    }

        //    var archive = ctx.Archives.FirstOrDefault(d => d.Country == country && d.Name == ArchiveName);
        //    if (archive == null)
        //    {
        //        archive = new Archive
        //        {
        //            Country = country,
        //            Name = ArchiveName,
        //            //REFID = REFID,
        //            BookInfoUrl = "https://data.matricula-online.eu/{BOOKID}",
        //            ViewerUrl = "the matricula viewer",
        //            ArchiveType = ArchiveType.MAT,
        //        };
        //        ctx.Archives.Add(archive);
        //    }

        //    var parish = ctx.Parishes.FirstOrDefault(parish => parish.Archive == archive && parish.Name == ParishName);
        //    if (parish == null)
        //    {
        //        parish = new Parish
        //        {
        //            Name = ParishName,
        //            Place = ParishName,
        //            RefId = REFID,
        //            Church = Church,
        //            Archive = archive,
        //            BookBaseUrl = BookBaseUrl,
        //        };
        //        ctx.Parishes.Add(parish);
        //    }

        //    ctx.Entry(parish).Collection(c => c.Books).Load();

        //    foreach (var bookInfo in bookInfos)
        //    {
        //        if (parish.Books.Any(b => b.RefId == bookInfo.REFID)) continue;

        //        parish.Books.Add(new Book
        //        {
        //            Title = bookInfo.Title,
        //            BookType = bookInfo.Type,
        //            RefId = bookInfo.REFID,
        //            BookInfoLink = bookInfo.InfoUrl,
        //            Parish = parish,
        //            Pages = [],
        //        });
        //    }

        //    ctx.AddEvent();
        //}
    }
}





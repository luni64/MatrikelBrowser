using MbCore;
using HtmlAgilityPack;
using System.IO;
using System.Net;
using System.Security.Policy;

namespace OtherRepoTest
{
    internal class MatrikulaParishParser : IParishParser
    {
        public string REFID { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Diocese { get; set; } = string.Empty;
        public string Parish { get; set; } = string.Empty;
        public string Church { get; set; } = string.Empty;
        public string BookBaseUrl { get; set; } = string.Empty;
        public string BookInfoUrl { get; set; }= string.Empty;

        public IList<IBookInfo> bookInfos { get; set; } = [];


        public bool Parse(Uri infoURL)
        {
            // caching while developing
            var filename = (infoURL.LocalPath.Trim('/') + infoURL.Query).toSafeFilename();
                //.Replace('/', '_').Replace('?', '_').Replace('=', '_'); 
            
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
            if (breadCrumbs == null) return false;

            var breadcrumbItems = breadCrumbs.SelectNodes(".//li");
            if (breadcrumbItems == null) return false;

            Country = breadcrumbItems[1].InnerText.Trim();
            Diocese = breadcrumbItems[2].InnerText.Trim();
            Parish = breadcrumbItems[3].InnerText.Trim();            

            var booksTable = htmlDoc.DocumentNode.SelectSingleNode("//table[@class='table table-bordered w-100']");
            if (booksTable == null) return false;

            // extract books
            var rows = booksTable.SelectNodes(".//tr");

            // use the fist book to look up the base url
            var c = rows[1].SelectNodes("td").FirstOrDefault();
            var path = Path.GetDirectoryName(c.SelectSingleNode(".//a[@href]").GetAttributeValue("href", string.Empty).Trim('/')).Replace('\\', '/');

            BookBaseUrl = "https://data.matricula-online.eu/" + path;

            foreach (var row in rows.Skip(1)) // skip header
            {
                var cells = row.SelectNodes("td");

                if (cells == null || cells.Count != 4) continue;

                var REFID = cells[1].InnerText.Trim().Replace("\r\n"," | ");
                var matrikeltyp = cells[2].InnerText.Trim().Replace("\r\n", " | ");
                var datum = cells[3].InnerText.Trim().Replace("\r\n", " | ");

                var linkNode = cells[0].SelectSingleNode(".//a[@href]");
                var link = linkNode?.GetAttributeValue("href", string.Empty);
                if (string.IsNullOrEmpty(link)) return false;

                //var parts = link.Split('/', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                //if (parts.Length != 5) continue;

                bookInfos.Add(new MatrikulaBookInfo
                {
                    REFID = REFID,
                    Type = matrikeltyp.toBookType(),
                    Title = $"{matrikeltyp} ({datum})",
                    //InfoUrl = REFID,
                    InfoUrl =  link // Basis-URL hinzufügen
                });


            }

            return true;
        }

        public void UpdateDB(MatrikelBrowserCTX ctx)
        {
            var country = ctx.Countries.FirstOrDefault(c => c.Name == Country);
            if (country == null)
            {
                country = new Country
                {
                    Name = Country
                };
                ctx.Countries.Add(country);
            }

            var archive = ctx.Archives.FirstOrDefault(d => d.Country == country && d.Name == Diocese);
            if (archive == null)
            {
                archive = new Archive
                {
                    Country = country,
                    Name = Diocese,
                    REFID = REFID,
                    BookInfoUrl = "https://data.matricula-online.eu/{BOOKID}",
                    ViewerUrl = "the matricula viewer",
                    ArchiveType = ArchiveType.MAT,
                };
                ctx.Archives.Add(archive);
            }

            var parish = ctx.Parishes.FirstOrDefault(parish => parish.Archive == archive && parish.Name == Parish);
            if (parish == null)
            {
                parish = new Parish
                {
                    Name = Parish,
                    Place = Parish,
                    RefId = REFID,
                    Church = Church,
                    Archive = archive,
                    BookBaseUrl = BookBaseUrl,                    
                };
                ctx.Parishes.Add(parish);
            }

            ctx.Entry(parish).Collection(c => c.Books).Load();

            foreach (var bookInfo in bookInfos)
            {
                if (parish.Books.Any(b => b.RefId == bookInfo.REFID)) continue;

                parish.AddBook(parish, bookInfo.InfoUrl, bookInfo.Title, bookInfo.REFID, bookInfo.Type);

                //parish.Books.Add(new Book
                //{
                //    Title = bookInfo.Title,
                //    BookType = bookInfo.Type,
                //    RefId = bookInfo.REFID,
                //    BookInfoLink = bookInfo.InfoUrl,
                //    Parish = parish,                    
                //    Pages = [],                    
                //});
            }

            ctx.SaveChanges();
        }
    }
}





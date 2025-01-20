using Interfaces;
using MbCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.IO;
//sing File = System.IO.File;

namespace OtherRepoTest
{
    public class MainVM : BaseViewModel
    {
        private RelayCommand? _cmdtest;
        public RelayCommand cmdTest => _cmdtest ??= new RelayCommand(doTest);

        void doTest(object? obj)
        {
            if (obj is string _url)
            {
                var p = new MatrikulaParishParser();
                _url = @"https://data.matricula-online.eu/de/oesterreich/salzburg/hallein/";

                if (p.Parse(new Uri(_url)) == true)
                {
                    using var ctx = new MatrikelBrowserCTX();
                    p.UpdateDB(ctx);
                }
            }
        }

        private RelayCommand? _cmdTranslate;
        public RelayCommand cmdTranslate => _cmdTranslate ??= new RelayCommand(doTranslate);

        void doTranslate(object? obj)
        {
            if (File.Exists("tectonics.json"))
            {
                using var ctx = new MatrikelBrowserCTX();
                ctx.Database.EnsureDeleted();
                ctx.Database.Migrate();

                var country = ctx.Countries.FirstOrDefault(c => c.Name == "Deutschland");
                if (country == null)
                {
                    country = new Country { Name = "Deutschland" };
                    ctx.Add(country);
                }

                var archive = ctx.Archives.FirstOrDefault(d => d.Country == country && d.Name == "München Freising");
                if (archive == null)
                {
                    archive = new Archive
                    {
                        Name = "München Freising",
                        Country = country,
                        Breadcrumb = "https://digitales-archiv.erzbistum-muenchen.de/actaproweb/mets?id=Rep_{BOOKID}_mets_actapro.xml",
                        ViewerUrl = "https://dfg-viewer.de/show/?tx_dlf[Id]={BOOKURL}",
                        ArchiveType = ArchiveType.AEM,
                    };
                    ctx.Add(archive);
                }

                var json = File.ReadAllText("tectonics.json");
                JArray data = JArray.Parse(json);

                foreach (JObject parishEntry in data)
                {
                    // Extract dynamic values
                    string? id = parishEntry["ID"]?.ToString();
                    string? place = parishEntry["Place"]?.ToString();
                    string? church = parishEntry["Church"]?.ToString();
                    string? startYear = parishEntry["startYear"]?.ToString();
                    string? edYear = parishEntry["endYear"]?.ToString();



                    var parish = new Parish
                    {
                        RefId = id??"",
                        Breadcrumb = "",
                        Archive = archive,
                        Place = place!,
                        Church = church??"",
                        Name = $"{place} {church}",
                        Books = new List<Book>(),
                    };
                    ctx.Add(parish);

                    //Trace.WriteLine($"ID: {Id}, Place: {place}, Church: {church}");

                    // Access nested Books array
                    JArray books = (JArray)parishEntry["Books"]!;
                    foreach (JObject bookInfo in books)
                    {
                         string? bookId = bookInfo["ID"]!.ToString();
                        string? title = bookInfo["Title"]!.ToString();
                        string? BookInfoID = bookInfo["BookInfoID"]!.ToString();

                        parish.AddBook(BookInfoID,title,bookId,title.toBookType());

                        //parish.Books.Add(new Book()
                        //{
                        //    Parish = parish,
                        //    Breadcrumb = BookInfoID,
                        //    Title = title,
                        //    RefId = bookId,
                        //    BookType = title.toBookType(),
                        //    Pages = []
                        //}); ;
                    }
                }
                ctx.SaveChanges();
            }
        }


        public MainVM()
        {
            var core = new Core();

            var DatabaseFile = (Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "lunOptics", "MatrikelBrowser", "MatrikelBrowser.db"));
            MatrikelBrowserCTX.DatabaseFile = DatabaseFile;

            using var ctx = new MatrikelBrowserCTX();

            //ctx.Database.EnsureDeleted();
            //ctx.Database.EnsureCreated();
            var y = ctx.Database.GetPendingMigrations();
            ctx.Database.Migrate();

            var largeparishes = ctx.Parishes.Where(p => p.Books.Count > 10).ToList();
        }
    }

    internal static class Extensions
    {
        public static BookType toBookType(this string title)
        {
            var t = title.ToLower();
            BookType bt = BookType.None;

            if (t.Contains("tauf")) bt |= BookType.Taufbücher;
            if (t.Contains("trau")) bt |= BookType.Hochzeitsbücher;
            if (t.Contains("misch")) bt |= BookType.Mischbände;
            if (t.Contains("sterb") || t.Contains("beerd")) bt |= BookType.Sterbebücher;
            if (bt == BookType.None) bt = BookType.Verschiedenes;
            return ((int)bt & ((int)bt - 1)) == 0 ? bt : BookType.Mischbände;
        }
    }
}

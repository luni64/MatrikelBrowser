using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AEM
{
    public class Book
    {
        public string ID { get; set; } = "";
        public string Title { get; set; } = "";
        public string? Details { get; set; } = null;
        public Guid BookInfoID { get; set; }
        public Guid DescriptionID { get; set; }
        public List<Page> Pages { get; } = [];
        public BookInfo Note { get; set; } = new();//=> _bookinfo ??= new BookInfo(this);
        public bool hasInfo { get; set; } = false;
        public string Type
        {
            get
            {
                string t = Title.ToLower();
                if (t.Contains("tauf")) return "Taufen";
                if (t.Contains("trau")) return "Trauungen";
                if (t.Contains("misch")) return "Mischbände";
                if (t.Contains("sterb")) return "Sterbefälle";
                return "Verschiedenes";
            }
        }
        public static string? baseFolder { get; set; }
        public override string ToString() => $"{ID}-{Title}-{BookInfoID}";

        public void LoadInfo()
        {
            if (hasInfo) return;  // did we already load the page info? -> no need to parse again

            DirectoryInfo bookFolder = new(Path.Combine(baseFolder!, "books", ID));
            DirectoryInfo pagesFolder = new(Path.Combine(bookFolder.FullName, "pages"));

            loadBookInfo(bookFolder);
            //loadBookDetails(bookFolder);


            hasInfo = true;
            //Task.Delay(5000).Wait();
        }

        static HttpClient httpClient = new();

        private void loadBookInfo(DirectoryInfo bookFolder)
        {
            FileInfo bookInfoFile = new(Path.Combine(bookFolder.FullName, "bookInfo.xml"));
            DirectoryInfo pagesFolder = new(Path.Combine(bookFolder.FullName, "pages"));

            string bookInfoXML;
            if (bookInfoFile.Exists && bookInfoFile.Length > 0) // did we already download the info file from the aem webpage? 
            {
                Trace.WriteLine("Read cached bookInfoFile");
                bookInfoXML = File.ReadAllText(bookInfoFile.FullName);
            }
            else // We need to download the info file from the aem webpage first. Keep a local copy for subsequent use 
            {
                Trace.WriteLine("download bookInfoFile");
                pagesFolder.Create();
                string bookInfoURL = $"https://digitales-archiv.erzbistum-muenchen.de/actaproweb/mets?id=Rep_{BookInfoID}_mets_actapro.xml";

                bookInfoXML = httpClient.GetStringAsync(bookInfoURL).GetAwaiter().GetResult();
                File.WriteAllText(bookInfoFile.FullName, bookInfoXML);
            }

            if (!string.IsNullOrEmpty(bookInfoXML))
            {
                mets bookInfo = bookInfoXML.ParseXML<mets>() ?? new mets(); //see: https://de.wikipedia.org/wiki/Metadata_Encoding_%26_Transmission_Standard

                foreach (var pageInfo in bookInfo.fileSec.fileGrp.file) // generate Page objects from the information given in bookInfo.xml
                {
                    string url = pageInfo.FLocat.href;
                    string localName = Path.Combine(pagesFolder.FullName, pageInfo.ID) + Path.GetExtension(url);
                    Pages.Add(new Page(url, localName));
                }
            }
        }
        private void loadBookDetails(DirectoryInfo bookFolder)
        {
            // client.DownloadFile("https://digitales-archiv.erzbistum-muenchen.de/actaproweb/archive.jsf?id=Vz++++++DB0061AF-127D-4903-9045-9D91A4536B64", "test.html");
            string url = $"https://digitales-archiv.erzbistum-muenchen.de/actaproweb/document/Vz_{DescriptionID}";
            var description = httpClient.GetStringAsync(url).GetAwaiter().GetResult();

            var matches = Regex.Matches(description, @"(?<=data-label=)[^<]*");
            foreach (Match match in matches)
            {

                Trace.WriteLine(match.Value);
            }
            //match = Regex.Match(description, @"(?<=Verweis:)[^<]*");

            //File.WriteAllText(bookInfoFile.FullName, bookInfoXML);
        }

       // private BookInfo? _bookinfo;

        //public Book()
        //{
        //    Note = new(this);
        //}
    }
}
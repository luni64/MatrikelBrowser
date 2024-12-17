//using Interfaces;
//using Org.BouncyCastle.Crypto.Signers;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.IO;
//using System.Linq;
//using System.Net.Http;
//using System.Text.RegularExpressions;

//namespace AEM
//{
//    public class Book //: IBook
//    {
//        public string Title => dto.Title;
//        public string RefId => dto.RefId;
//        public List<PageDTO> pageDTOs => dto.Pages;
//        public ParishDTO ParishDTO => dto.Parish;
//        public BookType BookType => dto.BookType;
        

//       // public List<Page> Pages { get; } = [];
//        public IBookInfo Info { get; set; } = new BookInfo();
//        public Parish? Parish { get; set; } = null;
        
//        public override string ToString() => $"{Parish?.ToString()} {RefId}-{Title}";

//        //public void LoadPageInfo()
//        //{
//        //    if (!dto.Pages.Any()) // only load if necessary
//        //    {
//        //        loadPages();
//        //    }

//        //    //if (hasInfo) return;  // did we already load the page info? -> no need to parse again
//        //    //var bookFolder = new DirectoryInfo(Path.Combine(baseFolder!.FullName, "books", RefId));
//        //    //pagesFolder = new(Path.Combine(bookFolder.FullName, "pages"));
//        //    //loadBookInfo(bookFolder);
//        //    //hasInfo = true;
//        //}

//        //public Book(BookDTO dto)
//        //{
//        //    //this.dto = dto;

//        //    //if (dto.Parish.Archive.ArchiveType == ArchiveType.AEM)
//        //    //    loadPages = () => this.GetAEMPages();
//        //    //else
//        //    //    loadPages = () => Trace.WriteLine("MAT");
//        //}

//        public BookDTO dto { get; }

//        internal static DirectoryInfo? baseFolder { get; set; }

//        //private void loadBookInfo(DirectoryInfo bookFolder)
//        //{

//        //    FileInfo bookInfoFile = new(Path.Combine(bookFolder.FullName, "bookInfo.xml"));
//        //    DirectoryInfo pagesFolder = new(Path.Combine(bookFolder.FullName, "pages"));

//        //    string bookInfoURL = $"https://digitales-archiv.erzbistum-muenchen.de/actaproweb/mets?id=Rep_{BookInfoID}_mets_actapro.xml";
//        //    Info.METS_URL = $"https://dfg-viewer.de/show/?tx_dlf[id]={bookInfoURL}";
//        //    string bookInfoXML;
//        //    if (bookInfoFile.Exists && bookInfoFile.Length > 0) // did we already download the info file from the aem webpage? 
//        //    {
//        //        Trace.WriteLine("Read cached bookInfoFile");
//        //        bookInfoXML = File.ReadAllText(bookInfoFile.FullName);
//        //    }
//        //    else // We need to download the info file from the aem webpage first. Keep a local copy for subsequent use 
//        //    {
//        //        Trace.WriteLine("download bookInfoFile");
//        //        pagesFolder.Create();
//        //        bookInfoXML = httpClient.GetStringAsync(bookInfoURL).GetAwaiter().GetResult();
//        //        File.WriteAllText(bookInfoFile.FullName, bookInfoXML);
//        //    }

//        //    if (!string.IsNullOrEmpty(bookInfoXML))
//        //    {
//        //        mets bookInfo = bookInfoXML.ParseXML<mets>() ?? new mets(); //see: https://de.wikipedia.org/wiki/Metadata_Encoding_%26_Transmission_Standard

//        //        foreach (var pageInfo in bookInfo.fileSec.fileGrp.file) // generate Page objects from the information given in bookInfo.xml
//        //        {
//        //            string url = pageInfo.FLocat.href;
//        //            string localName = Path.Combine(pagesFolder.FullName, pageInfo.ID) + Path.GetExtension(url);
//        //            Pages.Add(new Page(url, localName));
//        //        }
//        //    }
//        //}

//        //private static HttpClient httpClient = new();
//        private DirectoryInfo? pagesFolder;
//        //private Guid DescriptionID;
//        //private bool hasInfo;
//    }
//}
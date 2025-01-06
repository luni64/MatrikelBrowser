using Interfaces;
using MbCore;
using Microsoft.VisualBasic.ApplicationServices;
using static System.Reflection.Metadata.BlobBuilder;
using System.IO;
using System.Diagnostics;
using System;
using System.Net;


namespace MatrikelBrowser.ViewModels
{
    public class PageVM : ItemVM
    {
        public int SheetNr { get; }
        public Uri URL { get; }
        public string ImageFilename => model.GetOrCreateImage();


        //public string LoadImage()
        //{
        //    var filename = $"C:/Users/lutz/AppData/Roaming/lunOptics/aemBrowser/books/{marriageModel.Book.RefId}/pages/file_{SheetNr + 1}.jpg";
        //    if (!File.Exists(filename))
        //    {
        //        Trace.TraceInformation("download image");

        //        using (WebClient client = new())
        //        {
        //            client.DownloadFile(URL, filename);
        //        }
        //    }
        //    else
        //        Trace.WriteLine("using cached image");
        //    return filename;            
        //}

        private Page model;

        public PageVM(Page model, BookVM parent) : base(parent)
        {
            this.model = model;
            
            SheetNr = model.Book.Pages.IndexOf(model);
            URL = new Uri(model.ImageURL);
           // ImageFilename =  marriageModel.GetLocalFilename();
        }
    }
}

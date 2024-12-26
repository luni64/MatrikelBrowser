using System;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace MbCore
{
    public static  class PageExtensions
    {
        public static string GetOrCreateImage(this Page page)
        {
            var baseFolder = "C:\\Users\\lutz\\AppData\\Roaming\\lunOptics\\cache";

            var sheetNr = page.Book.Pages.IndexOf(page) + 1;

            string cacheFolder = Path.Combine(baseFolder,
                page.Book.Parish.Archive.Country.Name.toSafeFilename(),
                page.Book.Parish.Archive.Name.toSafeFilename(),
                $"{page.Book.Parish.RefId}_{page.Book.Parish.Name}".toSafeFilename(),
                $"{page.Book.RefId}_{page.Book.Title}".toSafeFilename()
                );            
            Directory.CreateDirectory(cacheFolder);

            var file = Path.Combine(cacheFolder, $"folio_{sheetNr}.jpg");            
            if (!System.IO.File.Exists(file))
            {
                Trace.TraceInformation($"download image {page.ImageURL} to {file}");

                using (WebClient client = new())
                {                    
                    client.DownloadFile(page.ImageURL, file);
                }
            }
            else
                Trace.TraceInformation($"using cached image {file}");
            return file;
        }
    }
}

using System;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace MbCore
{
    public static class PageExtensions
    {
        public static int getSheetNr(this Page page)
        {
            return page.Book.Pages.IndexOf(page)+1;
        }

        public static string GetOrCreateImage(this Page page)
        {
            // var baseFolder = "C:\\Users\\lutz\\AppData\\Roaming\\lunOptics\\cache";

           // var sheetNr = page.Book.Pages.IndexOf(page) + 1;

            string cacheFolder = Path.Combine(Core.CacheFolder,
                page.Book.Parish.Archive.Country.Name.toSafeFilename(),
                page.Book.Parish.Archive.Name.toSafeFilename(),
                $"{page.Book.Parish.RefId}_{page.Book.Parish.Name}".toSafeFilename(),
                $"{page.Book.RefId}_{page.Book.Title}".toSafeFilename()
                );
            Directory.CreateDirectory(cacheFolder);

            var file = Path.Combine(cacheFolder, $"folio_{page.getSheetNr()}.jpg");
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

        public static string toViewerUrl(this Page page)
        {
            string url = string.Empty;
            if (page.Book.Parish.Archive.ArchiveType == ArchiveType.MAT)
            {
                url = Path.Combine(@"https://data.matricula-online.eu\de",
                   page.Book.Parish.Archive.Country.Breadcrumb,
                   page.Book.Parish.Archive.Breadcrumb,
                   page.Book.Parish.Breadcrumb,
                   page.Book.Breadcrumb,                   
                   $"?pg={page.getSheetNr()}"
                   );
            }
            else
            {

            }
            return url;
        }
    }
}

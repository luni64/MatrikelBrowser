﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.SymbolStore;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AEM
{

    public class Page
    {
        public string URL { get; }
        public string localFilename { get; }
        public List<Bookmark> Bookmarks { get; } = new();

        public override string ToString() => localFilename;

        public Page(string downloadUrl, string localFilename)
        {
            this.URL = downloadUrl;
            this.localFilename = localFilename;
        }

        public string loadImage()
        {
            if (!File.Exists(localFilename))
            {
                Trace.WriteLine("download image");
                var url = new System.Uri(URL);
                using (WebClient client = new())
                {
                    client.DownloadFile(url, localFilename);
                }
            }
            else
                Trace.WriteLine("cached image");
            return localFilename;
        }
    }
}
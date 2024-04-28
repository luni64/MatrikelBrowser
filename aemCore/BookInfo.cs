using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEM
{
    public class BookInfo : IBookInfo
    {
        public List<IBookmark> Bookmarks { get; set; } = [];
        public string BookID { get; set; } = string.Empty;
        public string note { get; set; } = string.Empty;

        public BookInfo(List<Bookmark>? Bookmarks = null, string BookID = "", string note="")
        {
            this.Bookmarks = Bookmarks?.ToList<IBookmark>() ?? [];
            this.BookID=BookID;
            this.note = note;                
        }   
    }
}

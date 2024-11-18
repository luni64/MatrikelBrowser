using Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace AEM
{
    public class BookInfo : IBookInfo
    {
        public BookInfo(List<BookmarkBase>? Bookmarks = null, string BookID = "", string note = "")
        {
            this.Bookmarks = Bookmarks?.ToList<IBookmarkBase>() ?? [];
            this.BookID = BookID;
            this.note = note;
        }        
        public string BookID { get; set; } = string.Empty;
        public string note { get; set; } = string.Empty;
        public override string ToString() => BookID;
        public string METS_URL { get; set; }
        public List<IBookmarkBase> Bookmarks { get; set; }

    }    
}


using Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace MbCore
{
    public class BookInfo(List<BookmarkBase>? Bookmarks = null, string BookID = "", string note = "") : IBookInfo
    {
        public string BookID { get; set; } = BookID;
        public string note { get; set; } = note;
        public override string ToString() => BookID;
        public string METS_URL { get; set; } = string.Empty;
        public List<IBookmarkBase> Bookmarks { get; } = Bookmarks?.ToList<IBookmarkBase>() ?? [];

    }    
}


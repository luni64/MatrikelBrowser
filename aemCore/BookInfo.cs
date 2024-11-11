using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEM
{

    public class oldBookInfo : IOldBookInfo
    {
        public List<IBookmark> Bookmarks { get; set; } = [];
        public string BookID { get; set; } = string.Empty;
        public string note { get; set; } = string.Empty;

        public oldBookInfo(List<Bookmark>? Bookmarks = null, string BookID = "", string note = "")
        {
            this.Bookmarks = Bookmarks?.ToList<IBookmark>() ?? [];
            this.BookID = BookID;
            this.note = note;
        }

        public override string ToString()
        {
            return BookID;
        }
    }

    public class BookInfo : IBookInfo
    {
        public List<IBookmarkBase> Bookmarks { get; set; } = [];
        public string BookID { get; set; } = string.Empty;
        public string note { get; set; } = string.Empty;
        public override string ToString() => BookID;

        public BookInfo(List<BookmarkBase>? Bookmarks = null, string BookID = "", string note = "")
        {
            this.Bookmarks = Bookmarks?.ToList<IBookmarkBase>() ?? [];
            this.BookID = BookID;
            this.note = note;
        }
    }

    //public class BookInfo2 
    //{
    //    public List<BookmarkBase> Bookmarks { get; set; } = [];
    //    public string BookID { get; set; } = string.Empty;
    //    public string note { get; set; } = string.Empty;

    //    //public BookInfo2(List<BookmarkBase> Bookmarks, string BookID, string note)
    //    //{
    //    //    if(Bookmarks != null) this.Bookmarks = Bookmarks;
    //    //    this.BookID = BookID;
    //    //    this.note = note;
    //    //}
    //}
}


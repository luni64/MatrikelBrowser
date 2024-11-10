using System.Collections.Generic;


namespace Interfaces
{
    public interface IBookInfo
    {
        List<IBookmark> Bookmarks { get; set; }

      //  List<BookmarkBase> Bookmarks2 { get; set; }

        string BookID { get; set; }
        string note { get; set; }
    }

    //public interface IBookInfo2
    //{
    //    List<BookmarkBase> Bookmarks { get; set; }
    //    string BookID { get; set; }
    //    string note { get; set; }
    //}
}
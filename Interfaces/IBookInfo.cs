using System.Collections.Generic;


namespace Interfaces
{
    public interface IBookInfo
    {
        List<IBookmarkBase> Bookmarks { get; set; }

        string BookID { get; set; }
        string note { get; set; }
    }

    public interface IOldBookInfo
    {
        List<IBookmark> Bookmarks { get; set; }

        string BookID { get; set; }
        string note { get; set; }
    }
}
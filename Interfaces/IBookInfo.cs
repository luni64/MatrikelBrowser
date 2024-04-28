using System.Collections.Generic;

namespace Interfaces
{
    public interface IBookInfo
    {
        List<IBookmark> Bookmarks { get; set; }
        string BookID { get; set; }
        string note { get; set; }
    }
}
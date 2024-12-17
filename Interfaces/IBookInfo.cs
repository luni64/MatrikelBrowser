namespace Interfaces
{
    public interface IBookInfo
    {
        string BookID { get; set; }
        string METS_URL { get; set; }
        string note { get; set; }
        List<IBookmarkBase> Bookmarks { get;  }
    }    
}
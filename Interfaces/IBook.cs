
namespace Interfaces
{
    public interface IBook
    {
        string ID { get; set; }
        string Title { get; set; }
        IParish? Parish { get; set; }
        IBookInfo Info { get; set; }
        List<IPage> Pages { get; }
        BookType Type { get; }
        
        void LoadPageInfo();
        string ToString();
    }
}
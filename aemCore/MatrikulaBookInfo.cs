using Interfaces;

namespace MbCore
{
    public class MatrikulaBookInfo 
    {
        public string REFID { get; set; } = string.Empty;
        public BookType Type { get; set; } = BookType.None;
        public string Title { get; set; } = string.Empty;
        public string InfoUrl { get; set; } = string.Empty;       
    }
}





using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEM
{
    public class BookInfo
    {
        public string BookID { get; set; } = string.Empty;
        public string note { get; set; } = string.Empty;
        public List<Bookmark> Bookmarks { get; set; } = [];

    //public BookInfo(Book parent)
    //{
    //    BookID = parent.ID;            
    //}


}
}

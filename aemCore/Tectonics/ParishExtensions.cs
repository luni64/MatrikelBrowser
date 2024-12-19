
using Interfaces;
using System.Net;

namespace MbCore
{
    public static class ParishExtensions
    {
        public static void LoadBooks(this Parish parish)
        {
            using var ctx = new MatrikelBrowserCTX();
            ctx.Entry(parish).Collection(p => p.Books).Load();
        }

        public static void AddBook(this Parish parish, Parish p, string bi, string ti, string refid, BookType bt )
        {
            parish.Books.Add(new Book()
            {
                Parish = p,
                BookInfoLink = bi,
                Title = ti,
                RefId = refid,
                BookType = bt,
                Pages = []
            }); ;

        }
    }
}

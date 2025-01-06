
using AEM.Tectonics;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Linq;
using System.Net;

namespace MbCore
{
    public static class ParishExtensions
    {
        public static void LoadBooks(this Parish parish)
        {
            using var ctx = new MatrikelBrowserCTX();
            ctx.Entry(parish).Collection(p => p.Books).Load();
            //foreach (var book in parish.Books)
            //{
            //    ctx.Entry(book).Collection(b => b.Events).Load();  ///ToDo: this is not efficient                               
            //}
        }

        public static void AddBook(this Parish parish, string bi, string ti, string refid, BookType bt)
        {
            parish.Books.Add(new Book()
            {
                Parish = parish,
                BookInfoLink = bi,
                Title = ti,
                RefId = refid,
                BookType = bt,
                Pages = []
            }); ;

        }
    }
}

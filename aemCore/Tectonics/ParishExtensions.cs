
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
                        
            ctx.Attach(parish);

            if (ctx.Books.Where(b => b.Parish.Id == parish.Id).Count() == 0)
            {
                var infoURL = parish.Archive.Country.Breadcrumb +'/'+ parish.Archive.Breadcrumb + '/' + parish.Breadcrumb;

                var books = MatParser.ParseBooks(infoURL);
                parish.Books.AddRange(books);
                ctx.SaveChanges();
            }

            ctx.Entry(parish).Collection(c => c.Books).Load();



            //ctx.Entry(parish).Collection(p => p.Books).Load();
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
                Breadcrumb = bi,
                Title = ti,
                RefId = refid,
                BookType = bt,
                Pages = []
            }); ;

        }
    }
}

using AEM;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MbCore
{
    internal static class aemParser
    {
        public static List<Book> ParseBooks(string bookInfoURL)
        {          
            var htmlDoc = MatParser.GetBookInfo(bookInfoURL);
         
            List<Book> result = [];
            
            var json = File.ReadAllText("tectonics.json");
            var tectonics = JsonConvert.DeserializeObject<List<aemParish>>(json)!;

            var id = bookInfoURL.Split("/").Last();
            var parish = tectonics.Where(d => d.ID == id)?.FirstOrDefault();
            if (parish != null)
            {
                foreach (var book in parish.Books)
                {
                    result.Add(new Book
                    {
                        RefId = book.ID,
                        BookType = book.Title.toBookType(),
                        Title = book.Title,
                        Breadcrumb = book.BookInfoID.ToString(),
                    });
                }
            }
            return result;
        }
    }
}





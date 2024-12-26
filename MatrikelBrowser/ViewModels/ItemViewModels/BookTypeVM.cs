using MbCore;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MatrikelBrowser.ViewModels
{
    public class BookTypeVM : ItemVM
    {
        public string Name { get; }
        public List<BookVM> BookVMs { get; } = new();

        public BookTypeVM(IGrouping<BookType, Book> books, ParishVM parent) : base(parent)
        {
            this.Name = Enum.GetName(typeof(BookType), books.Key) ?? "?";

            foreach (var book in books.OrderBy(b => b.RefId))
            {
                BookVMs.Add(new BookVM(book,this));
            }


            Indent = 10;
        }
    }
}

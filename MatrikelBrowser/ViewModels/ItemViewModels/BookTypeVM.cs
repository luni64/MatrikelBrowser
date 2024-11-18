using Interfaces;
using System;
using System.Collections.Generic;

namespace ArchiveBrowser.ViewModels
{
    public class BookTypeVM : ItemVM
    {
        public string Name { get; }
        public List<BookVM> BookVMs { get; } = new();

        public BookTypeVM(BookType Name)
        {
            this.Name = Enum.GetName(typeof(BookType), Name) ?? "?";            
            Indent = 10;
        }
    }
}

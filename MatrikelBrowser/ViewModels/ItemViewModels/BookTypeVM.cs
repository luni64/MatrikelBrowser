using System.Collections.Generic;

namespace ArchiveBrowser.ViewModels
{
    public class BookTypeVM : ItemVM
    {
        public string Name { get; }
        public List<BookVM> BookVMs { get; } = new();

        public BookTypeVM(string Name)
        {
            this.Name = Name;
            Indent = 10;
        }
    }
}

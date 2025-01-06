using Interfaces;

namespace MatrikelBrowser.ViewModels
{
    public class TabItemVM : BaseViewModel
    {
        public string Header { get; private set; }        
        public string Letter { get; }
        public string Parish { get; }
        public string Date { get; } 
        public BookVM book { get; set; }

        public TabItemVM(BookVM book)
        {
            this.book = book;

            Header = book.Title;
            Parish = book.model.Parish.Name;

            if (book.model.StartDate.HasValue && book.model.EndDate.HasValue)
            {
                Date = $"{book.model.StartDate.Value.Year}-{book.model.EndDate.Value.Year}";
            }
            else { Date = ""; }

            Letter = book.BookType switch
            {
                BookType.Mischbände => "M",
                BookType.Taufbücher => "T",
                BookType.Hochzeitsbücher => "H",
                BookType.Verschiedenes => "V",
                _ => ""
            };
        }
    }
}

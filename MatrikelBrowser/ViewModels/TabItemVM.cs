using Interfaces;

namespace MatrikelBrowser.ViewModels
{
    public class TabItemVM : BaseViewModel
    {
        public string Title => book.model.Title;
        public string Archive => book.model.Parish.Archive.Name;
        public string Parish => book.model.Parish.Name;
        public string Date => $"{book.model.StartDate?.Year}-{book.model.EndDate?.Year}";
        public string RefId => book.model.RefId;
        public BookVM book { get; set; }
        public string Letter { get; }

        public TabItemVM(BookVM book)
        {
            this.book = book;

           
            //Parish = book.model.Parish.Name;

            //if (book.model.StartDate.HasValue && book.model.EndDate.HasValue)
            //{
            //    Date = $"{book.model.StartDate.Value.Year}-{book.model.EndDate.Value.Year}";
            //}
            //else { Date = ""; }

            Letter = book.BookType switch
            {
                BookType.Mischbände => "M",
                BookType.Taufbücher => "T",
                BookType.Sterbebücher=> "S",
                BookType.Hochzeitsbücher => "H",
                BookType.Verschiedenes => "V",
                _ => ""
            };
        }
    }
}

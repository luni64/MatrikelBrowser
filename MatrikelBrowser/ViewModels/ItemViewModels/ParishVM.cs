using MbCore;
using System.Collections.ObjectModel;
using System.Linq;

namespace MatrikelBrowser.ViewModels
{
    public class ParishVM : ItemVM
    {
        public ObservableCollection<BookGroupVM> BookTypeVMs { get; } = new();
        public BookGroupVM? SelectedBookGroup
        {
            get => _selectedBookGroup;
            set
            {
                SetProperty(ref _selectedBookGroup, value);
                (parent.parent.parent.parent as TectonicsVM)!.selectedBook = _selectedBookGroup?.SelectedBook; ///hack    
            }
        }
        public string Title => model?.Name ?? string.Empty;
        public string Church => model?.Church ?? string.Empty;
        public string SubTitle => $"{model?.RefId} {model?.Church} (#{model?.Books.Count}";
        public string RefNr => model?.RefId ?? string.Empty;


        public override bool IsExpanded
        {
            get => base.IsExpanded;
            set
            {
                if (value == true && parent is LetterVM letterVM && BookTypeVMs.Any(a => a.parent == null)) // if expanding and has a dummy entry (i.e. is empty)
                {
                    BookTypeVMs.Clear();
                    model.LoadBooks();

                    var bookGroups = model.Books.OrderBy(b => b.BookType).ToLookup(b => b.BookType); // group books by type
                    foreach (var bookGroup in bookGroups)
                    {
                        BookTypeVMs.Add(new BookGroupVM(bookGroup, this));
                    }
                }
                base.IsExpanded = value;
            }
        }


        private BookGroupVM? _selectedBookGroup;
        public ParishVM(Parish model, LetterVM parent) : base(parent)
        {
            this.model = model;
            BookTypeVMs.Add(new BookGroupVM()); // dummy           

            Indent = -10;
        }

        internal readonly Parish model;
    }
}

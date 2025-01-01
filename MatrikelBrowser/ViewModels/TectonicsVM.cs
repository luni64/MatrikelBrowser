using Interfaces;
using MbCore;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace MatrikelBrowser.ViewModels
{
    public class TabItemVM : BaseViewModel
    {
        public string Header { get; private set; }
        //public string Letter { get; } = (book.parent.parent as ParishVM)!.Title.Substring(0, 1);
        public string Letter { get; }

        public string Parish { get; }
        public string Date { get; } = string.Empty;
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

    public class TectonicsVM(aemCore model) : ItemVM(null)
    {
        #region commands
        public RelayCommand cmdToogleFavorite => _cmdToogleFavorite ??= new RelayCommand(doToggleFavorite);
        void doToggleFavorite(object? param)
        {
            if (param is BookVM bookVM)
            {
                if (Favorites.Contains(bookVM))
                {
                    Favorites.Remove(bookVM);
                    model.Favorites.Remove(bookVM.ID);
                    bookVM.IsFavorite = false;
                }
                else
                {
                    Favorites.Add(bookVM);
                    model.Favorites.Add(bookVM.ID);
                    bookVM.IsFavorite = true;
                }
            }
        }
        #endregion

        #region properties
        public ObservableCollection<CountryVM> CountryVMs { get; } = [];
        public ObservableCollection<BookVM> Favorites { get; } = [];

        public ObservableCollection<TabItemVM> DisplayedBooks { get; } = [];

        TabItemVM? _selectedTab;
        public TabItemVM? selectedTab
        {
            get => _selectedTab;
            set => SetProperty(ref _selectedTab, value);
        }

        private CountryVM? _selectedCountry;
        public CountryVM? SelectedCountry
        {
            get => _selectedCountry;
            set => SetProperty(ref _selectedCountry, value);
        }

        public BookVM? selectedBook  ///ToDo: move to BookGroupVM??
        {
            get => _selectedBook;
            set
            {
                SetProperty(ref _selectedBook, value);
                if (value != null) DisplayedBooks.Add(new TabItemVM(value));
                selectedTab = DisplayedBooks.Last();
            }
        }
        #endregion

        public void UpdateData()
        {
            CountryVMs.Clear();
            foreach (var country in model.Countries)
            {
                CountryVMs.Add(new CountryVM(country, this));
            }
        }

        private BookVM? _selectedBook;
        private RelayCommand? _cmdToogleFavorite;
        private readonly aemCore model = model;
    }
}

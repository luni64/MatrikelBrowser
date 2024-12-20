using AEM;
using System.Collections.ObjectModel;
using System.Linq;

namespace ArchiveBrowser.ViewModels
{
    public class TectonicsVM : BaseViewModel
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
        public ObservableCollection<CountryVM> CountryVMs { get; }       
        public ObservableCollection<BookVM> Favorites { get; } = [];
        public BookVM? selectedBook  ///ToDo: move to BookTypeVM??
        {
            get => _selectedBook;
            set
            {
                if (_selectedBook != value)
                {
                    if (_selectedBook != null) // reset the currently selected book
                    {
                        _selectedBook.IsSelected = false;
                        _selectedBook.SubTitle = null;
                    }
                    SetProperty(ref _selectedBook, value);

                    if (_selectedBook != null)
                    {
                        _selectedBook.Intialize(); // lazy load pages information 
                        _selectedBook.IsSelected = true;
                    }
                }
            }
        }
        #endregion

        public TectonicsVM(aemCore model) 
        {
            this.model = model;
            CountryVMs = new(model.Countries.Select(c => new CountryVM(c)).ToList());   // recursively setup the view modesl
        }

        private BookVM? _selectedBook;
        private RelayCommand? _cmdToogleFavorite;
        private readonly aemCore model;
    }
}

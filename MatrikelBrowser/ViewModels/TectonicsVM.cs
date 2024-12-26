using MbCore;
using System.Collections.ObjectModel;
using System.Linq;

namespace MatrikelBrowser.ViewModels
{
    public class TectonicsVM(aemCore model) : BaseViewModel
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
                        _selectedBook.Initialize(); // lazy load pages information 
                        _selectedBook.IsSelected = true;
                    }
                }
            }
        }
        #endregion

        public void UpdateData()
        {  
            CountryVMs.Clear();
            foreach (var country in model.Countries)
            {
                CountryVMs.Add(new CountryVM(country));
            }
        }

        private BookVM? _selectedBook;
        private RelayCommand? _cmdToogleFavorite;
        private readonly aemCore model = model;
    }
}

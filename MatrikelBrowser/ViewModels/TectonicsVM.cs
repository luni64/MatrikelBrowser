using AEM;
using MbCore;
using AEM.Tectonics;
using Interfaces;
using MahApps.Metro.IconPacks;
using MbCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.Logging;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Controls;

namespace MatrikelBrowser.ViewModels
{


    class letter
    {
        public Dictionary<int, List<int>> parishes = [];
    }

    class archive
    {
        public Dictionary<string, List<letter>> letters = [];
    }
    class country
    {
        public Dictionary<int, List<archive>>? archives = [];
    }

    class settingss
    {
        public Dictionary<int, List<country>>? countries = [];
    }


    public class TectonicsVM(Core model) : ItemVM(null)
    {

        #region commands
        // public RelayCommand cmdToogleFavorite => _cmdToogleFavorite ??= new RelayCommand(doToggleFavorite);
        //void doToggleFavorite(object? param)
        //{
        //    if (param is Book bookVM)
        //    {
        //        if (Favorites.Contains(bookVM))
        //        {
        //            Favorites.Remove(bookVM);
        //            model.Favorites.Remove(bookVM.ID);
        //            bookVM.IsFavorite = false;
        //        }
        //        else
        //        {
        //            Favorites.Add(bookVM);
        //            model.Favorites.Add(bookVM.ID);
        //            bookVM.IsFavorite = true;
        //        }
        //    }
        //}
        #endregion

        #region properties
        public ObservableCollection<CountryVM> CountryVMs { get; } = [];
        public ObservableCollection<TabItemVM> DisplayedBooks { get; } = [];

        public TabItemVM? selectedTab
        {
            get => _selectedTab;
            set
            {
                if (_selectedTab != value)  // new tab selected
                {
                    _selectedTab?.book.model.Save(); // save the old one before switching to the new tab
                    SetProperty(ref _selectedTab, value);
                }
            }
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
                if (value != null)
                {
                    var existing = DisplayedBooks.FirstOrDefault(b => b.book == value);
                    if (existing == null)
                    {
                        DisplayedBooks.Add(new TabItemVM(value));
                    }
                    selectedTab = existing != null ? existing : DisplayedBooks.Last();
                }
            }
        }
        #endregion
        public void SaveSelectedTab()
        {
            if (_selectedTab != null)
            {
                _selectedTab.book.model.Save();
            }

            return;
        }

        public void UpdateData()
        {
            using var ctx = new MatrikelBrowserCTX();

            CountryVMs.Clear();

            foreach (var country in model.Countries)
            {
                CountryVMs.Add(new CountryVM(country, this));
            }

            // open previously opened books
            var openBooksSetting = ctx.SettingsTable.FirstOrDefault(s => s.Key == "OpenBooks")?.Value;

            if (!string.IsNullOrEmpty(openBooksSetting))
            {
                DisplayedBooks.Clear();
                var bookIDs = openBooksSetting.Split('-').Select(b => int.Parse(b));     // parse the list of book IDs from the settigs entry
                var books = ctx.Books.Where(b => bookIDs.Contains(b.Id));                // get corresponding entities from db 

                var parishIDs = books.Select(b => b.Parish.Id).Distinct().ToList();      // list of all parish IDs belonging to the books
                var parishes = ctx.Parishes.Where(p => parishIDs.Contains(p.Id));        // get corresponding entities from db 

                var archiveIDs = parishes.Select(p => p.Archive.Id).Distinct().ToList(); // list of all archive IDs belonging to the parishes
                var archives = ctx.Archives.Where(a => archiveIDs.Contains(a.Id));       // get corresponding entities from db 

                var countryIDs = archives.Select(a => a.Country.Id).Distinct().ToList(); // list of all country IDs belonging to the archives

                foreach (var countryVM in CountryVMs.Where(c => countryIDs.Contains(c.model.Id)))
                {
                    countryVM.LoadArchives();
                    foreach (var archiveVM in countryVM.ArchiveVMs.Where(a => archiveIDs.Contains(a.model.Id)))
                    {
                        archiveVM.LoadLetters();
                        foreach (var letterVM in archiveVM.LetterVMs.Where(l => l.ParishVMs.Any(p => parishIDs.Contains(p.model.Id))))
                        {
                            foreach (var parishVM in letterVM.ParishVMs.Where(p => parishIDs.Contains(p.model.Id)))
                            {
                                parishVM.LoadBooks();
                                foreach (var bookGroupVM in parishVM.BookTypeVMs.Where(x => x.BookVMs.Any(b => bookIDs.Contains(b.model.Id))))
                                {
                                    foreach (var bookVM in bookGroupVM.BookVMs.Where(b => bookIDs.Contains(b.model.Id)))
                                    {
                                        bookVM.Initialize();
                                        DisplayedBooks.Add(new TabItemVM(bookVM));
                                    }
                                }
                            }
                        }
                    }
                }
                selectedTab = DisplayedBooks.FirstOrDefault();
            }
        }

        public void SaveSettings()
        {
            using var ctx = new MatrikelBrowserCTX();

            SettingsEntry? OpenBooks;
            OpenBooks = ctx.SettingsTable.FirstOrDefault(s => s.Key == "OpenBooks");
            if (OpenBooks == null)
            {
                OpenBooks = new SettingsEntry { Key = "OpenBooks" };
                ctx.SettingsTable.Add(OpenBooks);
            }

            var books = DisplayedBooks.Select(d => d.book.model).ToList();

            string s = DisplayedBooks.FirstOrDefault()?.book.model.Id.ToString() ?? "";
            OpenBooks.Value = DisplayedBooks.Skip(1).Aggregate(seed: s, (c, n) => c + $"-{n.book.model.Id}");

            ctx.SaveChanges();

        }

        TabItemVM? _selectedTab;
        private BookVM? _selectedBook;
        private RelayCommand? _cmdToogleFavorite;
        private readonly Core model = model;
    }
}

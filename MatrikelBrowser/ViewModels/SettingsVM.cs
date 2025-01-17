using MbCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace MatrikelBrowser.ViewModels
{
    public class SettingsVM : BaseViewModel
    {
        public RelayCommand cmdAddParish => _cmdAddParish ??= new RelayCommand((object? _) =>
        {
            var p = model.ImportParish(importLink);
            if (p != null)
            {
                NewParish = p.Name;
                int i = 1;
                NewBooks.Clear();
                foreach (var book in p.Books)
                {
                    NewBooks.Add($"{i++}) {book.Title} - {book.RefId}");
                }
            }
        });

        public ObservableCollection<string> NewBooks { get; } = [];

        string _newParish = string.Empty;
        public string NewParish
        {
            get => _newParish;
            set => SetProperty(ref _newParish, value);
        }


        public ObservableCollection<Book> BookmarkedBooks { get; } = [];
        public Book? SelectedBookmarkedBook { get; set; }


        public string DataBaseFile
        {
            get => _databaseFile;
            set
            {
                if (_databaseFile != value)
                {
                    try
                    {
                        if (model.SetDatabase(value))
                        {
                            SetProperty(ref _databaseFile, value);
                            var settings = MatrikelBrowser.Properties.Settings.Default;
                            settings.DatabaseFile = value;
                            settings.Save();
                        }
                        else
                        {
                            Trace.TraceWarning($"Errors opening new database file {value}");
                            MainViewModel.dialogService.ShowDialog(new string($"Die Datenbank \n{value}\n\n konnte nicht geladen werden. Bitte wählen sie eine kompatible Datenbank aus"));
                        }
                    }
                    catch
                    {
                        Trace.TraceWarning($"Errors opening new database file {value}");
                        MainViewModel.dialogService.ShowDialog(new string($"Die Datenbank \n{value}\n\n konnte nicht geladen werden. Bitte wählen sie eine kompatible Datenbank aus"));
                    }
                }
            }
        }

        public string importLink { get; set; } = "";
        // public ObservableCollection<Parish> Parishes { get; }

        public SettingsVM(aemCore model)
        {
            this.model = model;

            using var ctx = new MatrikelBrowserCTX();

            var bb = ctx.Books.Include(b=>b.Events).Include(b=>b.Parish).ThenInclude(p=>p.Archive).ThenInclude(a=>a.Country).Where(b => b.Events.Count > 0);

            var b = model.Countries.SelectMany(c => c.Archives).SelectMany(a => a.Parishes).SelectMany(p => p.Books).Where(b => b.Events.Count > 0);
            foreach (var book in bb)
            {
                BookmarkedBooks.Add(book);
            }
            SelectedBookmarkedBook = b.FirstOrDefault();
        }

        private string _databaseFile;
        private RelayCommand? _cmdAddParish;
        private readonly aemCore model;
    }
}

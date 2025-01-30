using MatrikelBrowser.ViewModels.Settings;
using MbCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace MatrikelBrowser.ViewModels
{
    public class SettingsVM : BaseViewModel
    {
        //private RelayCommand? _cmdNewDB;
        //public RelayCommand cmdNewDB => _cmdNewDB ??= new RelayCommand((object? _) =>
        //{
        //    string? folder = Path.GetDirectoryName(MatrikelBrowserCTX.DatabaseFile);
        //    if (folder != null)
        //    {
        //        int i = 0;
        //        string filename = "newDb.db";
        //        while (File.Exists(filename))
        //        {
        //            filename = Path.Combine(folder, $"newDb({++i}).db");
        //        }

        //        // File.WriteAllText(filename, "");
        //        MatrikelBrowserCTX.DatabaseFile = filename;

        //        using var ctx = new MatrikelBrowserCTX();
        //        ctx.Database.Migrate();
        //        ////ctx.Database.EnsureDeleted();
        //        //ctx.Database.EnsureCreated();
        //        //folders DataBaseFile = filename;
        //    }
        //});

        //public RelayCommand cmdAddParish => _cmdAddParish ??= new RelayCommand((object? _) =>
        //{
        //    var p = model.ImportParish(importLink);
        //    if (p != null)
        //    {
        //        NewParish = p.Name;
        //        int i = 1;
        //        NewBooks.Clear();
        //        foreach (var book in p.Books)
        //        {
        //            NewBooks.Add($"{i++}) {book.Title} - {book.RefId}");
        //        }
        //    }
        //});

        public SettingsFoldersVM foldersVM { get; }

        public ObservableCollection<string> NewBooks { get; } = [];

        string _newParish = string.Empty;
        public string NewParish
        {
            get => _newParish;
            set => SetProperty(ref _newParish, value);
        }


        public ObservableCollection<Book> BookmarkedBooks { get; } = [];
        public Book? SelectedBookmarkedBook { get; set; }




        public string importLink { get; set; } = "";
        // public ObservableCollection<Parish> Parishes { get; }

        public SettingsVM(Core model)
        {
            this.model = model;

            foldersVM = new SettingsFoldersVM(model);

            //using var ctx = new MatrikelBrowserCTX();

            //var bb = ctx.Books.Include(b=>b.Events).Include(b=>b.Parish).ThenInclude(p=>p.Archive).ThenInclude(a=>a.Country).Where(b => b.Events.Count > 0);

            //var b = model.Countries.SelectMany(c => c.Archives).SelectMany(a => a.Parishes).SelectMany(p => p.Books).Where(b => b.Events.Count > 0);
            //foreach (var book in bb)
            //{
            //    BookmarkedBooks.Add(book);
            //}
            //SelectedBookmarkedBook = b.FirstOrDefault();
        }

        //private string _databaseFile;
        private RelayCommand? _cmdAddParish;
        private readonly Core model;
    }
}

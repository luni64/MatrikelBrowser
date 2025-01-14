using MbCore;
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
                    }
                    catch
                    {
                        Trace.TraceWarning($"Errors opening new database file {value}");
                        return;
                    }
                }
            }
        }

        public string importLink { get; set; } = "";
        // public ObservableCollection<Parish> Parishes { get; }

        public SettingsVM(aemCore model)
        {
            this.model = model;
            _databaseFile = MatrikelBrowserCTX.DatabaseFile;



            //using var ctx = new MatrikelBrowserCTX();
            //Parishes = new([.. ctx.Parishes]);
        }

        private string _databaseFile;
        private RelayCommand? _cmdAddParish;
        private readonly aemCore model;
    }
}

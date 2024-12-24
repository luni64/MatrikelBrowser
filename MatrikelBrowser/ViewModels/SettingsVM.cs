using AEM;
using ArchiveBrowser.ViewModels;
using iText.Commons.Utils;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Signers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveBrowser.ViewModels
{
    public class SettingsVM : BaseViewModel
    {
        public RelayCommand cmdAddParish => _cmdAddParish ??= new RelayCommand((object? o) =>
        {
            var Parser = aemCore.ImportParish(importLink);
        });

        private string _database;
        public string DataBaseFile
        {
            get => _database;
            set
            {
                if (_database != value)
                {
                    try
                    {
                        MatrikelBrowserCTX.DatabaseFile = value;
                        using var ctx = new MatrikelBrowserCTX();
                        var test = ctx.Countries.Include(c => c.Archives).FirstOrDefault();

                        var settings = MatrikelBrowser.Properties.Settings.Default;
                        settings.DatabaseFile = value;
                        settings.Save();
                    }
                    catch
                    {
                        MatrikelBrowserCTX.DatabaseFile = _database; // reset to old file
                        Trace.TraceWarning($"Errors opening new database file {value}");
                        return;
                    }
                    SetProperty(ref _database, value);
                }
            }
        }

        public string importLink { get; set; }


        public ObservableCollection<Parish> Parishes { get; }

        public SettingsVM(aemCore model)
        {
            this.model = model;
            //using var ctx = new MatrikelBrowserCTX();
            //Parishes = new(ctx.Parishes.ToList());
            _database = MatrikelBrowserCTX.DatabaseFile;
        }

        aemCore model;

        private RelayCommand _cmdAddParish;
    }
}

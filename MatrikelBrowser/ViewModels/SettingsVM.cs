using MbCore;
using MatrikelBrowser.ViewModels;
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

namespace MatrikelBrowser.ViewModels
{
    public class SettingsVM : BaseViewModel
    {
        public RelayCommand cmdAddParish => _cmdAddParish ??= new RelayCommand((object? _) =>
        {
            var Parser = aemCore.ImportParish(importLink);
        });

        public string DataBaseFile
        {
            get => _databaseFile;
            set
            {
                if (_databaseFile != value)
                {
                    try
                    {
                       if( model.SetDatabase(value))
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
        public ObservableCollection<Parish> Parishes { get; }

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

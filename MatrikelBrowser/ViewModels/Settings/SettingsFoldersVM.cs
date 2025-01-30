using MbCore;
using static MatrikelBrowser.Properties.Settings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrikelBrowser.ViewModels.Settings
{
    public class SettingsFoldersVM : BaseViewModel
    {      
        public string CacheFolder
        {
            //get => Core.GetSetting("CacheFolder") ?? Core.DefaultCacheFolder;
            get => Core.CacheFolder;
            set
            {
                Core.SetSetting("CacheFolder", value);
                Core.CacheFolder = value;
                SetProperty(ref _cacheFolder, value);
            }
        }

        public string DataBaseFile
        {
            get => settings.DatabaseFile;
            set
            {
                if (_databaseFile != value)
                {
                    try
                    {
                        if (model.SetDatabase(value))
                        {                            
                            SetProperty(settings, s=>s.DatabaseFile, value);                                                       
                            settings.Save();
                        }
                        else
                        {
                            Trace.TraceWarning($"Errors opening new database file {value}");
                            MainViewModel.dialogService.ShowDialog($"Die Datenbank \n{value}\n\n konnte nicht geladen werden. Bitte wählen sie eine kompatible Datenbank aus");
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

        private string _databaseFile = string.Empty;
        private string? _cacheFolder = null;
        private readonly Core model;
        private Properties.Settings settings;

        public SettingsFoldersVM(Core model)
        {
            this.model = model;

            settings = Properties.Settings.Default;
        }
    }

}

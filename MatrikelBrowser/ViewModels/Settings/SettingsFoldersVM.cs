using MbCore;
using static MatrikelBrowser.Properties.Settings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data.Common;

namespace MatrikelBrowser.ViewModels.Settings
{
    public class SettingsFoldersVM : BaseViewModel, IDataErrorInfo
    {
        public async Task trySetNewDbAsync(string dbFile)
        {           
            dbOK = await Core.CheckDatabaseAsync(dbFile);       
            if(dbOK)
            {
                await model.SetDatabase(dbFile);
                await Task.Delay(5000);
            }
            DataBaseFile = dbFile;
        }

        private bool dbOK = true;

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
            get => _databaseFile;
            set
            {
                SetProperty(ref _databaseFile, value);
                //if (_databaseFile != value)
                //{
                //    try
                //    {
                //        if (await model.SetDatabase(value))
                //        {
                //            SetProperty(settings, s => s.DatabaseFile, value);
                //            settings.Save();
                //        }
                //        else
                //        {
                //            Trace.TraceWarning($"Errors opening new database file {value}");
                //            MainViewModel.dialogService.ShowDialog($"Die Datenbank \n{value}\n\n konnte nicht geladen werden. Bitte wählen sie eine kompatible Datenbank aus");
                //        }
                //    }
                //    catch
                //    {
                //        Trace.TraceWarning($"Errors opening new database file {value}");
                //        MainViewModel.dialogService.ShowDialog(new string($"Die Datenbank \n{value}\n\n konnte nicht geladen werden. Bitte wählen sie eine kompatible Datenbank aus"));
                //    }
                //}
            }
        }

        public string Error => null!;

        public string this[string columnName]
        {
            get
            {
                if (columnName == nameof(DataBaseFile))
                {
                    return dbOK ? null : "Datenbank nicht kompatibel";                    
                }
                return null;
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
            _databaseFile = settings.DatabaseFile;
        }
    }

}

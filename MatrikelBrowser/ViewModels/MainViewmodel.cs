using AEM;
using iText.Commons.Utils;
using System;
using System.IO;

namespace ArchiveBrowser.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        #region Commands                
        public RelayCommand cmdSave => _cmdSave ??= new RelayCommand(doSave);
        private void doSave(object? o)
        {
            model.saveNotes();
        }
        #endregion

        #region Properties
        public TectonicsVM tectonicsVM { get; }
        public SettingsVM SettingsVM { get; }
        #endregion

        public MainViewModel()
        {
            setup();
            var settings = MatrikelBrowser.Properties.Settings.Default;
            string dbFilename = settings.DatabaseFile;
            model = new aemCore();

            var dbInfo = model.CheckDatabase(dbFilename);
            if(dbInfo.IsCompatible)
            {
                if(dbInfo.PendingMigrations > 0)
                {
                    model.MigrateDatabase(dbFilename);  // todo: ask user to confirm first
                }
                model.SetDatabase(dbFilename);
            }


            SettingsVM = new(model);
            tectonicsVM = new(model);
        }

        #region private methods and fields  
        private void setup()
        {
            var settings = MatrikelBrowser.Properties.Settings.Default;

            if (settings.MustUpgrade) // handle app version upgrades
            {
                settings.Upgrade();
                settings.MustUpgrade = false;
                settings.Save();
            }

            if (string.IsNullOrEmpty(settings.DatabaseFile))
            {
                settings.DatabaseFile = new(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "lunOptics", "MatrikelBrowser", "MatrikelBrowser.db"));
                settings.Save();
            }
            //MatrikelBrowserCTX.DatabaseFile = settings.DatabaseFile;
        }

        private RelayCommand? _cmdSave;
        private readonly aemCore model;
        #endregion
    }
}

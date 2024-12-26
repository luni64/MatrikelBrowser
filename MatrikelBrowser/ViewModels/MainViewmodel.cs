using MbCore;
using MatrikelBrowser.Infrastructure;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;

namespace MatrikelBrowser.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        #region Commands                
        public RelayCommand cmdSave => _cmdSave ??= new RelayCommand((object? _) => aemCore.saveNotes());
        public RelayCommand cmdSettings => _cmdSettings ??= new RelayCommand((object? _) => dialogService.ShowDialog(new SettingsVM(model)));
        #endregion

        #region Properties
        public TectonicsVM tectonicsVM { get; }
        #endregion

        public MainViewModel()
        {
            model = new();
            tectonicsVM = new(model);
            model.DatabaseChanged += () => tectonicsVM.UpdateData();  // update the displayed data in case of db changes

            var dbFilname = getDatabaseFilename(); // read from user settings
            if (!model.SetDatabase(dbFilname))     // try to set the database, data will be empty in case of errors
            {
                Trace.TraceInformation("Error loading data");
            }
        }

        #region private methods and fields  
        private static string getDatabaseFilename()
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
            return settings.DatabaseFile;
        }

        private RelayCommand? _cmdSave;
        private RelayCommand? _cmdSettings;
        private aemCore model { get; set; }
        private readonly IDialogService dialogService = new DialogService();
        #endregion
    }
}

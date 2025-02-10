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
        public RelayCommand cmdSave => _cmdSave ??= new RelayCommand((object? _) =>
        {
            tectonicsVM.SaveSelectedTab();
            tectonicsVM.SaveSettings();
        });
        public RelayCommand cmdSettings => _cmdSettings ??= new RelayCommand((object? _) => dialogService.ShowDialog(new SettingsVM(model)));
        #endregion

        #region Properties
        public TectonicsVM tectonicsVM { get; }
        #endregion

        public MainViewModel(Core model)
        {
            this.model = model;
            tectonicsVM = new(model);
            model.DatabaseChanged += () => tectonicsVM.UpdateData();  // update the displayed data in case of db changes
            tectonicsVM.UpdateData();

            //var dbFilename = getDatabaseSetting(); // read from user settings
            //if (!model.SetDatabase(dbFilename))     // try to set the database, data will be empty in case of errors
            //{
            //    Trace.TraceInformation("Error loading data");
            //    dialogService.ShowDialog(new string($"Die Datenbank \n{dbFilename}\n\n konnte nicht geladen werden. Bitte wählen sie in den Datenbank Einstellungen eine kompatible Datenbank aus"));
            //}
        }

        #region private methods and fields  
        //private static string getDatabaseSetting()
        //{
        //    var settings = MatrikelBrowser.Properties.Settings.Default;

        //    if (settings.MustUpgrade) // handle app version upgrades
        //    {
        //        settings.Upgrade();
        //        settings.MustUpgrade = false;
        //        settings.Save();
        //    }

        //    if (string.IsNullOrEmpty(settings.DatabaseFile))
        //    {
        //        settings.DatabaseFile = new(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "lunOptics", "MatrikelBrowser", "MatrikelBrowser.db"));
        //        settings.Save();
        //    }
        //    return settings.DatabaseFile;
        //}

        private RelayCommand? _cmdSave;
        private RelayCommand? _cmdSettings;
        private Core model { get; set; }
        public static IDialogService dialogService = new DialogService();
        #endregion
    }
}

using MatrikelBrowser.ViewModels;
using MatrikelBrowser.Views;
using MbCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MatrikelBrowser
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private async void startup(object sender, StartupEventArgs e)
        {
            initSettings();

            var splash = new Splash();
            splash.Show();

            IProgress<string> progress = new Progress<string>(p => splash.AddLog(p));
            string dbFile = settings.DatabaseFile;

            while (true)
            {
                if (await Core.CheckDatabaseAsync(dbFile!, progress)) break;  // db is valid
                dbFile = await splash.getNewDbFileAsync(dbFile);
                if (string.IsNullOrEmpty(dbFile)) Shutdown(); // user did not provide an alternative db 
            }
            settings.DatabaseFile = dbFile; // save as new default
            settings.Save();

            var core = new Core();
            await core.SetDatabase(dbFile, progress);

            var mainVM = new MainViewModel(core);

            var mainWin = new MainWindow();
            mainWin.DataContext = mainVM;
            mainWin.Show();
            splash.Close();
        }

        private Properties.Settings? settings;

        [MemberNotNull(nameof(settings))]
        void initSettings()  // handle app version upgrades and set default values 
        {
            settings = MatrikelBrowser.Properties.Settings.Default;
            if (settings.MustUpgrade) 
            {
                settings.Upgrade();
                settings.MustUpgrade = false;
                settings.Save();
            }
            if (string.IsNullOrEmpty(settings.DatabaseFile))
            {
                settings.DatabaseFile = new(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "lunOptics", "MatrikelBrowser", "MatrikelBrowser.mbdb"));
                settings.Save();
            }
        }       
    }
}

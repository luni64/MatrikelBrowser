using MatrikelBrowser.ViewModels;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace MatrikelBrowser
{
    /// <summary>
    /// Interaction logic for SettingsDatabase.xaml
    /// </summary>
    public partial class SettingsDatabase : UserControl
    {
        public SettingsDatabase()
        {
            InitializeComponent();
        }

        private void SelectDatabaseClick(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.InitialDirectory = Path.GetDirectoryName(tbDatabase.Text);
            dialog.DefaultExt = ".mbdb"; // Default file extension           
            dialog.Filter = "MatrikelBrowser Datenbanken (.mbdb)|*.mbdb|Alle Dateien|*.*"; // Filter files by extension
            dialog.CheckFileExists = true;

            bool? result = dialog.ShowDialog();

            if (result == true && DataContext is SettingsVM vm)
            {
                vm.foldersVM.DataBaseFile = dialog.FileName;
            }

        }

        private void SelectCacheFolder(object sender, RoutedEventArgs e)
        {
            if (DataContext is SettingsVM vm)
            {
                var dialog = new OpenFolderDialog
                {
                    Title = "Verzeichnis Auswählen",
                    InitialDirectory = vm.foldersVM.CacheFolder,
                   // FolderName = vm.foldersVM.CacheFolder,
                };

                if (dialog.ShowDialog() == true)
                {
                    tbCache.Text = dialog.FolderName; 
                    vm.foldersVM.CacheFolder = dialog.FolderName;
                };
            }

        }
    }
}

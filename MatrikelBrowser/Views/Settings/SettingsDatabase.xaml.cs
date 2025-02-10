using MatrikelBrowser.ViewModels;
using Microsoft.Win32;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
               
        private async void SelectDatabaseClick(object sender, RoutedEventArgs e)
        {
                Cursor = Cursors.ArrowCD;
            var dialog = new OpenFileDialog();
            dialog.InitialDirectory = Path.GetDirectoryName(tbDatabase.Text);
            dialog.DefaultExt = ".mbdb"; // Default file extension           
            dialog.Filter = "MatrikelBrowser Datenbanken (.mbdb)|*.mbdb|Alle Dateien|*.*"; // Filter files by extension
            dialog.CheckFileExists = true;

            //bool? result = dialog.ShowDialog();

            if (dialog.ShowDialog() == true && DataContext is SettingsVM vm)
            {
                await vm.foldersVM.trySetNewDbAsync(dialog.FileName);                
            }


                Cursor = Cursors.Arrow;

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

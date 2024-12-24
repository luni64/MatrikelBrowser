using ArchiveBrowser.ViewModels;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace ArchiveBrowser
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
            dialog.DefaultExt = ".db"; // Default file extension           
            dialog.Filter = "SQLite databases (.db)|*.db|All files|*.*"; // Filter files by extension
            dialog.CheckFileExists = true;
                        
            bool? result = dialog.ShowDialog();
                        
            if (result == true && DataContext is SettingsVM vm)
            {
                vm.DataBaseFile = dialog.FileName;
            }

        }
    }
}

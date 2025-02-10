using MahApps.Metro.Controls;
using MatrikelBrowser.ViewModels;
using Microsoft.Win32;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MatrikelBrowser.Views
{
    /// <summary>
    /// Interaction logic for Splash.xaml
    /// </summary>
    public partial class Splash : MetroWindow
    {
        public Splash()
        {
            InitializeComponent();                       
        }

        bool clicked = false;
        public async Task<string> getNewDbFileAsync(string oldFile)
        {
           spButtons.Visibility = Visibility.Visible;
            while(!clicked)
            {
                await Task.Delay(250);
            }
            clicked = false;

            var dlg = new OpenFileDialog();
            dlg.Filter = "Datenbank Dateien|*.mbdb|Alle Dateien|*.*";
            dlg.DefaultExt = ".db";
            dlg.InitialDirectory = Path.GetDirectoryName(oldFile);

            spButtons.Visibility = Visibility.Hidden;
            return dlg.ShowDialog() == true ? dlg.FileName : string.Empty;
        }

        public void AddLog(string message)
        {
            tbLog.Text += (message + "\n");
        }

        private void BrowseClick(object sender, RoutedEventArgs e)
        {
            clicked = true;          
        }
        private void scrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            (sender as ScrollViewer)?.ScrollToEnd();
        }
    }
}

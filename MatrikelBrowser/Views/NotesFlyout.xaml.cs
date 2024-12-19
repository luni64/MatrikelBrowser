using MatrikelBrowser.ViewModels;
using MatrikelBrowser.Views;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

//using System.Windows.Forms;
//using System.Windows.Input;

namespace MatrikelBrowser
{
    /// <summary>
    /// Interaction logic for NotesFlyout.xaml
    /// </summary>
    public partial class NotesFlyout : UserControl
    {
        public NotesFlyout()
        {
            InitializeComponent();
            lbBookmarks.Items.SortDescriptions.Add(new SortDescription("SheetNr", ListSortDirection.Ascending));
        }


        private void ReportClick(object sender, RoutedEventArgs e)
        {
            if (DataContext is BookVM dc)
            {
                Cursor = System.Windows.Input.Cursors.Wait;
                dc.cmdGenerateReport.Execute(null);
                if (dc.ReportFile != null)
                {
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {dc.ReportFile}")
                    {
                        CreateNoWindow = true,
                        WindowStyle = ProcessWindowStyle.Minimized
                    });                   
                }
                else
                {
                    System.Windows.MessageBox.Show(
                        "Der Report kann nicht erzeugt werden!\nFalls der Report bereits geöffnet ist, schließen Sie bitte die Datei und versuchen Sie es nochmal.",
                        "Datei-Zugriffsfehler",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                Cursor = System.Windows.Input.Cursors.Arrow;

            }

        }
    }
}

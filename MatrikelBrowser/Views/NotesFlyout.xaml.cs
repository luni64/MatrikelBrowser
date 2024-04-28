using ArchiveBrowser.ViewModels;
using System.ComponentModel;
using ArchiveBrowser.Views;
using SimpleWPFReporting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ArchiveBrowser
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is BookVM vm)
            {
                ////vm.cmdGenerateReport.Execute(null);

                //ReportView report = new ReportView();
                //report.DataContext = vm.reportVM;
                //report.ShowDialog();

                //   Report.PrintReport(report.mainGrid,vm.reportVM,ReportOrientation.Portrait);
            }
        }

        private void ReportClick(object sender, RoutedEventArgs e)
        {
            if (DataContext is BookVM dc)
            {
                Cursor = Cursors.Wait;
                dc.cmdGenerateReport.Execute(null);
                if (dc.ReportFile != null)
                {
                    var view = new ReportView(dc.ReportFile);
                    view.Show();
                }
                Cursor = Cursors.Arrow;

            }

        }
    }
}

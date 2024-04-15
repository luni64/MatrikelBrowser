using ArchiveBrowser.ViewModels;
using MatrikelBrowser.Views;
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

            lbBookmarks.Items.SortDescriptions.Add(
                new System.ComponentModel.SortDescription("Sheet", System.ComponentModel.ListSortDirection.Ascending)
                );
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(DataContext is BookVM vm) 
            {
                //vm.cmdGenerateReport.Execute(null);
                                
                ReportView report = new ReportView();
                report.DataContext = vm.reportVM;
                report.ShowDialog();

             //   Report.PrintReport(report.mainGrid,vm.reportVM,ReportOrientation.Portrait);
            }
        }
    }
}

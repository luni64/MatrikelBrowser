using MatrikelBrowser.ViewModels;
using MahApps.Metro.Controls;
using System.Windows;


namespace MatrikelBrowser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TreeViewItem_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e) => e.Handled = true;
        private void Button_Click(object sender, RoutedEventArgs e) => FirstFlyout.IsOpen = true;
        private void Button_Click_1(object sender, RoutedEventArgs e) => NotesFlyout.IsOpen = true;
        
        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (DataContext is MainViewModel dc)
            {
                dc.cmdSave.Execute(null);
            }
        }       
    }
}

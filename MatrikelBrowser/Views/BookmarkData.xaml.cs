using MatrikelBrowser.ViewModels;
using MahApps.Metro.Controls;
using System.Windows;

namespace MatrikelBrowser.Views
{
    public partial class BookmarkDetailsView : MetroWindow
    {
        public BookmarkDetailsView(BookmarkVM DataContext)
        {
            InitializeComponent();
            this.DataContext = DataContext;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            if(this.DataContext is BookmarkVM bm)
            {
                bm.cmdSaveDetails.Execute(null);
            }
            this.Close();
        }
    }
}

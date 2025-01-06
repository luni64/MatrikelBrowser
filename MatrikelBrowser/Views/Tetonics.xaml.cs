using MatrikelBrowser.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace MatrikelBrowser
{
    /// <summary>
    /// Interaction logic for Tetronics.xaml
    /// </summary>
    public partial class Tectonics : UserControl
    {
        public Tectonics()
        {
            InitializeComponent();
        }
        private void SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (DataContext is TectonicsVM vm && e.NewValue is BookVM bookVM)
            {
                vm.selectedBook = bookVM;
            }
        }

        //prevent auto horizontal scrolling for wide entries
        private void TreeViewItem_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
        {
            e.Handled = true;
        }
        private void ListBoxItem_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
        {
            e.Handled = true;
        }

        //private void TreeBookMenu_Click(object sender, RoutedEventArgs e)
        //{
        //    if (DataContext is TectonicsVM vm &&
        //        e.Source is ToggleButton btn &&
        //        btn.DataContext is Book bookVM)
        //    {
        //        vm.cmdToogleFavorite.Execute(bookVM);
        //    }
        //}

        //private void FavoritesMenu_Click(object sender, RoutedEventArgs e)
        //{
        //    if (DataContext is TectonicsVM vm &&
        //       e.Source is Button btn &&
        //       btn.DataContext is Book bookVM)
        //    {
        //        vm.cmdToogleFavorite.Execute(bookVM);                
        //    }
        //}
    }
}



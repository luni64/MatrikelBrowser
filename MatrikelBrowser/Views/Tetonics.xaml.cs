using MatrikelBrowser.ViewModels;
using MahApps.Metro.IconPacks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        private void TreeBookMenu_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is TectonicsVM vm &&
                e.Source is ToggleButton btn &&
                btn.DataContext is BookVM bookVM)
            {
                vm.cmdToogleFavorite.Execute(bookVM);
            }
        }

        private void FavoritesMenu_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is TectonicsVM vm &&
               e.Source is Button btn &&
               btn.DataContext is BookVM bookVM)
            {
                vm.cmdToogleFavorite.Execute(bookVM);                
            }

        }

    }
}



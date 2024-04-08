using ArchiveBrowser.ViewModels;
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
            if (DataContext is MainViewModel dc && e.NewValue is BookVM bookVM)
            {
                dc.selectedBook = bookVM;
            }
        }

        //prevent auto horizontal scrolling for wide entries
        private void TreeViewItem_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
        {
            e.Handled = true;
        }

    }
}



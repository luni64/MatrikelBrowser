using MatrikelBrowser.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MatrikelBrowser
{
    /// <summary>
    /// Interaction logic for BooksTabControl.xaml
    /// </summary>
    public partial class BooksTabControl : UserControl
    {
        public BooksTabControl()
        {
            InitializeComponent();
            MahApps.Metro.Behaviors.ReloadBehavior.SetOnSelectedTabChanged(tabControl , false);
        }

        private TabItem? draggedTab;

        private void onMouseRightDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is TabItem tabItem)
            {
                draggedTab = tabItem;              
            }
            else
                draggedTab = null;

        }

        private void onMouseMove(object sender, MouseEventArgs e)
        {
            if (draggedTab != null && e.RightButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(draggedTab, draggedTab, DragDropEffects.All);
            }
        }

        private void onDragEnter(object sender, DragEventArgs e)
        {  
            if (sender is TabItem targetTab && draggedTab != null && targetTab != draggedTab)
            {
                var vm = (TectonicsVM)tabControl.DataContext;
                var targetIdx = vm.DisplayedBooks.IndexOf((TabItemVM)targetTab.DataContext);
                var sourceIdx = vm.DisplayedBooks.IndexOf((TabItemVM)draggedTab.DataContext);
                vm.DisplayedBooks.Move(sourceIdx, targetIdx);
            }
        }        
    }
}

using MatrikelBrowser.ViewModels;
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
            if (sender is TabItem tabItem /*&& Keyboard.Modifiers.HasFlag(ModifierKeys.Control)*/)
            {
                draggedTab = tabItem;
                //var senderVM = tabItem.DataContext as TabItemVM;               
                //Trace.WriteLine($"start drag {senderVM.Letter}");
                //e.Handled = true;
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


        private void MyTabControl_DragEnter(object sender, DragEventArgs e)
        {
            //var targetTab = GetTargetTabItem(e.OriginalSource);
            var targetTab = sender as TabItem;

            if (draggedTab != null && targetTab != draggedTab)
            {
                var vm = (TectonicsVM)tabControl.DataContext;
                var targetIdx = vm.DisplayedBooks.IndexOf((TabItemVM)targetTab.DataContext);
                var sourceIdx = vm.DisplayedBooks.IndexOf((TabItemVM)draggedTab.DataContext);
                vm.DisplayedBooks.Move(sourceIdx, targetIdx);
            }
        }

        //private void MyTabControl_Drop(object sender, DragEventArgs e)
        //{
        //    var tabItemTarget = GetTargetTabItem(e.OriginalSource);
        //    if (tabItemTarget != null)
        //    {
        //        var tabItemSource = (TabItem)e.Data.GetData(typeof(TabItem));
        //        if (tabItemTarget != tabItemSource)
        //        {
        //            if (tabControl.DataContext is TectonicsVM vm)
        //            {
        //                int sourceIndex = tabControl.Items.IndexOf(tabItemSource.DataContext);
        //                int targetIndex = tabControl.Items.IndexOf(tabItemTarget.DataContext);// the rest of your code
        //                Trace.WriteLine($"dragDrop {oldTgtIdx} -> {oldSrcIdx}");
        //                // vm.DisplayedBooks.Move(sourceIndex, targetIndex);
        //            }
        //        }
        //    }
        //}

        //private TabItem GetTargetTabItem(object originalSource)
        //{
        //    var current = originalSource as DependencyObject;

        //    while (current != null)
        //    {
        //        var tabItem = current as TabItem;
        //        if (tabItem != null)
        //        {
        //            return tabItem;
        //        }

        //        current = VisualTreeHelper.GetParent(current);
        //    }

        //    return null;
        //}
    }
}

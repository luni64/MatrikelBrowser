using MatrikelBrowser.ViewModels;
using MahApps.Metro.Controls;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;
using System.Diagnostics;
using iText.Layout.Properties;



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


        //private void MyTabControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    var originalSource = e.OriginalSource as DependencyObject;
        //    while (originalSource != null && !(originalSource is TabItem))
        //    {
        //        originalSource = VisualTreeHelper.GetParent(originalSource);
        //    }

        //    var p = originalSource;
        //    while (p != null && !(p is TabControl))
        //    {
        //        p = VisualTreeHelper.GetParent(p);
        //    }


        //    if (originalSource is TabItem tabItem)
        //    {
        //        _draggedItem = tabItem;
        //    }

        //}

        private void MyTabControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Source is TabItem tabItem && e.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(tabItem, tabItem, DragDropEffects.All);
            }




            //if (_draggedItem != null && e.LeftButton == MouseButtonState.Pressed)
            //{
            //    Trace.WriteLine(DragDrop.DoDragDrop(_draggedItem, _draggedItem, DragDropEffects.All));
            //}
        }

        private void MyTabControl_Drop(object sender, DragEventArgs e)
        {
            var tabItemTarget = GetTargetTabItem(e.OriginalSource);
            if (tabItemTarget != null)
            {
                var tabItemSource = (TabItem)e.Data.GetData(typeof(TabItem));
                if (tabItemTarget != tabItemSource)
                {
                    if (tabControl.DataContext is TectonicsVM vm)
                    {
                        int sourceIndex = tabControl.Items.IndexOf(tabItemSource.DataContext);
                        int targetIndex = tabControl.Items.IndexOf(tabItemTarget.DataContext);// the rest of your code

                        vm.DisplayedBooks.Move(sourceIndex, targetIndex);
                    }
                }
            }
        }

        private TabItem GetTargetTabItem(object originalSource)
        {
            var current = originalSource as DependencyObject;

            while (current != null)
            {
                var tabItem = current as TabItem;
                if (tabItem != null)
                {
                    return tabItem;
                }

                current = VisualTreeHelper.GetParent(current);
            }

            return null;
        }

        private TabItem _draggedItem;

        private int oldTgtIdx, oldSrcIdx;

        private void MyTabControl_DragLeave(object sender, DragEventArgs e)
        {
            if (tabControl.DataContext is TectonicsVM vm)
            {               
                vm.DisplayedBooks.Move(oldTgtIdx, oldSrcIdx);
                Trace.TraceInformation($"dragleave {oldTgtIdx} -> {oldSrcIdx}");
            }
        }

        private void MyTabControl_DragEnter(object sender, DragEventArgs e)
        {
            var tabItemTarget = GetTargetTabItem(e.OriginalSource);
            if (tabItemTarget != null)
            {
                Trace.TraceInformation("dragEnter");
                var tabItemSource = (TabItem)e.Data.GetData(typeof(TabItem));
                if (tabItemTarget != tabItemSource)
                {
                    if (tabControl.DataContext is TectonicsVM vm)
                    {
                        oldSrcIdx = tabControl.Items.IndexOf(tabItemSource.DataContext);
                        oldTgtIdx = tabControl.Items.IndexOf(tabItemTarget.DataContext);// the rest of your code

                        vm.DisplayedBooks.Move(oldSrcIdx, oldTgtIdx);
                    }
                }
            }
        }
    }
}


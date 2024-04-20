using ArchiveBrowser.ViewModels;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;


namespace ArchiveBrowser
{
    /// <summary>
    /// Interaction logic for PageDisplay.xaml
    /// </summary>
    public partial class PageDisplay : UserControl
    {
        public PageDisplay()
        {
            InitializeComponent();
        }

        public PageVM Page
        {
            get => (PageVM)GetValue(PageProperty);
            set => SetValue(PageProperty, value);
        }

        public static readonly DependencyProperty PageProperty =
            DependencyProperty.Register("Page", typeof(PageVM), typeof(PageDisplay), new PropertyMetadata(null, OnPageChanged));


        static void OnPageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is PageVM pageVM)
            {
                var that = ((PageDisplay)d);
                pageVM.Parent.bookmarkVMs.CollectionChanged -= that.Bookmarks_CollectionChanged;
                pageVM.Parent.bookmarkVMs.CollectionChanged += that.Bookmarks_CollectionChanged;

                that.ClearBookmarks();
                foreach (var bmVM in pageVM.Parent.bookmarkVMs.Where(b => b.Sheet == pageVM.SheetNr))
                {
                    that.AddBookmark(bmVM);
                }
            }
        }

        private void Bookmarks_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:

                    foreach (BookmarkVM bm in e.NewItems!)
                    {
                        AddBookmark(bm);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    RemoveBookmarks(e.OldItems!);
                    break;
            }
        }

        void RemoveBookmarks(IList bookmarkVMs)
        {
            var bookmarkUIs = PageCanvas.Children.OfType<Bookmark>();  // we are only interested in canvas-children of type Bookmark

            foreach (BookmarkVM bookmarkVM in bookmarkVMs)
            {
                var bookmarkUI = bookmarkUIs.FirstOrDefault(b => b.Uid == bookmarkVM.ID);  // find the canvas child which corresponds to the removed view model
                if (bookmarkUI != null)
                {
                    PageCanvas.Children.Remove(bookmarkUI);
                }
            }
        }

        void ClearBookmarks()
        {
            var bml = PageCanvas.Children.OfType<Bookmark>().ToList();
            foreach (var bookmark in bml)
            {
                PageCanvas.Children.Remove(bookmark);
            }
        }

        void AddBookmark(BookmarkVM bmVM)
        {
            var bookmark = new Bookmark(bmVM);           

            bookmark.SetBinding(Bookmark.TextProperty, new Binding
            {
                Source = bmVM,
                Path = new PropertyPath("Title"),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

            bookmark.SetBinding(Canvas.TopProperty, new Binding
            {
                Source = bmVM,
                Path = new PropertyPath("Y"),
                Mode = BindingMode.TwoWay,
            });

            bookmark.SetBinding(Canvas.LeftProperty, new Binding
            {
                Source = bmVM,
                Path = new PropertyPath("X"),
                Mode = BindingMode.TwoWay,
            });

            bookmark.SetBinding(Bookmark.WProperty, new Binding
            {
                Source = bmVM,
                Path = new PropertyPath("W"),
                Mode = BindingMode.TwoWay,
            });

            bookmark.SetBinding(Bookmark.HProperty, new Binding
            {
                Source = bmVM,
                Path = new PropertyPath("H"),
                Mode = BindingMode.TwoWay,
            });


            //bookmark.Width = bmVM.W;
            //bookmark.Height = bmVM.H;

            Canvas.SetLeft(bookmark, bmVM.X);
            Canvas.SetTop(bookmark, bmVM.Y);
            bookmark.flip(bmVM.X);


            int idx = PageCanvas.Children.Add(bookmark);
            PageCanvas.Children[idx].Uid = bmVM.ID;
        }

        public override string ToString()
        {
            var dc = DataContext as BookVM;
            return $"B.{dc!.SelectedPage?.SheetNr} Book:{dc.Title}";
        }

        private void AddBookmarkClick(object sender, RoutedEventArgs e)
        {
            if (DataContext is BookVM dc)
            {
                var pos = Mouse.GetPosition(pageimg);                
                dc.cmdAddBookmark.Execute(((int)pos.X,(int)pos.Y));
            }
        }
    }

}
;
using MatrikelBrowser.ViewModels;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;


namespace MatrikelBrowser
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
            if (e.NewValue is PageVM pageVM && pageVM.parent is BookVM bookVM)
            {               
                var that = ((PageDisplay)d);
                bookVM.EventVMs.CollectionChanged -= that.Bookmarks_CollectionChanged;
                bookVM.EventVMs.CollectionChanged += that.Bookmarks_CollectionChanged;

                that.ClearBookmarks();
                foreach (var eventVM in bookVM.EventVMs.Where(e=> e.SheetNr-1 == pageVM.SheetNr))
                {
                    that.AddBookmark(eventVM);
                }
            }
        }

        private void Bookmarks_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:

                    foreach (EventVM bm in e.NewItems!)
                    {
                        AddBookmark(bm);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    RemoveBookmarks(e.OldItems!);
                    break;
            }
        }

        void RemoveBookmarks(IList eventVMs)
        {
            var bookmarkUIs = PageCanvas.Children.OfType<Bookmark>();  // we are only interested in canvas-children of type Bookmark

            foreach (EventVM eventVM in eventVMs)
            {
                var bookmarkUI = bookmarkUIs.FirstOrDefault(b => b.Uid == eventVM.model.Id.ToString());  // find the canvas child which corresponds to the removed view marriageModel
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

        void AddBookmark(EventVM eventVM)
        {
            var bookmark = new Bookmark(eventVM);           

            bookmark.SetBinding(Bookmark.TextProperty, new Binding
            {
                Source = eventVM,
                Path = new PropertyPath("Title"),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });

            bookmark.SetBinding(Canvas.TopProperty, new Binding
            {
                Source = eventVM,
                Path = new PropertyPath("Y"),
                Mode = BindingMode.TwoWay,
            });

            bookmark.SetBinding(Canvas.LeftProperty, new Binding
            {
                Source = eventVM,
                Path = new PropertyPath("X"),
                Mode = BindingMode.TwoWay,
            });

            bookmark.SetBinding(Bookmark.WProperty, new Binding
            {
                Source = eventVM,
                Path = new PropertyPath("W"),
                Mode = BindingMode.TwoWay,
            });

            bookmark.SetBinding(Bookmark.HProperty, new Binding
            {
                Source = eventVM,
                Path = new PropertyPath("H"),
                Mode = BindingMode.TwoWay,
            });


            //bookmark.Width = eventVM.W;
            //bookmark.Height = eventVM.H;

            Canvas.SetLeft(bookmark, eventVM.X);
            Canvas.SetTop(bookmark, eventVM.Y);
            bookmark.flip(eventVM.X);


            int idx = PageCanvas.Children.Add(bookmark);
            PageCanvas.Children[idx].Uid = eventVM.ID.ToString();
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
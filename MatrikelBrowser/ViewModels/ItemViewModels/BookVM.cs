using MbCore;
using Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AEM.Tectonics;
using AEM;
using AEM.Tectonics.Events;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using iText.Kernel.Pdf.Canvas.Parser;

namespace MatrikelBrowser.ViewModels
{
    public class BookVM : ItemVM
    {
        #region Commands ------------------------------------------------------
        public RelayCommand cmdChangePage => _cmdChangePage ??= new RelayCommand(doChangePage);
        void doChangePage(object? delta)
        {
            int pageNr = SelectedPageNr + (int)(delta ?? 0);
            SelectedPageNr = Math.Min(Math.Max(1, pageNr), PageVMs.Count); // clamp
        }

        public RelayCommand cmdAddBookmark => _cmdAddBookmark ??= new RelayCommand(doAddBookmark);
        void doAddBookmark(object? pos)
        {
            if (SelectedPage != null)
            {
                var (x, y) = pos != null ? ((int X, int Y))pos! : (0, 0);


                Event evnt = new()
                {
                    Book = model,
                    SheetNr = PageVMs.IndexOf(SelectedPage) + 1,
                    Title = "Neue Fundstelle",
                    X = x,
                    Y = y,
                    W = 500,
                    H = 250,

                    EventType = this.model.BookType switch
                    {
                        BookType.Taufbücher => BookmarkType.birth,
                        BookType.Hochzeitsbücher => BookmarkType.marriage,
                        BookType.Sterbebücher => BookmarkType.death,
                        BookType.Mischbände => BookmarkType.misc,
                        _ => BookmarkType.misc
                    }
                };

                model.AddEvent(evnt);

                var evm = evnt.EventType switch
                {
                    BookmarkType.birth => (EventVM) new BirthEventVM(evnt),
                    BookmarkType.death => (EventVM) new DeathEventVM(evnt),
                    BookmarkType.marriage => (EventVM)new MarriageEventVM(evnt),
                    _ => new BirthEventVM(evnt)
                } ;
                evm.isLocked = false;
                EventVMs.Add(evm);




                //IBookmarkBase bm = new BookmarkBase()
                //{
                //    SheetNr = PageVMs.IndexOf(SelectedPage) + 1,
                //    Title = "Neue Fundstelle",
                //    X = x,
                //    Y = y,
                //};

                //bookmarkVMs.Add(
                //    new BookmarkVM(bm, this)
                //    {
                //        ID = Guid.NewGuid().ToString(),
                //        Page = SelectedPage,
                //        isLocked = false,
                //        bookmarkType = this.model.BookType switch
                //        {
                //            BookType.Taufbücher => EventType.birth,
                //            BookType.Hochzeitsbücher => EventType.marriage,
                //            BookType.Sterbebücher => EventType.death,
                //            _ => EventType.misc,
                //        }
                //    }
                //);
                //marriageModel.Info.Bookmarks.Add(bm);
            }
        }

        public RelayCommand cmdDelEvent => _cmdDelEvent ??= new RelayCommand(doDelEvent);
        void doDelEvent(object? o)
        {
            //var bm = SelectedBookmark;
            if (o is EventVM eventVM)
            {
                // marriageModel.Info.Bookmarks.Remove(bookmarkVM.marriageModel);
                EventVMs.Remove(eventVM);
                eventVM.model.RemoveFromDatabase();
            }
        }

        public RelayCommand cmdGenerateReport => _cmdGenerateReport ??= new RelayCommand((object? _) => ReportFile = Report.Generate(model)?.FullName);
        #endregion

        #region Properties ----------------------------------------------------
        public ObservableCollection<PageVM> PageVMs { get; } = [];
        public PageVM? SelectedPage   // binds to the displayed page image
        {
            get => _selectedPage;
            set => SetProperty(ref _selectedPage, value);
        }
        public ObservableCollection<BookmarkVM> bookmarkVMs { get; } = [];
        public BookmarkVM? SelectedBookmark
        {
            get => _selectedBookmark;
            set
            {
                SetProperty(ref _selectedBookmark, value);
                SelectedPageNr = _selectedBookmark?.SheetNr ?? 1;
            }
        }
        public ObservableCollection<EventVM> EventVMs { get; } = [];
        public EventVM? SelectedEvent
        {
            get => _selectedEvent;
            set
            {
                SetProperty(ref _selectedEvent, value);
                SelectedPageNr = _selectedEvent?.SheetNr ?? 1;
            }
        }
        public string Title => model.Title;
        public string ID => model.RefId;
        public string? SubTitle
        {
            get => _subTitle;
            set => SetProperty(ref _subTitle, value);
        }
        public string Note
        {
            get => model.Note;
            set => SetProperty(model, b => b.Note, value);
        }
        public BookType BookType => model.BookType;


        public override bool IsSelected
        {
            get => base.IsSelected;
            set
            {
                if (value != base.IsSelected)
                {
                    if (value == true) Initialize();
                    else SubTitle = null;

                    (parent as BookGroupVM)!.SelectedBook = value ? this : null;

                    base.IsSelected = value;
                    OnPropertyChanged();
                }
            }
        }

        public int SelectedPageNr     // binds to the page slider, delayed update, 
        {
            get => _selectedPageNr;
            set
            {
                SetProperty(ref _selectedPageNr, value);
                SelectedPage = PageVMs?.ElementAtOrDefault(SelectedPageNr - 1);
            }
        }

        public double zoom
        {
            get => _zoom;
            set => SetProperty(ref _zoom, value);
        }
        public double PanX
        {
            get => _panX;
            set => SetProperty(ref _panX, value);
        }
        public double PanY
        {
            get => _panY;
            set => SetProperty(ref _panY, value);
        }

        public string? ReportFile { get; internal set; }

        #endregion
        public void Initialize()
        {
            if (model.Pages.Count == 0) // we lazy load pages
            {
                SubTitle = $"downloading page informations for book: {Title}";
                //await Task.Run(() =>
                //{
                model.LoadPageInfo(); // load or read page info from mets.xml
                if (model.Pages.Count > 0) // setup page viewmodels
                {
                    PageVMs.Clear();
                    foreach (var page in model.Pages)
                    {
                        PageVMs.Add(new PageVM(page, this));
                    }
                    SelectedPageNr = 1;
                    OnPropertyChanged("bookmarkVMs");
                }
                // });

                model.LoadEvents();
                if (model.Events.Count > 0)
                {
                    EventVMs.Clear();



                    foreach (var evnt in model.Events)
                    {
                        EventVMs.Add(evnt.EventType switch
                        {
                            BookmarkType.birth => (EventVM)new BirthEventVM(evnt),
                            BookmarkType.marriage => (EventVM) new MarriageEventVM(evnt),
                            BookmarkType.death => (EventVM)new DeathEventVM(evnt),
                            _ => new BirthEventVM(evnt),
                        });







                    }
                }
                SubTitle = $"{PageVMs.Count} Blätter";
            }
            else
            {
                SubTitle = $"{PageVMs.Count} Blätter";
            }
        }


        public bool IsFavorite
        {
            get => _isFavorite;
            set => SetProperty(ref _isFavorite, value);
        }


        public BookVM(MbCore.Book model, BookGroupVM parent) : base(parent)
        {
            this.model = model;
            this.Note = model.Note;

            //foreach (var f in model.Events)
            //{
            //    EventVMs.Add(f switch
            //    {
            //        BirthEvent => new BirthFindingVM((BirthEvent)f),
            //        MarriageEvent => new MarriageFindingVM((MarriageEvent)f),
            //        _ => new FindingVM(f)
            //    });
            //}

            Indent = -10;
        }

        public readonly MbCore.Book model;
        private EventVM? _selectedEvent;
        private RelayCommand? _cmdDelEvent;
        private RelayCommand? _cmdChangePage;
        private RelayCommand? _cmdAddBookmark;
        private RelayCommand? _cmdGenerateReport;
        //private bool _isSelected;
        private bool _isFavorite;

        private PageVM? _selectedPage;
        private int _selectedPageNr;
        private BookmarkVM? _selectedBookmark = null;
        private NoteVM? _noteVM;
        private string? _subTitle;
        //private ReportVM? _reportVM;
        private double _panX;
        private double _panY;
        private double _zoom = 0.3;
    }
}


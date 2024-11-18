using AEM;
using Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ArchiveBrowser.ViewModels
{
    public class BookVM : ItemVM
    {
        #region Commands --------------------------------

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

                IBookmarkBase bm = new BookmarkBase()
                {
                    SheetNr = PageVMs.IndexOf(SelectedPage)+1,
                    Title = "Neue Fundstelle",
                    X = x,
                    Y = y,                               
                };

                bookmarkVMs.Add(
                    new BookmarkVM(bm, this)
                    {
                        ID = Guid.NewGuid().ToString(),
                        Page = SelectedPage,
                        isLocked = false,
                        bookmarkType = this.model.Type switch
                        {
                            BookType.Taufen => BookmarkType.birth,
                            BookType.Trauungen => BookmarkType.marriage,
                            BookType.Sterbefälle => BookmarkType.death,
                            _ => BookmarkType.misc,                            
                        }
                    }
                ); 
                model.Info.Bookmarks.Add(bm);
            }
        }

        public RelayCommand cmdDelBookmark => _cmdDelBookmark ??= new RelayCommand(doDelBookmark);
        void doDelBookmark(object? s)
        {            
            var bm = SelectedBookmark;
            if (SelectedBookmark != null)
            {
                model.Info.Bookmarks.Remove(SelectedBookmark.model);
                bookmarkVMs.Remove(SelectedBookmark);
            }
        }

        public RelayCommand cmdGenerateReport => _cmdGenerateReport ??= new RelayCommand(doGenerateReport);
        void doGenerateReport(object? s)
        {
            ReportFile = Report.Generate(model)?.FullName;
        }   

        #endregion

        #region Properties ----------------------------------------
        public string Title { get; }
        public string ID { get; }
        public string? SubTitle
        {
            get => _subTitle;
            set => SetProperty(ref _subTitle, value);
        }
        public ObservableCollection<PageVM> PageVMs { get; } = [];
        public ObservableCollection<BookmarkVM> bookmarkVMs { get; } = [];
        public PageVM? SelectedPage   // binds to the displayed page image
        {
            get => _selectedPage;
            set
            {
                SetProperty(ref _selectedPage, value);
            }
        }
        public NoteVM NoteVM
        {
            get
            {
                if (_noteVM == null)
                {
                    model.Info.BookID = model.ID;
                    _noteVM = new NoteVM(model.Info);
                }
                return _noteVM;
            }
        }
        public ParishVM ParishVM { get; }
        public int SelectedPageNr     // binds to the page slider, delayed update, 
        {
            get => _selectedPageNr;
            set
            {
                SetProperty(ref _selectedPageNr, value);
                SelectedPage = PageVMs?.ElementAtOrDefault(SelectedPageNr-1);
            }
        }
        public override bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
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

        public BookmarkVM? SelectedBookmark
        {
            get => _selectedBookmark;
            set
            {
                SetProperty(ref _selectedBookmark, value);
                SelectedPageNr = _selectedBookmark?.SheetNr ?? 1;
            }
        }
        public string? ReportFile { get; internal set; }

        #endregion
        public async void Intialize()
        {
            if (model.Pages.Count == 0) // we lazy load pages
            {
                SubTitle = "Downloading...";
                await Task.Run(() =>
                {
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
                });
                SubTitle = $"{PageVMs.Count} Blätter";

                foreach (var bm in model.Info.Bookmarks)
                {
                    if (bm.SheetNr <= PageVMs.Count) // just to be sure...
                    {                       
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            bookmarkVMs.Add(new BookmarkVM(bm,this)
                            {
                                ID = Guid.NewGuid().ToString(),
                                Page = PageVMs[bm.SheetNr],
                            });
                        });
                    }
                }
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

        public BookVM(IBook model, ParishVM parish)
        {
            this.model = model;
            this.ParishVM = parish;
            Title = $"{model.ID} {model.Title}";         
            ID = model.ID;
            Indent = 7;
        }

        private RelayCommand? _cmdDelBookmark;
        private RelayCommand? _cmdChangePage;
        private RelayCommand? _cmdAddBookmark;
        private RelayCommand? _cmdGenerateReport;
        private bool _isSelected;
        private bool _isFavorite;
        private PageVM? _selectedPage;
        private int _selectedPageNr;
        private BookmarkVM? _selectedBookmark = null;
        private NoteVM? _noteVM;
        private string? _subTitle;
        //private ReportVM? _reportVM;
        private readonly IBook model;
        private double _panX;
        private double _panY;
        private double _zoom = 0.3;
    }
}


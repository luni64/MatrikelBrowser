﻿using AEM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;

namespace ArchiveBrowser.ViewModels
{
    public class BookVM : ItemVM
    {
        #region Commands --------------------------------

        public RelayCommand cmdChangePage => _cmdChangePage ??= new RelayCommand(doChangePage);
        void doChangePage(object? delta)
        {
            int pageNr = SelectedPageNr + (int)(delta ?? 0);
            SelectedPageNr = Math.Min(Math.Max(0, pageNr), PageVMs.Count - 1); // clamp
        }

        public RelayCommand cmdAddBookmark => _cmdAddBookmark ??= new RelayCommand(doAddBookmark);
        void doAddBookmark(object? pos)
        {
            if (SelectedPage != null)
            {
                var (x, y) = pos != null ? ((int X, int Y))pos! : (0, 0);

                AEM.Bookmark bm = new()
                {
                    Sheet = PageVMs.IndexOf(SelectedPage),
                    Title = "Neues Lesezeichen",
                    X = x,
                    Y = y,
                };

                bookmarkVMs.Add(
                    new BookmarkVM(bm)
                    {
                        ID = Guid.NewGuid().ToString(),
                        Page = SelectedPage
                    }
                );
                model.Note.Bookmarks.Add(bm);
            }
        }

        public RelayCommand cmdDelBookmark => _cmdDelBookmark ??= new RelayCommand(doDelBookmark);
        void doDelBookmark(object? s)
        {
            var bm = SelectedBookmark;
            if (SelectedBookmark != null)
            {
                model.Note.Bookmarks.Remove(SelectedBookmark.model);
                bookmarkVMs.Remove(SelectedBookmark);
            }
        }


        #endregion

        #region Properties ----------------------------------------
        public string Title { get; }
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
                    model.Note.BookID = model.ID;
                    _noteVM = new NoteVM(model.Note);
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
                SelectedPage = PageVMs?.ElementAtOrDefault(SelectedPageNr);
            }
        }
        public override bool IsSelected
        {
            get => _isSelected;
            set
            {
                SetProperty(ref _isSelected, value);
                if (value == true)
                {
                    if (model.Pages.Count == 0)
                    {
                        SubTitle = "Downloading...";
                        Task.Run(() =>
                        {
                            model.LoadInfo();
                            if (model.Pages.Count > 0)
                            {
                                PageVMs.Clear();
                                foreach (var page in model.Pages)
                                {
                                    PageVMs.Add(new PageVM(page, this));
                                }
                                SelectedPageNr = 0;
                                OnPropertyChanged("bookmarkVMs");
                            }
                            SubTitle = $"{PageVMs.Count} Blätter";


                            foreach (var bm in model.Note.Bookmarks)
                            {
                                if (bm.Sheet < PageVMs.Count) // just to be sure...
                                {
                                    Application.Current.Dispatcher.Invoke(() =>
                                    {
                                        bookmarkVMs.Add(new BookmarkVM(bm)
                                        {
                                            ID = Guid.NewGuid().ToString(),
                                            Page = PageVMs[bm.Sheet],
                                        });
                                    });
                                }

                            }
                        });
                    }
                    else // already loaded, display details
                    {
                        SubTitle = $"{PageVMs.Count} Blätter";
                    }
                }
                else  // selection lost, hide details
                {
                    SubTitle = null;
                }
            }
        }

        private double _zoom = 0.3;
        public double zoom
        {
            get => _zoom;
            set => SetProperty(ref _zoom, value);
        }

        private double _panX = 0;

        public double PanX
        {
            get => _panX;
            set => SetProperty(ref _panX, value);
        }

        private double _panY = 0;
        public double PanY
        {
            get => _panY;
            set => SetProperty(ref _panY, value);
        }



        private BookmarkVM? _selectedBookmark = null;
        public BookmarkVM? SelectedBookmark
        {
            get => _selectedBookmark;
            set
            {
                SetProperty(ref _selectedBookmark, value);
                SelectedPageNr = _selectedBookmark?.Sheet ?? 0;
            }
        }

        #endregion

        public BookVM(Book model, ParishVM parish)
        {
            this.model = model;
            this.ParishVM = parish;
            Title = $"{model.ID} {model.Title}";

            //foreach (var bm in model.Note.Bookmarks)
            //{
            //    if (bm.Sheet < PageVMs.Count) // just to be sure...
            //    {
            //        bookmarkVMs.Add(new BookmarkVM(bm)
            //        {
            //            ID = Guid.NewGuid().ToString(),
            //            Page = PageVMs[bm.Sheet],
            //        }
            //        );
            //    }

            //}

            Indent = 7;

            //bookmarkVMs = new();
            //bookmarkVMs.Source = _bookmarkVMs;


        }

        private RelayCommand? _cmdDelBookmark;
        private RelayCommand? _cmdChangePage = null;
        private RelayCommand? _cmdAddBookmark = null;
        private int _selectedPageNr = 0;
        private bool _isSelected;
        private PageVM? _selectedPage;
        private NoteVM? _noteVM;
        private String? _subTitle;
        private readonly Book model;
    }
}


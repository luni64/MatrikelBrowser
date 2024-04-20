using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Media.Media3D;

namespace ArchiveBrowser.ViewModels
{
    public enum BookmarkType
    {
        birth, marriage, death, misc
    }

    public class BookmarkVM : BaseViewModel
    {
        public string Title
        {
            get => model.Title;
            set
            {
                if (value != model.Title)
                {
                    model.Title = value;
                    OnPropertyChanged();
                }
            }
        }
        public string? note
        {
            get;
            set;
        }





        private BookmarkType _bookmarkType;
        public BookmarkType bookmarkType
        {
            get => _bookmarkType;
            set
            {
                SetProperty(ref _bookmarkType, value);
                SelectedViewModel = detailViewmodels[bookmarkType]!;
            }
        }

        private BaseViewModel _selectedViewModel;
        public BaseViewModel SelectedViewModel
        {
            get => _selectedViewModel;
            set => SetProperty(ref _selectedViewModel, value);
        }


        public int Sheet
        {
            get => model.Sheet;
            set => model.Sheet = value;
        }
        public int X
        {
            get => model.X;
            set => model.X = value;
        }
        public int Y
        {
            get => model.Y;
            set => model.Y = value;
        }
        public double W
        {
            get => model.W;
            set => model.W = value;
        }
        public double H
        {
            get => model.H;
            set => model.H = value;
        }


        public string ID { get; set; } = Guid.Empty.ToString();
        public PageVM? Page { get; set; }

        public BookmarkVM(AEM.Bookmark model)
        {
            this.model = model;
            detailViewmodels.Add(BookmarkType.birth, new BirthBookmarkVM(model));
            detailViewmodels.Add(BookmarkType.marriage,new MarriageBookmarkVM(model));
            detailViewmodels.Add(BookmarkType.death, new DeathBookmarkVM(model));
            detailViewmodels.Add(BookmarkType.misc, new MiscBookmarkVM(model));
            SelectedViewModel = detailViewmodels[BookmarkType.birth]!;
        }

        public AEM.Bookmark model { get; }

        Dictionary<BookmarkType, BaseViewModel?> detailViewmodels = new();
    }

    class BirthBookmarkVM : BaseViewModel 
    { 
        public string Child { get; set; }
        public string Father { get; set; }
        public string Mother { get; set; }
        public string BaptizeDate { get; set; }
        public string Witnesses { get; set; }
        public string Transcript { get; set; }

        public BirthBookmarkVM(AEM.Bookmark model)     
        {
            this.model = model;
        }

        private AEM.Bookmark model;

    }
    class MarriageBookmarkVM : BaseViewModel { public MarriageBookmarkVM(AEM.Bookmark model)  { } }
    class DeathBookmarkVM : BaseViewModel { public DeathBookmarkVM(AEM.Bookmark model)   { } }
    class MiscBookmarkVM : BaseViewModel { public MiscBookmarkVM(AEM.Bookmark model) { } }
}


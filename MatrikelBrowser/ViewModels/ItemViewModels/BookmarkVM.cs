using AEM;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Media.Media3D;


namespace ArchiveBrowser.ViewModels
{
    //public enum BookmarkTypes
    //{
    //    Birth, Marriage, Death, Misc
    //};

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
        public string Transcript
        {
            get => model.Transkript;
            set
            {
                if (value != model.Transkript)
                {
                    model.Transkript = value;
                    OnPropertyChanged();
                }
            }
        }

        private BookmarkType _bookmarkType;
        public BookmarkType bookmarkType
        {
            get => _bookmarkType;
            set 
            {
                SetProperty(ref  _bookmarkType, value);

            }
        }


        //public BookmarkType bookmarkType
        //{
        //    get => model.bookmarkType;
        //    set
        //    {
        //        if (value != model.bookmarkType)
        //        {
        //            model.bookmarkType = value;
        //            SelectedViewModel = detailViewmodels[bookmarkType]!;
        //            OnPropertyChanged();
        //        }
        //    }
        //}
        private BaseViewModel _selectedViewModel;
        public BaseViewModel SelectedViewModel
        {
            get => _selectedViewModel;
            set => SetProperty(ref _selectedViewModel, value);
        }

        private bool _isLocked = true;
        public bool isLocked
        {
            get => _isLocked;
            set => SetProperty(ref _isLocked, value);
        }

        public int SheetNr
        {
            get => model.SheetNr;
            set => model.SheetNr = value;
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

        public BookmarkVM(IBookmarkBase model)
        {
            this.model = model;
            detailViewmodels.Add(BookmarkType.birth, new BirthBookmarkVM(model));
            //detailViewmodels.Add(BookmarkType.marriage, new MarriageBookmarkVM(model));
            //detailViewmodels.Add(BookmarkType.death, new DeathBookmarkVM(model));
            //detailViewmodels.Add(BookmarkType.misc, new MiscBookmarkVM(model));
            ////_selectedViewModel = detailViewmodels[model.bookmarkType]!;
            //_selectedViewModel = detailViewmodels[BookmarkType.marriage];

            bookmarkType = BookmarkType.marriage;
        }

        public IBookmarkBase model { get; }

        Dictionary<BookmarkType, BaseViewModel?> detailViewmodels = new();
    }

    class BirthBookmarkVM : BaseViewModel
    {
        public string Child
        {
            get => model.Person1;
            set
            {
                if (model.Person1 != value)
                {
                    model.Person1 = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool Legitimate
        {
            get => model.Child?.state == birthState.legitmate;
            set
            {
                if (value != (model.Child?.state == birthState.legitmate))
                {
                    model.Child?.state = value;
                    OnPropertyChanged();
                }
            }
        }
        public Person? Father
        {
            get => model.Father;
            set
            {
                if (model.Father != value)
                {
                    model.Father = value;
                    OnPropertyChanged();
                }
            }
        }
        public string? Mother
        {
            get => model.Mother?.Name;
            set
            {
                if (model.Mother?.Name != value)
                {
                    model.Mother.Name = value;
                    OnPropertyChanged();
                }
            }
        }
        public string BaptizeDate
        {
            get => model.EventDate;
            set
            {
                if (model.EventDate != value)
                {
                    model.EventDate = value;
                    OnPropertyChanged();
                }
            }
        }
        public string? Witnesses
        {
            get => model.GodParent?.Name;
            set
            {
                if (model.GodParent?.Name != value)
                {
                    model.GodParent!.Name = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Transcript
        {
            get => model.Transkript;
            set
            {
                if (model.Transkript != value)
                {
                    model.Transkript = value;
                    OnPropertyChanged();
                }
            }
        }

        public BirthBookmarkVM(BirthBookmark model)
        {
            this.model = model;
        }

        private BirthBookmark model;

    }
    class MarriageBookmarkVM : BaseViewModel
    {
        public string MariageDate
        {
            get => model.EventDate;
            set
            {
                if (model.Date1 != value)
                {
                    model.EventDate = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Groom
        {
            get => model.Person1;
            set
            {
                if (model.Person1 != value)
                {
                    model.Person1 = value;
                    OnPropertyChanged();
                }
            }
        }
        public string GroomBirthday
        {
            get => model.Date1;
            set
            {
                if (model.Date1 != value)
                {
                    model.Date1 = value;
                    OnPropertyChanged();
                }
            }
        }
        public string GroomStatus
        {
            get => model.Status1;
            set
            {
                if (model.Status1 != value)
                {
                    model.Status1 = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Bride
        {
            get => model.Person2;
            set
            {
                if (model.Person2 != value)
                {
                    model.Person2 = value;
                    OnPropertyChanged();
                }
            }
        }
        public string BrideBirthday
        {
            get => model.Date2;
            set
            {
                if (model.Date2 != value)
                {
                    model.Date2 = value;
                    OnPropertyChanged();
                }
            }
        }
        public string BrideStatus
        {
            get => model.Status2;
            set
            {
                if (model.Status2 != value)
                {
                    model.Status2 = value;
                    OnPropertyChanged();
                }
            }
        }
        public string GroomFather
        {
            get => model.Person3;
            set
            {
                if (model.Person3 != value)
                {
                    model.Person3 = value;
                    OnPropertyChanged();
                }
            }
        }
        public string GroomMother
        {
            get => model.Person4;
            set
            {
                if (model.Person4 != value)
                {
                    model.Person4 = value;
                    OnPropertyChanged();
                }
            }
        }
        public string GroomParentsStatus
        {
            get => model.Status3;
            set
            {
                if (model.Status3 != value)
                {
                    model.Status3 = value;
                    OnPropertyChanged();
                }
            }
        }
        public string BrideFather
        {
            get => model.Person5;
            set
            {
                if (model.Person5 != value)
                {
                    model.Person5 = value;
                    OnPropertyChanged();
                }
            }
        }
        public string BrideMother
        {
            get => model.Person5;
            set
            {
                if (model.Person5 != value)
                {
                    model.Person6 = value;
                    OnPropertyChanged();
                }
            }
        }
        public string BrideParentsStatus
        {
            get => model.Status4;
            set
            {
                if (model.Status4 != value)
                {
                    model.Status4 = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Witnesses
        {
            get => model.Others;
            set
            {
                if (model.Others != value)
                {
                    model.Others = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Transcript
        {
            get => model.Transkript;
            set
            {
                if (model.Transkript != value)
                {
                    model.Transkript = value;
                    OnPropertyChanged();
                }
            }
        }

        public MarriageBookmarkVM(IBookmark model)
        {
            this.model = model;

        }

        private IBookmark model;
    }



    class DeathBookmarkVM : BaseViewModel { public DeathBookmarkVM(IBookmark model) { } }
    class MiscBookmarkVM : BaseViewModel { public MiscBookmarkVM(IBookmark model) { } }
}


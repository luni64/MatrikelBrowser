using AEM;
using Interfaces;
using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Media.Media3D;


namespace ArchiveBrowser.ViewModels
{

    public class BookmarkVM : BaseViewModel
    {
        public RelayCommand cmdSaveDetails { get; }
        private void doSaveDetails(object? param)
        {

        }

        private BookmarkType _bookmarkType;
        public BookmarkType bookmarkType
        {
            get => _bookmarkType;
            set
            {
                _bookmarkType = value;
                SelectedViewModel = detailViewmodels[bookmarkType]!;
                OnPropertyChanged();

            }
        }

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
            cmdSaveDetails = new RelayCommand(doSaveDetails);

            this.model = model;
            detailViewmodels.Add(BookmarkType.birth, new BirthBookmarkVM(this));
            detailViewmodels.Add(BookmarkType.marriage, new MarriageBookmarkVM(this));
            detailViewmodels.Add(BookmarkType.death, new DeathBookmarkVM(model));
            detailViewmodels.Add(BookmarkType.misc, new MiscBookmarkVM(model));
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
            get => model.Child.Name;
            set
            {
                if (model.Child.Name != value)
                {
                    model.Child.Name = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool Legitimate
        {
            get => model.Child.state == birthState.legitmate;
            set
            {
                if (value != (model.Child.state == birthState.legitmate))
                {
                    if (model.Child != null) model.Child.state = value ? birthState.legitmate : birthState.illegitmate;
                    OnPropertyChanged();
                }
            }
        }
        public string Father
        {
            get => model.Father.Name;
            set
            {
                if (model.Father.Name != value)
                {
                    model.Father.Name = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Mother
        {
            get => model.Mother.Name;
            set
            {
                if (model.Mother.Name != value)
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
        public string Witnesses
        {
            get => model.GodParent.Name;
            set
            {
                if (model.GodParent.Name != value)
                {
                    model.GodParent.Name = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Transcript
        {
            get => bm.Transcript;
            set
            {
                if (bm.Transcript != value)
                {
                    bm.Transcript = value;
                    OnPropertyChanged();
                }
            }
        }

        public BirthBookmarkVM(BookmarkVM bookmarkVM)
        {
            this.bm = bookmarkVM;
            this.model = new BirthBookmark(bookmarkVM.model);
        }

        private BookmarkVM bm;
        private BirthBookmark model;

    }
    class MarriageBookmarkVM : BaseViewModel
    {
        public string MariageDate
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
        public string Groom
        {
            get => model.Groom.Name;
            set
            {
                if (model.Groom.Name != value)
                {
                    model.Groom.Name = value;
                    OnPropertyChanged();
                }
            }
        }
        public string GroomBirthday
        {
            get => model.Groom.BirthDate;
            set
            {
                if (model.Groom.BirthDate != value)
                {
                    model.Groom.BirthDate = value;
                    OnPropertyChanged();
                }
            }
        }
        public string GroomStatus
        {
            get => model.Groom.Occupation;
            set
            {
                if (model.Groom.Occupation != value)
                {
                    model.Groom.Occupation = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Bride
        {
            get => model.Bride.Name;
            set
            {
                if (model.Bride.Name != value)
                {
                    model.Bride.Name = value;
                    OnPropertyChanged();
                }
            }
        }
        public string BrideBirthday
        {
            get => model.Bride.BirthDate;
            set
            {
                if (model.Bride.BirthDate != value)
                {
                    model.Bride.BirthDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _brideStatus = string.Empty;
        public string BrideStatus
        {
            get => model.Bride.Occupation;
            set
            {
                if (model.Bride.Occupation != value)
                {
                    model.Bride.Occupation = value;
                    OnPropertyChanged();
                }
            }
        }
        public string GroomFather
        {
            get => model.GroomFather.Name;
            set
            {
                if (model.GroomFather.Name != value)
                {
                    model.GroomFather.Name = value;
                    OnPropertyChanged();
                }
            }
        }
        public string GroomMother
        {
            get => model.GroomMother.Name;
            set
            {
                if (model.GroomMother.Name != value)
                {
                    model.GroomMother.Name = value;
                    OnPropertyChanged();
                }
            }
        }
        public string GroomParentsStatus
        {
            get => model.GroomFather.Occupation;
            set
            {
                if (model.GroomFather.Occupation != value)
                {
                    model.GroomFather.Occupation = value;
                    OnPropertyChanged();
                }
            }
        }
        public string BrideFather
        {
            get => model.BrideFather.Name;
            set
            {
                if (model.BrideFather.Name != value)
                {
                    model.BrideFather.Name = value;
                    OnPropertyChanged();
                }
            }
        }
        public string BrideMother
        {
            get => model.BrideMother.Name;
            set
            {
                if (model.BrideMother.Name != value)
                {
                    model.BrideMother.Name = value;
                    OnPropertyChanged();
                }
            }
        }
        public string BrideParentsStatus
        {
            get => model.BrideFather.Occupation;
            set
            {
                if (model.BrideFather.Occupation != value)
                {
                    model.BrideFather.Occupation = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Witnesses
        {
            get => model.Witnesses.Name;
            set
            {
                if (model.Witnesses.Name != value)
                {
                    model.Witnesses.Name = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Transcript
        {
            get => bm.Transcript;
            set
            {
                if (bm.Transcript != value)
                {
                    bm.Transcript = value;
                    OnPropertyChanged();
                }
            }
        }

        public MarriageBookmarkVM(BookmarkVM? bm)
        {
            this.bm = bm;
            this.model = new MarriageBookmark(bm.model);
        }

        private BookmarkVM bm;
        private MarriageBookmark model;
    }
    class DeathBookmarkVM : BaseViewModel
    {
        public string DeathDate
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
        public string Deceased
        {
            get => model.Deceased.Name;
            set
            {
                if (model.Deceased.Name != value)
                {
                    model.Deceased.Name = value;
                    OnPropertyChanged();
                }
            }
        }

        public DeathBookmarkVM(IBookmarkBase? model)
        {
            this.model = new DeathBookmark(model);
        }

        private DeathBookmark model;
    }
    class MiscBookmarkVM : BaseViewModel
    {
        public MiscBookmarkVM(IBookmarkBase? model)
        {
        }
    }
}


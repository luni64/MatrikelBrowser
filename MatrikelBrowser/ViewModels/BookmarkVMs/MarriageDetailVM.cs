using AEM;
using Interfaces;


namespace ArchiveBrowser.ViewModels
{
    class MarriageDetailVM : BaseViewModel, IDetailsVM
    {
        public string MariageDate
        {
            get => parent.model.EventDate;
            set
            {
                if (parent.model.EventDate != value)
                {
                    parent.model.EventDate = value;
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
            get => parent.model.Transcript;
            set
            {
                if (parent.model.Transcript != value)
                {
                    parent.model.Transcript = value;
                    OnPropertyChanged();
                }
            }
        }

        public MarriageDetailVM(BookmarkVM bm)
        {
            this.parent = bm;
            this.model = new MarriageDetails(bm?.model);
        }

        public BookmarkVM parent { get; }
        internal MarriageDetails model;

        public IBookmarkDetails detailsModel => model;
    }
}


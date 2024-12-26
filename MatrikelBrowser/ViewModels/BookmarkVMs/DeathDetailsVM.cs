using MbCore;
using Interfaces;


namespace MatrikelBrowser.ViewModels
{
    class DeathDetailsVM : BaseViewModel, IDetailsVM
    {
        public string DeathDate
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
        public string FuneralDate
        {
            get => model.FuneralDate;
            set
            {
                if (model.FuneralDate != value)
                {
                    model.FuneralDate = value;
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
        public string Cause
        {
            get => model.Cause;
            set
            {
                if (model.Cause != value)
                {
                    model.Cause = value;
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

        public DeathDetailsVM(BookmarkVM parent)
        {
            this.parent = parent;
            this.model = new DeathDetails(this.parent.model);
        }

        public BookmarkVM parent { get; }
        private DeathDetails model;

        public IBookmarkDetails detailsModel => model;
    }
}


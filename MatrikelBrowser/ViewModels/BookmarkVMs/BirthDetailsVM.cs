using AEM;
using Interfaces;


namespace ArchiveBrowser.ViewModels
{
    class BirthDetailsVM : BaseViewModel, IDetailsVM
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
        public string BaptismDate
        {
            get => model.Child.BaptismDate;
            set
            {
                if (model.Child.BaptismDate != value)
                {
                    model.Child.BaptismDate = value;
                    OnPropertyChanged();
                }
            }
        }
        public string BirthDate
        {
            get => model.Child.BirthDate;
            set
            {
                if (model.Child.BirthDate != value)
                {
                    model.Child.BirthDate = value;
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

        public BirthDetailsVM(BookmarkVM parent)
        {
            this.parent = parent;
            this.model = new BirthDetails(this.parent.model);
        }

        public BookmarkVM parent { get; }
        private BirthDetails model;
        public IBookmarkDetails detailsModel => model;

    }
}


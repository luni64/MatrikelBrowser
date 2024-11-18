using AEM;
using Interfaces;


namespace ArchiveBrowser.ViewModels
{
    class MiscBookmarkVM : BaseViewModel, IDetailsVM
    {
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

        public MiscBookmarkVM(BookmarkVM? bm)
        {
            this.model = new MiscDetails(bm.model);
            this.parent = bm;
        }

        public BookmarkVM parent { get; }
        public MiscDetails model;

        public IBookmarkDetails detailsModel => model;

    }
}


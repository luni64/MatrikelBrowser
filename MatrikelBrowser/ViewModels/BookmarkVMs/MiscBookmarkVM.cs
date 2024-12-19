using MbCore;
using Interfaces;


namespace MatrikelBrowser.ViewModels
{
    class MiscBookmarkVM(BookmarkVM bm) : BaseViewModel, IDetailsVM
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

        public BookmarkVM parent { get; } = bm;
        public MiscDetails model = new MiscDetails(bm.model);

        public IBookmarkDetails detailsModel => model;

    }
}


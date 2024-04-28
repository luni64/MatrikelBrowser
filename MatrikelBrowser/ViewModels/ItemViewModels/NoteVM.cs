using Interfaces;

namespace ArchiveBrowser.ViewModels
{
    public class NoteVM : BaseViewModel
    {
        public string? document
        {
            get => model.note;
            set
            {
                if (model.note != value)
                {
                    model.note = value;
                    OnPropertyChanged();
                }
            }
        }

        public NoteVM(IBookInfo model)
        {
            this.model = model;        
        }

        private IBookInfo model;

    }
}

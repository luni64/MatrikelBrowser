using AEM;
using Interfaces;

namespace ArchiveBrowser.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        #region Commands                
        public RelayCommand cmdSave => _cmdSave ??= new RelayCommand(doSave);
        private void doSave(object? o)
        {
            model.saveNotes();
        }
        #endregion

        #region Properties
        public TectonicsVM tectonicsVM { get; }
        #endregion

        public MainViewModel()
        {
            model = new aemCore();
            tectonicsVM = new(model);
        }

        #region private methods and fields       
        private RelayCommand? _cmdSave;
        private readonly aemCore model;
        #endregion
    }
}

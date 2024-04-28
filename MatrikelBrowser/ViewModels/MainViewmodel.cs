using AEM;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading;

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
            tectonicsVM = new(model);
        }

        #region private methods and fields       
        private RelayCommand? _cmdSave;
        private readonly aemCore model = new();
        #endregion
    }
}

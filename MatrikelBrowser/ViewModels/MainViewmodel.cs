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

        public RelayCommand cmdTest => _cmdTest ??= new RelayCommand(doTest);
        public RelayCommand cmdSave => _cmdSave ??= new RelayCommand(doSave);
        #endregion

        #region Properties
        public List<LetterVM> LetterVMs { get; set; } = new List<LetterVM>();
        public BookVM? selectedBook
        {
            get => _selectedBook;
            set => SetProperty(ref _selectedBook, value);
        }
        #endregion

        public MainViewModel()
        {
            // setup all item viewmodels
            var ParishGroups = model.Parishes.Where(p=>p.Books.Any()).GroupBy(p => p.Place[0]);
            foreach (var ParishGroup in ParishGroups)
            {
                LetterVM letterVM = new(ParishGroup.Key.ToString());
                foreach (var Parish in ParishGroup.OrderBy(p => p.Place))
                {
                    ParishVM parishVM = new(Parish);
                    var BookGroups = Parish.Books.GroupBy(b => b.Type);
                    foreach (var bookGroup in BookGroups)
                    {
                        BookTypeVM typeVM = new(bookGroup.Key);
                        foreach (var book in bookGroup)
                        {
                            typeVM.BookVMs.Add(new BookVM(book,parishVM));
                        }
                        parishVM.BookTypeVMs.Add(typeVM);
                    }
                    letterVM.ParishVMs.Add(parishVM);
                }
                this.LetterVMs.Add(letterVM);
            };
        }


        #region private methods and fields
        private void doTest(object? o)
        {
            //selectedSection?.Sections?.Add(new Section("test", null));
        }
        private void doSave(object? o)
        {
            model.saveNotes();
        }

        private RelayCommand? _cmdSave;
        private RelayCommand? _cmdTest;
        private  readonly aemCore model = new();        
        private BookVM? _selectedBook;
        #endregion
    }
}

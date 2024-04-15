using AEM;
using ArchiveBrowser.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveBrowser.ViewModels
{

    public class ReportVM : BaseViewModel
    {
        //RelayCommand? _cmdPrepareReport;
        //public RelayCommand cmdPrepareReport => _cmdPrepareReport ??= new RelayCommand(doPrepareReport);

        //void doPrepareReport(object? o)
        //{
        ////    Parish = book.Parish!.Place;
        ////    Church = book.Parish!.Church;
        ////    BookTitle = book.Title;
        ////    BookNote = book.b .NoteVM.document ?? string.Empty;
        ////    //Bookmarks = new(bvm.bookmarkVMs);
        //}

        public string Parish => $"{book.Parish!.Place}";
        public string Church => $"{book.Parish!.ID} - {book.Parish!.Church}";
        
        public string BookTitle => $"{book.ID} {book.Title}";
        public string BookNote => book.Info.note;
        public List <AEM.Bookmark> Bookmarks => book.Info.Bookmarks;
        public string BookDescriptionURL => book.BookDescriptionURL;


        public ReportVM(Book bvm)
        {
          this.book = bvm;
        }

        Book book;
    
    }
}




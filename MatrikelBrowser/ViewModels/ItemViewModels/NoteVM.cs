using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AEM;
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

        //public string? BookID
        //{
        //    get => model.AffectedBookID;
        //    set
        //    {
        //        if(model.AffectedBookID != value)
        //        {
        //            model.AffectedBookID = value;
        //            OnPropertyChanged();
        //        }
        //    }
        //}


        public NoteVM(BookInfo model)
        {
            this.model = model;
        
        }

        private BookInfo model;

    }
}

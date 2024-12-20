using Interfaces;
using AEM;

namespace ArchiveBrowser.ViewModels
{
    public class PageVM : ItemVM
    {     
       // public int SheetNr => parent.PageVMs.IndexOf(this);
        public int SheetNr => model.Book.Pages.IndexOf(model);
        public string URL { get;  } 

        public string ImageFilename  => model.loadImage();

       
        private Page model;

        public PageVM(Page model, BookVM parent) : base(parent)
        {
            this.model = model;
            //this.URL = model.URL;
            
        }        
    }
}

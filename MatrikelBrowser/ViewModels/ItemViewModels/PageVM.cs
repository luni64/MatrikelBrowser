using Interfaces;
using AEM;

namespace ArchiveBrowser.ViewModels
{
    public class PageVM : ItemVM
    {     
        public int SheetNr => Parent.PageVMs.IndexOf(this);
        public string URL { get;  } 

        public string ImageFilename  => model.loadImage();

        public BookVM Parent { get; }

        private Page model;

        public PageVM(Page model, BookVM parent)
        {
            this.model = model;
            //this.URL = model.URL;
            this.Parent = parent;                     
        }        
    }
}

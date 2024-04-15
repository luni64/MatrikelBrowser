using System;
using System.Drawing;
using System.Windows.Media.Media3D;

namespace ArchiveBrowser.ViewModels
{
    public class BookmarkVM : BaseViewModel
    {        
        public string Title
        {
            get => model.Title;
            set
            {
                if(value != model.Title) 
                {
                    model.Title = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Sheet
        {
            get => model.Sheet;
            set => model.Sheet = value;
        }
        public int X
        {
            get => model.X;
            set => model.X = value;
        }
        public int Y
        {
            get => model.Y;
            set => model.Y = value;
        }
        public double W
        {
            get => model.W;
            set => model.W = value;
        }

        public double H
        {
            get => model.H;
            set => model.H = value;
        }

        public string ID { get; set; } = Guid.Empty.ToString();
        public PageVM? Page { get; set; }

        public BookmarkVM(AEM.Bookmark model)
        {
            this.model = model;
        }

        public AEM.Bookmark model { get; }
    }
}


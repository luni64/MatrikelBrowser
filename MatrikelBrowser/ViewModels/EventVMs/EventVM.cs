
using AEM;
using Interfaces;
using iText.Kernel.Pdf.Canvas.Parser;

namespace MatrikelBrowser.ViewModels
{
    public class EventVM(Event evnt) : BaseViewModel
    {
        public int ID { get => model.Id; }
        public int SheetNr { get => model.SheetNr; set => SetProperty(model, e => e.SheetNr, value); }
        public string Title { get => model.Title; set => SetProperty(model, e => e.Title, value); }
        public string Transcript { get => model.Transcript; set => SetProperty(model, e => e.Transcript, value); }
        public string Remarks { get => model.Remarks; set => SetProperty(model, e => e.Remarks, value); }
        public double X { get => model.X; set => SetProperty(model, e => e.X, value); }
        public double Y { get => model.Y; set => SetProperty(model, e => e.Y, value); }
        public double W { get => model.W; set => SetProperty(model, e => e.W, value); }
        public double H { get => model.H; set => SetProperty(model, e => e.H, value); }
        public bool isLocked { get => _isLocked; set => SetProperty(ref _isLocked, value); }
        public int EventIdx
        {
            get => (int)model.EventType; 
            set => SetProperty(model, e => e.EventType,(BookmarkType) value);
//            {
//                model.EventType = (BookmarkType)value;
////            
//            }
        }

        private bool _isLocked = true;
        public readonly Event model = evnt;
    }
}

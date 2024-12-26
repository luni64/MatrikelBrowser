using Interfaces;

namespace MbCore
{
    public class BookmarkBase : IBookmarkBase
    {
        public string Title { get; set; }
        public string EventDate { get; set; }
        public string Transcript { get; set; }
        public int SheetNr { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public double W { get; set; }
        public double H { get; set; }        
        public IBookmarkDetails Details { get; set; }

        public BookmarkBase(BookmarkBase? b = null)
        {
            if (b != null)
            {
                this.Title = b.Title;
                this.EventDate = b.EventDate;
                this.Transcript = b.Transcript;
                this.SheetNr = b.SheetNr;
                this.X = b.X;
                this.Y = b.Y;
                this.W = b.W;
                this.H = b.H;
                this.Details = b.Details;
            }
            else
            {
                this.Title = string.Empty;
                this.EventDate = string.Empty;
                this.Transcript = string.Empty;
                this.SheetNr = 1;
                this.X = 0;
                this.Y = 0;
                this.W = 400;
                this.H = 200;
                this.Details = new MiscDetails();
            }
        }

        public override string ToString() => Title;
    }

  
}
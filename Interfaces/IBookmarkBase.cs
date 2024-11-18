namespace Interfaces
{
    public interface IBookmarkDetails {    }

    public interface IBookmarkBase
    {
        string Title { get; set; }
        string EventDate { get; set; }
        string Transcript { get; set; }
        int X { get; set; }
        int Y { get; set; }
        double W { get; set; }
        double H { get; set; }
        int SheetNr { get; set; }
        IBookmarkDetails Details { get; set; }        

        string ToString();
    }
}
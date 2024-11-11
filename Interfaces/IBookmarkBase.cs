namespace Interfaces
{
    public interface IBookmarkBase
    {
        string EventDate { get; set; }
        double H { get; set; }
        int SheetNr { get; set; }
        string Title { get; set; }
        string Transkript { get; set; }
        double W { get; set; }
        int X { get; set; }
        int Y { get; set; }

        string ToString();
    }
}
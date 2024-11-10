using Interfaces;
using System.Drawing;

namespace Interfaces
{
    public interface IBookmark2
    {

    }

    public interface IBookmark
    {
        string Title { get; set; }
        string Person1 { get; set; }
        string Person2 { get; set; }
        string Person3 { get; set; }
        string Person4 { get; set; }
        string Person5 { get; set; }
        string Person6 { get; set; }
        string Father { get; set; }
        string Mother { get; set; }
        string EventDate { get; set; } 
        string Date1 { get; set; }
        string Date2 { get; set; }
        string Others { get; set; }
        string Status1 { get; set; }
        string Status2 { get; set; }
        string Status3 { get; set; }
        string Status4 { get; set; }
        string Transkript { get; set; }
        bool Flag1 { get; set; }
        bool Flag2 { get; set; }


        BookmarkType bookmarkType { get; set; }
        Rectangle cutOut { get; set; }
        int SheetNr { get; set; }

        double W { get; set; }
        double H { get; set; }
        int X { get; set; }
        int Y { get; set; }
    }
}
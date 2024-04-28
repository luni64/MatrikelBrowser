using Interfaces;
using System;
using System.Drawing;

namespace AEM
{ 
    public class Bookmark : IBookmark
    {
        public string Title { get; set; } = String.Empty;
        public string Person1 { get; set; } = String.Empty;
        public string Person2 { get; set; } = String.Empty;
        public string Person3 { get; set; } = String.Empty;
        public string Person4 { get; set; } = String.Empty;
        public string Person5 { get; set; } = String.Empty;
        public string Person6 { get; set; } = String.Empty;
        public string Father { get; set; } = String.Empty;
        public string Mother { get; set; } = String.Empty;
        public string Others { get; set; } = String.Empty;
        public string EventDate { get; set; } = String.Empty;
        public string Date1 { get; set; } = String.Empty;
        public string Date2 { get; set; } = String.Empty;
        public string Status1 { get; set; } = String.Empty;
        public string Status2 { get; set; } = String.Empty;
        public string Status3 { get; set; } = String.Empty;
        public string Status4 { get; set; } = String.Empty;
        public bool Flag1 { get; set; } = true;
        public bool Flag2 { get; set; }

        public string Transkript { get; set; } = String.Empty;
        public BookmarkType bookmarkType { get; set; }
        public int SheetNr { get; set; }

        public Rectangle cutOut { get; set; }

        public int X { get; set; }
        public int Y { get; set; }
        public double W { get; set; } = 400;
        public double H { get; set; } = 200;
      
    }
}
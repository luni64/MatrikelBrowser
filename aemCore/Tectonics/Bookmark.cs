using System;
using System.Drawing;

namespace AEM
{
    public class Bookmark
    {
        public string Title { get; set; } = String.Empty;
        public int Sheet { get; set; }

        public Rectangle cutOut { get; set; }

        public int X { get; set; }
        public int Y { get; set; }
        public double W { get; set; } = 400;
        public double H { get; set; } = 200;

    }
}

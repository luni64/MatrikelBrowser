using System;
using System.Collections.Generic;

namespace AEM
{
    public class Parish
    {
        public string ID { get; set; } = "";
        public string Place { get; set; } = "";
        public string Church { get; set; } = "";
        public int startYear { get; set; }
        public int endYear { get; set; }

        public List<Book> Books { get; set; } = new List<Book>();

        public override string ToString() => $"{ID}-{Place}-{Church}";        
    }
}
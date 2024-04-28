using Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AEM
{
    public class Parish : IParish
    {
        public string ID { get; set; } = "";
        public string Place { get; set; } = "";
        public string Church { get; set; } = "";
        public int startYear { get; set; }
        public int endYear { get; set; }

        public Parish(string iD, string place, string church, int startYear, int endYear, List<Book> books )
        {
            ID = iD;
            Place = place;
            Church = church;
            this.startYear = startYear;
            this.endYear = endYear;
            Books = books.ToList<IBook>();
        }

       // [JsonConverter(typeof(ConcreteConverter<List<Book>>))]
        public List<IBook> Books { get; set; } = new();

        public override string ToString() => $"{ID}-{Place}-{Church}";
    }
}
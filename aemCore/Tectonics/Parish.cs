using Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AEM
{
    public class Parish //: IParish
    {
        public string ID => dto.RefId;// { get; set; } = "";
        public string Place => dto.Place; // { get; set; } = "";
        public string Church => dto.Church;//{ get; set; } = "";
        public int startYear { get; set; }
        public int endYear { get; set; }

        //public Parish(string iD, string place, string church, int startYear, int endYear, List<Book> books )
        public Parish(ParishDTO dto)
        {
            this.dto = dto;
        }

        ParishDTO dto;

        // [JsonConverter(typeof(ConcreteConverter<List<Book>>))]
        public List<Book> Books => dto.Books;// { get; set; } = new();
        

        public override string ToString() => $"{ID}-{Place}-{Church}";
    }
}
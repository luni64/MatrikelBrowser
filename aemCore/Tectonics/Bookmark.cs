using Interfaces;
using iText.Layout.Element;
using JsonSubTypes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.Json.Serialization;

namespace AEM
{
    ////[JsonConverter(typeof(BookmarkBase), "base")]
    //[JsonDerivedType(typeof(BookmarkBase), typeDiscriminator: "base")]
    //[JsonDerivedType(typeof(BirthBookmark), typeDiscriminator: "birth")]
    //[JsonDerivedType(typeof(MarriageBookmark), typeDiscriminator: "marriage")]
    public class BookmarkBase
    {
        public string Title { get; set; } = String.Empty;
        public string EventDate { get; set; } = string.Empty;

        public string Transkript { get; set; } = String.Empty;
        public int SheetNr { get; set; } = 0;
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;
        public double W { get; set; } = 400;
        public double H { get; set; } = 200;

        public override string ToString()
        {
            return Title;
        }
    }


    public class Person
    {
        public string Name { get; set; } = String.Empty;
        public string Occupation { get; set; } = String.Empty;
        public string BirthDate { get; set; } = String.Empty;
        public string DeathDate { get; set; } = String.Empty;
        public bool living { get; set; } = true;

        public override string ToString()
        {
            return Name + (!string.IsNullOrEmpty(Occupation) ? $"({Occupation})" : "");
        }
    }

   
    public class BirthBookmark : BookmarkBase
    {
        public Person? Child { get; set; }
        public Person? Father { get; set; }
        public Person? Mother { get; set; }
        public Person? GodParent { get; set; }
    }

    public class MarriageBookmark : BookmarkBase
    {
        public Person? Groom { get; set; }
        public Person? GroomFather { get; set; }
        public Person? GroomMother { get; set; }
        public Person? Bride { get; set; }
        public Person? BrideFather { get; set; }
        public Person? BrideMother { get; set; }
        public List<Person> Witnesses { get; set; } = new();

        
    }



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
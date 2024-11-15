using Interfaces;
using iText.Commons.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using static iText.Layout.Borders.Border;
using System.Text.RegularExpressions;
using System.Threading;

namespace AEM
{
    public class Person
    {
        public string Name { get; set; } = String.Empty;
        public string Occupation { get; set; } = String.Empty;
        public string BirthDate { get; set; } = String.Empty;
        public string DeathDate { get; set; } = String.Empty;
        public bool living { get; set; } = true;
        public birthState state { get; set; } = birthState.unknown;

        public Person(Person? p = null)
        {
            if (p != null)
            {
                Name = p.Name;
                Occupation = p.Occupation;
                BirthDate = p.BirthDate;
                DeathDate = p.DeathDate;
                living = p.living;
                state = p.state;
            }
            else
            {
                Name = string.Empty;
                Occupation = string.Empty;
                BirthDate = string.Empty;
                DeathDate = string.Empty;
                living = false;
                state = birthState.unknown;
            }
        }

        public override string ToString() => Name;
    }

    public class BookmarkBase : IBookmarkBase
    {
        public string Title { get; set; }
        public string EventDate { get; set; }
        public string Transkript { get; set; }
        public int SheetNr { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public double W { get; set; }
        public double H { get; set; }

        public BookmarkBase(IBookmarkBase? b = null)
        {
            if (b != null)
            {
                this.Title = b.Title;
                this.EventDate = b.EventDate;
                this.Transkript = b.Transkript;
                this.SheetNr = b.SheetNr;
                this.X = b.X;
                this.Y = b.Y;
                this.W = b.W;
                this.H = b.H;
            }
            else
            {
                this.Title = string.Empty;
                this.EventDate = string.Empty;
                this.Transkript = string.Empty;
                this.SheetNr = 0;
                this.X = 0;
                this.Y = 0;
                this.W = 400;
                this.H = 200;
            }
        }

        public override string ToString() => Title;
    }

    class BookmarkDetail
    {

    }

    public class BirthBookmark : BookmarkBase
    {
        public Person Child { get; set; }
        public Person Father { get; set; }
        public Person Mother { get; set; }
        public Person GodParent { get; set; }

        public BirthBookmark(IBookmarkBase? bm = null) : base(bm)
        {
            var bbm = bm as BirthBookmark;
            Child = new(bbm?.Child);
            Father = new(bbm?.Father);
            Mother = new(bbm?.Mother);
            GodParent = new(bbm?.GodParent);
        }

        public override string ToString() => Title + " F:" + Father.Name;
    }

    public class MarriageBookmark : BookmarkBase
    {
        public Person Groom { get; set; } 
        public Person GroomFather { get; set; } 
        public Person GroomMother { get; set; } 
        public Person Bride { get; set; } 
        public Person BrideFather { get; set; } 
        public Person BrideMother { get; set; }
        public Person Witnesses { get; set; } 

        public MarriageBookmark(IBookmarkBase? bm = null) : base(bm)
        {
            var mbm = bm as MarriageBookmark;
            Groom = new(mbm?.Groom);
            GroomFather = new(mbm?.GroomFather);
            GroomMother = new(mbm?.GroomMother);
            Bride = new(mbm?.Bride);
            BrideFather = new(mbm?.BrideFather);
            BrideMother = new(mbm?.BrideMother);
            Witnesses = new(mbm?.Witnesses);
        }

        public override string ToString() => Groom.Name + " + " + Bride.Name;
    }

    public class DeathBookmark : BookmarkBase
    {
        public Person Deceased { get; set; }
        public Person Father { get; set; }
        public Person Mother { get; set; }       
        public string Cause { get; set; }
        public string Age { get; set; }

        public DeathBookmark(IBookmarkBase? bm = null) : base(bm)
        {
            var dbm = bm as DeathBookmark;
            Deceased = new(dbm?.Deceased);
            Father = new(dbm?.Father);
            Mother = new(dbm?.Mother);
            Cause = new(dbm?.Cause ?? String.Empty);
            Age = new(dbm?.Age ?? String.Empty);
        }

        public override string ToString() => Deceased.Name;
    }

    public class MiscBookmark : BookmarkBase
    {
        public override string ToString() => Transkript;
    };

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
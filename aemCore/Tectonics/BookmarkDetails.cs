using Interfaces;
using System;

namespace MbCore
{
    public class Person
    {
        public string Name { get; set; }
        public string Occupation { get; set; }
        public string BirthDate { get; set; }
       // public string BaptismDate { get; set; }
        public string DeathDate { get; set; }
        public bool living { get; set; }
        public birthState state { get; set; }

        public Person(Person? p = null)
        {
            if (p != null)
            {
                Name = p.Name;
                Occupation = p.Occupation;
                BirthDate = p.BirthDate;
               // BaptismDate = p.BaptismDate;
                DeathDate = p.DeathDate;
                living = p.living;
                state = p.state;
            }
            else
            {
                Name = string.Empty;
                Occupation = string.Empty;
                BirthDate = string.Empty;
                //BaptismDate = string.Empty;
                DeathDate = string.Empty;
                living = false;
                state = birthState.unknown;
            }
        }

        public override string ToString() => Name;
    }

    public class BirthDetails : IBookmarkDetails
    {
        public Person Child { get; set; }
        public Person Father { get; set; }
        public Person Mother { get; set; }
        public Person GodParent { get; set; }

        public BirthDetails(IBookmarkBase? bm = null)
        {
            var bbm = bm?.Details as BirthDetails;
            Child = new(bbm?.Child);
            Father = new(bbm?.Father);
            Mother = new(bbm?.Mother);
            GodParent = new(bbm?.GodParent);
        }

        public override string ToString() => "Birth: " + Child;
    }

    public class MarriageDetails : IBookmarkDetails
    {
        public Person Groom { get; set; }
        public Person GroomFather { get; set; }
        public Person GroomMother { get; set; }
        public Person Bride { get; set; }
        public Person BrideFather { get; set; }
        public Person BrideMother { get; set; }
        public Person Witnesses { get; set; }

        public MarriageDetails(IBookmarkBase? bm = null)
        {
            var mbm = bm?.Details as MarriageDetails;
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

    public class DeathDetails : IBookmarkDetails
    {
        public Person Deceased { get; set; }
        public Person Father { get; set; }
        public Person Mother { get; set; }
        public string FuneralDate { get; set; }
        public string Cause { get; set; }
        public string Age { get; set; }

        public DeathDetails(IBookmarkBase? bm = null)
        {
            var dbm = bm?.Details as DeathDetails;
            Deceased = new(dbm?.Deceased);
            Father = new(dbm?.Father);
            Mother = new(dbm?.Mother);
            Cause = new(dbm?.Cause ?? String.Empty);
            Age = new(dbm?.Age ?? String.Empty);
            FuneralDate = new(dbm?.FuneralDate ?? String.Empty);
        }

        public override string ToString() => Deceased.Name;
    }

    public class MiscDetails : IBookmarkDetails
    {
        public MiscDetails(IBookmarkBase? bm = null) { }

    };


}
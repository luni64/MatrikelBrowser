using Interfaces;
using System;

namespace MbCore
{
    public class PersonOld
    {
        public string Name { get; set; }
        public string Occupation { get; set; }
        public string BirthDate { get; set; }
       // public string BaptismDate { get; set; }
        public string DeathDate { get; set; }
        public bool living { get; set; }
        public BirthState state { get; set; }

        public PersonOld(PersonOld? p = null)
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
                state = BirthState.unknown;
            }
        }

        public override string ToString() => Name;
    }

    public class BirthDetails : IBookmarkDetails
    {
        public PersonOld Child { get; set; }
        public PersonOld Father { get; set; }
        public PersonOld Mother { get; set; }
        public PersonOld GodParent { get; set; }

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
        public PersonOld Groom { get; set; }
        public PersonOld GroomFather { get; set; }
        public PersonOld GroomMother { get; set; }
        public PersonOld Bride { get; set; }
        public PersonOld BrideFather { get; set; }
        public PersonOld BrideMother { get; set; }
        public PersonOld Witnesses { get; set; }

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
        public PersonOld Deceased { get; set; }
        public PersonOld Father { get; set; }
        public PersonOld Mother { get; set; }
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
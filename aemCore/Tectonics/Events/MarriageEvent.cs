using MbCore;

namespace AEM.Tectonics
{
    public class MarriageEvent(Event evnt) : Event
    {
        public string Groom { get => evnt.Person1; set => evnt.Person1 = value; }
        public string GroomFather { get => evnt.Person2; set => evnt.Person2 = value; }
        public string GroomMother { get => evnt.Person3; set => evnt.Person3 = value; }
        public string Bride { get => evnt.Person4; set => evnt.Person4 = value; }
        public string BrideFather { get => evnt.Person5; set => evnt.Person5 = value; }
        public string BrideMother { get => evnt.Person6; set => evnt.Person6 = value; }
        public string Witnesses { get => evnt.Person7; set => evnt.Person7 = value; }

        public string MarriageDate { get => evnt.Date1; set => evnt.Date1 = value; }
        public string GroomBirthday { get => evnt.Date3; set => evnt.Date3 = value; }
        public string BrideBirthday { get => evnt.Date4; set => evnt.Date4 = value; }

        public string GroomOccupation { get => Occupation3; set => evnt.Date2 = value; }
        public string FatherOccupation { get => Occupation3; set => evnt.Date2 = value; }

        public override string ToString() => $"Marriage: {Groom}&{Bride}";
    }


}

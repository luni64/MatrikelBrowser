
using AEM;
using Microsoft.EntityFrameworkCore.Query;

namespace MatrikelBrowser.ViewModels
{

    public class EventVM(Event evnt) : BaseViewModel
    {
        public int ID { get => model.Id; }
        public int SheetNr { get => model.SheetNr; set => SetProperty(model, e => e.SheetNr, value); }
        public string Title { get => model.Title; set => SetProperty(model, e => e.Title, value); }
        public string Transcript { get => model.Transcript; set => SetProperty(model, e => e.Transcript, value); }
        public string Remarks { get => model.Remarks; set => SetProperty(model, e => e.Remarks, value); }
        public double X { get => model.X; set => SetProperty(model, e => e.X, value); }
        public double Y { get => model.Y; set => SetProperty(model, e => e.Y, value); }
        public double W { get => model.W; set => SetProperty(model, e => e.W, value); }
        public double H { get => model.H; set => SetProperty(model, e => e.H, value); }
        public bool isLocked { get => _isLocked; set => SetProperty(ref _isLocked, value); }

        private bool _isLocked = true;
        public readonly Event model = evnt;
    }

    public class BirthEventVM(Event evnt) : EventVM(evnt)
    {
        public string ChildName { get => model.Person1; set => SetProperty(model, e => e.Person1, value); }
        public string Father { get => model.Person2; set => SetProperty(model, e => e.Person2, value); }
        public string Mother { get => model.Person3; set => SetProperty(model, e => e.Person3, value); }
        public string GodFather { get => model.Person7; set => SetProperty(model, e => e.Person7, value); }

        public string Birthday { get => model.Date1; set => SetProperty(model, e => e.Date1, value); }
        public string Baptism { get => model.Date2; set => SetProperty(model, e => e.Date2, value); }

        public string FatherOccupation { get => model.Occupation3; set => SetProperty(model, e => e.Occupation3, value); }

        public override string ToString() => $"Birth: {ChildName}";

    }

    public class DeathEventVM : EventVM
    {
        public DeathEventVM(Event evnt) : base(evnt) { }

        public string Name { get => model.Person1; set => SetProperty(model, e => e.Person1, value); }
        public string Occupation { get => model.Occupation1; set => SetProperty(model, e => e.Occupation1, value); }
        public string Father { get => model.Person2; set => SetProperty(model, e => e.Person2, value); }
        public string Mother { get => model.Person3; set => SetProperty(model, e => e.Person3, value); }

        public string Death { get => model.Date1; set => SetProperty(model, e => e.Date1, value); }
        public string Burial { get => model.Date2; set => SetProperty(model, e => e.Date2, value); }

        public string Reason { get => model.Misc; set => SetProperty(model, e => e.Misc, value); }

        public override string ToString() => $"Death: {Name}";
    }

    public class MarriageEventVM : EventVM
    {
        public MarriageEventVM(Event evnt) : base(evnt) { }

        public string Groom { get => model.Person1; set => SetProperty(model, e => e.Person1, value); }
        public string GroomFather { get => model.Person2; set => SetProperty(model, e => e.Person2, value); }
        public string GroomMother { get => model.Person3; set => SetProperty(model, e => e.Person3, value); }
        public string Bride { get => model.Person4; set => SetProperty(model, e => e.Person4, value); }
        public string BrideFather { get => model.Person5; set => SetProperty(model, e => e.Person5, value); }
        public string BrideMother { get => model.Person6; set => SetProperty(model, e => e.Person6, value); }
        public string Witnesses { get => model.Person7; set => SetProperty(model, e => e.Person7, value); }

        public string GroomOccupation { get => model.Occupation1; set => SetProperty(model, e => e.Occupation1, value); }
        public string BrideOccupation { get => model.Occupation2; set => SetProperty(model, e => e.Occupation2, value); }
        public string GroomFatherOcc { get => model.Occupation3; set => SetProperty(model, e => e.Occupation3, value); }


        public string MarriageDate { get => model.Date1; set => SetProperty(model, e => e.Date1, value); }
        public string GroomBirthday { get => model.Date3; set => SetProperty(model, e => e.Date3, value); }
        public string BrideBirthday { get => model.Date4; set => SetProperty(model, e => e.Date4, value); }
        
        public override string ToString() => $"Marriage: {Groom} & {Bride}";
    }
}

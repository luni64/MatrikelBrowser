
using AEM;

namespace MatrikelBrowser.ViewModels
{
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
        public string GroomMotherOcc { get => model.Occupation4; set => SetProperty(model, e => e.Occupation4, value); }
        public string BrideParentsOcc { get => model.Occupation5; set => SetProperty(model, e => e.Occupation5, value); }


        public string MarriageDate { get => model.Date1; set => SetProperty(model, e => e.Date1, value); }
        public string GroomBirthday { get => model.Date3; set => SetProperty(model, e => e.Date3, value); }
        public string BrideBirthday { get => model.Date4; set => SetProperty(model, e => e.Date4, value); }
        
        public override string ToString() => $"Marriage: {Groom} & {Bride}";
    }
}

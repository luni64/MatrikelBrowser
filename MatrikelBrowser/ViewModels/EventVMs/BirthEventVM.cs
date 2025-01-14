
using AEM;

namespace MatrikelBrowser.ViewModels
{
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
}

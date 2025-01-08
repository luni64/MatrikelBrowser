
using AEM;
using Microsoft.EntityFrameworkCore.Query;

namespace MatrikelBrowser.ViewModels
{

    public class EventVM : BaseViewModel
    {
        public readonly Event model;

        public EventVM(Event evnt)
        {
            this.model = evnt;
        }

        public int ID { get => model.Id; }
        public int SheetNr { get => model.SheetNr; set => SetProperty(model, e => e.SheetNr, value); }
        public string Title { get => model.Title; set => SetProperty(model, e => e.Title, value); }
        public string Transcript { get => model.Transcript; set => SetProperty(model, e => e.Transcript, value); }
        public string Remarks { get => model.Remarks; set => SetProperty(model, e => e.Remarks, value); }
        public double X { get => model.X; set => SetProperty(model, e => e.X, value); }
        public double Y { get => model.Y; set => SetProperty(model, e => e.Y, value); }
        public double W { get => model.W; set => SetProperty(model, e => e.W, value); }
        public double H { get => model.H; set => SetProperty(model, e => e.H, value); }

        private bool _isLocked = true;
        public bool isLocked
        {
            get => _isLocked;
            set => SetProperty(ref _isLocked, value);
        }
    }

    public class BirthEventVM : EventVM
    {
        public BirthEventVM(Event evnt) : base(evnt) { }

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
        public string Father { get => model.Person2; set => SetProperty(model, e => e.Person2, value); }
        public string Mother { get => model.Person3; set => SetProperty(model, e => e.Person3, value); }

        public string Death { get => model.Date1; set => SetProperty(model, e => e.Date1, value); }
        public string Burial { get => model.Date2; set => SetProperty(model, e => e.Date2, value); }

        public override string ToString() => $"Birth: {Name}";
    }
}

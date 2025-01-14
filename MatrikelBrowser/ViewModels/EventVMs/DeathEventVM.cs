
using AEM;

namespace MatrikelBrowser.ViewModels
{
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
}

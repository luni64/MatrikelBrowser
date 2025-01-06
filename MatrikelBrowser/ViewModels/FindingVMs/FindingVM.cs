using AEM;
using AEM.Tectonics;
using System.Collections.Generic;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
//using Event = AEM.Tectonics.Event;

namespace MatrikelBrowser.ViewModels
{
    public class FindingVM : BaseViewModel
    {
        private RelayCommand? _cmdDelSelf;
        public RelayCommand cmdDelSelf => _cmdDelSelf ??= new RelayCommand((object? _) => doDelSelf());
        private void doDelSelf()
        {
            bookVM.cmdDelEvent.Execute(this);
            //   parent.cmdDelEvent.Execute(this);
            //marriageModel.Info.Bookmarks.Remove(SelectedBookmark.marriageModel);
            //parent.bookmarkVMs.Remove(this);
        }

        public string Name { get; set; }
        
        public int SheetNr { get; set; }

        //public List<Event> possibleEvents { get; }
        //public Event SelectedEvent { get; set; }

        public FindingVM(Event evnt, BookVM bookVM)
        {
            this.Name = evnt.Title;
            this.SheetNr = evnt.SheetNr;
            this.bookVM = bookVM;


            //possibleEvents = [new BirthEvent(model), new MarriageEvent(model), new DeathEvent(model)];
            //SelectedEvent = model switch
            //{
            //    BirthEvent => possibleEvents[0],
            //    MarriageEvent => possibleEvents[1],
            //    DeathEvent => possibleEvents[2],
            //    _ => possibleEvents[0]
            //};

            //model.Person3 = "333";

            //possibleEvents[0].Person6 = "mmmm";
        }

        BookVM bookVM;

        
    }

    //public class BirthFindingVM(BirthEvent model) : FindingVM(model,null)
    //{
    //    // public string Child => birthModel.ChildName;
    //    //public string  Father => birthModel.Father;
    //    public string Mother => birthModel.Mother;

    //    public BirthEvent birthModel { get; } = model;
    //}

    //public class MarriageFindingVM(MarriageEvent finding) : FindingVM(finding,null)
    //{
    //    //public Person? Groom => marriageModel.Groom;
    //    //public Person? Bride => marriageModel.Bride;

    //    MarriageEvent marriageModel = finding;
    //}


}

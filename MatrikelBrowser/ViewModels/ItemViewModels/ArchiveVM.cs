using AEM.Tectonics;
using MbCore;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace MatrikelBrowser.ViewModels
{
    public class ArchiveVM : ItemVM
    {
        public string Name => model.Name;
        public ObservableCollection<LetterVM> LetterVMs { get; } = [];

        public override bool IsExpanded
        {
            get => base.IsExpanded;
            set
            {
                if (value == true) LoadLetters();
                base.IsExpanded = value;
            }
        }

        public void LoadLetters()
        {
            if (model.Parishes.Count == 0) // if expanded the first time
            {
                LetterVMs.Clear(); // remove dummy

                model.LoadParishes();

                var pp = model.ParishBatches(10);

                var parishGroups = model.Parishes.ToLookup(l => l.Name[0]);  // group parishes by first letter

                Trace.Write($"  {Name}: ");


                foreach (var parishGroup in pp)
                {
                    Trace.Write($"{parishGroup.Key} ");
                    LetterVMs.Add(new LetterVM(parishGroup, this)); // Create a new LetterVM for each letter and add it to the list.
                }
                Trace.WriteLine("");
            }
        }


        public ArchiveVM(Archive archive = null!, CountryVM parent = null!) : base(parent)
        {
            if (archive == null) return;
            model = archive;
            LetterVMs.Add(new LetterVM()); // dummy, load actual entries only if expanded
            Indent = -8;
        }

        internal readonly Archive model;

        public static ArchiveVM Dummy => new();
    }
}

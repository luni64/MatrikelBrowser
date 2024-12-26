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
                if (value == true && LetterVMs.Any(a => a.parent == null)) // if expanded -> Check for dummy entries.
                {
                    LetterVMs.Clear(); 
                                      
                    using var ctx = new MatrikelBrowserCTX();

                    // Retrieve the distinct first letters of parish names associated with the given model.
                    var letters = ctx.Parishes
                        .Where(p => p.Archive == model) 
                        .Select(e => e.Name.Substring(0, 1)) // Extract the first character of each name.
                        .Distinct() 
                        .OrderBy(letter => letter) 
                        .ToList();
                   
                    Trace.Write($"  {Name}: ");
                    foreach (var letter in letters)
                    {
                        Trace.Write($"{letter} "); 
                        LetterVMs.Add(new LetterVM(letter, this)); // Create a new LetterVM for each letter and add it to the list.
                    }
                    Trace.WriteLine(""); 
                }
                base.IsExpanded = value; 
            }
        }


        public ArchiveVM(Archive? archive = null, CountryVM? parent = null) : base(parent)
        {
            model = archive!;           
            LetterVMs.Add(new LetterVM()); // dummy, load actual entries only if expanded
            Indent = -8;
        }

        internal readonly Archive model;
    }
}

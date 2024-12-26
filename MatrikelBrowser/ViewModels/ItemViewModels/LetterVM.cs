using MbCore;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Crmf;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;

namespace MatrikelBrowser.ViewModels
{
    public class LetterVM : ItemVM
    {
        public string Letter { get; }
        public ObservableCollection<ParishVM> ParishVMs { get; } = new();


        public override bool IsExpanded
        {
            get => base.IsExpanded;
            set
            {
                if (value == true && ParishVMs.Any(a => a.parent == null)) // if expanded -> Check for dummy entries.
                {
                    ParishVMs.Clear();

                    using var ctx = new MatrikelBrowserCTX();

                    if (parent is ArchiveVM archiveVM)
                    {
                        // Retrieve the parishes with the corresponding first letter
                        var parishes = ctx.Parishes
                            .Where(p => p.Archive == archiveVM.model && p.Name.Substring(0, 1) == Letter)
                            .OrderBy(p => p.Name)
                            .ToList();

                        foreach (var parish in parishes)
                        {
                            ParishVMs.Add(new ParishVM(parish, this));
                        }
                    }


                }
                base.IsExpanded = value;


            }
        }

        public LetterVM(string Letter = "", ArchiveVM? parent = null) : base(parent)
        {
            this.Letter = Letter;
            ParishVMs.Add(new ParishVM());          
        }


        //IGrouping<char, Parish> group;
        //List<Parish> Parishes = [];
    }
}
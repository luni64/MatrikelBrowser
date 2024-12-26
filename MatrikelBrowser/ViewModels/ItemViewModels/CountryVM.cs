using MbCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace MatrikelBrowser.ViewModels
{
    public class CountryVM : ItemVM
    {
        public string Name => model.Name;
        public ObservableCollection<ArchiveVM> ArchiveVMs { get; } = [];

        public override bool IsExpanded
        {
            get => base.IsExpanded;
            set
            {
                if (value == true && ArchiveVMs.Any(a => a.parent == null)) // check for dummy
                {
                    ArchiveVMs.Clear();
                    using var ctx = new MatrikelBrowserCTX();
                    ctx.Entry(model).Collection(c => c.Archives).Load();

                    foreach (var a in model.Archives)
                    {
                        var avm = new ArchiveVM(a, this);

                        ArchiveVMs.Add(new ArchiveVM(a, this));
                    }

                }
                base.IsExpanded = value;
            }
        }

        public CountryVM(Country model) : base(null)
        {
            this.model = model;
            //ArchiveVMs = new (model.Archives.Select(a=>new ArchiveVM(a, this)).ToList());
            ArchiveVMs.Add(new ArchiveVM()); // dummy
        }
        Country model;
    }
}

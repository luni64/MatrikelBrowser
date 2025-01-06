using MbCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Collections.ObjectModel;

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
                if (value == true)  LoadArchives();                
                base.IsExpanded = value;
            }
        }

        public void LoadArchives()
        {
            if (model.Archives.Count == 0)
            {
                ArchiveVMs.Clear(); // remove dummy

                model.LoadArchives();
                foreach (var a in model.Archives)
                {
                    ArchiveVMs.Add(new ArchiveVM(a, this));
                }
            }
        }

        public CountryVM(Country model, TectonicsVM parent) : base(parent)
        {
            this.model = model;
            ArchiveVMs.Add(ArchiveVM.Dummy);
        }
        public readonly Country model;
    }
}

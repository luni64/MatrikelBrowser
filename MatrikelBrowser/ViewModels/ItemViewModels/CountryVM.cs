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
                if (value == true && model.Archives.Count == 0)
                {
                    ArchiveVMs.Clear(); // remove dummy

                    model.LoadArchives();
                    foreach (var a in model.Archives)
                    {
                        ArchiveVMs.Add(new ArchiveVM(a, this));
                    }
                }
                base.IsExpanded = value;
            }
        }

        //public override bool IsSelected
        //{
        //    get => base.IsSelected;
        //    set
        //    {
        //        ((TectonicsVM)parent).SelectedCountry = this;               
        //        base.IsSelected = value;
        //    }
        //}
        public CountryVM(Country model, TectonicsVM parent) : base(parent)
        {
            this.model = model;
            ArchiveVMs.Add(ArchiveVM.Dummy);
        }
        Country model;
    }
}

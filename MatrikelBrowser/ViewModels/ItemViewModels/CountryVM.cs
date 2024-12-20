using AEM;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ArchiveBrowser.ViewModels
{
    public class CountryVM : ItemVM
    {
        public string Name => model.Name;
        public ObservableCollection<ArchiveVM> ArchiveVMs { get; } //= [];

        public CountryVM(Country model) : base(null)
        {
            this.model = model;
            ArchiveVMs = new (model.Archives.Select(a=>new ArchiveVM(a, this)).ToList());                     
        }
        Country model;
    }
}

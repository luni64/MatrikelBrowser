using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ArchiveBrowser.ViewModels
{
    public class CountryVM : ItemVM
    {
        public string Name { get; }
        public ObservableCollection<DioceseVM> DioceseVMs { get; } = [];

        public CountryVM(string Name)
        {   
            this.Name = Name;            
        }
    }
}

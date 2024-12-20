using AEM;
using System.Collections.ObjectModel;
using System.Linq;

namespace ArchiveBrowser.ViewModels
{
    public class ArchiveVM : ItemVM
    {
        public string Name => model.Name;
        public ObservableCollection<LetterVM> LetterVMs { get; }

        public ArchiveVM(Archive archive, CountryVM parent) : base(parent)
        {
            this.model = archive;
            LetterVMs = new( archive.Parishes.GroupBy(p => p.Place[0]).Select(g=>new LetterVM(g, this)));
           
            Indent = -8;
        }

        private readonly Archive model;
    }
}

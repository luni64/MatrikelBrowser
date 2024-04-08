using AEM;
using System.Collections.Generic;

namespace ArchiveBrowser.ViewModels
{
    public class ParishVM : ItemVM
    {
        public string Title { get; }
        public string SubTitle { get; }
        public string RefNr { get; }
        public List<BookTypeVM> BookTypeVMs { get; } = new();
        public ParishVM(Parish model)
        {
            Title = $"{model.Place} ";
            SubTitle = $"{model.ID} {model.Church} (#{model.Books.Count}, {model.startYear}-{model.endYear})";
            RefNr = model.ID;

            Indent = -10;
        }
    }
}

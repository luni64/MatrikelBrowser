using AEM;
using Interfaces;
using System.Collections.Generic;

namespace ArchiveBrowser.ViewModels
{
    public class ParishVM : ItemVM
    {
        public string Title { get; }
        public string Church { get; }
        public string SubTitle { get; }
        public string RefNr { get; }
        public List<BookTypeVM> BookTypeVMs { get; } = new();
        public ParishVM(Parish model)
        {
            Title = $"{model.Place} ";
            Church = model.Church;
            SubTitle = $"{model.RefId} {model.Church} (#{model.Books.Count}";//, {model.startYear}-{model.endYear})";
            RefNr = model.RefId;

            Indent = -10;
        }
    }
}

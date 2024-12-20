using AEM;
using Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Linq;

namespace ArchiveBrowser.ViewModels
{
    public class ParishVM : ItemVM
    {
        public string Title => model.Name;
        public string Church => model.Church;
        public string SubTitle => $"{model.RefId} {model.Church} (#{model.Books.Count}";
        public string RefNr => model.RefId;
        public List<BookTypeVM> BookTypeVMs { get; }//= new();
        public ParishVM(Parish model, LetterVM parent): base(parent)
        {
            this.model = model;

            var BookGroups = model.Books.GroupBy(b => b.BookType).OrderBy(bt => bt.Key);
            BookTypeVMs = BookGroups.Select(bt => new BookTypeVM(bt, this)).ToList();            

            Indent = -10;
        }

        private readonly Parish model;
    }
}

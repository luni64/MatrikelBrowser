using MbCore;
using Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;

namespace MatrikelBrowser.ViewModels
{
    public class ParishVM : ItemVM
    {
        public string Title => model?.Name ?? string.Empty;
        public string Church => model?.Church ?? string.Empty;
        public string SubTitle => $"{model?.RefId} {model?.Church} (#{model?.Books.Count}";
        public string RefNr => model?.RefId ?? string.Empty;

        

        public ObservableCollection<BookTypeVM>? BookTypeVMs { get; }= new();
        public ParishVM(Parish? model = null, LetterVM? parent = null): base(parent)
        {
            this.model = model;

            //var BookGroups = model.Books.GroupBy(b => b.BookType).OrderBy(bt => bt.Key);
            //BookTypeVMs = BookGroups.Select(bt => new BookTypeVM(bt, this)).ToList();            

            Indent = -10;
        }

        internal readonly Parish? model;
    }
}

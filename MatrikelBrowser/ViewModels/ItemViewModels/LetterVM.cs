using AEM;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ArchiveBrowser.ViewModels
{
    public class LetterVM : ItemVM
    {
        public string Letter { get; }
        public ObservableCollection<ParishVM> ParishVMs { get; } //= new();

        override public bool IsSelected 
        { get; 
            set; } 

        public LetterVM(IGrouping<char, Parish> group, ArchiveVM parent) :base (parent)
        {
            Letter = group.Key.ToString();
            ParishVMs = new ( group.Select(p => new ParishVM(p,this)));
        }
    }
}
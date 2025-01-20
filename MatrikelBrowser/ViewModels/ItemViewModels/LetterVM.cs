using MbCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MatrikelBrowser.ViewModels
{
    public class LetterVM : ItemVM
    {
        public string Letter { get; } = string.Empty;
        public ObservableCollection<ParishVM> ParishVMs { get; } = new();

        //public LetterVM(IGrouping<char, Parish> parishGroup = null!, ArchiveVM parent = null!) : base(parent)
        //{


        //    if (parishGroup == null) return;

        //    this.Letter = parishGroup.Key.ToString();
        //    foreach (var parish in parishGroup)
        //    {
        //        ParishVMs.Add(new ParishVM(parish, this));
        //    }
        //}

        public LetterVM(KeyValuePair<string, List<Parish>>? parishGroup = null!, ArchiveVM parent = null!) : base(parent)
        {
            if (parishGroup == null) return;

            if (parishGroup.HasValue)
            {
                this.Letter = parishGroup.Value.Key;

                foreach (var parish in parishGroup.Value.Value)
                {
                    ParishVMs.Add(new ParishVM(parish, this));
                }
            }
            Indent = 10;
        }
    }
}
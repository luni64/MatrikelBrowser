using MbCore;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Crmf;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;

namespace MatrikelBrowser.ViewModels
{
    public class LetterVM : ItemVM
    {
        public string Letter { get; } = string.Empty;
        public ObservableCollection<ParishVM> ParishVMs { get; } = new();

        public LetterVM(IGrouping<char,Parish> parishGroup = null!, ArchiveVM parent = null!) : base(parent)
        {
            if (parishGroup == null) return;

            this.Letter = parishGroup.Key.ToString();
            foreach(var parish in parishGroup)
            {
                ParishVMs.Add(new ParishVM(parish, this));
            }
        }
    }
}
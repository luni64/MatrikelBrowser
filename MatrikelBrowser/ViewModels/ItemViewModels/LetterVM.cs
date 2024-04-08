using System.Collections.Generic;

namespace ArchiveBrowser.ViewModels
{
    public class LetterVM : ItemVM
    {
        public string Letter { get; }
        public List<ParishVM> ParishVMs { get; } = new();

        public LetterVM(string Letter)
        {
            this.Letter = Letter;            
        }
    }
}

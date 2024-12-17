using System.Collections.Generic;

namespace ArchiveBrowser.ViewModels
{
    public class DioceseVM : ItemVM
    {
        public string Name { get; }
        public List<LetterVM> LetterVMs { get; } = new();

        public DioceseVM(string Name)
        {
            this.Name = Name;
            Indent = -8;
        }
    }
}

using System.Collections.Generic;

namespace AEM
{
    public class Archive
    {
        public int Id { get; set; }
        public string REFID { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public required string BookInfoUrl { get; set; }
        public required string ViewerUrl { get; set; }
        public List<Parish> Parishes { get; set; } = [];
        required public Country Country { get; set; }
        required public ArchiveType ArchiveType { get; set; }

        public override string ToString() => Name;
    }

}
 
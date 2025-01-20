using System.Collections.Generic;

namespace MbCore
{
    public class Archive
    {
        public int Id { get; set; }
        public string REFID { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public required string Breadcrumb { get; set; }
        public required string ViewerUrl { get; set; }
        required public ArchiveType ArchiveType { get; set; }
        
        virtual public List<Parish> Parishes { get; set; } = [];
        virtual public Country Country { get; set; } = null!;

        public override string ToString() => Name;
    }

}
 
using System.Collections.Generic;

namespace MbCore
{
    //public class PlaceDTO
    //{
    //    public int Id { get; set; }
    //    required public string Name { get; set; }
    //    public string? Location { get; set; }

    //}

    public class Country
    {
        public int Id { get; set; }
        required public string Name { get; set; }
        public string Breadcrumb { get; set; } = string.Empty;

        virtual public List<Archive> Archives { get; set; } = [];
        public override string ToString() => Name;
    }

}
 
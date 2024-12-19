using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MbCore
{
    public class Parish
    {
        public int Id { get; set; }
        public required string Name { get; set; } = string.Empty;
        public string RefId { get; set; } = string.Empty;
        public required string Place { get; set; }
        public string Church { get; set; } = string.Empty;
        required public Archive Archive { get; set; }
        public string BookBaseUrl { get; set; } = string.Empty;
        public List<Book> Books { get; set; } = [];

        [NotMapped]
        public bool hasBooks { get; set; }
        public override string ToString() => Name;
    }

}

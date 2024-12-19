using System.Collections.Generic;

namespace AEM
{
    public class Parish
    {
        public int Id { get; set; }
        public required string Name { get; set; } = string.Empty;
        public string RefId { get; set; } = string.Empty;
        public required string Place { get; set; }
        public string Church { get; set; } = string.Empty;


        required public Archive Archive { get; set; }
        required public string BookBaseUrl { get; set; }
        public List<Book> Books { get; set; } = [];
        public override string ToString() => Name;
    }

}
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEM
{
    internal class aemBook
    {
        public string ID { get; set; } = "";
        public string Title { get; set; } = "";
        public Guid BookInfoID { get; set; }
        public Guid DescriptionID { get; set; }
        public override string ToString() => $"{ID}-{Title}-{BookInfoID}";
    }

    internal class aemParish
    {
        public string ID { get; set; } = "";
        public string Place { get; set; } = "";
        public string Church { get; set; } = "";
        public int startYear { get; set; }
        public int endYear { get; set; }

        public List<aemBook> Books { get; set; } = new List<aemBook>();

        public override string ToString() => $"{ID}-{Place}-{Church}";
    }
}

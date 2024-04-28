using System.Collections.Generic;

namespace Interfaces
{
    public interface IParish
    {
        List<IBook> Books { get; set; }
        string Church { get; set; }
        int endYear { get; set; }
        string ID { get; set; }
        string Place { get; set; }
        int startYear { get; set; }

        string ToString();
    }
}
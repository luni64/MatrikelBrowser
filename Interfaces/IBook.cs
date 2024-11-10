using System;
using System.Collections.Generic;
using System.IO;

namespace Interfaces
{
    public interface IBook
    {
      //  DirectoryInfo? bookFolder { get; }
        string BookDescriptionURL { get; }
        Guid BookInfoID { get; set; }
        Guid DescriptionID { get; set; }
        string? Details { get; set; }
        bool hasInfo { get; set; }
        string ID { get; set; }
        IBookInfo Info { get; set; }

     

        List<IPage> Pages { get; }
        DirectoryInfo? pagesFolder { get; }
        IParish? Parish { get; set; }
        string Title { get; set; }
        string Type { get; }

        void LoadPageInfo();
        string ToString();
    }
}
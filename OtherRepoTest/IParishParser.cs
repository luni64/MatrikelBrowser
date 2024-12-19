using MbCore;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Converters;
using static System.Net.WebRequestMethods;

namespace OtherRepoTest
{
    public interface IBookInfo
    {
        public string REFID { get; set; }
        public BookType Type { get; set; }
        public string Title { get; set; }
        public string InfoUrl { get; set; }
    }

    public interface IParishParser
    {
        public bool Parse(Uri infoUrl);
        public void UpdateDB(MatrikelBrowserCTX ctx);

        public string Country { get; set; }
        public string Diocese { get; set; }
        public string Parish { get; set; }
        public string Church { get; set; }
        public string REFID { get; set; }
        public string BookBaseUrl { get; set; }
        public IList<IBookInfo> bookInfos { get; set; }

    }
}





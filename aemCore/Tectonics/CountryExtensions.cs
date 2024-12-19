using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MbCore
{
    public static class CountryExtensions
    {
        public static void LoadArchives(this Country country)
        {
            using var ctx = new MatrikelBrowserCTX();
            ctx.Entry(country).Collection(c => c.Archives).Load();
        }
    }
}

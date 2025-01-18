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
            ctx.Attach(country);

            if(ctx.Archives.Where(a=>a.Country.Id == country.Id).Count() == 0)
            {
                var archives = MatParser.ParseArchives(country.InfoLink);
                country.Archives.AddRange(archives);
                ctx.SaveChanges();
            }


            ctx.Entry(country).Collection(c => c.Archives).Load();
        }


        
    }
}

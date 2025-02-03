using iText.Layout.Properties.Grid;
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

            if (ctx.Archives.Where(a => a.Country.Id == country.Id).Count() == 0)
            {
                var archives = MatParser.ParseArchives(country.Breadcrumb);

                if (country.Name == "Deutschland")
                {
                    var archive = archives.Where(a=>a.Breadcrumb == "muenchen").FirstOrDefault();
                    if (archive != null)
                    {
                        archive.ArchiveType = ArchiveType.AEM;
                        archive.ViewerUrl = "https://digitales-archiv.erzbistum-muenchen.de/actaproweb/mets?id=Rep_{BOOKID}_mets_actapro.xml";
                    }

                    archives.RemoveAll(a => a.Breadcrumb == "essen");  // system not yet supported                  
                }

                country.Archives.AddRange(archives);
                ctx.SaveChanges();
            }


            ctx.Entry(country).Collection(c => c.Archives).Load();
        }



    }
}

using MbCore;
using System.CodeDom;
using System.Diagnostics.Metrics;
using System.Linq;

namespace AEM.Tectonics
{
    public static class ArchiveExtensions
    {
        public static void LoadParishes(this Archive archive)
        {
            using var ctx = new MatrikelBrowserCTX();
            ctx.Attach(archive);

            if (ctx.Parishes.Where(p => p.Archive.Id == archive.Id).Count() == 0)
            {
                var parishes = MatParser.ParseParishes(archive.BookInfoUrl);
                archive.Parishes.AddRange(parishes);
                ctx.SaveChanges();
            }

            ctx.Entry(archive).Collection(c => c.Parishes).Load();

        }
    }
}

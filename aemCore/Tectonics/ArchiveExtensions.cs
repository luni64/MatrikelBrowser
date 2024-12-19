using MbCore;
using System.CodeDom;

namespace AEM.Tectonics
{
    public static class ArchiveExtensions
    {
        public static void LoadParishes(this Archive archive)
        {
            using var ctx = new MatrikelBrowserCTX();
            //if (archive.Parishes.Count == 0)
            {
                ctx.Entry(archive).Collection(c => c.Parishes).Load();
            }
        }
    }
}

using MbCore;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
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
                var URL = archive.Country.Breadcrumb + '/' + archive.Breadcrumb;
                var parishes = MatParser.ParseParishes(URL);
                archive.Parishes.AddRange(parishes);
                var firstParish = archive.Parishes.FirstOrDefault();
                if (firstParish != null && firstParish.RefId.Length == 5 && firstParish.RefId.StartsWith("CB"))
                {
                    archive.ArchiveType = ArchiveType.AEM;
                }
                ctx.SaveChanges();
            }

            ctx.Entry(archive).Collection(c => c.Parishes).Load();

        }

        public static Dictionary<string, List<Parish>> ParishBatches(this Archive archive, int batchSize)
        {
            var ps = archive.Parishes;
            var result = new Dictionary<string, List<Parish>>();
            int totalItems = ps.Count;
            int groupSize = (int)Math.Ceiling(totalItems / (double)batchSize);

            for (int i = 0; i < totalItems; i += groupSize)
            {
                var groupItems = ps.Skip(i).Take(groupSize).ToList();
                string startKey = groupItems.First().Name.Substring(0, Math.Min(10, groupItems.First().Name.Length));
                string endKey = groupItems.Last().Name.Substring(0, Math.Min(10, groupItems.Last().Name.Length));
                string key = $"{startKey} - {endKey}";

                result[key] = groupItems;
            }

            return result;

        }
    }
}

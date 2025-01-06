using MbCore;
using Org.BouncyCastle.Asn1.Cmp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEM.Tectonics.Events
{
    public static class EventExtensions
    {
        public static void RemoveFromDatabase(this Event e)
        {
            using var ctx = new MatrikelBrowserCTX();

            if (ctx.Events.Contains(e))
            {
                ctx.Events.Remove(e);
                ctx.SaveChanges();
            }        
        }

        public static void AddToDatabase(this Event e)
        {
            using var ctx = new MatrikelBrowserCTX();

            if (!ctx.Events.Contains(e))
            {
               
                ctx.Add(e);
                //e.Book.Events.Add(e);

                ctx.SaveChanges();
            }
        }
    }
}

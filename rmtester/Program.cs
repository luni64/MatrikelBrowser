using Microsoft.EntityFrameworkCore;
using RMDatabase;
using RMDatabase.Models;
using rmSharp;
using System.Diagnostics;
using System.Net;
using static System.Diagnostics.Trace;
//using static System.Console;

namespace rmtester
{
    internal class Program
    {

        static void Main(string[] args)
        {
            DB.sqLiteFile = "oberhauser-niggl.rmtree";


            using (var db = new DB())
            {
                var p = db.PayloadTables
                    //.Where(p => p.OwnerType == 8)
                    //.Select(p => p.Names.Where(n=>n.IsPrimary))
                   .ToList()
                    
                    ;

               // var en = db.GroupEntries.Where(e => e.GroupId == p.First().OwnerId);

                //foreach (var url in p)
                //{
                //    Trace.Write($"Checking {url}... ");
                //    var request = WebRequest.Create(url);
                //    request.Method = "HEAD";
                //    var response = (HttpWebResponse)request.GetResponse();
                //    if (response != null && response.StatusCode == HttpStatusCode.OK)
                //    {
                //        Trace.WriteLine($"OK");
                //    }
                //    else Trace.WriteLine("NOK");

                //    response?.Close();
                    


                //}



                WriteLine(p.Count());

                //var q2 = p.Where(p => p.Events.Count() > 0).Select(p => p.Names.Where(n=>n.IsPrimary));

                //var q3 = q2.Select(x => new { x.First().Surname, x.First().Given });





                //    //Trace.WriteLine($"{p.ToQueryString()}");


                //    var n = db.NameTable.Where(f=>f.Citations.Count()> 1).ToList();


                //    foreach (var person in db.Persons)
                //    {
                //        WriteLine($"{person.PrimaryName.Surname} {person.PrimaryName.Given}");
                //        foreach (var evnt in person.Events)
                //        {
                //            WriteLine($"  Event: {evnt.FactType} {evnt.Date} {evnt.Details}");
                //            foreach (var citation in evnt.Citations)
                //            {
                //                WriteLine($"    Source:{citation.Source} ({citation.CitationName})");
                //                WriteLine($"           {citation.Source.Repositories.FirstOrDefault()}");
                //            }
                //        }
                //    }
                //}



                //new Example.MaleAncestrors().Execute(Surname: "Niggl", Given: "Günther");
                //new Example.DecendantTree().Execute("Oberhauser", "Johann");
                //new Example.AddPerson().Execute();
                //new Example.Pedigree().Execute(Surname: "Niggl", Given: "Lutz Günther");
                //new Example.MultiplePrimaryNames().Execute();
                //new Example.MultipleParents().Execute();
            }
        }
    }
}








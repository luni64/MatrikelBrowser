

using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using RMDatabase;

namespace rmtester
{


    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            DB.sqLiteFile = "test.rmtree";

            using (var db = new DB())
            {
                var q = db.Persons.Where(p => p.PersonId == 451)
                ;

                var s = q.ToQueryString();

                //var x = q.ToList();


                foreach (var person in db.Persons.Where(p => p.PersonId == 451))
                {

                    var parent = person.Parent;

                    foreach (var n in person.Names.Where(p => p.NameType <100))
                    {
                        Console.WriteLine($"{n.Given} {n.Surname} {n.NameType}");
                    }
                    //foreach (var a in person.Addresses.Where(p => p.NameType < 100))
                    //{
                    //    Console.WriteLine($"{n.Given} {n.Surname} {n.NameType}");
                    //}
                }
            }
        }
    }
}


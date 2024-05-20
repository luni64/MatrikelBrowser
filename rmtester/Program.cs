using Microsoft.EntityFrameworkCore;
using RMDatabase;
using RMDatabase.Models;
using System.Collections.Immutable;

namespace rmtester
{
    internal class Program
    {
        
        static (RelationShip father, RelationShip mother) getRelation(DB db, Person child, Family family)
        {
            var childTableEntry = db.ChildTable.Single(e => e.ChildId == child.PersonId && e.Family == family);
            return (childTableEntry.RelFather, childTableEntry.RelMother);
        }

        static void Main(string[] args)
       {            
            DB.sqLiteFile = "test.rmtree";

            
            using (var db = new DB())
            {
                var p = db.PersonTable.First(p => p.PrimaryName.Given.Contains("Günther") && p.PrimaryName.Surname == "Niggl");

                var q = p.getFamilies(db);

                var fams = p.getFamilies(db).Select(f=>f.FamilyId).ToList();
                var children = p.getChildren(db).ToList();


                Console.WriteLine(p.getFamilies(db).Select(f=>f.FamilyId).ToQueryString());
                Console.WriteLine("\n-----------------\n");
                Console.WriteLine(p.getChildren(db).Select(p=>p.PersonId).ToQueryString());







                //var lut = db.ChildTable.ToLookup(c => (c.Family.Father, c.Family.Mother), c=>c.Child);

                //var families = lut.Where(e => e.Key.Father == p || e.Key.Mother == p);
                //foreach (var family in families)
                //{
                //    Console.WriteLine($"{family.Key.Father} & {family.Key.Mother}");
                //    foreach(var child in family)
                //    {
                //        Console.WriteLine($"  {child}");
                //    }
                //}






                //var families = db.ChildTable
                //    .Where(c => (p == c.Family.Father && c.RelFather == RelationShip.Birth) || (p == c.Family.Mother && c.RelMother == RelationShip.Birth))
                //    .SelectMany(x=>x.Child); 

                //var children = db.ChildTable
                //    .Where(c => families.Contains(c.FamilyId))
                //    .Select(p => p.Child).ToList();



                //                Console.WriteLine(children.ToQueryString());

                //children.ForEach(c => Console.WriteLine(c));


            }



            //new Example.AddPerson().Execute();
            //new Example.MaleAncestrors().Execute(Surname: "Niggl", Given: "Lutz Günther");
            //new Example.Pedigree().Execute(Surname: "Niggl", Given: "Lutz Günther");
            //new Example.MultiplePrimaryNames().Execute();
            // new Example.MultipleParents().Execute();
        }
    }



}




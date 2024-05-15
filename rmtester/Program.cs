using Microsoft.EntityFrameworkCore;
using RMDatabase;
using RMDatabase.Models;

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

            //using(var db = new DB())
            //{
            //    var x = db.Persons.Where(p => p.PrimaryName().Surname == "Niggl").Select(p=>p.PrimaryName()).ToList();
            //    //var q = x.ToQueryString();
            //}


            new Example.AddPerson().Execute();
            //new Example.MaleAncestrors().Execute(Surname: "Niggl", Given: "Lutz Günther");
            //new Example.Pedigree().Execute(Surname: "Niggl", Given: "Lutz Günther");
            //new Example.MultiplePrimaryNames().Execute();
            // new Example.MultipleParents().Execute();
        }
    }



}




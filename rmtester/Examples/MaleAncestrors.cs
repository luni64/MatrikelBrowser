using RMDatabase;
using RMDatabase.Models;
using static System.Console;

namespace Example
{
    class MaleAncestrors
    {
        public void Execute(string Surname, string Given)
        {
            using (var db = new DB()) // connect to the database
            {
                int g = 0; // tracks generations
                var person = db.Persons.FirstOrDefault(p => p.PrimaryName.Surname == Surname && p.PrimaryName.Given == Given);

                WriteLine($"({g++}) {person?.PrimaryName.ToString() ?? "Startperson not found"}");
                while (person != null)
                {
                    Person? father = person                                     // this query selects the biological father of person
                      .ParentRelations                                          // relation info from the ChildTable
                      .SingleOrDefault(cr=>cr.RelFather == RelationShip.Birth)? // returns null if zero or more than one biological father
                      .family.Husband;                                          // get the male ancestor 

                    WriteLine($"({g++}) {father?.PrimaryName.ToString() ?? "--"}");
                    person = father;
                }
            }
        }
    }
}







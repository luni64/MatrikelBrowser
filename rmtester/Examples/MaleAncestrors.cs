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
                var person = db.Persons.FirstOrDefault(p => p.PrimaryName.Surname == Surname && p.PrimaryName.Given == Given); // startperson

                while (person != null)
                {
                    printPerson(person, g++);

                    Person? father = person                                              // find biological father of person:
                      .ParentRelations.Where(cr => cr.RelFather == RelationShip.Birth)   // filter for parent relations with a biological father (should be only one of course)
                      .FirstOrDefault()?.Father;                                         // take the first relation (or null if none found)                                                                                           

                    person = father;                                                     // use the found father for the next iteration
                }
            }
        }

        void printPerson(Person p, int generation)
        {
            var birthDate = p.Events.FirstOrDefault(e => e.FactType.Name == "Birth")?.Date;  // find birth and death dates from the corresponding events
            var deathDate = p.Events.FirstOrDefault(e => e.FactType.Name == "Death")?.Date;

            WriteLine($"({generation})\t{p.PrimaryName} ({birthDate?.year} - {deathDate?.year})");
        }
    }
}







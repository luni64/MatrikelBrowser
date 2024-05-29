using RMDatabase;
using RMDatabase.Models;

namespace Example
{ 
    class DecendantTree
    {
        public void Execute(string Surname, string Given)
        {
            using (var db = new DB())
            {
                Person? root = db
                    .Persons
                    .Where(p => p.PrimaryName.Surname == Surname && p.PrimaryName.Given == Given)
                    .FirstOrDefault();

                if (root != null)
                {
                    printDescendants(root);
                }
            }
        }

        private void printDescendants(Person person, int level = 0) //recursively print descendants
        {
            Console.WriteLine($"{new string(' ', level * 3)}(G{level})-{person}");
            level += 1;
            foreach (var family in person.ChildRelations)
            {
                foreach (var childRelation in family.ChildInfos)
                {
                    printDescendants(childRelation.Child, level);
                }
            }
        }
    }
}







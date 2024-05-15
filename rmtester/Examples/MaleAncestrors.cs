using Microsoft.EntityFrameworkCore;
using RMDatabase;
using RMDatabase.Models;

namespace Example
{
    class MaleAncestrors
    {
        public void Execute(string Surname, string Given)
        {
            using (var db = new DB())
            {
                // query the first person with the passed in surname and given name
                var rootPerson = db.Persons
                    .Where(p => p.PrimaryName.Surname == Surname && p.PrimaryName.Given == Given)
                    .FirstOrDefault();


                if (rootPerson != null)
                {
                    var maleAncestors = getMaleAncestors(db, rootPerson);
                    foreach (var ancestor in maleAncestors)
                    {
                        var name = ancestor.PrimaryName;
                        Console.WriteLine($"{name.Surname} {name.Given}");
                    }
                }
            }
        }


        /*********************************************************************** 
         * Returns the list of all male ancestors to the passed in root person.
         * Only birth relationships are considered
         ***********************************************************************/
        List<Person> getMaleAncestors(DB db, Person root)
        {
            List<Person> ancestors = [root];

            while (true)
            {
                bool found = false;
                foreach (var family in root.Families)
                {
                    var relation = getRelation(db, root, family);  // query the parents relationship of this family from the childTable

                    if (family.Father != null && relation.father == RelationShip.Birth) // we have found the father with birth relationship
                    {
                        ancestors.Add(family.Father);
                        root = family.Father;
                        found = true;
                        break;
                    }
                }
                if (!found) break; // no further father found
            }
            return ancestors;
        }


        private (RelationShip father, RelationShip mother) getRelation(DB db, Person child, Family family)
        {
            var childTableEntry = db.ChildTable.Single(e => e.ChildId == child.PersonId && e.Family == family);
            return (childTableEntry.RelFather, childTableEntry.RelMother);
        }
    }
}







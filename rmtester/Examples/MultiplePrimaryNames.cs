using RMDatabase;

namespace Example
{
    class MultiplePrimaryNames
    {
        public void Execute()
        {
            using (var db = new DB())
            {
                foreach (var person in db.Persons)
                {
                    var primaryNames = person.Names.Where(n => n.IsPrimary).ToList();

                    if (primaryNames.Count == 0)
                    {
                        Console.WriteLine($"RIN: {person.PersonId} has no primary name");
                    }
                    else if (primaryNames.Count > 1)
                    {
                        Console.WriteLine($"RIN: {person.PersonId} has {primaryNames.Count} primary names");
                        foreach (var name in primaryNames)
                        {
                            Console.WriteLine($"  - {name}");
                        }
                    }
                }
            }
        }
    }
}







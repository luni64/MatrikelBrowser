using RMDatabase;
using RMDatabase.Models;

namespace Example
{
    class AddPerson
    {
        public void Execute()
        {
            using (var db = new DB())
            {
                Person p = new()
                {
                    Names = new List<Name>()
                    {
                        new Name()
                        {
                            Surname= "Miller",
                            Given = "Peter",
                            IsPrimary= true,
                        }
                    }
                };

                db.Persons.Add(p);
                db.SaveChanges();
            }
        }
    }
}







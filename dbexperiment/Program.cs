// See https://aka.ms/new-console-template for more information



using dbexperiment.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Data.Common;
using System.Reflection.Metadata;

internal class Program
{
    private static void Main(string[] args)
    {
        using (var db = new CTX())
        {         
            foreach (var person in db.Persons)
            {
                Console.WriteLine(person.Name);
                foreach (var family in db.Families.Where(f => f.FatherID == person.Id || f.MotherID == person.Id))
                {
                    Console.WriteLine($" *{family.Id}");
                }
                Console.WriteLine();                
            }
        };
    }
}

namespace dbexperiment.Models
{

    public partial class Person
    {
        public override string ToString() => Name;
    };

    public partial class Family
    {
        public override string ToString() => $"{Father} / {Mother}";
    };
}

using dbexperiment.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing;
using System.Diagnostics;

internal class Program
{
    private static void Main(string[] args)
    {
        using (var db = new CTX())
        {
            var child = db.Persons.FirstOrDefault(p => p.Name == "Enkel2");

            Trace.WriteLine($"S: {child?.Name ?? "Startperson not found"}");
            while (child != null)
            {
                var father = child.ParentRelations.FirstOrDefault()?.family.Husband;
                Trace.WriteLine($"F: {father?.Name ?? "--"}");
                child = father;
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
        public override string ToString() => $"{Husband} / {Wife}";
    };
}

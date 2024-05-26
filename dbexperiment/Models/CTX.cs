#nullable disable

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text;

namespace dbexperiment.Models;

public partial class Family
{
    public long Id { get; set; }
    public long? FatherID { get; set; }
    public long? MotherID { get; set; }
    public virtual Person Husband { get; set; }
    public virtual Person Wife { get; set; }

    public virtual ICollection<ChildTableEntry> ChildInfos { get; set; } = [];
}

public partial class Person
{
    public long Id { get; set; }
    public string Name { get; set; }
        
    public  ICollection<Family> ChildRelations => _familiesF.Union(_familiesM).ToList();
    public virtual ICollection<ChildTableEntry> ParentRelations { get; set; }

    public virtual ICollection<Family> _familiesF { get; set; } = [];
    public virtual ICollection<Family> _familiesM { get; set; } = [];
}

public class ChildTableEntry
{
    public long Id { get; set; }
    public long PersonId { get; set; }
    public long FamilyId { get; set; }
    public string data { get; set; } = "";

    public virtual Person person { get; set; }
    public virtual Family family { get; set; }

    public override string ToString()
    {
        return $"Fam:  {family}";
    }
}


public class CTX : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlite("Data Source=C:\\Users\\lutz\\OneDrive\\Desktop\\test.db")
            .UseLazyLoadingProxies()
            .LogTo(Console.WriteLine, LogLevel.Information);
    }

    public DbSet<Family> Families { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<ChildTableEntry> ChildTable { get; set; }





    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Family>(entity =>
        {
            entity.HasKey(f => f.Id);

            entity.HasOne(f => f.Husband).WithMany(e => e._familiesF).HasForeignKey(f => f.FatherID);
            entity.HasOne(d => d.Wife).WithMany(e => e._familiesM).HasForeignKey(f => f.MotherID);
            //entity.HasMany(e => e.ChildInfos).WithOne(e => e.family).HasForeignKey(x => x.FamilyId);
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Ignore(p => p.ChildRelations);
            
        });

        modelBuilder.Entity<ChildTableEntry>(e =>
        {
            e.HasKey(e => e.Id);

            e.HasOne(e => e.family).WithMany(x => x.ChildInfos).HasForeignKey(x => x.FamilyId);
            //e.HasOne(e => e.person).WithMany().HasForeignKey(x => x.PersonId);
            e.HasOne(e=>e.person).WithMany(p=>p.ParentRelations).HasForeignKey(e => e.PersonId);
       


        });
    }


    public IQueryable<Family> GetOwnFamilies(Person p)
    {
        return Families.Where(f => p.Id == f.FatherID || p.Id == f.MotherID).Include(f => f.Husband).Include(f => f.Wife);
    }

    public IQueryable<Person> GetChildren(Person p, bool onlyBirth = true)
    {
        var fams = ChildTable.Where(ct => ct.PersonId == p.Id);

        return null;
    }
}
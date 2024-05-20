#nullable disable

using Microsoft.EntityFrameworkCore;

namespace dbexperiment.Models;

public partial class Family
{
    public long Id { get; set; }
    public long? FatherID { get; set; }
    public long? MotherID { get; set; }
    public virtual Person Father { get; set; }
    public virtual Person Mother { get; set; }
}

public partial class Person
{
    public long Id { get; set; }
    public string Name { get; set; }
    public ICollection<Family> Families => _familiesF.Union(_familiesM).ToList();
    private ICollection<Family> _familiesF { get; set; } = new List<Family>();
    private ICollection<Family> _familiesM { get; set; } = new List<Family>();
}


public class CTX : DbContext
{  
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)=> optionsBuilder.UseSqlite("Data Source=C:\\Users\\lutz\\OneDrive\\Desktop\\test.db");
       
    public DbSet<Family> Families { get; set; }
    public  DbSet<Person> Persons { get; set; }    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Family>(entity =>
        {           
            entity.HasOne(f => f.Father).WithMany("_familiesF").HasForeignKey(f => f.FatherID);
            entity.HasOne(d => d.Mother).WithMany("_familiesM").HasForeignKey(f => f.MotherID);                      
        });

        modelBuilder.Entity<Person>(entity =>
        {   
            entity.Ignore(e => e.Families);            
        });       
    }
}
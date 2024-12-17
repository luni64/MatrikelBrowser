using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;

namespace AEM
{
    //public class PlaceDTO
    //{
    //    public int Id { get; set; }
    //    required public string Name { get; set; }
    //    public string? Location { get; set; }

    //}

    public class CountryDTO
    {
        public int Id { get; set; }
        required public string Name { get; set; }
        public ICollection<Archive> Archives { get; set; } = [];
        public override string ToString() => Name;
    }

    public enum ArchiveType { AEM, MAT }

    public class Archive
    {
        public int Id { get; set; }
        public string REFID { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public required string BookInfoUrl { get; set; }
        public required string ViewerUrl { get; set; }
        public List<ParishDTO> Parishes { get; set; } = [];
        required public CountryDTO Country { get; set; }
        required public ArchiveType ArchiveType { get; set; }

        public override string ToString() => Name;
    }

    public class ParishDTO
    {
        public int Id { get; set; }
        public string RefId { get; set; } = string.Empty;
        public required string Place { get; set; }
        public string Church { get; set; } = string.Empty;


        public string Name { get; set; } = string.Empty;
        required public Archive Archive { get; set; }
        required public string BookBaseUrl { get; set; }
        public List<Book> Books { get; set; } = [];
        public override string ToString() => Name;
    }



    public class MatrikelBrowserCTX : DbContext
    {
        public DbSet<Page> Pages { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<ParishDTO> Parishes { get; set; }
        public DbSet<Archive> Archives{ get; set; }
        public DbSet<CountryDTO> Countries { get; set; }
        //public DbSet<PlaceDTO> Place { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(
                "Data Source= c:/Users/lutz/Documents/MatrikelBrowser.db");//, b=>b.MigrationsAssembly("../MatrikelBrowser")); C:\Users\lutz\Documents
        }
    }   

}
 
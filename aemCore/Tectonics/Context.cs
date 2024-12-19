using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace AEM
{
    public enum ArchiveType { AEM, MAT }

    public class MatrikelBrowserCTX : DbContext
    {
        public DbSet<Page> Pages { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Parish> Parishes { get; set; }
        public DbSet<Archive> Archives { get; set; }
        public DbSet<Country> Countries { get; set; }        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(
                "Data Source= c:/Users/lutz/Documents/MatrikelBrowser.db");//, b=>b.MigrationsAssembly("../MatrikelBrowser")); C:\Users\lutz\Documents
        }
    }

}

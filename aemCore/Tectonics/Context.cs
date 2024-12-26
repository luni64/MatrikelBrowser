using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace MbCore
{
    public enum ArchiveType { AEM, MAT }

    public class MatrikelBrowserCTX : DbContext
    {
        public DbSet<Page> Pages { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Parish> Parishes { get; set; }
        public DbSet<Archive> Archives { get; set; }
        public DbSet<Country> Countries { get; set; }

        public MatrikelBrowserCTX(string? database = null)
        {
            
        }

        public static string DatabaseFile { get; set; } = string.Empty;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DatabaseFile}");
        }
    }

}

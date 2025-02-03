using AEM;
using AEM.Tectonics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace MbCore
{
    public enum ArchiveType { AEM, MAT, UNKNOWN }

    public class MatrikelBrowserCTX : DbContext
    {
        public DbSet<Page> Pages { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Parish> Parishes { get; set; }
        public DbSet<Archive> Archives { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<SettingsEntry> SettingsTable {get; set;}
               

        public static string DatabaseFile { get; set; } = string.Empty;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlite($"Data Source={DatabaseFile}")
                //.UseLazyLoadingProxies()
                ;
        }
    }

}

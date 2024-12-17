using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;

namespace OtherRepoTest
{
    //public class Place
    //{
    //    public int Id { get; set; }
    //    required public string Name { get; set; }
    //    public string? Location { get; set; }

    //}

    //public class Country
    //{
    //    public int Id { get; set; }
    //    required public string Name { get; set; }
    //    public List<Diocese> Dioceses { get; set; } = [];
    //    public override string ToString() => Name;
    //}
    //public class Diocese
    //{
    //    public int Id { get; set; }
    //    public string REFID { get; set; }=string.Empty;
    //    public string Name { get; set; } = string.Empty;
    //    public List<Parish> Parishes { get; set; } = [];
    //    required public Country Country { get; set; }
    //    public override string ToString() => Name;
    //}

    //public class Parish
    //{
    //    public int Id { get; set; }
    //    public string ArchiveID { get; set; } = string.Empty;
    //    public required Place Place { get; set; }
    //    public string Church { get; set; } = string.Empty;

    //    public string Name { get; set; } = string.Empty;
    //    required public Diocese Diocese { get; set; }
    //    required public string BookBaseUrl { get; set; }
    //    public List<Book> Books { get; set; } = [];
    //    public override string ToString() => Name;
    //}

    //public class Book
    //{
    //    public int Id { get; set; }
    //    public string REFID { get; set; }=string.Empty;
    //    public string Name { get; set; } = string.Empty;
    //    public string BookInfoLink { get; set; } = string.Empty;

    //    public List<Page> Pages { get; set; } = [];
    //    required public Parish Parish { get; set; }


    //    public override string ToString() => Name;
    //}

    //public class Page
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; } = string.Empty;
    //    public int Folio { get; set; }
    //    public required Uri link { get; set; }

    //    public override string ToString() => Name;
    //}

    

    //public class MatrikelBrowserCTX : DbContext
    //{
    //    public DbSet<Page> Pages { get; set; }
    //    public DbSet<Book> Books { get; set; }
    //    public DbSet<Parish> Parishes { get; set; }
    //    public DbSet<Diocese> Dioceses { get; set; }
    //    public DbSet<Country> Countries { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    {
    //        optionsBuilder.UseSqlite("Data Source=MatrikelBrowser.db");
    //    }
    //}


}

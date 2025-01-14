using AEM.Tectonics;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MS.WindowsAPICodePack.Internal;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MbCore
{
    //internal class dbInfo : IDatabaseInformation
    //{
    //    public bool IsCompatible { get; set; } = false;
    //    public bool Exists { get; set; } = false;
    //    public int PendingMigrations { get; set; }
    //}

    public class aemCore
    {
        public List<Country> Countries { get; private set; } = [];

        public List<string> Favorites { get; } = [];


        /// <summary>
        /// Occurs when the State of the database has changed.
        /// </summary>
        /// <remarks>
        /// This event is triggered whenever there are changes to the database,
        /// such as updates, deletions, or insertions of countries, archieves, and parishes.        
        /// </remarks>
        public event Action? DatabaseChanged;

        /// <summary>
        /// Sets the database file, initializes the application context and querries the database to update the provided data. 
        /// </summary>
        /// <param name="database">The path to the database file to be set.</param>
        /// <returns>
        ///     <c>true</c> if the database was successfully set and initialized; 
        ///     </returns>
        /// <remarks>      
        /// <list type="bullet">
        ///   <item>Validates the provided database. </item>      
        ///   <item>Applies any pending database migrations.</item>
        ///   <item>Triggers the <c>DatabaseChanged</c> event in case anything changed</item>
        /// </list>
        /// </remarks>
        public bool SetDatabase(string database)
        {
            Trace.TraceInformation($"Trying to set database {database}");
            if (!CheckDatabase(database))
            {
                Trace.TraceWarning("Database missing or not compatible");
                return false;
            }

            MatrikelBrowserCTX.DatabaseFile = database;
            try
            {

                using var ctx = new MatrikelBrowserCTX();

                //ctx.Database.EnsureDeleted();

                int n = ctx.Database.GetPendingMigrations().Count();
                if (n > 0)
                {
                    Trace.TraceInformation($"Apply {n} pending database migrations");
                    ctx.Database.Migrate();
                }

                Countries.Clear();
                Countries.AddRange(
                    ctx.Countries.Where(c => c.Archives.Count > 0)
                    //.Include(c => c.Archives)
                    //.ThenInclude(d => d.Parishes.Where(p => p.Books.Count != 0))
                    .OrderBy(c => c.Name)
                );
                OnDatabaseChanged();
            }
            catch
            {
                Trace.TraceError("Unexpected Error while loading data");
                return false;
            }
            Trace.TraceInformation($"Database {database} set successfully");
            return true;
        }

        public aemCore()
        {
            //baseFolder.Create();
            //FileInfo notesFile = new(Path.Combine(baseFolder.FullName, "test.json"));
            //FileInfo favoritesFile = new(Path.Combine(baseFolder.FullName, "favorites.json"));
            //FileInfo tectonicsFile = new("MatrikelBrowser.db");


            //if (notesFile.Exists)
            //{
            //    //    var json = File.ReadAllText(notesFile.FullName);
            //    //    var infoRecords = JsonConvert.DeserializeObject<List<BookInfo>>(json, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });

            //    //    var allb = ctx.Books.ToDictionary(b => b.Id);
            //    //    var allBooks = Parishes.SelectMany(p => p.Books).ToDictionary(b => b.ID);
            //    //    infoRecords?.ForEach(r => allBooks[r.BookID].Info = r);
            //    //    Trace.WriteLine($"{allBooks.Count} Kirchenbücher in {Parishes.Count()} Pfarreien gefunden. Davon {infoRecords?.Count ?? 0} Matriken mit Notizen oder Fundstellen");
            //}
            //else
            //{
            //    Trace.WriteLine($"File {notesFile} not found!");
            //}

            //if (favoritesFile.Exists)
            //{
            //    var json = File.ReadAllText(favoritesFile.FullName);
            //    Favorites = JsonConvert.DeserializeObject<List<string>>(json) ?? [];
            //}
            //else
            //    Favorites = [];
        }

        public static void saveNotes()
        {


            //var allBookInfos = Parishes
            //    .SelectMany(p => p.Books)
            //    .Select(book => book.Info)
            //    .Where(info => !string.IsNullOrEmpty(info?.note) || info?.Bookmarks.Count > 0)
            //    .ToList();

            //var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto, Formatting = Formatting.Indented };

            //var json = JsonConvert.SerializeObject(Favorites, settings);
            //FileInfo favoritesFile = new(Path.Combine(baseFolder.FullName, "favorites.json"));
            //File.WriteAllText(favoritesFile.FullName, json);

            //json = JsonConvert.SerializeObject(allBookInfos, settings);
            //FileInfo testFile = new(Path.Combine(baseFolder.FullName, "test.json"));
            //File.WriteAllText(testFile.FullName, json);
        }

        public Parish? ImportParish(string parishInfoLink)
        {
            var parishParser = new MatParishParser();
            var parishInfo = parishParser.Parse(new Uri(parishInfoLink));            
                        
            if (parishInfo != null)
            {
                using var ctx = new MatrikelBrowserCTX();

                var country = ctx.Countries.Include(c => c.Archives).FirstOrDefault(c => c.Name == parishInfo.CountryName) ?? new MbCore.Country
                {
                    Name = parishInfo.CountryName
                };

                var archive = country.Archives.FirstOrDefault(a => a.Name == parishInfo.ArchiveName) ?? new Archive
                {
                    Name = parishInfo.ArchiveName,
                    Country = country,
                    ArchiveType = ArchiveType.MAT,
                    ViewerUrl = "",
                    BookInfoUrl = "https://data.matricula-online.eu/{BOOKID}",
                };

                //archive.LoadParishes();

                var parish = ctx.Parishes.Include(p => p.Books).FirstOrDefault(p => p.Name == parishInfo.ParishName) ?? new Parish
                {
                    Archive = archive,
                    Name = parishInfo.ParishName,
                    Place = parishInfo.ParishName,
                    Church = parishInfo.ChurchName,
                    BookBaseUrl = parishInfo.BookBaseUrl,
                };

                var bid = parish.Books.Select(b => b.RefId).ToList();

                foreach (var bookInfo in parishInfo.bookInfos)
                {
                    if (!bid.Contains(bookInfo.BookREFID))
                    {
                        var book = new Book
                        {
                            Parish = parish,
                            RefId = bookInfo.BookREFID,
                            Title = bookInfo.Title,
                            BookType = bookInfo.Type,
                            BookInfoLink = bookInfo.InfoUrl,
                        };

                        book.StartDate = DateOnly.TryParse("1.1." + bookInfo.StartYear, out var sd) ? sd : null;
                        book.EndDate = DateOnly.TryParse("31.12." + bookInfo.EndYear, out var ed) ? ed : null;

                        parish.Books.Add(book);
                    }
                }
                ctx.Update(parish);
                ctx.SaveChanges();

                using var ctx2 = new MatrikelBrowserCTX();

                var c = ctx2.Countries.Where(c => c.Archives.Count > 0).ToList();

                Countries.Clear();
                Countries.AddRange(
                    ctx2.Countries.Where(c => c.Archives.Count > 0)
                    .OrderBy(c => c.Name)
                );
                OnDatabaseChanged();

                return parish;
            }
            return null;
        }
        private static bool CheckDatabase(string database)
        {
            if (!File.Exists(database)) return false;
            using (var connection = new SqliteConnection($"Data Source={database}"))
            {
                connection.Open();
                var command = new SqliteCommand("SELECT name FROM sqlite_master WHERE type='table';", connection);

                // check if the database is a MatrikelBrowser db by requiring a few table names               
                using (var reader = command.ExecuteReader())
                {
                    List<string> expected = ["Countries", "Archives", "Parishes", "Books"];

                    while (reader.Read() && expected.Count > 0)
                    {
                        expected.Remove(reader.GetString(0));
                    }

                    if (expected.Count > 0) return false;
                }
            }
            return true;
        }



        private void OnDatabaseChanged() => DatabaseChanged?.Invoke();

    }
}

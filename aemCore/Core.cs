using AEM.Tectonics;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace MbCore
{
    public class Core
    {
        public Core()
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

        public List<Country> Countries { get; private set; } = [];

        /// <summary>
        /// Occurs when the State of the database has changed.
        /// </summary>
        /// <remarks>
        /// This event is triggered whenever there are changes to the database,
        /// such as updates, deletions, or insertions of foundCountries, archieves, and parishes.        
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

                // grab a list of all supported countries not yet in the database
                if (ctx.Countries.Count() < 2) /// hack
                {
                    var foundCountries = MatParser.ParseCountries(BaseURL + "bestande/");
                    var existingCountries = ctx.Countries.Select(c => c.Name).ToList();
                    foreach (var country in foundCountries.Where(c => !existingCountries.Contains(c.Name)))
                    {
                        ctx.Countries.Add(country);
                    }
                    ctx.SaveChanges();
                }

                Countries.Clear();
                Countries.AddRange(
                    ctx.Countries/*.Where(c => c.Archives.Count > 0).OrderBy(c => c.Name)*/
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

        static public string? GetSetting(string key)
        {
            using var ctx = new MatrikelBrowserCTX();
            var setting = ctx.SettingsTable.FirstOrDefault(s => s.Key == key);
            return setting?.Value;            
        }


        static public void SetSetting(string key, string value)
        {
            using var ctx = new MatrikelBrowserCTX();
            var setting = ctx.SettingsTable.FirstOrDefault(s => s.Key == key);
            if (setting == null)
            {
                setting = new SettingsEntry { Key = key, };
                ctx.SettingsTable.Add(setting);                
            }
            setting.Value = value;
            ctx.SaveChanges();
        }

        //public static void saveNotes()
        //{


        //    //var allBookInfos = Parishes
        //    //    .SelectMany(p => p.Books)
        //    //    .Select(book => book.Info)
        //    //    .Where(info => !string.IsNullOrEmpty(info?.note) || info?.Bookmarks.Count > 0)
        //    //    .ToList();

        //    //var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto, Formatting = Formatting.Indented };

        //    //var json = JsonConvert.SerializeObject(Favorites, settings);
        //    //FileInfo favoritesFile = new(Path.Combine(baseFolder.FullName, "favorites.json"));
        //    //File.WriteAllText(favoritesFile.FullName, json);

        //    //json = JsonConvert.SerializeObject(allBookInfos, settings);
        //    //FileInfo testFile = new(Path.Combine(baseFolder.FullName, "test.json"));
        //    //File.WriteAllText(testFile.FullName, json);
        //}

        private static bool CheckDatabase(string database)
        {
            if (!File.Exists(database)) return false;
            try
            {
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
            catch (SqliteException ex)
            {
                return false;
            }

        }

        private void OnDatabaseChanged() => DatabaseChanged?.Invoke();

        public static string BaseURL = "https://data.matricula-online.eu/de/";
        public static string DefaultCacheFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "lunOptics", "MatrikelBrowser", "cache");
        public static string CacheFolder = GetSetting("CacheFolder") ?? DefaultCacheFolder;
    }
}

using Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OtherRepoTest;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AEM
{
    internal class dbInfo : IDatabaseInformation
    {
        public bool IsCompatible { get; set; } = false;
        public bool Exists { get; set; } = false;
        public int PendingMigrations { get; set; }
    }

    public class aemCore //: ICore
    {
        public List<Country> Countries { get; private set; } = [];

        public List<String> Favorites { get; }

        public IDatabaseInformation CheckDatabase(string database)
        {
            dbInfo info = new();

            if (!File.Exists(database)) return info;
            else info.Exists = true;

            var oldDB = MatrikelBrowserCTX.DatabaseFile;
            MatrikelBrowserCTX.DatabaseFile = database;

            using var ctx = new MatrikelBrowserCTX();

            using (var connection = ctx.Database.GetDbConnection())
            {
                connection.Open();

                // check if the database is a MatrikelBrowser db by requiring a few table names
                var command = connection.CreateCommand();
                command.CommandText = "SELECT name FROM sqlite_master WHERE type='table';";
                using (var reader = command.ExecuteReader())
                {
                    List<string> tableList = ["Countries", "Archives", "Parishes", "Books"];

                    while (reader.Read())
                    {
                        tableList.Remove(reader.GetString(0));
                    }

                    if (tableList.Count > 0) return info;
                    else info.IsCompatible = true;
                }
            }

            info.PendingMigrations = ctx.Database.GetPendingMigrations().Count();

            return info;

            //if (hasMigrations)
            //{

            //    Trace.TraceInformation("Database Migration...");
            //    ctx.Database.Migrate();
            //}
        }

        public bool MigrateDatabase(string database)
        {
            return true;
        }

        public bool SetDatabase(string database)
        {
            MatrikelBrowserCTX.DatabaseFile = database;
            try
            {
                using var ctx = new MatrikelBrowserCTX();
                Countries.Clear();
                Countries.AddRange(
                    ctx.Countries
                    .Include(c => c.Archives)
                    .ThenInclude(d => d.Parishes.Where(p => p.Books.Any()))
                    .OrderBy(c => c.Name)
                );
            }
            catch
            {
                Trace.TraceError("Unexpected Error while loading data");
                return false;
            }
            return true;
        }

        public aemCore()
        {
            //var info = CheckDatabase(database);

            baseFolder.Create();
            FileInfo notesFile = new(Path.Combine(baseFolder.FullName, "test.json"));
            FileInfo favoritesFile = new(Path.Combine(baseFolder.FullName, "favorites.json"));
            FileInfo tectonicsFile = new("MatrikelBrowser.db");





            if (notesFile.Exists)
            {
                //    var json = File.ReadAllText(notesFile.FullName);
                //    var infoRecords = JsonConvert.DeserializeObject<List<BookInfo>>(json, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });

                //    var allb = ctx.Books.ToDictionary(b => b.Id);
                //    var allBooks = Parishes.SelectMany(p => p.Books).ToDictionary(b => b.ID);
                //    infoRecords?.ForEach(r => allBooks[r.BookID].Info = r);
                //    Trace.WriteLine($"{allBooks.Count} Kirchenbücher in {Parishes.Count()} Pfarreien gefunden. Davon {infoRecords?.Count ?? 0} Matriken mit Notizen oder Fundstellen");
            }
            else
            {
                Trace.WriteLine($"File {notesFile} not found!");
            }

            if (favoritesFile.Exists)
            {
                var json = File.ReadAllText(favoritesFile.FullName);
                Favorites = JsonConvert.DeserializeObject<List<string>>(json) ?? [];
            }
            else
                Favorites = [];
        }

        public void saveNotes()
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

        static public bool ImportParish(string parishInfoLink)
        {
            var parishParser = new MatParishParser();
            var parish = parishParser.Parse(new Uri(parishInfoLink));

            if (parish != null)
            {
                using var ctx = new MatrikelBrowserCTX();
                ctx.Update(parish);
                ctx.SaveChanges();

                // parishParser.UpdateDB(ctx);
            }
            return true;
        }


        private readonly DirectoryInfo baseFolder = new(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "lunOptics", "MatrikelBrowser"));
        private List<Country> _countries = [];
    }
}

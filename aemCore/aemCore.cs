using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AEM
{
    public class aemCore //: ICore
    {        
        public IEnumerable<Country> Countries => _countries;
        public List<String> Favorites { get; }

        public aemCore()
        {
            baseFolder.Create();
            FileInfo notesFile = new(Path.Combine(baseFolder.FullName, "test.json"));
            FileInfo favoritesFile = new(Path.Combine(baseFolder.FullName, "favorites.json"));
            
            FileInfo tectonicsFile = new("MatrikelBrowser.db");

            using var ctx = new MatrikelBrowserCTX();

            var hasMigrations = ctx.Database.GetPendingMigrations().Any();

            if (hasMigrations)
            {
                Trace.TraceInformation("Database Migration...");                
                ctx.Database.Migrate();
            }
                        
            var countriesDTO = ctx.Countries.Include(c => c.Archives).ThenInclude(d => d.Parishes).ThenInclude(p => p.Books).ToList();
            foreach (var countryDTO in countriesDTO.OrderBy(c => c.Name))
            {
                var country = new Country(countryDTO.Name, []);
                foreach (var Archive in countryDTO.Archives)
                {                    
                    foreach (var parishDTO in Archive.Parishes)
                    {
                        var parish = new Parish(parishDTO);// parishDTO.RefId, parishDTO.Name, parishDTO.Church, 1900, 2000, []);
                        foreach (var b in parishDTO.Books)
                        {
                            //var book = new Book(b);


                            //if (b.Parish.Diocese.ArchiveType == ArchiveType.AEM)
                            //    book.loadPages = () => book.GetAEMPages();
                            //else
                            //    book.loadPages = () => Trace.WriteLine("MAT");

                            //parish.Books.Add(b);
                        }
                        //diocese.Parishes.Add(parish);
                    }
                    country.Archives.Add(Archive);
                }
                _countries.Add(country);
            }


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

            //   Book.baseFolder = baseFolder;

            ///--------------------
            ///



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

        private readonly DirectoryInfo baseFolder = new(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "lunOptics", "aemBrowser"));
       // private readonly List<Parish> _parishes;
        private readonly List<Country> _countries = [];
    }
}


using Interfaces;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;

namespace AEM
{
    //public class ConcreteConverter<T> : JsonConverter
    //{
    //    public override bool CanConvert(Type objectType) => true;

    //    public override object ReadJson(JsonReader reader,
    //     Type objectType, object existingValue, JsonSerializer serializer)
    //    {
    //        return serializer.Deserialize<T>(reader);
    //    }

    //    public override void WriteJson(JsonWriter writer,
    //        object value, JsonSerializer serializer)
    //    {
    //        serializer.Serialize(writer, value);
    //    }
    //}


    public class aemCore
    {
        //  [JsonConverter(typeof(ConcreteConverter<List<Parish>>))]
        public List<Parish> Parishes;
        public List<String> Favorites;

        public aemCore()
        {
            baseFolder.Create();
            FileInfo notesFile = new(Path.Combine(baseFolder.FullName, "notesout.json"));
            FileInfo favoritesFile = new(Path.Combine(baseFolder.FullName, "favorites.json"));
            FileInfo tectonicsFile = new("tectonics.json");

            if (tectonicsFile.Exists)
            {
                var json = File.ReadAllText(tectonicsFile.FullName);
                Parishes = JsonConvert.DeserializeObject<List<Parish>>(json) ?? new List<Parish>();

                foreach (var parish in Parishes)
                {
                    foreach (var book in parish.Books)
                    {
                        book.Parish = parish;
                    }
                }
            }
            else
            {
                Trace.WriteLine($"File not found: {tectonicsFile}");
                Parishes = new();
            }

            if (notesFile.Exists)
            {
                var json = File.ReadAllText(notesFile.FullName);
                var infoRecords = JsonConvert.DeserializeObject<List<BookInfo>>(json) ?? new List<BookInfo>();

                var allBooks = Parishes.SelectMany(p => p.Books).ToDictionary(b => b.ID);

                foreach (var note in infoRecords)
                {
                    allBooks[note.BookID].Info = note;
                }

            }
            else
            {
                Trace.WriteLine($"File not found: {notesFile}");
                //Notes = new();
            }

            if (favoritesFile.Exists)
            {
                var json = File.ReadAllText(favoritesFile.FullName);
                Favorites = JsonConvert.DeserializeObject<List<string>>(json) ?? new List<string>();
            }
            else
                Favorites = [];


            Book.baseFolder = baseFolder;
        }

        public void saveNotes()
        {
            var allNotes = Parishes
                .SelectMany(p => p.Books)
                .Select(book => book.Info)
                .Where(info => !string.IsNullOrEmpty(info?.note) || info?.Bookmarks.Count > 0)
                .ToList();

            string json = JsonConvert.SerializeObject(allNotes, Formatting.Indented);
            FileInfo notesFile = new(Path.Combine(baseFolder.FullName, "notesout.json"));
            File.WriteAllText(notesFile.FullName, json);

            json = JsonConvert.SerializeObject(Favorites, Formatting.Indented);
            FileInfo favoritesFile = new(Path.Combine(baseFolder.FullName, "favorites.json"));
            File.WriteAllText(favoritesFile.FullName, json);
        }

        private readonly DirectoryInfo baseFolder = new(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "lunOptics", "aemBrowser"));
    }
}

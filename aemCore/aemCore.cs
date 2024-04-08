
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AEM
{

    public class aemCore
    {
        public List<Parish> Parishes;
      //  public List<BookInfo> Notes = new();


        public aemCore()
        {
            FileInfo tectonicsFile = new("tectonics.json");

            baseFolder.Create();
            FileInfo notesFile = new(Path.Combine(baseFolder.FullName, "notesout.json"));


            if (tectonicsFile.Exists)
            {
                var json = File.ReadAllText(tectonicsFile.FullName);
                Parishes = JsonConvert.DeserializeObject<List<Parish>>(json) ?? new List<Parish>();
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
                    allBooks[note.BookID].Note = note;          
                }


            }
            else
            {
                Trace.WriteLine($"File not found: {notesFile}");
                //Notes = new();
            }

            Book.baseFolder = baseFolder.FullName;
            return;
        }

        public void saveNotes()
        {
            var allNotes = Parishes.SelectMany(p => p.Books).Select(b => b.Note).Where(n => !string.IsNullOrEmpty(n?.note)).ToList();
            string json = JsonConvert.SerializeObject(allNotes, Formatting.Indented);

            FileInfo notesFile = new(Path.Combine(baseFolder.FullName, "notesout.json"));
            File.WriteAllText(notesFile.FullName, json);
        }

        private readonly DirectoryInfo baseFolder = new(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "lunOptics", "aemBrowser"));
    }
}

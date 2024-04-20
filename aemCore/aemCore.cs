
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
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
            //string imgFolder = Path.Combine(baseFolder.FullName, "books", "M9972","pages");
            //string imgFile = Path.Combine(imgFolder, "file_9.jpg");

            //if(File.Exists(imgFile) )
            //{
            //    System.Drawing.Image img  = new System.Drawing.Bitmap(imgFile);

            //    Rectangle crop = new Rectangle(444, 1040+50, 1110, 214);

            //    var bmp = new Bitmap(crop.Width, crop.Height);
            //    using (var gr = Graphics.FromImage(bmp))
            //    {
            //        gr.DrawImage(img, new Rectangle(0, 0, bmp.Width, bmp.Height), crop, GraphicsUnit.Pixel);
            //    }

            //    bmp.Save("testimg.jpg");
            //    // Image img = new Image(imgFile);
            //}



            //PdfWriter pdfWriter = new PdfWriter("heelo.pdf");
            //PdfDocument pdfDocument = new PdfDocument(pdfWriter);

            //Document document = new Document(pdfDocument);

            //PdfFont font = PdfFontFactory.CreateFont(StandardFonts.COURIER);

            //document.Add(new Paragraph("iText is:").SetFont(font));

            //List list = new List().SetSymbolIndent(32).SetListSymbol("-").SetFont(font);

            //list
            //    .Add(new ListItem("Never gonna give you up"))
            //    .Add(new ListItem("Never gonna let you down"))
            //    .Add(new ListItem("Never gonna run around and desert you"))
            //    .Add(new ListItem("Never gonna make you cry"))
            //    .Add(new ListItem("Never gonna say goodbye"))
            //    .Add(new ListItem("Never gonna tell a lie and hurt you"));

            //for(int i =0; i<50; i++)
            //{
            //    list.Add(new ListItem(i.ToString()));
            //}

            //document.Add(list);

            //document.Add(new Paragraph("Hello World!"));

            //String imageFile = "testimg.jpg";
            //ImageData data = ImageDataFactory.Create(imageFile);
            //iText.Layout.Element.Image image = new(data);

            //document.Add(image);

            //document.Close();





            FileInfo tectonicsFile = new("tectonics.json");

            baseFolder.Create();
            FileInfo notesFile = new(Path.Combine(baseFolder.FullName, "notesout.json"));


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

            Book.baseFolder =  baseFolder;

            

            //Book bk = Parishes
            //    .First(p => p.Place == "Ruhpolding").Books
            //    .First(b => b.ID == "M9972");

            //if (bk.Pages.Count == 0)
            //    bk.LoadInfo();

            //Report.Generate(bk, new FileInfo("report2.pdf"));

            //bk.Pages.Clear();
            //bk.hasInfo = false;

            return;
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
        }

        private readonly DirectoryInfo baseFolder = new(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "lunOptics", "aemBrowser"));
    }
}

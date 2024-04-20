﻿using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Action;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using Image = iText.Layout.Element.Image;
using Rectangle = System.Drawing.Rectangle;

namespace AEM
{
    public static class Report
    {
        static Style Title = new();
        static Style normal = new();
        static Style heading1 = new();
        static Style heading2 = new();
        static Style link = new();


        public static void Generate(Book book)
        {
            if (book.bookFolder!.Exists)
            {
                PdfFont centaur = PdfFontFactory.CreateFont("Assets/centaur.ttf", PdfFontFactory.EmbeddingStrategy.FORCE_EMBEDDED);


                Title
                    .SetFont(centaur)
                    .SetFontSize(34)
                    .SetBold()
                    .SetTextAlignment(TextAlignment.CENTER);


                normal
                    .SetFont(centaur)
                    .SetFontSize(12);

                heading1
                    .SetFont(centaur)
                    .SetFontSize(18)
                    .SetBold();

                heading2
                    .SetFont(centaur)
                    .SetFontSize(14);

                link
                    .SetFont(centaur)
                    .SetFontSize(12)
                    .SetUnderline()
                    .SetStrokeColor(ColorConstants.BLUE);

                FileInfo outputFile = new FileInfo(book.bookFolder.FullName + "\\report.pdf");
                var pdfDoc = new PdfDocument(new PdfWriter(outputFile));
                var report = new Document(pdfDoc);

                report.SetProperty(Property.LEADING, new Leading(Leading.MULTIPLIED, 1));


                TitlePage(book, report);
                BookNotes(book, report);
                Bookmarks(book, report);

                report.Close();
            }
        }

        static void TitlePage(Book book, Document report)
        {
            if (book.Pages.Count == 0)
                book.LoadInfo();

            var imageFileName = book.Pages[0].loadImage();
            var coverImage = new Image(ImageDataFactory.Create(imageFileName));

            report.Add(new Paragraph(book.Parish.Place)
                .AddStyle(Title)
                .SetMarginBottom(0)
                );

            report.Add(new Paragraph($"{book.Parish.ID} - {book.Parish.Church}")
                .AddStyle(heading1)
                .SetTextAlignment(TextAlignment.CENTER)
                );

            report.Add(new Paragraph()
                .AddStyle(heading1)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetMarginTop(30)
                .Add($"{book.ID}\r{book.Title}"));

            report.Add(new Paragraph()
                .AddStyle(heading2)
                .SetMarginTop(30)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add($"{book.Pages.Count} Blätter, {book.Info.Bookmarks.Count} Fundstellen")
              );

            coverImage
                .SetMarginTop(20)
                .SetMaxWidth(500)
                .SetMaxHeight(600)
                .SetHorizontalAlignment(HorizontalAlignment.CENTER);

            report.Add(coverImage);


            report.Add(new Paragraph()
                .AddTabStops(new TabStop(36))
                .SetMarginTop(150)
                //.SetWidth(450)
                //.SetHorizontalAlignment(HorizontalAlignment.RIGHT)
                //.SetTextAlignment (TextAlignment.RIGHT)
                .AddStyle(normal)
                .SetFontSize(10)
                .Add("Alle Abbildungen von Kirchenbüchern in diesem Dokument stammen vom digitalen " +
                     "Archiv des Erzbistums München und Freising. Das Archiv hat diese Abbildungen unter der Lizenz \"Creative Commons CC BY-NC-SA\" " +
                     "zur nichtkommerziellen Nutzung zur Verfügung gestellt.\n\n")
                .Add("Lizenz:").Add(new Tab())
                .Add(new Link(" https://creativecommons.org/licenses/by-nc-sa/4.0/deed.de ", PdfAction.CreateURI("https://creativecommons.org/licenses/by-nc-sa/4.0/deed.de")))
                .Add("\nNutzung:").Add(new Tab())
                .Add(new Link(" https://www.erzbistum-muenchen.de/archiv-und-bibliothek/organisation/recht/nutzungsbedingungen-digitales-archiv/93561 ", PdfAction.CreateURI("https://www.erzbistum-muenchen.de/archiv-und-bibliothek/organisation/recht/nutzungsbedingungen-digitales-archiv/93561")))
                );

            report.Add(new AreaBreak());
        }
        static void BookNotes(Book book, Document report)
        {
            report.Add(new Paragraph()
                .AddStyle(heading1)
                .Add("Allgemeine Notizen")
                );
            report.Add(new Paragraph()
                .AddStyle(normal)
                .Add(book.Info.note)
                );

        }
        static void Bookmarks(Book book, Document report)
        {
            report.Add(new Paragraph()
                .AddStyle(heading1)
                .Add("Fundstellen")
                );


            foreach (var bookmark in book.Info.Bookmarks.OrderBy(b => b.Sheet).Take(3))
            {
                report.Add(new Paragraph()
                    .AddTabStops(new TabStop(50))
                    .AddStyle(normal)
                    .Add($"Blatt {bookmark.Sheet}:")
                    .Add(new Tab()).Add($"{bookmark.Title}")
                    );

                var sheetImgFile = book.Pages[bookmark.Sheet].loadImage();

                if (File.Exists(sheetImgFile))
                {
                    System.Drawing.Image sheetImg = new Bitmap(sheetImgFile);

                    Rectangle crop = new(bookmark.X, bookmark.Y + 50, (int)bookmark.W, (int)bookmark.H);

                    var bmp = new Bitmap(crop.Width, crop.Height);
                    using (var gr = Graphics.FromImage(bmp))
                    {
                        gr.DrawImage(sheetImg, new Rectangle(0, 0, bmp.Width, bmp.Height), crop, GraphicsUnit.Pixel);
                    }

                    bmp.Save("testimg.jpg");
                    ImageData data = ImageDataFactory.Create("testimg.jpg");
                    Image image = new(data);

                    report.Add(image.SetWidth(400).SetHorizontalAlignment(HorizontalAlignment.CENTER));


                    report.Add(new LineSeparator(new SolidLine()).SetMarginTop(5).SetMarginBottom(15));
                }
            }
        }




    }
}
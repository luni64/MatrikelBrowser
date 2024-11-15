﻿using Interfaces;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Action;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Linq;
using Image = iText.Layout.Element.Image;
using Path = System.IO.Path;
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


        public static FileInfo? Generate(IBook book)
        {
            PdfFont headingFont = PdfFontFactory.CreateFont(StandardFonts.COURIER);
            PdfFont textFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            //PdfFont textFont = PdfFontFactory.CreateFont(StandardFonts.COURIER);

            Title
                .SetFont(headingFont)
                .SetFontSize(34)
                .SetBold()
                .SetTextAlignment(TextAlignment.CENTER);


            normal
                .SetFont(textFont)
                .SetFontSize(12);

            heading1
                .SetFont(headingFont)
                .SetFontSize(18)
                .SetBold();

            heading2
                .SetFont(textFont)
                .SetFontSize(14)
                .SetBold();

            link
                .SetFont(textFont)
                .SetFontSize(10)
                .SetFontColor(ColorConstants.BLUE)
                ;

            var outputFile = new FileInfo(Path.Combine(Path.GetTempPath(), "lunOptics", book.Title + ".pdf").Replace(" ", "_"));
            outputFile.Directory!.Create();

            //FileInfo outputFile = new FileInfo(book.bookFolder.FullName + "\\report.pdf");
            var pdfDoc = new PdfDocument(new PdfWriter(outputFile));
            var report = new Document(pdfDoc);

            report.SetProperty(Property.LEADING, new Leading(Leading.MULTIPLIED, 1));


            TitlePage(book, report);
            BookNotes(book, report);
            Bookmarks(book, report);

            report.Close();
            return outputFile;
        }

        static void TitlePage(IBook book, Document report)
        {
            if (book.Pages.Count == 0)
                book.LoadPageInfo();

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
                .SetMarginTop(80)
                //.SetWidth(450)
                //.SetHorizontalAlignment(HorizontalAlignment.RIGHT)
                //.SetTextAlignment (TextAlignment.RIGHT)
                .AddStyle(normal)
                .SetFontSize(10)
                .Add("Alle Abbildungen von Kirchenbüchern in diesem Dokument stammen vom digitalen " +
                     "Archiv des Erzbistums München und Freising. Das Archiv hat diese Abbildungen unter der Lizenz \"Creative Commons CC BY-NC-SA\" " +
                     "zur nichtkommerziellen Nutzung zur Verfügung gestellt.\n\n")
                .Add("Lizenz:").Add(new Tab())
                .Add(makeLink(" https://creativecommons.org/licenses/by-nc-sa/4.0/deed.de ", "https://creativecommons.org/licenses/by-nc-sa/4.0/deed.de").SetFontSize(10))
                .Add("\nNutzung:").Add(new Tab())
                .Add(makeLink(" https://www.erzbistum-muenchen.de/archiv-und-bibliothek/organisation/recht/nutzungsbedingungen-digitales-archiv/93561 ", "https://www.erzbistum-muenchen.de/archiv-und-bibliothek/organisation/recht/nutzungsbedingungen-digitales-archiv/93561").SetFontSize(10))
                );

            report.Add(new AreaBreak());
        }
        static void BookNotes(IBook book, Document report)
        {
            report.Add(new Paragraph()
                .AddStyle(heading1)
                .Add("Allgemeine Notizen")
                );

            report.Add(makeTabbedText(book.Info.note)
                .AddStyle(normal)
                );

            report.Add(new AreaBreak());

            //report.Add(new Paragraph()
            //    .AddTabStops(new TabStop(100))
            //    .AddStyle(normal)
            //    .Add(book.Info.note)
            //    );

        }
        static void Bookmarks(IBook book, Document report)
        {
            report.Add(new Paragraph()
                .AddStyle(heading1)
                .Add("Fundstellen")
                );


            foreach (var bookmark in book.Info.Bookmarks.OrderBy(b => b.SheetNr))//.Take(3))
            {
                var page = book.Pages[bookmark.SheetNr];

                report.Add(new Paragraph()
                    .AddStyle(heading2)
                    .Add($"{bookmark.Title}, Blatt Nr.: {bookmark.SheetNr}")
                    .SetMarginBottom(20)
                    );

                var sheetImgFile = page.loadImage();
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

                    report.Add(image
                        .SetWidth(400)
                        .SetHorizontalAlignment(HorizontalAlignment.CENTER))
                        .SetBottomMargin(20);
                }

                if (!string.IsNullOrEmpty(bookmark.Transkript))
                {
                    report.Add(new Paragraph()
                        .AddStyle(normal)
                        .Add(bookmark.Transkript)
                        .SetWidth(388)
                        .SetHorizontalAlignment(HorizontalAlignment.CENTER)
                        .SetBorder(new iText.Layout.Borders.SolidBorder(ColorConstants.LIGHT_GRAY, (float)0.3))
                        .SetPadding(5)
                        );
                }

                /////
                //switch (bookmark.bookmarkType)
                //{
                //    case BookmarkType.birth:

                //        report.Add(new Paragraph()
                //           .AddTabStops(new TabStop(65))
                //           .AddStyle(normal)
                //           .Add("Taufdatum:").Add(new Tab()).Add(bookmark.Date1)
                //           .Add("\nKind:").Add(new Tab()).Add(bookmark.Person1)
                //           .Add("\nVater:").Add(new Tab()).Add(bookmark.Father)
                //           .Add("\nMutter:").Add(new Tab()).Add(bookmark.Mother)   
                //           .Add("\nTaufpate:").Add(new Tab()).Add(bookmark.Others)
                //           .SetMarginTop(20)
                //           .SetMarginBottom(20)
                //           );
                //        break;

                //    case BookmarkType.marriage:
                //        report.Add(new Paragraph()
                //           .AddTabStops(new TabStop(10))
                //           .AddTabStops(new TabStop(65))
                //           .AddStyle(normal)
                //           .Add("Datum:").Add(new Tab()).Add(bookmark.EventDate)
                //           .Add("\nBräutigam:").Add(new Tab()).Add(bookmark.Person1).Add("\n")
                //           .Add(new Tab()).Add("Vater:").Add(new Tab()).Add(bookmark.Person3)
                //           .Add("\nMutter:").Add(new Tab()).Add(new Tab()).Add(bookmark.Person4)
                //           .Add("\nBraut:").Add(new Tab()).Add(bookmark.Person2)
                //           .Add("\n  Vater:").Add(new Tab()).Add(bookmark.Person5)
                //           .Add("\n  Mutter:").Add(new Tab()).Add(bookmark.Person6)
                //           .SetMarginTop(20)
                //           .SetMarginBottom(20)
                //           );
                //        break;
                //}

                report.Add(new Paragraph()
                      .SetMarginTop(20)
                      .AddStyle(normal)
                      .SetMarginBottom(30)
                      .Add("Archiv-Bild (Permalink):\n")
                      .Add(makeLink(page.URL, page.URL))
                      .Add("\n\nLokal gespeichertes Bild:\n")
                      .Add(makeLink(page.localFilename, page.localFilename)
                      //.SetFontSize(8)
                      ));

                report.Add(new AreaBreak(areaBreakType: AreaBreakType.NEXT_PAGE));
            }
        }

        static Link makeLink(string txt, string uri)
        {
            Link lnk = new Link(txt, PdfAction.CreateURI(uri));
            lnk.GetLinkAnnotation().SetBorder(new PdfAnnotationBorder(0, 0, 0));
            lnk.AddStyle(link);
            return lnk;
        }

        static Paragraph makeTabbedText(string tabbedText)
        {
            Paragraph p = new Paragraph();
            //p.AddTabStops(new TabStop(50, TabAlignment.LEFT));
            //p.AddTabStops(new TabStop(100, TabAlignment.LEFT));
            //p.AddTabStops(new TabStop(150, TabAlignment.LEFT));
            //p.AddTabStops(new TabStop(200, TabAlignment.LEFT));


            int nrOfTabs = 0;

            // Text aufteilen und in Paragraph einfügen
            var lines = tabbedText.Split("\n");
            foreach (var line in lines)
            {
                var parts = line.Split("\t");
                foreach (var part in parts.Take(parts.Length - 1))
                {
                    p.Add(part);
                    p.Add(new Tab());
                }
                p.Add(parts.Last());                
                nrOfTabs = Math.Max(nrOfTabs, parts.Length - 1);
            }

            int tabDistance = 50;
            for (int i = tabDistance; i <= tabDistance * nrOfTabs; i += tabDistance)
            {
                p.AddTabStops(new TabStop(i));
            }
            return p;
        }




    }
}

using Interfaces;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Action;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.WindowsAPICodePack.Shell;
using System;
using System.Diagnostics.SymbolStore;
using System.Drawing;
using System.IO;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;
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
            PdfFont headingFont = PdfFontFactory.CreateFont(StandardFonts.COURIER_BOLD);
            PdfFont textFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            PdfFont boldTextFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
            //PdfFont textFont = PdfFontFactory.CreateFont(StandardFonts.COURIER);

            Title
                .SetFont(headingFont)
                .SetFontSize(34)
                .SetTextAlignment(TextAlignment.CENTER);

            normal
                .SetFont(textFont)
                .SetFontSize(12);

            heading1
                .SetFont(headingFont)
                .SetFontSize(18);

            heading2
                .SetFont(boldTextFont)
                .SetFontSize(14);

            link
                .SetFont(textFont)
                .SetFontSize(10)
                .SetFontColor(ColorConstants.BLUE)
                ;

            var outputFile = makeCleanFile(book);

            outputFile.Directory!.Create();


            PdfDocument pdfDoc;

            try
            {
                pdfDoc = new PdfDocument(new PdfWriter(outputFile.FullName));
            }
            catch (System.IO.IOException)
            {
                return null;
            }

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

            //report.Add(new AreaBreak());
        }
        static void BookNotes(IBook book, Document report)
        {
            if (string.IsNullOrEmpty(book.Info.note)) return;

            report.Add(new AreaBreak());

            report.Add(new Paragraph()
                .AddStyle(heading1)
                .Add("Allgemeine Notizen")
                );

            report.Add(makeTabbedText(book.Info.note)
                .AddStyle(normal)
                );

            // report.Add(new AreaBreak());

            //report.Add(new Paragraph()
            //    .AddTabStops(new TabStop(100))
            //    .AddStyle(normal)
            //    .Add(book.Info.note)
            //    );

        }
        static void Bookmarks(IBook book, Document report)
        {
            if (book.Info.Bookmarks.Count == 0) return;


            var bookmarks = book.Info.Bookmarks.OrderBy(b => b.SheetNr);

            foreach (var bookmark in bookmarks)//.Take(3))
            {
                report.Add(new AreaBreak());

                if (bookmark == bookmarks.First())
                {
                    report.Add(new Paragraph()
                        .AddStyle(heading1)
                        .Add("Fundstellen")
                        );
                }

                var page = book.Pages[bookmark.SheetNr - 1];  // SheetNr starts at 1

                report.Add(new Paragraph()
                    .AddStyle(heading2)
                    .Add($"Blatt {bookmark.SheetNr}: {bookmark.Title}")
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

                    var converter = new ImageConverter();
                    var bytes = (byte[]?)converter.ConvertTo(bmp, typeof(byte[]));

                    report.Add(new Image(ImageDataFactory.Create(bytes))
                        .SetWidth(400)
                        .SetHorizontalAlignment(HorizontalAlignment.CENTER))
                        .SetBottomMargin(20);
                }

                if (!string.IsNullOrEmpty(bookmark.Transcript))
                {
                    report.Add(new Paragraph()
                        .AddStyle(normal)
                        .Add(bookmark.Transcript)
                        .SetWidth(388)
                        .SetHorizontalAlignment(HorizontalAlignment.CENTER)
                        .SetBorder(new iText.Layout.Borders.SolidBorder(ColorConstants.LIGHT_GRAY, (float)0.3))
                        .SetPadding(5)
                        );
                }

                if (bookmark.Details is BirthDetails birthDetail)
                {
                    report.Add(new Paragraph()
                       .AddTabStops(new TabStop(65))
                       .AddStyle(normal)
                       .Add("Taufdatum:").Add(new Tab()).Add(birthDetail.Child.BirthDate)
                       .Add("\nKind:").Add(new Tab()).Add(birthDetail.Child.Name)
                       .Add("\nVater:").Add(new Tab()).Add(birthDetail.Father.Name)
                       .Add("\nMutter:").Add(new Tab()).Add(birthDetail.Mother.Name)
                       .Add("\nTaufpate:").Add(new Tab()).Add(birthDetail.GodParent.Name)
                       .SetMarginTop(20)
                       .SetMarginBottom(20)
                       );
                }
                else if (bookmark.Details is MarriageDetails md)
                {
                    report.Add(new Paragraph()
                       .AddStyle(normal)
                       .AddTabStops(new TabStop(10))
                       .AddTabStops(new TabStop(65))
                       .Add("Datum:").Add(new Tab()).Add(bookmark.EventDate).Add("\n")
                       .Add("\nBräutigam:").Add(new Tab()).Add(md.Groom.Name).Add(String.IsNullOrEmpty(md.Groom.BirthDate) ? " " : $" (*{md.Groom.BirthDate}) ").Add(md.Groom.Occupation).Add("\n")
                       .Add(new Tab()).Add("Vater:").Add(new Tab()).Add(md.GroomFather.Name).Add(" ").Add(md.GroomFather.Occupation).Add("\n")
                       .Add(new Tab()).Add("Mutter:").Add(new Tab()).Add(md.GroomMother.Name).Add("\n")

                       .Add("\nBraut:").Add(new Tab()).Add(md.Bride.Name).Add(String.IsNullOrEmpty(md.Bride.BirthDate) ? " " : $" (*{md.Bride.BirthDate}) ").Add(md.Bride.Occupation).Add("\n")
                       .Add(new Tab()).Add("Vater:").Add(new Tab()).Add(md.BrideFather.Name).Add(md.BrideFather.Occupation).Add("\n")
                       .Add(new Tab()).Add("Mutter:").Add(new Tab()).Add(md.BrideMother.Name).Add("\n")

                       .Add("\nZeugen:").Add(new Tab()).Add(md.Witnesses.Name)
                       .SetMarginTop(20)
                       .SetMarginBottom(20)
                    );
                }

                else if (bookmark.Details is DeathDetails dd)
                {
                    report.Add(new Paragraph()
                       .AddStyle(normal)
                       .AddTabStops(new TabStop(10))
                       .AddTabStops(new TabStop(65))
                       .Add("Verstorbener:").Add(new Tab()).Add(dd.Deceased.Name).Add("\n")
                       .Add("Wohnort/Stand").Add(new Tab()).Add(dd.Deceased.Occupation).Add("\n")
                       .Add("Todesdatum:").Add(new Tab()).Add(bookmark.EventDate).Add("\n")
                       .Add("Bestattung").Add(new Tab()).Add(dd.FuneralDate).Add("\n")
                       .Add("Vater:").Add(new Tab()).Add(dd.Father.Name).Add("\n")
                       .Add("Mutter:").Add(new Tab()).Add(dd.Mother.Name).Add("\n")
                       .SetMarginTop(20)
                       .SetMarginBottom(20)
                     );
                }

                var metsPageUrl = $"{book.Info.METS_URL}&tx_dlf[page]={bookmark.SheetNr}";

                report.Add(new Paragraph()
                      .SetMarginTop(20)
                      .AddStyle(normal)
                      .SetMarginBottom(30)
                      .Add("DFG Viewer (AEM):\n")
                      .Add(makeLink(metsPageUrl, metsPageUrl))
                      .Add("\n\nArchiv-Bild (Permalink):\n")
                      .Add(makeLink(page.URL, page.URL))
                      .Add("\n\nLokal gespeichertes Bild:\n")
                      .Add(makeLink(page.localFilename, page.localFilename))
                      //.SetFontSize(8)
                      );
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

        static FileInfo makeCleanFile(IBook book)
        {
            return new FileInfo(Path.Combine(KnownFolders.Downloads.Path, book.ToString() + ".pdf")
                .ToLower()
                .Replace(" ", "_")
                .Replace("ä", "ae")
                .Replace("ö", "oe")
                .Replace("ü", "ue")
                );
        }
    }
}

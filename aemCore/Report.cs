using Interfaces;
using iText.IO.Font.Constants;
using iText.IO.Image;
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
            PdfFont centaur = PdfFontFactory.CreateFont("Assets/centaur.ttf", PdfFontFactory.EmbeddingStrategy.FORCE_EMBEDDED);
            PdfFont courier = PdfFontFactory.CreateFont(StandardFonts.COURIER);

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
                .SetFontSize(14)
                .SetBold();

            link
                .SetFont(centaur)
                .SetFontSize(12)
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
            report.Add(new Paragraph()
                .AddStyle(normal)
                .Add(book.Info.note)
                );

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

                switch (bookmark.bookmarkType)
                {
                    case BookmarkType.birth:

                        report.Add(new Paragraph()
                           .AddTabStops(new TabStop(50))
                           .AddStyle(normal)
                           .Add("Kind:").Add(new Tab()).Add(bookmark.Person1)
                           .Add("\nVater:").Add(new Tab()).Add(bookmark.Father)
                           .Add("\nMutter:").Add(new Tab()).Add(bookmark.Mother)
                           .Add("\nTranskript/Notizen:\n")
                           .SetMarginTop(20)
                           .SetMarginBottom(20)
                           );
                        break;
                }

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


            }
        }

        static Link makeLink(string txt, string uri)
        {
            Link lnk = new Link(txt, PdfAction.CreateURI(uri));
            lnk.GetLinkAnnotation().SetBorder(new PdfAnnotationBorder(0, 0, 0));
            lnk.AddStyle(link);
            return lnk;
        }




    }
}

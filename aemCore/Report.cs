using Interfaces;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Action;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.WindowsAPICodePack.Shell;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using Image = iText.Layout.Element.Image;
using Path = System.IO.Path;
using Rectangle = System.Drawing.Rectangle;
using Tab = iText.Layout.Element.Tab;

namespace MbCore
{
    public static class Report
    {
        internal static Style Title = new();
        internal static Style normal = new();
        internal static Style heading1 = new();
        internal static Style heading2 = new();
        internal static Style blueHeading = new();
        internal static Style link = new();

        public static FileInfo? Generate(Book book)
        {
            PdfFont headingFont = PdfFontFactory.CreateFont(StandardFonts.COURIER_BOLD);
            PdfFont textFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            PdfFont boldTextFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);


            Title
                .SetFont(headingFont)
                .SetFontSize(34)
                .SetTextAlignment(TextAlignment.CENTER);

            normal
                .SetFont(textFont)
                .SetFontSize(10).SetFontColor(ColorConstants.DARK_GRAY);


            heading1
                .SetFont(headingFont)
                .SetFontSize(18);

            heading2
                .SetFont(boldTextFont)
                .SetFontSize(14);

            blueHeading
                .SetFont(boldTextFont)
                .SetFontSize(12)
                .SetFontColor(new DeviceRgb(18, 83, 112));

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

        static void TitlePage(Book book, Document report)
        {
            //if (book.Pages.Count == 0)
            //    book.LoadPageInfo();

            //var imageFileName = book.Pages[0].loadImage();
            //var coverImage = new Image(ImageDataFactory.Create(imageFileName));

            //report.Add(new Paragraph(book.Parish.Place)
            //    .AddStyle(Title)
            //    .SetMarginBottom(0)
            //    );

            //report.Add(new Paragraph($"{book.Parish.ID} - {book.Parish.Church}")
            //    .AddStyle(heading1)
            //    .SetTextAlignment(TextAlignment.CENTER)
            //    );

            //report.Add(new Paragraph()
            //    .AddStyle(heading1)
            //    .SetTextAlignment(TextAlignment.CENTER)
            //    .SetMarginTop(30)
            //    .Add($"{book.RefId}\r{book.Title}"));

            //report.Add(new Paragraph()
            //    .AddStyle(heading2)
            //    .SetMarginTop(30)
            //    .SetTextAlignment(TextAlignment.CENTER)
            //    .Add($"{book.Pages.Count} Blätter, XX Fundstellen")
            //  );

            //coverImage
            //    .SetMarginTop(20)
            //    .SetMaxWidth(500)
            //    .SetMaxHeight(600)
            //    .SetHorizontalAlignment(HorizontalAlignment.CENTER);

            //report.Add(coverImage);


            //report.Add(new Paragraph()
            //    .AddTabStops(new TabStop(36))
            //    .SetMarginTop(80)
            //    //.SetWidth(450)
            //    //.SetHorizontalAlignment(HorizontalAlignment.RIGHT)
            //    //.SetTextAlignment (TextAlignment.RIGHT)
            //    .AddStyle(normal)
            //    .SetFontSize(10)
            //    .Add("Alle Abbildungen von Kirchenbüchern in diesem Dokument stammen vom digitalen " +
            //         "Archiv des Erzbistums München und Freising. Das Archiv hat diese Abbildungen unter der Lizenz \"Creative Commons CC BY-NC-SA\" " +
            //         "zur nichtkommerziellen Nutzung zur Verfügung gestellt.\n\n")
            //    .Add("Lizenz:").Add(new Tab())
            //    .Add(makeLink(" https://creativecommons.org/licenses/by-nc-sa/4.0/deed.de ", "https://creativecommons.org/licenses/by-nc-sa/4.0/deed.de").SetFontSize(10))
            //    .Add("\nNutzung:").Add(new Tab())
            //    .Add(makeLink(" https://www.erzbistum-muenchen.de/archiv-und-bibliothek/organisation/recht/nutzungsbedingungen-digitales-archiv/93561 ", "https://www.erzbistum-muenchen.de/archiv-und-bibliothek/organisation/recht/nutzungsbedingungen-digitales-archiv/93561").SetFontSize(10))
            //    );

            ////report.Add(new AreaBreak());
        }
        static void BookNotes(Book book, Document report)
        {
            //if (string.IsNullOrEmpty(book.Info.note)) return;

            //report.Add(new AreaBreak());

            //report.Add(new Paragraph()
            //    .AddStyle(heading1)
            //    .Add("Allgemeine Notizen")
            //    );

            //report.Add(makeTabbedText(book.Info.note)
            //    .AddStyle(normal)
            //    );

            //// report.Add(new AreaBreak());

            ////report.Add(new Paragraph()
            ////    .AddTabStops(new TabStop(100))
            ////    .AddStyle(normal)
            ////    .Add(book.Info.note)
            ////    );

        }
        static void Bookmarks(Book book, Document report)
        {
            //if (book.Info.Bookmarks.Count == 0) return;

            //var bookmarks = book.Info.Bookmarks.OrderBy(b => b.SheetNr);

            //foreach (var bookmark in bookmarks)
            //{
            //    report.Add(new AreaBreak());
            //    if (bookmark == bookmarks.First())
            //    {
            //        report.Add(new Paragraph()
            //            .AddStyle(heading1)
            //            .Add("Fundstellen"));
            //    }

            //    report.Add(new Paragraph().AddStyle(heading2).Add($"Blatt {bookmark.SheetNr}: {bookmark.Title}").SetMarginBottom(20));

            //    Table table = new Table(UnitValue.CreatePercentArray([2.5F, 50])).UseAllAvailableWidth().AddStyle(normal);

            //    var page = book.Pages[bookmark.SheetNr - 1];  // SheetNr starts at 1                        

            //    table.AddCell(new Cell(1, 2).Add(new Paragraph(bookmark.EventDate).SetTextAlignment(TextAlignment.CENTER)).AddStyle(blueHeading));

            //    table.AddBlueHeader("Ausschnitt");
            //    table.AddCell(new Cell(1, 1).Add(getCutout(bookmark, page)?.SetAutoScale(true)));

            //    if (!string.IsNullOrEmpty(bookmark.Transcript))
            //    {
            //        table.AddBlueHeader("Transkript");
            //        table.AddCell(new Paragraph(bookmark.Transcript).SetTextAlignment(TextAlignment.JUSTIFIED).SetMargin(1));
            //    }

            //    if (bookmark.Details is MarriageDetails md)
            //    {
            //        table.AddBlueHeader("Brautpaar", 1);
            //        table.AddCell(
            //           new Table([1, 1])
            //            .AddCell(new Paragraph("Bräutigam:")).AddCell(new Paragraph($"{md.Groom.Name} {(string.IsNullOrEmpty(md.Groom.BirthDate) ? "" : $" ({md.Groom.BirthDate}) ")} {md.Groom.Occupation}"))
            //            .AddCell(new Paragraph("-Vater:")).AddCell($"{md.GroomFather.Name} {md.GroomFather.Occupation}")
            //            .AddCell(new Paragraph("-Mutter:")).AddCell($"{md.GroomMother.Name} {md.GroomMother.Occupation}")
            //            .AddCell(new Paragraph("Braut:").SetMarginTop(5)).AddCell(new Paragraph($"{md.Bride.Name} {(string.IsNullOrEmpty(md.Bride.BirthDate) ? "" : $" ({md.Bride.BirthDate}) ")} {md.Bride.Occupation}").SetMarginTop(5))
            //            .AddCell(new Paragraph("-Vater:")).AddCell($"{md.BrideFather.Name} {md.BrideFather.Occupation}")
            //            .AddCell(new Paragraph("-Mutter:")).AddCell($"{md.BrideMother.Name} {md.BrideMother.Occupation}")
            //            .RemoveBorders()
            //        );
            //        if (!string.IsNullOrEmpty(md.Witnesses.Name))
            //        {
            //            table.AddBlueHeader("Zeugen");
            //            table.AddCell(new Paragraph(md.Witnesses.Name).SetMargin(1));
            //        }
            //    }

            //    if (bookmark.Details is BirthDetails bd)
            //    {
            //        table.AddBlueHeader("Kind", 1);
            //        table.AddCell(
            //           new Table([60, 1])
            //            .AddCell(new Paragraph("Name:")).AddCell($"{bd.Child.Name}")
            //            .AddCell(new Paragraph("Geburtstag:")).AddCell($"{bd.Child.BirthDate}")
            //            .AddCell(new Paragraph("Taufe am:")).AddCell($"{bookmark.EventDate}")
            //            .RemoveBorders()
            //            );

            //        table.AddBlueHeader("Eltern", 1);
            //        table.AddCell(
            //             new Table([60, 1])
            //            .AddCell(new Paragraph("  Vater:")).AddCell($"{bd.Father.Name} {bd.Father.Occupation}")
            //            .AddCell(new Paragraph("  Mutter:")).AddCell($"{bd.Mother.Name} {bd.Mother.Occupation}")                       
            //            .RemoveBorders()
            //        );
            //        if (!string.IsNullOrEmpty(bd.GodParent.Name))
            //        {
            //            table.AddBlueHeader("Pate");
            //            table.AddCell(new Paragraph(bd.GodParent.Name).SetMargin(1));
            //        }
            //    }

            //    if (bookmark.Details is DeathDetails dd)
            //    {
            //        table.AddBlueHeader("Verstorbener", 1);
            //        table.AddCell(
            //           new Table([50, 1])
            //            .AddCell(new Paragraph("Name:")).AddCell($"{dd.Deceased.Name}")
            //            .AddCell(new Paragraph("Todestag:")).AddCell($"{bookmark.EventDate}")
            //            .AddCell(new Paragraph("Begräbnis:")).AddCell($"{dd.FuneralDate}")
            //            .RemoveBorders()
            //            );

            //        table.AddBlueHeader("Eltern", 1);
            //        table.AddCell(
            //             new Table([50, 1])
            //            .AddCell(new Paragraph("  Vater:")).AddCell($"{dd.Father.Name} {dd.Father.Occupation}")
            //            .AddCell(new Paragraph("  Mutter:")).AddCell($"{dd.Mother.Name} {dd.Mother.Occupation}")
            //            .RemoveBorders()
            //        );                  
        }
        #region old


        //var columnWidths = UnitValue.CreatePercentArray([5, 100]);
        //Table TranscriptTable = new Table(columnWidths).SetMarginTop(15)
        //    .AddCell(new Cell(1, 1).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Transkript").AddStyle(blueHeading).SetRotationAngle(Math.PI / 2)))
        //    .AddCell(bookmark.Transcript).SetFontSize(10).SetTextAlignment(TextAlignment.JUSTIFIED)
        //    .RemoveBorders();

        //table.AddCell(new Cell(1, 2).Add(TranscriptTable));


        //if (bookmark.Details is BirthDetails birthDetail)
        //{
        //    report.Add(new Paragraph()
        //       .AddTabStops(new TabStop(65))
        //       .AddStyle(normal)
        //       .Add("Taufdatum:").Add(new Tab()).Add(birthDetail.Child.BirthDate)
        //       .Add("\nKind:").Add(new Tab()).Add(birthDetail.Child.Name)
        //       .Add("\nVater:").Add(new Tab()).Add(birthDetail.Father.Name)
        //       .Add("\nMutter:").Add(new Tab()).Add(birthDetail.Mother.Name)
        //       .Add("\nTaufpate:").Add(new Tab()).Add(birthDetail.GodParent.Name)
        //       .SetMarginTop(20)
        //       .SetMarginBottom(20)
        //       );
        //}
        //else if (bookmark.Details is MarriageDetails md)
        //{

        //    //table.AddCell(new Cell(1, 2)
        //    //    .Add(new Paragraph(bookmark.EventDate).SetTextAlignment(TextAlignment.CENTER).AddStyle(blueHeading))
        //    //    .SetBorder(Border.NO_BORDER)
        //    //    .SetMargin(10));           


        //    Table GroomTable = new Table(columnWidths);
        //    GroomTable.AddCell(new Cell(3, 1).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Bräutigam").AddStyle(blueHeading).SetRotationAngle(Math.PI / 2)));
        //    GroomTable.AddCell(makePersonEntry("Name", md.Groom));
        //    GroomTable.AddCell(makePersonEntry("Vater", md.GroomFather));
        //    GroomTable.AddCell(makePersonEntry("Mutter", md.GroomMother));
        //    GroomTable.RemoveBorders();
        //    table.AddCell(new Cell().Add(GroomTable));

        //    Table BrideTable = new Table(columnWidths);
        //    BrideTable.AddCell(new Cell(3, 1).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Braut").AddStyle(blueHeading).SetRotationAngle(Math.PI / 2)));
        //    //BrideTable.AddCell(new Cell().SetBorder(Border.NO_BORDER).Add(new Paragraph("Braut:").AddStyle(blueHeading)));
        //    BrideTable.AddCell(makePersonEntry("Name", md.Bride));
        //    BrideTable.AddCell(makePersonEntry("Vater", md.BrideFather));
        //    BrideTable.AddCell(makePersonEntry("Mutter", md.BrideMother));
        //    BrideTable.RemoveBorders();
        //    table.AddCell(new Cell().Add(BrideTable));

        //    table.RemoveBorders();




        //    //// Add data rows                    
        //    //table.AddCell(new Cell().Add(new Paragraph("Hochzeitsdatum:")));
        //    //table.AddCell(new Cell().Add(new Paragraph(bookmark.EventDate)));


        //    //table.AddCell(new Cell().Add(new Paragraph("Bräutigam:")));
        //    //table.AddCell(new Cell().Add(new Paragraph(md.Groom.Name)));

        //    //foreach (var c in table.GetChildren().OfType<Cell>())
        //    //{
        //    //    c.SetBorder(Border.NO_BORDER);
        //    //}






        //    //report.Add(new Paragraph()
        //    //   .AddStyle(normal)
        //    //   .AddTabStops(new TabStop(10))
        //    //   .AddTabStops(new TabStop(65))
        //    //   .Add("Datum:").Add(new Tab()).Add(bookmark.EventDate).Add("\n")
        //    //   .Add("\nBräutigam:").Add(new Tab()).Add(md.Groom.Name).Add(String.IsNullOrEmpty(md.Groom.BirthDate) ? " " : $" (*{md.Groom.BirthDate}) ").Add(md.Groom.Occupation).Add("\n")
        //    //   .Add(new Tab()).Add("Vater:").Add(new Tab()).Add(md.GroomFather.Name).Add(" ").Add(md.GroomFather.Occupation).Add("\n")
        //    //   .Add(new Tab()).Add("Mutter:").Add(new Tab()).Add(md.GroomMother.Name).Add("\n")

        //    //   .Add("\nBraut:").Add(new Tab()).Add(md.Bride.Name).Add(String.IsNullOrEmpty(md.Bride.BirthDate) ? " " : $" (*{md.Bride.BirthDate}) ").Add(md.Bride.Occupation).Add("\n")
        //    //   .Add(new Tab()).Add("Vater:").Add(new Tab()).Add(md.BrideFather.Name).Add(" ").Add(md.BrideFather.Occupation).Add("\n")
        //    //   .Add(new Tab()).Add("Mutter:").Add(new Tab()).Add(md.BrideMother.Name).Add("\n")

        //    //   .Add("\nZeugen:").Add(new Tab()).Add(md.Witnesses.Name)
        //    //   .SetMarginTop(20)
        //    //   .SetMarginBottom(20)
        //    //);
        //}

        //else if (bookmark.Details is DeathDetails dd)
        //{
        //    report.Add(new Paragraph()
        //       .AddStyle(normal)
        //       .AddTabStops(new TabStop(10))
        //       .AddTabStops(new TabStop(65))
        //       .Add("Verstorbener:").Add(new Tab()).Add(dd.Deceased.Name).Add("\n")
        //       .Add("Wohnort/Stand").Add(new Tab()).Add(dd.Deceased.Occupation).Add("\n")
        //       .Add("Todesdatum:").Add(new Tab()).Add(bookmark.EventDate).Add("\n")
        //       .Add("Bestattung").Add(new Tab()).Add(dd.FuneralDate).Add("\n")
        //       .Add("Vater:").Add(new Tab()).Add(dd.Father.Name).Add("\n")
        //       .Add("Mutter:").Add(new Tab()).Add(dd.Mother.Name).Add("\n")
        //       .SetMarginTop(20)
        //       .SetMarginBottom(20)
        //     );
        //}

        #endregion
        //    report.Add(table);

        //    var metsPageUrl = $"{book.Info.METS_URL}&tx_dlf[page]={bookmark.SheetNr}";

        //    report.Add(new Paragraph()
        //          .SetMarginTop(20)
        //          .AddStyle(normal)
        //          .SetMarginBottom(30)
        //          .Add("DFG Viewer (AEM):\n")
        //          .Add(makeLink(metsPageUrl, metsPageUrl))
        //          .Add("\n\nArchiv-Bild (Permalink):\n")
        //          //.Add(makeLink(page.URL, page.URL))
        //          //.Add("\n\nLokal gespeichertes Bild:\n")
        //          //.Add(makeLink(page.localFilename, page.localFilename))
        //          //.SetFontSize(8)
        //          );
        //}
        // }



        static Image? getCutout(IBookmarkBase bookmark, Page page)
        {
            var sheetImgFile = page.GetOrCreateImage();
            if (!File.Exists(sheetImgFile)) return null;

            System.Drawing.Image sheetImg = new Bitmap(sheetImgFile);
            Rectangle crop = new(bookmark.X, bookmark.Y + 50, (int)bookmark.W, (int)bookmark.H);
            var bmp = new Bitmap(crop.Width, crop.Height);
            using (var gr = Graphics.FromImage(bmp))
            {
                gr.DrawImage(sheetImg, new Rectangle(0, 0, bmp.Width, bmp.Height), crop, GraphicsUnit.Pixel);
            }

            var converter = new ImageConverter();
            var bytes = (byte[]?)converter.ConvertTo(bmp, typeof(byte[]));

            return new Image(ImageDataFactory.Create(bytes));

            //report.Add(new Image(ImageDataFactory.Create(bytes))
            //    .SetWidth(400)
            //    .SetHorizontalAlignment(HorizontalAlignment.CENTER))
            //    .SetBottomMargin(20);

        }
        static Link makeLink(string txt, string uri)
        {
            Link lnk = new Link(txt, PdfAction.CreateURI(uri));
            lnk.GetLinkAnnotation().SetBorder(new PdfAnnotationBorder(0, 0, 0));
            lnk.AddStyle(link);
            return lnk;
        }
        static Cell makePersonEntry(string title, PersonOld p)
        {
            var table = new Table(UnitValue.CreatePointArray(new float[] { 25, 350 })).SetBorder(Border.NO_BORDER).SetFixedLayout().UseAllAvailableWidth()
                .AddCell(new Cell(2, 1).SetBorder(Border.NO_BORDER).SetFontSize(7).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph(title)))
                .AddCell(new Cell().SetBorder(Border.NO_BORDER).SetFontSize(10).SetPaddingBottom(1).Add(new Paragraph(p.Name)));

            if (!String.IsNullOrEmpty(p.Occupation))
                table.AddCell(new Cell().SetBorder(Border.NO_BORDER).SetFontSize(10).SetPaddingTop(1).Add(new Paragraph(p.Occupation)));

            return new Cell().SetBorder(Border.NO_BORDER).Add(table);


        }
        static Paragraph makeTabbedText(string tabbedText)
        {
            Paragraph p = new Paragraph();



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
        static FileInfo makeCleanFile(Book book)
        {
            return new FileInfo(Path.Combine(KnownFolders.Downloads!.Path!, book.ToString() + ".pdf")
                .ToLower()
                .Replace(" ", "_")
                .Replace("ä", "ae")
                .Replace("ö", "oe")
                .Replace("ü", "ue")
                );
        }
    }

    public static class TableExtensions
    {
        public static Table RemoveBorders(this Table table)
        {
            foreach (var cell in table.GetChildren().OfType<Cell>())
            {
                cell.SetBorder(Border.NO_BORDER);
            }
            return table;
        }

        public static Table AddBlueHeader(this Table table, string title, int rows = 1)
        {
            return table.AddCell(new Cell(rows, 1)
                .AddStyle(Report.blueHeading)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                //.SetHorizontalAlignment(HorizontalAlignment.CENTER)
                .Add(new Paragraph(title).SetRotationAngle(Math.PI / 2).SetMargin(5)/*Left(10).SetMarginRight(10)*/));
        }

        public static Link ToLink(this string txt, string uri)
        {
            Link lnk = new Link(txt, PdfAction.CreateURI(uri));
            lnk.GetLinkAnnotation().SetBorder(new PdfAnnotationBorder(0, 0, 0));
            // lnk.AddStyle(link);
            return lnk;
        }


    }
}

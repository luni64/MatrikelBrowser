using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System.Drawing;
using System.IO;
using iText.Layout.Properties;

namespace AEM
{
    internal static class Report
    {
        static internal void Generate(Book book, FileInfo outputFile)
        {
            if (outputFile.Directory.Exists)
            {

                var report = new Document(new PdfDocument(new PdfWriter(outputFile)));

                TitlePage(book, report);
                //BookNotes(info, report);
                //Bookmarks(info, report);

                report.Close();

            }
        }

        static void TitlePage(Book book, Document report)
        {

            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.COURIER);

            report.Add(new Paragraph(book.ID)
                .SetFont(font).SetFontSize(24)
                .SetTextAlignment(TextAlignment.CENTER));
            report.Add(new Paragraph(book.Title)
                .SetFontSize(28)
                .SetTextAlignment(TextAlignment.CENTER));

            //List list = new List().SetSymbolIndent(32).SetListSymbol("-").SetFont(font);

            //list
            //    .Add(new ListItem("Never gonna give you up"))
            //    .Add(new ListItem("Never gonna let you down"))
            //    .Add(new ListItem("Never gonna run around and desert you"))
            //    .Add(new ListItem("Never gonna make you cry"))
            //    .Add(new ListItem("Never gonna say goodbye"))
            //    .Add(new ListItem("Never gonna tell a lie and hurt you"));

            //for (int i = 0; i < 50; i++)
            //{
            //    list.Add(new ListItem(i.ToString()));
            //}

            //document.Add(list);

            //document.Add(new Paragraph("Hello World!"));
        }

        static void BookNotes(BookInfo info, Document report)
        {

        }

        static void Bookmarks(BookInfo info, Document report)
        {

        }




    }
}

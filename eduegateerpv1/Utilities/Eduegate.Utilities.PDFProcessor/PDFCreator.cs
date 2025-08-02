using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Utilities.PDFProcessor
{
    public class PDFCreator
    {
        public static void Generate(DataTable dt, string filename)
        {
            iTextSharp.text.Document document = new iTextSharp.text.Document();
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filename, FileMode.Create));
            document.Open();
            iTextSharp.text.Font font5 = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 5);

            PdfPTable table = new PdfPTable(dt.Columns.Count);
            float[] widths = new float[] { 4f, 4f, 4f, 4f };

            table.SetWidths(widths);

            table.WidthPercentage = 100;
            PdfPCell cell = new PdfPCell(new Phrase("Products"));

            cell.Colspan = dt.Columns.Count;

            foreach (DataColumn c in dt.Columns)
            {
                table.AddCell(new Phrase(c.ColumnName, font5));
            }

            foreach (DataRow r in dt.Rows)
            {
                if (dt.Rows.Count > 0)
                {
                    table.AddCell(new Phrase(r[0].ToString(), font5));
                    table.AddCell(new Phrase(r[1].ToString(), font5));
                    table.AddCell(new Phrase(r[2].ToString(), font5));
                    table.AddCell(new Phrase(r[3].ToString(), font5));
                }
            }
            document.Add(table);
            document.Close();

        }

        //itext 7 pdf generation code
        //public static void GeneratePDF(DataTable dt, string filename)
        //{
        //    PdfWriter writer = new PdfWriter(new FileStream(filename, FileMode.Create));
        //    PdfDocument pdf = new PdfDocument(writer);
        //    Document document = new Document(pdf);

        //    PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

        //    // Table
        //    Table table = new Table(dt.Columns.Count, false);
        //    //table.SetWidth(4f);
        //    table.UseAllAvailableWidth();

        //    foreach (DataColumn c in dt.Columns)
        //    {
        //        table.AddCell(new Cell(1, 1)
        //            .SetFont(font)
        //            .SetFontSize(5)
        //            .Add(new Paragraph(c.ColumnName)));
        //    }

        //    if (dt.Rows.Count > 0)
        //    {
        //        foreach (DataRow r in dt.Rows)
        //        {
        //            table.AddCell(new Cell(1, 1)
        //            .SetFont(font)
        //            .SetFontSize(5)
        //            .Add(new Paragraph(r[0].ToString())));

        //            table.AddCell(new Cell(1, 1)
        //            .SetFont(font)
        //            .SetFontSize(5)
        //            .Add(new Paragraph(r[1].ToString())));

        //            table.AddCell(new Cell(1, 1)
        //            .SetFont(font)
        //            .SetFontSize(5)
        //            .Add(new Paragraph(r[2].ToString())));

        //            table.AddCell(new Cell(1, 1)
        //            .SetFont(font)
        //            .SetFontSize(5)
        //            .Add(new Paragraph(r[3].ToString())));
        //        }
        //    }

        //    document.Add(table);
        //    document.Close();
        //}

    }
}

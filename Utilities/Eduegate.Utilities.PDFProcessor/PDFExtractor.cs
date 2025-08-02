using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace Eduegate.Utilities.PDFProcessor
{
    public class PDFExtractor
    {
        public static string Extract(string fileName)
        {
            PdfReader reader = new PdfReader(fileName);

            StringBuilder builder = new StringBuilder();

            for (int x = 1; x <= reader.NumberOfPages; x++)
            {
                var page = reader.GetPageN(x);
                IRenderListener listener = new Helpers.TextRenderer(builder);
                var processor = new PdfContentStreamProcessor(listener);
                var pageDic = reader.GetPageN(x);
                var resourcesDic = pageDic.GetAsDict(PdfName.RESOURCES);
                processor.ProcessContent(ContentByteUtils.GetContentBytesForPage(reader, x), resourcesDic);
            }

            return builder.ToString();
        }

        public static List<string> ExtractImagesFromPDF(string sourcePdf, string outputPath, bool keepItInSubfolder = false)
        {
            var extractedFiles = new List<string>();

            // NOTE:  This will only get the first image it finds per page.
            PdfReader pdf = new PdfReader(sourcePdf);
            RandomAccessFileOrArray raf = new iTextSharp.text.pdf.RandomAccessFileOrArray(sourcePdf);
            if (keepItInSubfolder)
            {
                outputPath = System.IO.Path.Combine(outputPath, System.IO.Path.GetFileNameWithoutExtension(sourcePdf));
            }

            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }

            try
            {
                for (int pageNumber = 1; pageNumber <= pdf.NumberOfPages; pageNumber++)
                {
                    PdfDictionary pg = pdf.GetPageN(pageNumber);

                    // recursively search pages, forms and groups for images.
                    PdfObject obj = FindImageInPDFDictionary(pg);
                    if (obj != null)
                    {

                        int XrefIndex = Convert.ToInt32(((PRIndirectReference)obj).Number.ToString(System.Globalization.CultureInfo.InvariantCulture));
                        PdfObject pdfObj = pdf.GetPdfObject(XrefIndex);
                        PdfStream pdfStrem = (PdfStream)pdfObj;
                        byte[] bytes = PdfReader.GetStreamBytesRaw((PRStream)pdfStrem);
                        if ((bytes != null))
                        {
                            using (System.IO.MemoryStream memStream = new System.IO.MemoryStream(bytes))
                            {
                                memStream.Position = 0;
                                System.Drawing.Image img = System.Drawing.Image.FromStream(memStream);
                                // must save the file while stream is open. 
                                string path = System.IO.Path.Combine(outputPath, String.Format(@"{0}.jpg", pageNumber));
                                System.Drawing.Imaging.EncoderParameters parms = new System.Drawing.Imaging.EncoderParameters(1);
                                parms.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Compression, 0);
                                System.Drawing.Imaging.ImageCodecInfo jpegEncoder = GetImageEncoder("JPEG");
                                img.Save(path, jpegEncoder, parms);
                                extractedFiles.Add(path);
                            }
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                pdf.Close();
                raf.Close();
            }

            return extractedFiles;
        }

        public static System.Drawing.Imaging.ImageCodecInfo GetImageEncoder(string imageType)
        {
            imageType = imageType.ToUpperInvariant();
            foreach (ImageCodecInfo info in ImageCodecInfo.GetImageEncoders())
            {
                if (info.FormatDescription == imageType)
                {
                    return info;
                }
            }
            return null;
        }

        private static PdfObject FindImageInPDFDictionary(PdfDictionary pg)
        {
            PdfDictionary res =
                (PdfDictionary)PdfReader.GetPdfObject(pg.Get(PdfName.RESOURCES));


            PdfDictionary xobj =
              (PdfDictionary)PdfReader.GetPdfObject(res.Get(PdfName.XOBJECT));
            if (xobj != null)
            {
                foreach (PdfName name in xobj.Keys)
                {

                    PdfObject obj = xobj.Get(name);
                    if (obj.IsIndirect())
                    {
                        PdfDictionary tg = (PdfDictionary)PdfReader.GetPdfObject(obj);

                        PdfName type =
                          (PdfName)PdfReader.GetPdfObject(tg.Get(PdfName.SUBTYPE));

                        //image at the root of the pdf
                        if (PdfName.IMAGE.Equals(type))
                        {
                            return obj;
                        }// image inside a form
                        else if (PdfName.FORM.Equals(type))
                        {
                            return FindImageInPDFDictionary(tg);
                        } //image inside a group
                        else if (PdfName.GROUP.Equals(type))
                        {
                            return FindImageInPDFDictionary(tg);
                        }

                    }
                }
            }

            return null;

        }

    }
}

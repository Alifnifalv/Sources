using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Eduegate.Utilities.ImageToTextProcessor;
using Eduegate.Utilities.PDFProcessor;

namespace PDFAndTextProcessor.Test
{
    [TestClass]
    public class ImagePDFProcessorTest
    {
        [TestMethod]
        public void Can_Covnert_PDF_TO_Text()
        {
            var data = PDFExtractor.Extract(@"C:\Rafeeq\Eduegate\EduegateV2\UnitTests\PDFAndTextProcessor.Test\Files\SSUP-STR-BL17091814460.pdf");
            Assert.IsNotNull(data);
        }

        [TestMethod]
        public void Can_Extract_Image_From_PDF()
        {
            var files = PDFExtractor.ExtractImagesFromPDF(@"C:\Rafeeq\Eduegate\EduegateV2\UnitTests\PDFAndTextProcessor.Test\Files\SSUP-STR-BL17091814460.pdf",
                @"C:\Rafeeq\Eduegate\EduegateV2\UnitTests\PDFAndTextProcessor.Test\Files\", true);
            Assert.IsNotNull(files.Count > 0);

            foreach(var file in files)
            {
                var data = ImageToTextHelper.Extract(file);
                System.IO.File.WriteAllText(Path.Combine(Path.GetDirectoryName(file), Path.GetFileNameWithoutExtension(file) + ".txt"), data);
                Assert.IsNotNull(data);
            }
        }

        [TestMethod]
        public void Can_Covnert_Image_TO_Text()
        {
            var data = ImageToTextHelper.Extract(@"C:\Rafeeq\Eduegate\EduegateV2\UnitTests\PDFAndTextProcessor.Test\Files\bill.jpg");
            System.IO.File.WriteAllText(@"C:\Rafeeq\Eduegate\EduegateV2\UnitTests\PDFAndTextProcessor.Test\Files\bill.txt", data);
            Assert.IsNotNull(data);
        }
    }
}

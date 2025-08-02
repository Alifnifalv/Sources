using System.Drawing;
using System.IO;
using Tesseract;

namespace Eduegate.Utilities.ImageToTextProcessor
{
    public class ImageToTextHelper
    {
        public static string Extract(string file)
        {
            string fileExtension = Path.GetExtension(file);

            //get file name without extenstion 
            string fileName =
              file.Replace(fileExtension, string.Empty);

            //Check for JPG File Format 
            if (fileExtension == ".jpg" || fileExtension == ".JPG")
            // or // ImageFormat.Jpeg.ToString()
            {
                var ocrtext = string.Empty;
                using (var engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default))
                {
                    Bitmap imgsource = new Bitmap(file);
                    using (var img = PixConverter.ToPix(imgsource))
                    {
                        using (var page = engine.Process(img))
                        {
                            ocrtext = page.GetText();
                        }
                    }
                }

                return ocrtext;
            }

            return string.Empty;
        }
    }
}

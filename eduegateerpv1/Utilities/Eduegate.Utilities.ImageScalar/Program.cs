using Eduegate.Framework.Helper.Enums;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Utilities.ImageScalar
{
    class Program
    {
        static void Main(string[] args)
        {

            if (args.Count() > 0)
            {
                switch (args[0])
                {
                    case "-?":
                    case "/?":
                        Usage();
                        break;
                    default:
                        ResizeImage(args);
                        break;
                }
            }
            else
                Usage();
        }

        private static void ResizeImage(string[] args)
        {
            ImageResize imageResize = new ImageResize();
            var imageList = new List<Contracts.ImageAttributes>();
            var image = new Contracts.ImageAttributes();
            image.SourcePath = args[0];
            image.SourceImageName = args[1];
            image.DestinationImagePath = args[2];
            image.DestinationImageName = args[3];
            image.DestinationImageType = ImageType.ThumbnailImage;
            imageList.Add(image);
            imageList = imageResize.Resize(imageList);

            foreach (Contracts.ImageAttributes imageFile in imageList)
            {
                if (imageFile.IsResized)
                    Console.WriteLine(string.Format("{0} is resized and copied to: {1}",
                        Path.Combine(image.SourcePath, imageFile.SourceImageName), Path.Combine(imageFile.DestinationImagePath, imageFile.DestinationImageName)));
                else
                    Console.WriteLine(string.Format("{0} could not be resized.",
                        Path.Combine(image.SourcePath, imageFile.SourceImageName)));
            }
        }

        private static void Usage()
        {
            System.Console.WriteLine("**************************");
            System.Console.WriteLine("          -Usage-           ");
            System.Console.WriteLine("Example as shown below:");
            System.Console.WriteLine(Environment.NewLine);
            System.Console.WriteLine(string.Format("Eduegate.Tools.ImageScalar.exe {0} {1} {2} {3}", "\"C:\\Image\"", "\"productimage.jpeg\"", "\"C:\\Resize\"", "\"ProductThumbnail.jpeg\""));
            System.Console.WriteLine("**************************");
            System.Console.ReadLine();
        }
    }
}

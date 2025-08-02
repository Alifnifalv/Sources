using Eduegate.Logger;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace Eduegate.Utilities.ImageScalar
{
    public enum ImageType
    {
        ThumbnailImage = 1,
        SmallImage = 2,
        LargeImage = 3,
        ListingImage = 8,
    }

    enum Dimensions
    {
        Width,
        Height
    }

    enum AnchorPosition
    {
        Top,
        Center,
        Bottom,
        Left,
        Right
    }

    public class ImageResize
    {
        #region Commented code
        //public void Resize()
        //{
        //    //set a working directory
        //    string WorkingDirectory = @"C:\Eshwar\Images\ProductImages\Large";

        //    //create a image object containing a verticel photograph
        //    Image imgPhotoVert = Image.FromFile(WorkingDirectory + @"\Penguins.jpg");
        //    Image imgPhotoHoriz = Image.FromFile(WorkingDirectory + @"\Koala.jpg");
        //    Image imgPhoto = null;

        //    imgPhoto = ScaleByPercent(imgPhotoVert, 50);
        //    imgPhoto.Save(WorkingDirectory + @"\images\imageresize_1.jpg", ImageFormat.Jpeg);
        //    imgPhoto.Dispose();

        //    imgPhoto = ConstrainProportions(imgPhotoVert, 200, Dimensions.Width);
        //    imgPhoto.Save(WorkingDirectory + @"\images\imageresize_2.jpg", ImageFormat.Jpeg);
        //    imgPhoto.Dispose();

        //    imgPhoto = FixedSize(imgPhotoVert, 200, 200);
        //    imgPhoto.Save(WorkingDirectory + @"\images\imageresize_3.jpg", ImageFormat.Jpeg);
        //    imgPhoto.Dispose();

        //    imgPhoto = Crop(imgPhotoVert, 200, 200, AnchorPosition.Center);
        //    imgPhoto.Save(WorkingDirectory + @"\images\imageresize_4.jpg", ImageFormat.Jpeg);
        //    imgPhoto.Dispose();

        //    imgPhoto = Crop(imgPhotoHoriz, 200, 200, AnchorPosition.Center);
        //    imgPhoto.Save(WorkingDirectory + @"\images\imageresize_5.jpg", ImageFormat.Jpeg);
        //    imgPhoto.Dispose();
        //}
        #endregion
        IDictionary<ImageType, string> defaultSetting = new Dictionary<ImageType, string>()
        {
            { ImageType.LargeImage, "1308,859,80" },
            { ImageType.ListingImage, "800,800,75" },
            { ImageType.SmallImage, "300,197,50" },
            { ImageType.ThumbnailImage, "70,46,50" },
        };

        private void ProcessImage(Contracts.ImageAttributes imageAttribute, int width, int height, int quality = 100)
        {
            using (var image = Image.Load(Path.Combine(imageAttribute.SourcePath, imageAttribute.SourceImageName)))
            {
                image.Mutate(i => i
                 .AutoOrient()
                .Resize(new ResizeOptions
                {
                    Size = new SixLabors.ImageSharp.Size(width, height),
                    Mode = ResizeMode.Max,
                }));

                if (!Directory.Exists(imageAttribute.DestinationImagePath))
                    Directory.CreateDirectory(imageAttribute.DestinationImagePath);

                string fileName = Path.Combine(imageAttribute.DestinationImagePath, imageAttribute.DestinationImageName);
                var jpegEncoder = new JpegEncoder { Quality = quality };
                image.SaveAsJpeg(fileName, jpegEncoder);
            }
        }

        public List<Contracts.ImageAttributes> Resize(List<Contracts.ImageAttributes> imagesToResize)
        {
            var imageSizeSetting = new Dictionary<ImageType, string>();

            foreach (var imageType in Enum.GetNames(typeof(ImageType)))
            {
                if (ConfigurationManager.AppSettings[imageType] != null)
                {
                    imageSizeSetting.Add((ImageType)Enum.Parse(typeof(ImageType), imageType),
                        ConfigurationManager.AppSettings[imageType]);
                }
                else
                if (Environment.GetEnvironmentVariable(imageType) != null)
                {
                    imageSizeSetting.Add((ImageType)Enum.Parse(typeof(ImageType), imageType),
                        Environment.GetEnvironmentVariable(imageType));
                }
                else
                {
                    imageSizeSetting.Add((ImageType)Enum.Parse(typeof(ImageType), imageType),
                       defaultSetting[(ImageType)Enum.Parse(typeof(ImageType), imageType)]);
                }
            }

            var imagesResized = new List<Contracts.ImageAttributes>();

            foreach (Contracts.ImageAttributes imageAttribute in imagesToResize)
            {
                using (FileStream stream = new FileStream(Path.Combine(imageAttribute.SourcePath, imageAttribute.SourceImageName), FileMode.Open, FileAccess.Read))
                {
                    if (imageSizeSetting[imageAttribute.DestinationImageType] == null) continue;

                    var size = imageSizeSetting[imageAttribute.DestinationImageType].Split(',');

                    if (size.Length < 2) continue;

                    int quality = size.Length == 3 && !string.IsNullOrEmpty(size[2]) ? int.Parse(size[2]) : 100;

                    try
                    {
                        ProcessImage(imageAttribute, int.Parse(size[0]), int.Parse(size[1]), quality);
                        imageAttribute.IsSuccess = true;
                    }
                    catch (Exception ex)
                    {
                        LogHelper<ImageResize>.Fatal(ex.Message, ex);
                        imageAttribute.IsSuccess = false;
                    }

                    imagesResized.Add(imageAttribute);
                }
            }
            return imagesResized;
        }
    }
}

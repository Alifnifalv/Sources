using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Payment;
using Eduegate.Hub.Client;
using Eduegate.PublicAPI.Common;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.Eduegates;
using Eduegate.Services.Contracts.PaymentGateway;
using Eduegate.Services.Contracts.Payments;
using Eduegate.Services.Payment;
using Hangfire;
using IronBarCode;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;

namespace Eduegate.Public.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BarcodeController : ApiControllerBase
    {
        private readonly ILogger<BarcodeController> _logger;
        private readonly dbEduegateSchoolContext _dbContext;
        private readonly IBackgroundJobClient _backgroundJobs;
        private readonly RealtimeClient _realtimeClient;
        private readonly IHttpContextAccessor _accessor;

        public BarcodeController(ILogger<BarcodeController> logger, IHttpContextAccessor context,
            dbEduegateSchoolContext dbContext, IBackgroundJobClient backgroundJobs,
            IServiceProvider serviceProvider, RealtimeClient realtimeClient,
            IHttpContextAccessor accessor) : base(context)
        {
            _logger = logger;
            _dbContext = dbContext;
            _backgroundJobs = backgroundJobs;
            _realtimeClient = realtimeClient;
            _accessor = accessor;
        }


        //IN RDL - ="https://api.pearlschool.org/api/Barcode/GenerateBarcode?code=" & First(Fields!AdmissionNumber.Value, "StudentData")
        [HttpGet]
        [Route("GenerateBarcode")]
        public IActionResult GenerateBarcode(string code)
        {
            var barcode = BarcodeWriter.CreateBarcode(code, BarcodeEncoding.Code128);
            barcode.ResizeTo(300, 150).SetMargins(5 * 5);

            string tempFilePath = string.Format("{0}\\{1}\\{2}\\{3}" + "_" + "Barcode_" + code,
            new Domain.Setting.SettingBL().GetSettingValue<string>("DocumentsPhysicalPath").ToString(), "Files", "Temp", Guid.NewGuid());

            //string tempFilePath = Path.GetTempFileName();
            string tempFileWithExtension = Path.ChangeExtension(tempFilePath, ".jpg");

            barcode.SaveAsJpeg(tempFileWithExtension);

            var memoryStream = new MemoryStream();
            using (var fileStream = new FileStream(tempFileWithExtension, FileMode.Open, FileAccess.Read))
            {
                fileStream.CopyTo(memoryStream);
            }
            memoryStream.Position = 0;  

            System.IO.File.Delete(tempFileWithExtension);

            return File(memoryStream, "image/jpeg", "barcode.jpg");
        }

        //[HttpGet]
        //[Route("GenerateBarcode")]
        //public IActionResult GenerateBarcode(string code)
        //{
        //    var barcode = BarcodeWriter.CreateBarcode(code, BarcodeEncoding.Code128);
        //    barcode.ResizeTo(300, 150).SetMargins(50 * 50);

        //    // Generate the file path
        //    string documentsPhysicalPath = new Domain.Setting.SettingBL().GetSettingValue<string>("DocumentsPhysicalPath").ToString();
        //    string directoryPath = Path.Combine(documentsPhysicalPath, "Files", "Temp", Guid.NewGuid().ToString());
        //    if (!Directory.Exists(directoryPath))
        //    {
        //        Directory.CreateDirectory(directoryPath);
        //    }

        //    string fileName = "Barcode_" + code + ".jpg";
        //    string filePath = Path.Combine(directoryPath, fileName);

        //    // Save the barcode to the specified file path
        //    barcode.SaveAsJpeg(filePath);

        //    // Return the file path as a JSON response
        //    return Ok(new { FilePath = filePath });
        //}


        //IN RDL - ="https://api.pearlschool.org/api/Barcode/GenerateQRCode?code=" & First(Fields!AdmissionNumber.Value, "StudentData")
        [HttpGet]
        [Route("GenerateQRCode")]
        public IActionResult GenerateQRCode(string code)
        {
            // Generate the QR code
            var qrCode = QRCodeWriter.CreateQrCode(code, 300); // Create QR code with a size of 300x300 pixels
            qrCode.ResizeTo(300, 300).SetMargins(50);

            // Generate a temporary file path
            //string tempFilePath = Path.GetTempFileName();

            string tempFilePath = string.Format("{0}\\{1}\\{2}\\{3}" + "_" + "QR_" + code,
            new Domain.Setting.SettingBL().GetSettingValue<string>("DocumentsPhysicalPath").ToString(), "Files", "Temp", Guid.NewGuid());

            string tempFileWithExtension = Path.ChangeExtension(tempFilePath, ".jpg");

            // Save the QR code to the temporary file
            qrCode.SaveAsJpeg(tempFileWithExtension);

            // Read the file into a memory stream
            var memoryStream = new MemoryStream();
            using (var fileStream = new FileStream(tempFileWithExtension, FileMode.Open, FileAccess.Read))
            {
                fileStream.CopyTo(memoryStream);
            }
            memoryStream.Position = 0;  // Reset stream position

            // Delete the temporary file
            System.IO.File.Delete(tempFileWithExtension);

            // Return the image as a file
            return File(memoryStream, "image/jpeg", "qrcode.jpg");
        }


        [HttpGet]
        [Route("ReadContentsByID")]
        public IActionResult ReadContentsByID(long? contentID)
        {
            var imageData = contentID.HasValue ? ClientFactory.ContentServicesClient(CallContext).ReadContentsById(contentID.Value) : null;

            if (imageData == null)
            {
                return File(new byte[0], "image/jpeg", "No file");
            }

            imageData.ContentFileName = Path.ChangeExtension(imageData.ContentFileName, ".jpg");

            string contentType;

            if (imageData.ContentFileName.EndsWith(".jpeg") || imageData.ContentFileName.EndsWith(".jfif") || imageData.ContentFileName.EndsWith(".webp") || imageData.ContentFileName.EndsWith(".jpg"))
            {
                contentType = "image/jpeg";
            }
            else
            {
                contentType = "image/jpeg";
            }

            if (IsCompressed(imageData.ContentData))
            {
                imageData.ContentData = Decompress(imageData.ContentData);
            }

            var memoryStream = new MemoryStream(imageData.ContentData);

            return File(memoryStream, contentType, imageData.ContentFileName);
        }


        private bool IsCompressed(byte[] data)
        {
            return data.Length > 2 && data[0] == 31 && data[1] == 139; // Check if the first two bytes match GZip header
        }

        public static byte[] Decompress(byte[] compressedData)
        {
            using (MemoryStream memoryStream = new MemoryStream(compressedData))
            {
                using (GZipStream decompressionStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                {
                    using (MemoryStream decompressedStream = new MemoryStream())
                    {
                        decompressionStream.CopyTo(decompressedStream);
                        return decompressedStream.ToArray();
                    }
                }
            }
        }
    }
}

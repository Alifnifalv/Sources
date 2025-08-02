using Eduegate.Application.Mvc;
using Eduegate.Services.Client.Factory;
using System.IO;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using System.IO.Compression;

namespace Eduegate.OnlineExam.PortalCore.Controllers
{
    public class ContentController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public void ReadContentsByID(long? contentID)
        {
            var imageData = contentID.HasValue ? ClientFactory.ContentServicesClient(CallContext).ReadContentsById(contentID.Value) : null;

            if (imageData != null)
            {
                if (imageData.ContentFileName.EndsWith(".pdf"))
                {
                    Response.ContentType = "application/pdf";
                }

                else if (imageData.ContentFileName.EndsWith(".xls") || imageData.ContentFileName.EndsWith(".xlsx"))
                {
                    Response.ContentType = "Application/x-msexcel";
                }

                else if (imageData.ContentFileName.EndsWith(".doc") || imageData.ContentFileName.EndsWith(".rtf") || imageData.ContentFileName.EndsWith(".docx"))
                {
                    Response.ContentType = "Application/msword";
                }

                else if (imageData.ContentFileName.EndsWith(".gif"))
                {
                    Response.ContentType = "image/GIF";
                }

                else if (imageData.ContentFileName.EndsWith(".htm") || imageData.ContentFileName.EndsWith(".html"))
                {
                    Response.ContentType = "text/HTML";
                }

                else if (imageData.ContentFileName.EndsWith(".txt"))
                {
                    Response.ContentType = "text/plain";
                }

                else if (imageData.ContentFileName.EndsWith(".jpeg") || imageData.ContentFileName.EndsWith(".jfif") || imageData.ContentFileName.EndsWith(".webp") || imageData.ContentFileName.EndsWith(".jpg"))
                {
                    Response.ContentType = "image/jpeg";
                }

                else
                {
                    Response.ContentType = "image/jpg";
                }
            }
            else
            {
                Response.ContentType = "image/jpg";
            }

            HttpContext.Response.Headers.Add("Content-Disposition", new System.Net.Mime.ContentDisposition("attachment") { FileName = imageData != null ? imageData.ContentFileName : "No file" }.ToString());

            if (imageData != null)
            {
                // Check if the content data is compressed
                if (IsCompressed(imageData.ContentData))
                {
                    // Decompress the content data
                    byte[] decompressedData = Decompress(imageData.ContentData);
                    Response.Body.WriteAsync(decompressedData);
                }
                else
                {
                    // If not compressed, write the data directly
                    Response.Body.WriteAsync(imageData.ContentData);
                }
            }
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

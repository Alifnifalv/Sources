using Eduegate.Application.Mvc;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.Contents;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace Eduegate.Recruitment.Portal.Controllers
{
    public class ContentController : BaseController
    {

        [HttpPost]
        public ActionResult UploadContents()
        {
            bool isSavedSuccessfully = false;
            var imageInfoList = new List<ContentFileDTO>();

            try
            {
                var userID = CallContext.LoginID;

                foreach (var formFile in Request.Form.Files)
                {
                    if (formFile.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            formFile.CopyTo(ms);
                            var fileBytes = ms.ToArray();

                            byte[] compressedBytes = Compress(fileBytes);

                            // act on the Base64 data
                            var fileInfo = new ContentFileDTO();
                            fileInfo.ContentFileName = formFile.FileName;
                            fileInfo.ContentData = compressedBytes;
                            fileInfo.FilePath = "Content/ReadContentsByID";
                            fileInfo.IsCompressed = true;

                            imageInfoList.Add(fileInfo);
                        }
                    }
                }

                imageInfoList = ClientFactory.ContentServicesClient(CallContext).SaveFiles(imageInfoList);
                imageInfoList.All(c => { c.ContentData = null; return true; });
                isSavedSuccessfully = true;
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<ContentController>.Fatal(ex.Message.ToString(), ex);
                return Json(new { Success = isSavedSuccessfully, Message = (ex.Message) });
            }

            return Json(new { Success = isSavedSuccessfully, FileInfo = imageInfoList });
        }

        public static byte[] Compress(byte[] data)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (GZipStream compressionStream = new GZipStream(memoryStream, CompressionMode.Compress))
                {
                    compressionStream.Write(data, 0, data.Length);
                }
                return memoryStream.ToArray();
            }
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
                else if (imageData.ContentFileName.EndsWith(".csv"))
                {
                    Response.ContentType = "text/csv";
                }
                else
                {
                    Response.ContentType = "image/jpg";
                }

                HttpContext.Response.Headers.Append("Content-Disposition", new System.Net.Mime.ContentDisposition("attachment") { FileName = imageData != null ? imageData.ContentFileName : "No file" }.ToString());

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
            else
            {
                Response.ContentType = "image/jpg";
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

        public long DeleteContentsByID(long? contentID)
        {
            if (contentID.HasValue)
            {
                return ClientFactory.ContentServicesClient(CallContext).DeleteEntity(contentID.Value);
            }

            return contentID.Value;
        }
    }
}

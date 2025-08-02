using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.Contents;
using Eduegate.Services.Contracts.Contents.Enums;
using System.IO.Compression;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Eduegate.Application.Mvc;

namespace Eduegate.ERP.Admin.Controllers
{
    public class ContentController : BaseController
    {
        // GET: Content
        public ActionResult Index()
        {
            return View();
        }

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

        public void ReadContents(ContentType contentType, long referenceID)
        {
            var imageData = ClientFactory.ContentServicesClient(CallContext).GetFile(contentType, referenceID);
            Response.ContentType = "image/jpg";
            //Response.BinaryWrite(imageData.ContentData);
            //HttpContext.Response.BodyWriter.WriteAsync(imageData != null ? imageData.ContentData : new byte[1]).GetAwaiter().GetResult();
            Response.Body.WriteAsync(imageData.ContentData);
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

                HttpContext.Response.Headers.Add("Content-Disposition", new System.Net.Mime.ContentDisposition("attachment") { FileName = imageData != null ? imageData.ContentFileName : "No file" }.ToString());

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

        public byte[] ConvertToByte(HttpPostedFileBase file)
        {
            byte[] imageByte = null;
            var rdr = new BinaryReader(file.InputStream);
            imageByte = rdr.ReadBytes((int)file.ContentLength);
            return imageByte;
        }

        public long DeleteContentsByID(long? contentID)
        {
            if (contentID.HasValue)
            {
               return ClientFactory.ContentServicesClient(CallContext).DeleteEntity(contentID.Value);
            }

            return contentID.Value;
        }

        public void ReadContentsByIDWithoutAttachment(long? contentID)
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

            //HttpContext.Response.Headers.Add("Content-Disposition", new System.Net.Mime.ContentDisposition("attachment") { FileName = imageData != null ? imageData.ContentFileName : "No file" }.ToString());
            Response.Body.WriteAsync(imageData != null ? imageData.ContentData : new byte[1]);
        }

        //public static byte[] Compress(byte[] data)
        //{
        //    MemoryStream output = new MemoryStream();
        //    using (DeflateStream dstream = new DeflateStream(output, CompressionLevel.Optimal))
        //    {
        //        dstream.Write(data, 0, data.Length);
        //    }
        //    return output.ToArray();
        //}

        //public static byte[] Decompress(byte[] data)
        //{
        //    MemoryStream input = new MemoryStream(data);
        //    MemoryStream output = new MemoryStream();
        //    using (DeflateStream dstream = new DeflateStream(input, CompressionMode.Decompress))
        //    {
        //        dstream.CopyTo(output);
        //    }
        //    return output.ToArray();
        //}

        private bool IsCompressed(byte[] data)
        {
            return data.Length > 2 && data[0] == 31 && data[1] == 139; // Check if the first two bytes match GZip header
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

        [HttpPost]
        public ActionResult GalleryUploadContents()
        {
            bool isSavedSuccessfully = false;
            var imageInfoList = new List<ContentFileDTO>();

            try
            {
                var userID = CallContext.LoginID;

                foreach (var formFile in Request.Form.Files)
                {
                    if (IsImageFile(formFile.FileName))
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
                    else
                    {
                        return Json(new { Success = isSavedSuccessfully, Message = "This only accepts files in image formats." });
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

        public bool IsImageFile(string filePath)
        {
            string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp" }; // Add more extensions if needed

            string extension = Path.GetExtension(filePath);

            foreach (string ext in imageExtensions)
            {
                if (string.Equals(extension, ext, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
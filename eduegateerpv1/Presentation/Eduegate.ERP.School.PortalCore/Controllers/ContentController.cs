using Eduegate.Application.Mvc;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.Contents;
using Eduegate.Services.Contracts.Contents.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Eduegate.Framework.Helper;
using System.Threading.Tasks;
using System.IO.Compression;

namespace Eduegate.ERP.School.ParentPortal.Controllers
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

        //TODO: Temporary hides and added new code in below
        //public ActionResult UploadImages()
        //{
        //    bool isSavedSuccessfully = false;
        //    var imageInfoList = new List<ContentFileDTO>();

        //    try
        //    {
        //        var userID = CallContext.LoginID;

        //        foreach (string fileName in Request.Files)
        //        {
        //            var file = Request.Files[fileName];
        //            //Save file content goes here
        //            if (file != null && file.ContentLength > 0)
        //            {
        //                var fileInfo = new ContentFileDTO();
        //                fileInfo.ContentFileName = file.FileName;
        //                fileInfo.ContentData = ConvertToByte(file);
        //                imageInfoList.Add(fileInfo);
        //            }
        //        }

        //        imageInfoList = ClientFactory.ContentServicesClient(CallContext).SaveFiles(imageInfoList);
        //        imageInfoList.All(c => { c.ContentData = null; return true; });
        //        isSavedSuccessfully = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Eduegate.Logger.LogHelper<ContentController>.Fatal(ex.Message.ToString(), ex);
        //        return Json(new { Success = isSavedSuccessfully, Message = (ex.Message) });
        //    }

        //    return Json(new { Success = isSavedSuccessfully, FileInfo = imageInfoList });
        //}

        //TODO: Temporary added code to upload images
        [HttpPost]
        public async Task<IActionResult> UploadImages()
        {
            bool isSavedSuccessfully = false;
            List<string> fileNames = new List<string>();
            var imageInfoList = new List<ContentFileDTO>();

            try
            {
                var userID = CallContext.LoginID;
                var imagePhysicalPath = new Domain.Setting.SettingBL(CallContext).GetSettingValue<string>("ImagesPhysicalPath");
                var imageHostUrl = new Domain.Setting.SettingBL(CallContext).GetSettingValue<string>("ImageHostUrl");

                string tempFolderPath = string.Format("{0}/{1}/{2}/{3}",
                    imagePhysicalPath, Request.Form["ImageType"], Constants.TEMPFOLDER, userID);

                foreach (var formFile in Request.Form.Files)
                {
                    if (formFile.Length > 0)
                    {
                        var imageInfo = new ContentFileDTO();
                        var imageExtension = System.IO.Path.GetExtension(formFile.FileName).ToLower();
                        var imageFileName = Guid.NewGuid() + imageExtension;

                        if (!Directory.Exists(tempFolderPath))
                        {
                            Directory.CreateDirectory(tempFolderPath);
                        }

                        using (var inputStream = new FileStream(tempFolderPath + "/" + imageFileName, FileMode.Create))
                        {
                            // read file to stream
                            await formFile.CopyToAsync(inputStream);
                            // stream to byte array
                            byte[] array = new byte[inputStream.Length];
                            inputStream.Seek(0, SeekOrigin.Begin);
                            inputStream.Read(array, 0, array.Length);
                            // get file name
                            string fName = formFile.FileName;
                        }

                        imageInfo.ContentFileName = imageFileName;
                        imageInfo.FilePath = string.Format("{0}/{1}/{2}/{3}/{4}",
                            imageHostUrl, Request.Form["ImageType"], Constants.TEMPFOLDER, userID, imageFileName);
                        imageInfoList.Add(imageInfo);
                    }
                }

                isSavedSuccessfully = true;
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<ContentController>.Fatal(ex.Message.ToString(), ex);
                return Ok(new { Success = isSavedSuccessfully, Message = (ex.Message) });
            }

            return Ok(new { Success = isSavedSuccessfully, FileInfo = imageInfoList });
        }

        public void ReadContents(ContentType contentType, long referenceID)
        {
            var imageData = ClientFactory.ContentServicesClient(CallContext).GetFile(contentType, referenceID);
            Response.ContentType = "image/jpg";
            //Response.BinaryWrite(imageData.ContentData);
            Response.Body.Write(imageData.ContentData);
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

        public string ReadContentsByIDForMobile(long? contentID)
        {
            var path = string.Empty;

            if (contentID.HasValue)
            {
                var imageData = ClientFactory.ContentServicesClient(CallContext).ReadContentsById(contentID.Value);
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

                else if (imageData.ContentFileName.EndsWith(".jpeg") || imageData.ContentFileName.EndsWith(".jfif") || imageData.ContentFileName.EndsWith(".webp"))
                {
                    Response.ContentType = "image/jpeg";
                }

                else
                {
                    Response.ContentType = "image/jpg";
                }

                //Response.Buffer = true;
                //Response.Charset = "";
                //Response.Cache.SetCacheability(HttpCacheability.NoCache);
                //Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(Request.QueryString["reportName"]) + "." + extension);
                //Response.AddHeader("Content-Disposition", new System.Net.Mime.ContentDisposition("attachment") { FileName = imageData.ContentFileName }.ToString());
                //Response.BinaryWrite(imageData.ContentData);
                //Response.Flush();
                //Response.End();
                //Response.Clear();
                string filename = string.Format("{0}\\{1}\\{2}\\{3}" + "_" + imageData.ContentFileName,
                new Domain.Setting.SettingBL().GetSettingValue<string>("DocumentsPhysicalPath").ToString(), "Files", "Temp", Guid.NewGuid());

                if (!Directory.Exists(Path.GetDirectoryName(filename)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(filename));
                }

                using (FileStream fs = new FileStream(filename, FileMode.Append))
                {
                    fs.Write(imageData.ContentData, 0, imageData.ContentData.Length);
                }

                path = Path.Combine(new Domain.Setting.SettingBL().GetSettingValue<string>("RootUrl").ToString() + new Domain.Setting.SettingBL().GetSettingValue<string>("DocumentsVirtualPath").ToString(), "Files", "Temp", Path.GetFileName(filename));
            }
            return path;
        }

        public ActionResult ReadImageContentsByID(long? contentID)
        {
            if (contentID.HasValue)
            {
                var imageData = ClientFactory.ContentServicesClient(CallContext).ReadContentsById(contentID.Value);
                if (imageData != null)
                {
                    byte[] data = imageData.ContentData;
                    var convertedData = File(data, "image/jpg");
                    return convertedData;
                }
                else
                {
                    return null;
                }
                
            }
            else
            return null;
        }

        public byte[] ConvertToByte(HttpPostedFileBase file)
        {
            byte[] imageByte = null;
            var rdr = new BinaryReader(file.InputStream);
            imageByte = rdr.ReadBytes((int)file.ContentLength);
            return imageByte;
        }

        [HttpPost]
        //public ActionResult UploadStudentDocuments(AssignmentDocuments args)
        //{
        //    bool isSavedSuccessfully = false;
        //    var imageInfoList = new List<ContentFileDTO>();

        //    try
        //    {
        //        var userID = CallContext.LoginID;

        //        foreach (string fileName in Request.Files)
        //        {
        //            var file = Request.Files[fileName];
        //            //Save file content goes here
        //            if (file != null && file.ContentLength > 0)
        //            {
        //                var fileInfo = new ContentFileDTO();
        //                fileInfo.ContentFileName = file.FileName;
        //                fileInfo.ContentData = ConvertToByte(file);
        //                imageInfoList.Add(fileInfo);
        //            }
        //        }

        //        imageInfoList = ClientFactory.ContentServicesClient(CallContext).SaveFiles(imageInfoList);
        //        imageInfoList.All(c => { c.ContentData = null; return true; });
        //        isSavedSuccessfully = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Eduegate.Logger.LogHelper<ContentController>.Fatal(ex.Message.ToString(), ex);
        //        return Json(new { Success = isSavedSuccessfully, Message = (ex.Message) });
        //    }

        //    if (isSavedSuccessfully == true)
        //    {
        //        using (IDbConnection conn = new SqlConnection(Infrastructure.ConfigHelper.GetDefaultConnectionString()))
        //        {
        //            try
        //            {
        //                var reader = conn.QueryMultiple("[schools].[Insert_Student_Assignment]",
        //                               param: new { AssignmentID = args.AssignmentIID, DateOfSubmission = DateTime.UtcNow, AssignmentStatusID = 2, CreatedBy = CallContext.LoginID, CreatedDate = DateTime.UtcNow, AttachmentReferenceId = imageInfoList[0].ContentFileIID, Notes = args.Notes, Description = args.Description, AttachmentName = args.AttachmentName, StudentId = args.StudentId },
        //                             commandType: CommandType.StoredProcedure);

        //            }
        //            catch
        //            {
        //                return RedirectToAction("AssignmentDocument", "Home", new { Success = false });
        //            }
        //        }
        //    }

        //    return RedirectToAction("AssignmentDocument", "Home", new { Success = isSavedSuccessfully });
        //    // return Json(new { Success = isSavedSuccessfully, FileInfo = imageInfoList });
        //}

        public long UploadStudentDocuments()
        {
            var contentFileList = new List<ContentFileDTO>();
            long contentFileID = 0;
            try
            {
                var userID = CallContext.LoginID;

                //TODO: Check later
                //foreach (string fileName in Request.Files)
                //{
                //    var file = Request.Files[fileName];
                //    //Save file content goes here
                //    if (file != null && file.ContentLength > 0)
                //    {
                //        var fileInfo = new ContentFileDTO();
                //        fileInfo.ContentFileName = file.FileName;
                //        fileInfo.ContentData = ConvertToByte(file);
                //        contentFileList.Add(fileInfo);
                //    }
                //}

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
                            contentFileList.Add(fileInfo);
                        }
                    }
                }

                contentFileList = ClientFactory.ContentServicesClient(CallContext).SaveFiles(contentFileList);
                contentFileID = contentFileList[0].ContentFileIID;
                //imageInfoList.All(c => { c.ContentData = null; return true; });
                //isSavedSuccessfully = true;
            }
            catch (Exception ex)
            {
                //Eduegate.Logger.LogHelper<MutualController>.Fatal(ex.Message.ToString(), ex);
                return contentFileID;
            }

            return contentFileID;
        }

        public long DeleteContentsByID(long? contentID)
        {
            if (contentID.HasValue)
            {

                return ClientFactory.ContentServicesClient(CallContext).DeleteEntity(contentID.Value);
            }
            return contentID.Value;
        }

        [HttpGet]
        public FileResult DirectDownloadByContentID(long? contentID)
        {
            var imageData = ClientFactory.ContentServicesClient(CallContext).ReadContentsById(contentID.Value);

            byte[] fileBytes = imageData.ContentData;
            string fileName = imageData.ContentFileName;

            string contentType;
            if (imageData.ContentFileName.EndsWith(".pdf"))
            {
                contentType = "application/pdf";
            }
            else if (imageData.ContentFileName.EndsWith(".xls") || imageData.ContentFileName.EndsWith(".xlsx"))
            {
                contentType = "application/vnd.ms-excel";
            }
            else if (imageData.ContentFileName.EndsWith(".doc") || imageData.ContentFileName.EndsWith(".rtf") || imageData.ContentFileName.EndsWith(".docx"))
            {
                contentType = "application/msword";
            }
            else if (imageData.ContentFileName.EndsWith(".gif"))
            {
                contentType = "image/gif";
            }
            else if (imageData.ContentFileName.EndsWith(".htm") || imageData.ContentFileName.EndsWith(".html"))
            {
                contentType = "text/html";
            }
            else if (imageData.ContentFileName.EndsWith(".txt"))
            {
                contentType = "text/plain";
            }
            else if (imageData.ContentFileName.EndsWith(".jpeg") || imageData.ContentFileName.EndsWith(".jpg") || imageData.ContentFileName.EndsWith(".jfif") || imageData.ContentFileName.EndsWith(".webp"))
            {
                contentType = "image/jpeg";
            }
            else if (imageData.ContentFileName.EndsWith(".png"))
            {
                contentType = "image/png";
            }
            else
            {
                contentType = "image/jpg";
            }

            return File(fileBytes, contentType, fileName);
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
    }
}
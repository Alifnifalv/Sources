using Dapper;
using Eduegate.Frameworks.Mvc.Controllers;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.Contents;
using Eduegate.Services.Contracts.Contents.Enums;
using Eduegate.Services.Contracts.School.Academics;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

                foreach (string fileName in Request.Files)
                {
                    var file = Request.Files[fileName];
                    //Save file content goes here
                    if (file != null && file.ContentLength > 0)
                    {
                        var fileInfo = new ContentFileDTO();
                        fileInfo.ContentFileName = file.FileName;
                        fileInfo.ContentData = ConvertToByte(file);
                        imageInfoList.Add(fileInfo);
                    }
                }

                imageInfoList = ClientFactory.ContentServicesClient(CallContext).SaveFiles(imageInfoList);
                imageInfoList.All(c => { c.ContentData = null; return true; });
                isSavedSuccessfully = true;
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<MutualController>.Fatal(ex.Message.ToString(), ex);
                return Json(new { Success = isSavedSuccessfully, Message = (ex.Message), JsonRequestBehavior.AllowGet });
            }
           
            return Json(new { Success = isSavedSuccessfully, FileInfo = imageInfoList }, JsonRequestBehavior.AllowGet);
        }

        public void ReadContents(ContentType contentType, long referenceID)
        {
            var imageData = ClientFactory.ContentServicesClient(CallContext).GetFile(contentType, referenceID);
            Response.ContentType = "image/jpg";
            Response.BinaryWrite(imageData.ContentData);
        }

        //public bool DeleteEntity(long? contentID)
        //{
        //    try
        //    {
        //        using (dbContentContext dbContext = new dbContentContext())
        //        {
        //            var data = dbContext.ContentFiles.FirstOrDefault(x => x.ContentFileIID == contentID);
        //            dbContext.ContentFiles.Remove(data);
        //            dbContext.SaveChanges();
        //        }
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //    return true;
        //}
        //DeleteEntity
        public void ReadContentsByID(long? contentID)
        {
            if (contentID.HasValue)
            {
                var imageData = ClientFactory.ContentServicesClient(CallContext).ReadContentsById(contentID.Value);

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
                    Response.AddHeader("Content-Disposition", new System.Net.Mime.ContentDisposition("attachment") { FileName = imageData.ContentFileName }.ToString());
                    Response.BinaryWrite(imageData.ContentData);
                }
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
            if (contentID.HasValue)
            {
                var imageData = ClientFactory.ContentServicesClient(CallContext).ReadContentsById(contentID.Value);

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
                    //Response.AddHeader("Content-Disposition", new System.Net.Mime.ContentDisposition("inline") { FileName = imageData.ContentFileName }.ToString());
                    Response.BinaryWrite(imageData.ContentData);
                }
            }
        }

        public static byte[] Compress(byte[] data)
        {
            MemoryStream output = new MemoryStream();
            using (DeflateStream dstream = new DeflateStream(output, CompressionLevel.Optimal))
            {
                dstream.Write(data, 0, data.Length);
            }
            return output.ToArray();
        }

        public static byte[] Decompress(byte[] data)
        {
            MemoryStream input = new MemoryStream(data);
            MemoryStream output = new MemoryStream();
            using (DeflateStream dstream = new DeflateStream(input, CompressionMode.Decompress))
            {
                dstream.CopyTo(output);
            }
            return output.ToArray();
        }
    }
}
using Dapper;
using Eduegate.ERP.School.ParentPortal.ViewModel;
using Eduegate.Framework.Extensions;
using Eduegate.Frameworks.Mvc.Controllers;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.Contents;
using Eduegate.Services.Contracts.Contents.Enums;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
        public ActionResult UploadContents(AssignmentDocuments args)
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
                Eduegate.Logger.LogHelper<ContentController>.Fatal(ex.Message.ToString(), ex);
                return Json(new { Success = isSavedSuccessfully, Message = (ex.Message), JsonRequestBehavior.AllowGet });
            }

            if (isSavedSuccessfully == true)
            {
                using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbEduegateERPContext"].ConnectionString))
                {
                    try
                    {
                        var reader = conn.QueryMultiple("[schools].[Insert_Student_Assignment]",
                                       param: new { AssignmentID = args.AssignmentIID, DateOfSubmission = DateTime.UtcNow, AssignmentStatusID = 2, CreatedBy = CallContext.LoginID, CreatedDate = DateTime.UtcNow, AttachmentReferenceId = imageInfoList[0].ContentFileIID, Notes = args.Notes, Description = args.Description, AttachmentName = args.AttachmentName, StudentId = args.StudentId },
                                     commandType: CommandType.StoredProcedure);
                        
                    }
                    catch 
                    {
                        return RedirectToAction("AssignmentDocument", "Home", new { Success = false });
                    }
                }
            }

            return RedirectToAction("AssignmentDocument", "Home", new { Success = isSavedSuccessfully });
            // return Json(new { Success = isSavedSuccessfully, FileInfo = imageInfoList }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UploadImages()
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
                Eduegate.Logger.LogHelper<ContentController>.Fatal(ex.Message.ToString(), ex);
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

                    else if (imageData.ContentFileName.EndsWith(".jpeg") || imageData.ContentFileName.EndsWith(".jfif") || imageData.ContentFileName.EndsWith(".webp"))
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
                ConfigurationExtensions.GetAppConfigValue("DocumentsPhysicalPath").ToString(), "Files", "Temp", Guid.NewGuid());

                if (!Directory.Exists(Path.GetDirectoryName(filename)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(filename));
                }

                using (FileStream fs = new FileStream(filename, FileMode.Append))
                {
                    fs.Write(imageData.ContentData, 0, imageData.ContentData.Length);
                }

                path = Path.Combine(ConfigurationExtensions.GetAppConfigValue("RootUrl").ToString() + ConfigurationExtensions.GetAppConfigValue("DocumentsVirtualPath").ToString(), "Files", "Temp", Path.GetFileName(filename));
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
        //        return Json(new { Success = isSavedSuccessfully, Message = (ex.Message), JsonRequestBehavior.AllowGet });
        //    }

        //    if (isSavedSuccessfully == true)
        //    {
        //        using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbEduegateERPContext"].ConnectionString))
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
        //    // return Json(new { Success = isSavedSuccessfully, FileInfo = imageInfoList }, JsonRequestBehavior.AllowGet);
        //}
        public long UploadStudentDocuments()
        {
            var contentFileList = new List<ContentFileDTO>();
            long contentFileID = 0;
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
                        contentFileList.Add(fileInfo);
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
   }
}
using DocumentFormat.OpenXml.Spreadsheet;
using Eduegate.Application.Mvc;
using Eduegate.ERP.Admin.Areas.Documents.Controllers;
using Eduegate.ERP.Admin.Controllers;
using Eduegate.Logger;
using Eduegate.Services.Contracts.Contents;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Web.Library.ViewModels.Accounts;
using Microsoft.AspNetCore.Mvc;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.Accounting;
using System.Globalization;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Domain.Entity.Models;
using Eduegate.Services.Contracts.Accounts.Accounting;
using StackExchange.Profiling.Internal;
using Eduegate.Web.Library.School.Academics;
using Eduegate.Services.Contracts.School.Academics;
using Eduegate.Domain.Mappers.School.Academics;
using Eduegate.Domain.AI;
using Eduegate.ERP.Admin.Areas.AI.Controllers;
namespace Eduegate.ERP.AdminCore.Areas.Schools.Controllers
{
    [Area("Schools")]
    public class AILessonPlannerController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AILessonPlanner()
        {
            return View();
        }



        [HttpGet]
        public ActionResult Upload()
        {
            return View(new UploadedFileDetailsViewModel());
        }

        [HttpPost]
        public async Task<JsonResult> ExtractPdfOrExcelData(IFormFile file , [FromForm] long ContentFileIID)
        {
            if (file == null || file.Length == 0)
            {
                return Json(new { success = false, message = "No file uploaded." });
            }

            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var formData = new MultipartFormDataContent())
                    {
                        using (var stream = new MemoryStream())
                        {
                            await file.CopyToAsync(stream);
                            stream.Seek(0, SeekOrigin.Begin);
                            var fileContent = new StreamContent(stream);
                            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);

                            formData.Add(fileContent, "files", file.FileName);

                            httpClient.Timeout = TimeSpan.FromMinutes(10);

                            // External API URL
                            var AI_URL = new Domain.Setting.SettingBL(null).GetSettingValue<string>("AI_RESOURCE_URL");

                            var externalApiUrl = AI_URL+"Extract";
                            var response = await httpClient.PostAsync(externalApiUrl, formData);

                            if (response.IsSuccessStatusCode)
                            {
                                var result = await response.Content.ReadAsStringAsync();

                                ChapterMapper.Mapper(CallContext).SaveExtractedJson(result, ContentFileIID);


                                return Json(new { success = true, data = result });
                            }
                            else
                            {
                                return Json(new { success = false, message = $"External API error: {response.ReasonPhrase}" });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error forwarding file: {ex.Message}" });
            }
        }


        //[HttpPost]
        //public ActionResult SaveChapterEntries([FromBody] List<ChapterDTO> chapterList)
        //{
        //    try
        //    {
        //        if (chapterList == null || !chapterList.Any())
        //        {
        //            return Json(new { IsError = true, Response = "Invalid data received!" });
        //        }

        //        // Save all chapters and get the updated list
        //        var updatedChapters = ChapterMapper.Mapper(CallContext).SaveChapterEntries(chapterList);

        //        if (updatedChapters != null && updatedChapters.Any())
        //        {
        //            return Json(new { IsError = false, Response = "Chapters saved successfully!", UpdatedChapters = updatedChapters });
        //        }
        //        else
        //        {
        //            return Json(new { IsError = true, Response = "Saving failed!" });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var errorMessage = ex.InnerException?.Message ?? ex.Message;
        //        Eduegate.Logger.LogHelper<string>.Fatal($"Chapter entry saving failed. Error: {errorMessage}", ex);

        //        return Json(new { IsError = true, Response = "Saving failed!" });
        //    }
        //}

        [HttpPost]

        [Route("Schools/AILessonPlanner/SaveChapterEntries")]
        public ActionResult SaveChapterEntries([FromBody] List<LessonPlanChapterDTO> chaptersToSave)
        {
            if (chaptersToSave == null || !chaptersToSave.Any())
            {
                return Json(new { IsError = true, Response = "No chapter data received to save." });
            }

            try
            {
                var updatedChapters = ChapterMapper.Mapper(CallContext).SaveChapterEntries(chaptersToSave);

                if (updatedChapters != null)
                {
                    return Json(new
                    {
                        IsError = false,
                        Response = "Lesson plans saved successfully!",
                        UpdatedChapters = updatedChapters
                    });
                }
                else
                {
                    return Json(new { IsError = true, Response = "Failed to save the lesson plans." });
                }
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException?.Message ?? ex.Message;
                Eduegate.Logger.LogHelper<string>.Fatal($"Chapter entry saving failed. Error: {errorMessage}", ex);

                return Json(new { IsError = true, Response = "An unexpected error occurred while saving." });
            }
        }


        [HttpPost]
        public async Task<ActionResult> UploadDocument()
        {
            bool isSavedSuccessfully = false;
            var imageInfoList = new List<ContentFileDTO>();

            try
            {
                var userID = CallContext.LoginID;

                foreach (var fileName in Request.Form.Files)
                {
                    var file = fileName;
                    //Save file content goes here
                    if (file != null)
                    {
                        var fileInfo = new ContentFileDTO();
                        fileInfo.ContentFileName = file.FileName;
                        fileInfo.ContentData = await ConvertToByte(file);
                        fileInfo.FilePath = "Content/ReadContentsByID";
                        imageInfoList.Add(fileInfo);
                    }
                }

                imageInfoList = ClientFactory.ContentServicesClient(CallContext).SaveFiles(imageInfoList);
                isSavedSuccessfully = true;
            }
            catch (Exception ex)
            {
                LogHelper<DocManagementController>.Fatal(ex.Message.ToString(), ex);
                return Json(new { Success = isSavedSuccessfully, Message = (ex.Message) });
            }

            return Json(new { Success = isSavedSuccessfully, FileInfo = imageInfoList });
        }
        public static async Task<byte[]> ConvertToByte(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
        [HttpPost]
        public ActionResult DeleteUploadedDocument(string fileName)
        {
            bool isSuccessFullyDeleted = false;

            try
            {
                var userID = base.CallContext.LoginID;//Needs to fech from call context
                if (!string.IsNullOrEmpty(fileName))
                {
                    string tempFolderPath = string.Format("{0}\\{1}\\{2}", new Domain.Setting.SettingBL().GetSettingValue<string>("DocumentsPhysicalPath").ToString(), userID, fileName);

                    if (System.IO.File.Exists(tempFolderPath))
                    {
                        System.IO.File.Delete(tempFolderPath);
                    }
                    isSuccessFullyDeleted = true;

                }
                else
                {
                    var file = string.Format("{0}\\{1}\\{2}", new Domain.Setting.SettingBL().GetSettingValue<string>("ImagesPhysicalPath").ToString(), userID);
                    if (System.IO.Directory.Exists(file))
                        System.IO.Directory.Delete(file, true);
                }
            }
            catch (Exception ex)
            {
                LogHelper<DocManagementController>.Fatal(ex.Message.ToString(), ex);
                return Json(new { Success = isSuccessFullyDeleted, Message = (ex.Message) });
            }

            return Json(new { Success = isSuccessFullyDeleted });
        }
        [HttpPost]
        public JsonResult SaveUploadedFiles(List<UploadedFileDetailsViewModel> files)
        {
            var dtos = UploadedFileDetailsViewModel.ToDocumentVM(files,
                Eduegate.Framework.Contracts.Common.Enums.EntityTypes.Others);

            foreach (var dto in dtos)
            {
                if (dto.ReferenceID.HasValue)
                {
                    var contentData = ClientFactory.ContentServicesClient(CallContext)
                        .ReadContentsById(dto.ReferenceID.Value);
                    dto.ContentData = contentData.ContentData;
                }
            }

            DocumentFileViewModel.FromDTO(ClientFactory.DocumentServiceClient(CallContext)
                .SaveDocuments(dtos));
            return Json(new { Success = true });
        }

        [HttpGet]
        public ActionResult Configure(long IID = 0)
        {
            return View(IID);
        }

        //public ActionResult GetDocument(long referenceID = 0, EntityTypes type = EntityTypes.Others)
        //{
        //    var document = DocumentFileViewModel.FromDTO(ClientFactory
        //        .DocumentServiceClient(CallContext).GetDocuments(referenceID, type));
        //    return Json(document.FirstOrDefault());
        //}
    }
}

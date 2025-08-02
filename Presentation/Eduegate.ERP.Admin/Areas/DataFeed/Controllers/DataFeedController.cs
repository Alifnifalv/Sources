using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Eduegate.ERP.Admin.Controllers;
using Eduegate.Framework.Extensions;

using Eduegate.Services.Contracts;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Services.Client.Factory;

namespace Eduegate.ERP.Admin.Areas.DataFeed.Controllers
{
    public class DataFeedController : BaseSearchController
    {
        private static string rootURL { get { return ConfigurationExtensions.GetAppConfigValue("RootUrl"); } }
        private static string documentPhysicalPath { get { return ConfigurationExtensions.GetAppConfigValue("DocumentsPhysicalPath"); } }
        private static string documentVirtualPath { get { return ConfigurationExtensions.GetAppConfigValue("DocumentsVirtualPath"); } }
        string dataFeedService = string.Concat(ConfigurationExtensions.GetAppConfigValue("ServiceHost"), Eduegate.Framework.Helper.Constants.DATAFEED_SERVICE_NAME);
        private static string DataFeedFolder = "DataFeed";

        public ActionResult List()
        {
            var metadata = ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Eduegate.Infrastructure.Enums.SearchView.DataFeed);

            return View(new SearchListViewModel
            {
                ControllerName = "DataFeed/" + Eduegate.Infrastructure.Enums.SearchView.DataFeed.ToString(),
                ViewName = Eduegate.Infrastructure.Enums.SearchView.DataFeed,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                IsEditableLink = false,
                //IsMultilineEnabled = metadata.MultilineEnabled,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                //InfoBar = ""
                FilterColumns = metadata.QuickFilterColumns,
                HasFilters = metadata.HasFilters,
                UserValues = metadata.UserValues
            });
        }

        [HttpGet]
        public JsonResult SearchData(int currentPage = 1, string orderBy = "", String runtimeFilter = "")
        {
            return base.SearchData(Eduegate.Infrastructure.Enums.SearchView.DataFeed, currentPage, orderBy, runtimeFilter);
        }

        [HttpGet]
        public ActionResult DetailedView(long IID = 0)
        {
            ViewBag.IID = IID;
            return View("DetailedView");
        }

        public ActionResult ImportData()
        {
            var model = DataFeedViewModel.FromDTO(null, ClientFactory.DataFeedServiceClient(CallContext).GetDataFeedTypes());
            return View(model);
        }

        public ActionResult DownloadTemplate(int templateTypeID)
        {
            try
            {
                //Get templates
                var client = ClientFactory.DataFeedServiceClient(CallContext);
                var template = client.GetDataFeedType(templateTypeID);

                if (template == null) throw new Exception("Invalid data feed.");

                //Create a stream for the file
                byte[] byteArray;

                using (var stream = client.DownloadDataFeedTemplate(templateTypeID))
                using (MemoryStream ms = new MemoryStream())
                {
                    int count = 0;

                    do
                    {
                        byte[] buffer = new byte[8192];
                        count = stream.Read(buffer, 0, 8192);
                        ms.Write(buffer, 0, count);
                    } while (stream.CanRead && count > 0);

                    byteArray = ms.ToArray();
                }

                string folderPath = Path.Combine(documentPhysicalPath, "DataFeed", "Templates");

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                var fileName = string.Concat(template.TemplateName, "_", Guid.NewGuid(), ".xlsx");
                System.IO.File.WriteAllBytes(Path.Combine(folderPath, fileName), byteArray);

                return Json(new { IsSuccess = true, DownloadPath = string.Concat(documentVirtualPath, "/DataFeed/", "/Templates/", fileName) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string message = string.Format("Template: {0} could not be downloaded due to technical issues.", ((Eduegate.Services.Contracts.Enums.DataFeedTypes)templateTypeID).ToString());
                Eduegate.Logger.LogHelper<DataFeedController>.Fatal(message, ex);
                return Json(new { IsSuccess = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DownloadProcessedFeedFile(long feedLogID)
        {
            try
            {
                //Create a stream for the file
                Stream stream = null;

                // The number of bytes read

                //Create a WebRequest to get the file
                var uri = string.Format("{0}/{1}", dataFeedService, "DownloadProcessedFeedFile?feedLogID=" + feedLogID);
                HttpWebRequest fileReq = (HttpWebRequest)HttpWebRequest.Create(uri);

                //Create a response for this request
                HttpWebResponse fileResp = (HttpWebResponse)fileReq.GetResponse();

                if (fileReq.ContentLength > 0)
                    fileResp.ContentLength = fileReq.ContentLength;

                byte[] byteArray = null;
                using (stream = fileResp.GetResponseStream())
                using (MemoryStream ms = new MemoryStream())
                {

                    int count = 0;
                    do
                    {
                        byte[] buffer = new byte[8192];
                        count = stream.Read(buffer, 0, 8192);
                        ms.Write(buffer, 0, count);
                    } while (stream.CanRead && count > 0);
                    byteArray = ms.ToArray();
                }
                string pathWithoutRoot = Path.Combine("DataFeed", "ProcessedFeed", CallContext.LoginID.ToString(), feedLogID.ToString());
                string folderPath = Path.Combine(documentPhysicalPath, pathWithoutRoot);
                string fileName = fileResp.Headers["Content-Disposition"].Split(';')[1].Split('=')[1].Trim();
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);
                System.IO.File.WriteAllBytes(Path.Combine(folderPath, fileName), byteArray);

                return Json(new { IsSuccess = true, DownloadPath = string.Concat(documentVirtualPath, pathWithoutRoot, "/", fileName) }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                string message = string.Format("Processed Feed Log ID: {0} could not be downloaded due to technical issues.", feedLogID.ToString());
                Eduegate.Logger.LogHelper<DataFeedController>.Fatal(message, ex);
                return Json(new { IsSuccess = false }, JsonRequestBehavior.AllowGet);

            }
        }

        [HttpPost]
        public ActionResult InitiateFeed(DataFeedViewModel model)
        {
            model.StatusID = Services.Contracts.Enums.DataFeedStatus.InProcess;
            var dataFeedLogDTO = ClientFactory.DataFeedServiceClient(CallContext).SaveDataFeedLog(DataFeedViewModel.ToDTO(model));

            var dataFeedViewModel = DataFeedViewModel.FromDTO(dataFeedLogDTO, null);
            string returnHTML = Eduegate.Framework.Mvc.Dynamics.ViewBuilder.RenderViewToString("~/Areas/DataFeed/Views/DataFeed/ProcessFeed.cshtml", dataFeedViewModel);
            return Json(new { ID = dataFeedViewModel.DataFeedID, RawHTML = returnHTML, JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public ActionResult ProcessFeed(long ID)
        {
            bool isSuccessful = false;

            try
            {
                var dfsc = ClientFactory.DataFeedServiceClient(CallContext);
                var dataFeedLogDTO = dfsc.GetDataFeedLogByID(ID);
                DataFeedViewModel model = DataFeedViewModel.FromDTO(dataFeedLogDTO, null);
                //Upload file to server first and then process the file
                //Services.Contracts.Enums.DataFeedTypes dataFeedType = (Services.Contracts.Enums.DataFeedTypes)Enum.Parse(typeof(Services.Contracts.Enums.DataFeedTypes), uploadFileType);
                string fileLocation = Path.Combine(documentPhysicalPath, DataFeedFolder, CallContext.LoginID.ToString(), model.DataFeedTypeID.ToString());
                var uploadedFiles = Eduegate.Utilities.DataExportImport.Upload.UploadToServer(Request, fileLocation);

                dataFeedLogDTO = ProcessUpload(uploadedFiles.FirstOrDefault(), model, ID);

                var dataFeedViewModel = DataFeedViewModel.FromDTO(dataFeedLogDTO, null);
                string returnHTML = Eduegate.Framework.Mvc.Dynamics.ViewBuilder.RenderViewToString("~/Areas/DataFeed/Views/DataFeed/ProcessFeed.cshtml", dataFeedViewModel);
                return Json(new { ID = dataFeedViewModel.DataFeedID, RawHTML = returnHTML, JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                string message = "Data import was not successful!";
                Eduegate.Logger.LogHelper<DataFeedController>.Fatal(message, ex);
                return Json(new { IsSuccess = isSuccessful, Message = message, JsonRequestBehavior.AllowGet });
            }
        }

        private DataFeedLogDTO ProcessUpload(string filePath, DataFeedViewModel model, long feedLogID)
        {
            using (var fileStream = System.IO.File.OpenRead(filePath))
            {
                return ClientFactory.DataFeedServiceClient(CallContext).UploadFeedFile(Path.GetFileName(filePath), feedLogID, filePath, fileStream);
            }
        }
    }
}
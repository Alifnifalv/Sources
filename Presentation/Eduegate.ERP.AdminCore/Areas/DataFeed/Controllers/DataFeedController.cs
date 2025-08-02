using System.Net;
using Microsoft.AspNetCore.Mvc;
using Eduegate.ERP.Admin.Controllers;
using Eduegate.Services.Contracts;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Services.Client.Factory;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Eduegate.Infrastructure.Enums;
using Eduegate.Services.Contracts.Enums;

namespace Eduegate.ERP.Admin.Areas.DataFeed.Controllers
{
    [Area("DataFeed")]
    public class DataFeedController : BaseSearchController
    {
        private static string rootURL { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("RootUrl"); } }
        private static string documentPhysicalPath { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("DocumentsPhysicalPath"); } }
        private static string documentVirtualPath { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("DocumentsVirtualPath"); } }
        string dataFeedService = string.Concat(new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"), Eduegate.Framework.Helper.Constants.DATAFEED_SERVICE_NAME);
        private static string DataFeedFolder = "DataFeed";

        private readonly IRazorViewEngine _razorViewEngine;
        private readonly ITempDataProvider _tempDataProvider;
        private readonly IServiceProvider _serviceProvider;

        public DataFeedController(IRazorViewEngine razorViewEngine,
                ITempDataProvider tempDataProvider,
                IServiceProvider serviceProvider)
        {
            _razorViewEngine = razorViewEngine;
            _tempDataProvider = tempDataProvider;
            _serviceProvider = serviceProvider;
        }

        public async Task<IActionResult> List()
        {
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Eduegate.Infrastructure.Enums.SearchView.DataFeed);

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
        public Task<IActionResult> SearchData(int currentPage = 1, string orderBy = "", string runtimeFilter = "")
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

                return Json(new { IsSuccess = true, DownloadPath = string.Concat(documentVirtualPath, "/DataFeed/", "/Templates/", fileName) });
            }
            catch (Exception ex)
            {
                string message = string.Format("Template: {0} could not be downloaded due to technical issues.", ((Eduegate.Services.Contracts.Enums.DataFeedTypes)templateTypeID).ToString());
                Eduegate.Logger.LogHelper<DataFeedController>.Fatal(message, ex);
                return Json(new { IsSuccess = false });
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

                return Json(new { IsSuccess = true, DownloadPath = string.Concat(documentVirtualPath, pathWithoutRoot, "/", fileName) });

            }
            catch (Exception ex)
            {
                string message = string.Format("Processed Feed Log ID: {0} could not be downloaded due to technical issues.", feedLogID.ToString());
                Eduegate.Logger.LogHelper<DataFeedController>.Fatal(message, ex);
                return Json(new { IsSuccess = false });

            }
        }

        [HttpPost]
        public async Task<IActionResult> InitiateFeed(DataFeedViewModel model)
        {
            model.StatusID = Services.Contracts.Enums.DataFeedStatus.InProcess;
            var dataFeedLogDTO = ClientFactory.DataFeedServiceClient(CallContext).SaveDataFeedLog(DataFeedViewModel.ToDTO(model));

            var dataFeedViewModel = DataFeedViewModel.FromDTO(dataFeedLogDTO, null);
            string returnHTML = await RenderViewToStringAsync("ProcessFeed", dataFeedViewModel, ControllerContext);
            return Json(new { ID = dataFeedViewModel.DataFeedID, RawHTML = returnHTML });
        }


        [HttpPost]
        public async Task<IActionResult> ProcessFeed(long ID)
        {
            bool isSuccessful = false;

            try
            {
                var dfsc = ClientFactory.DataFeedServiceClient(CallContext);
                var dataFeedLogDTO = dfsc.GetDataFeedLogByID(ID);
                DataFeedViewModel model = DataFeedViewModel.FromDTO(dataFeedLogDTO, null);
                //Upload file to server first and then process the file
                //Services.Contracts.Enums.DataFeedTypes dataFeedType = (Services.Contracts.Enums.DataFeedTypes)Enum.Parse(typeof(Services.Contracts.Enums.DataFeedTypes), uploadFileType);
                string fileLocation = Path.Combine(documentPhysicalPath, "DataFeed",
                        CallContext.LoginID.ToString(), model.DataFeedTypeID.ToString());

                //TODO: dll code changed so Request.Form.Files.ToList() not support
                //var uploadedFiles = Utilities.DataExportImport.Upload.UploadToServer(Request.Form.Files.ToList(), fileLocation);

                var uploadedFiles = Utilities.DataExportImport.Upload.UploadToServer(Request, fileLocation);

                dataFeedLogDTO = ProcessUpload(uploadedFiles.FirstOrDefault(), model, ID);

                var dataFeedViewModel = DataFeedViewModel.FromDTO(dataFeedLogDTO, null);
                string returnHTML = await RenderViewToStringAsync("ProcessFeed", dataFeedViewModel, ControllerContext);
                return Ok(new { ID = dataFeedViewModel.DataFeedID, RawHTML = returnHTML });
            }
            catch (Exception ex)
            {
                string message = "Data import was not successful!";
                Eduegate.Logger.LogHelper<DataFeedController>.Fatal(message, ex);
                return Ok(new { IsSuccess = isSuccessful, Message = message });
            }
        }

        private DataFeedLogDTO ProcessUpload(string filePath, DataFeedViewModel model, long feedLogID)
        {
            using (var fileStream = System.IO.File.OpenRead(filePath))
            {
                return ClientFactory.DataFeedServiceClient(CallContext).UploadFeedFile(Path.GetFileName(filePath), feedLogID, filePath, fileStream);
            }
        }

        public static async Task<string> RenderViewToStringAsync(
            string viewName, object model,
            ControllerContext controllerContext,
            bool isPartial = false)
        {
            var actionContext = controllerContext as ActionContext;

            var serviceProvider = controllerContext.HttpContext.RequestServices;
            var razorViewEngine = serviceProvider.GetService(typeof(IRazorViewEngine)) as IRazorViewEngine;
            var tempDataProvider = serviceProvider.GetService(typeof(ITempDataProvider)) as ITempDataProvider;

            using (var sw = new StringWriter())
            {
                var viewResult = razorViewEngine.FindView(actionContext, viewName, !isPartial);

                if (viewResult?.View == null)
                    throw new ArgumentException($"{viewName} does not match any available view");

                var viewDictionary =
                    new ViewDataDictionary(new EmptyModelMetadataProvider(),
                        new ModelStateDictionary())
                    { Model = model };

                var viewContext = new ViewContext(
                    actionContext,
                    viewResult.View,
                    viewDictionary,
                    new TempDataDictionary(actionContext.HttpContext, tempDataProvider),
                    sw,
                    new HtmlHelperOptions()
                );

                await viewResult.View.RenderAsync(viewContext);
                return sw.ToString();
            }
        }

        public void DownloadExcelTemplate(Screens screen)
        {
            int templateTypeID = 0;
            try
            {
                switch (screen)
                {
                    case Screens.BankReconcilation:
                        templateTypeID = (int)DataFeedTypes.BankReconciliationOpening;
                        break;
                    case Screens.AssetManualEntry:
                        templateTypeID = (int)DataFeedTypes.AssetOpening;
                        break;

                    default:
                        templateTypeID = 0;
                        break;
                }

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

                var fileName = string.Concat(template.TemplateName, "_", Guid.NewGuid(), ".xlsx");

                Response.ContentType = "Application/x-msexcel";
                HttpContext.Response.Headers.Append("Content-Disposition", new System.Net.Mime.ContentDisposition("attachment") { FileName = !string.IsNullOrEmpty(fileName) ? fileName : "NoName" }.ToString());

                Response.Body.WriteAsync(byteArray);
            }
            catch (Exception ex)
            {
                string message = string.Format("Template: {0} could not be downloaded due to technical issues.", ((Eduegate.Services.Contracts.Enums.DataFeedTypes)templateTypeID).ToString());
                Eduegate.Logger.LogHelper<DataFeedController>.Fatal(message, ex);
            }
        }

    }
}
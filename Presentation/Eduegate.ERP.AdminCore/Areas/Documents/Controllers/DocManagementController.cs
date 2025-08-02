using Eduegate.ERP.Admin.Controllers;
using Eduegate.Framework.Extensions;
using Eduegate.Logger;
using Eduegate.Services.Client.Factory;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using Eduegate.Domain;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Contents;
using DocumentFormat.OpenXml.Wordprocessing;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Services.Contracts.School.Exams;
using Eduegate.Web.Library.School.Exams;
using Eduegate.Services.Contracts.Accounting;
using Eduegate.Web.Library.ViewModels.Accounts;
using Newtonsoft.Json;
using System.Globalization;
using DocumentFormat.OpenXml.Vml;
using System.Runtime.Serialization;
using Eduegate.Domain.Entity.Models;
using Eduegate.Services.Contracts.Enums;
using Microsoft.AspNetCore.Http;
using Syncfusion.Office;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using System.Text.RegularExpressions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.JsonPatch.Internal;
using Eduegate.ERP.Admin.Areas.DataFeed.Controllers;
using Eduegate.Services.Contracts;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;


using System.Linq;
using System;
using System.Threading.Tasks;
using System.IO;
using Eduegate.Domain.Entity.Contents;
using Org.BouncyCastle.Bcpg.OpenPgp;
using Eduegate.Services.Contracts.Accounts.Accounting;
using DinkToPdf.Contracts;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.AI;
using Eduegate.Domain.Entity.Accounts.Models.Accounts;
using StackExchange.Profiling.Internal;
using ZXing;
using Eduegate.Infrastructure.Enums;

namespace Eduegate.ERP.Admin.Areas.Documents.Controllers
{
    [Area("Documents")]
    public class DocManagementController : BaseSearchController
    {

        private static string rootURL { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("RootUrl"); } }
        private static string documentPhysicalPath { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("DocumentsPhysicalPath"); } }
        private static string documentVirtualPath { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("DocumentsVirtualPath"); } }
        string dataFeedService = string.Concat(new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"), Eduegate.Framework.Helper.Constants.DATAFEED_SERVICE_NAME);
        private static string DataFeedFolder = "DataFeed";
        private Microsoft.AspNetCore.Hosting.IWebHostEnvironment _hostingEnvironment;

        public DocManagementController(Microsoft.Extensions.Caching.Memory.IMemoryCache memoryCache,
          Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;

        }
        // GET: Documents/DocManagement
        public ActionResult Index()
        {
            return View();
        }



        [HttpGet]
        public async Task<IActionResult> List()
        {
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Eduegate.Infrastructure.Enums.SearchView.DocManagement);
            return View(new SearchListViewModel
            {
                ControllerName = "Documents/" + Eduegate.Infrastructure.Enums.SearchView.DocManagement,
                ViewName = Eduegate.Infrastructure.Enums.SearchView.DocManagement,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                IsCommentLink = true,
                HasFilters = metadata.HasFilters,
                FilterColumns = metadata.QuickFilterColumns,
                //IsMultilineEnabled = metadata.MultilineEnabled,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                InfoBar = @"<li class='status-label-mobile'>
                                        <div class='right status-label'>
                                            <div class='status-label-color'><label class='status-color-label  green'></label>Complete</div>
                                            <div class='status-label-color'><label class='status-color-label red'></label>Fail</div>
                                             <div class='status-label-color'><label class='status-color-label Yellow'></label>Others</div>
                                        </div>
                                    </li>",

                IsChild = false,
                HasChild = true,
                UserValues = metadata.UserValues
            });
        }

        [HttpGet]
        public Task<IActionResult> SearchData(int currentPage = 1, string orderBy = "", string runtimeFilter = "")
        {
            var view = Eduegate.Infrastructure.Enums.SearchView.DocManagement;
            if (runtimeFilter.Trim().IsNotNullOrEmpty())
            {
                view = Eduegate.Infrastructure.Enums.SearchView.DocManagement;
                runtimeFilter = runtimeFilter + " AND CompanyID = " + CallContext.CompanyID.ToString();
            }
            else
            {
                runtimeFilter = " CompanyID = " + CallContext.CompanyID.ToString();
            }

            return base.SearchData(view, currentPage, orderBy, runtimeFilter);
        }

        [HttpGet]
        public Task<IActionResult> SearchSummaryData()
        {
            return base.SearchSummaryData(Eduegate.Infrastructure.Enums.SearchView.DocManagement);
        }

        [HttpGet]
        public ActionResult DetailedView(long IID = 0)
        {
            ViewBag.IID = IID;
            return View();
        }

        [HttpGet]
        public ActionResult Upload()
        {
            return View(new UploadedFileDetailsViewModel());
        }

        //TODO: Commented code which not support in .net core
        //[HttpPost]
        //public ActionResult UploadDocument()
        //{
        //    var files = new List<UploadedFileDetailsViewModel>();
        //    foreach (string fileName in Request.Files)
        //    {
        //        HttpPostedFileBase file = Request.Files[fileName];
        //        //Save file content goes here
        //        if (file != null && file.ContentLength > 0)
        //        {
        //            var docVM = new UploadedFileDetailsViewModel();

        //            var imageExtension = System.IO.Path.GetExtension(file.FileName).ToLower();
        //            var imageFileName = Guid.NewGuid() + file.FileName;
        //            var path = new Domain.Setting.SettingBL().GetSettingValue<string>("DocumentsPhysicalPath");

        //            if (!Directory.Exists(path))
        //            {
        //                Directory.CreateDirectory(path);
        //            }

        //            docVM.ActualFileName = file.FileName;
        //            docVM.FileName = imageFileName;
        //            docVM.FilePath = new Domain.Setting.SettingBL().GetSettingValue<string>("DocumentsVirtualPath") + imageFileName;
        //            files.Add(docVM);
        //            file.SaveAs(Path.Combine(path, imageFileName));
        //        }
        //    }
        //    return Json(new { Success = true, FileInfo = files });
        //}
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

                string folderPath = System.IO.Path.Combine(documentPhysicalPath, "DataFeed", "Templates");

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                var fileName = string.Concat(template.TemplateName, "_", Guid.NewGuid(), ".xlsx");
                System.IO.File.WriteAllBytes(System.IO.Path.Combine(folderPath, fileName), byteArray);

                return Json(new { IsSuccess = true, DownloadPath = string.Concat(documentVirtualPath, "/DataFeed/", "/Templates/", fileName) });
            }
            catch (Exception ex)
            {
                string message = string.Format("Template: {0} could not be downloaded due to technical issues.", ((Eduegate.Services.Contracts.Enums.DataFeedTypes)templateTypeID).ToString());
                Eduegate.Logger.LogHelper<DataFeedController>.Fatal(message, ex);
                return Json(new { IsSuccess = false });
            }
        }

        [HttpPost]
        public async Task<ActionResult> UploadPdfDocument()
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
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception") ? ex.InnerException?.Message : ex.Message;

                LogHelper<string>.Fatal($"Exception in DownloadTemplate. Error message: {errorMessage}", ex);
                return Json(new { Success = isSavedSuccessfully, Message = (ex.Message) });
            }

            return Json(new { Success = isSavedSuccessfully, FileInfo = imageInfoList });
        }

        [HttpPost]
        public async Task<ActionResult> UploadExcelDocument()
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
                var filePath = SaveUploadedFile(imageInfoList.FirstOrDefault());
                imageInfoList.ForEach(x => x.FilePath = filePath);
                isSavedSuccessfully = true;
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception") ? ex.InnerException?.Message : ex.Message;

                LogHelper<string>.Fatal($"Exception in UploadExcelDocument. Error message: {errorMessage}", ex);
                return Json(new { Success = isSavedSuccessfully, Message = (ex.Message) });
            }

            return Json(new { Success = isSavedSuccessfully, FileInfo = imageInfoList });
        }

        private string SaveUploadedFile(ContentFileDTO content)
        {
            string strFilePath = null;
            try
            {
                using (var dbContext = new dbContentContext())
                {

                    if (content != null)
                    {
                        var fileName = content.ContentFileName;
                        var contentData = content.ContentData;
                        //fileDirPath + @"/Documents/DataFeed/Reconc/" + reportName + ".rdl";
                        string serverPath = _hostingEnvironment.WebRootPath;// "C:\\PEARL_CORE\\ParentPortal\\wwwroot\\Documents\\DataFeed";
                        string dirPath = serverPath + @"/Documents/DataFeed/Reconc/";
                        strFilePath = dirPath + @"/" + fileName;
                        if (System.IO.File.Exists(strFilePath) == false)
                        {
                            if (Directory.Exists(dirPath) == false)
                            {
                                Directory.CreateDirectory(dirPath);
                            }

                            System.IO.File.WriteAllBytes(dirPath + "\\" + fileName, contentData);// Compress(content)
                        }
                        else
                        {
                            System.IO.File.Delete(strFilePath);

                            System.IO.File.WriteAllBytes(dirPath + "\\" + fileName, contentData);// Compress(content)
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception") ? ex.InnerException?.Message : ex.Message;

                LogHelper<string>.Fatal($"Exception in SaveUploadedFile(ContentFileDTO content). Error message: {errorMessage}", ex);
                return null;
            }

            return strFilePath;
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
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception") ? ex.InnerException?.Message : ex.Message;

                LogHelper<string>.Fatal($"Exception in DeleteUploadedDocument(). Error message: {errorMessage}", ex);
                return Json(new { Success = isSuccessFullyDeleted, Message = (ex.Message) });
            }

            return Json(new { Success = isSuccessFullyDeleted });
        }
        [HttpPost]
        public async Task<IActionResult> SaveBankStatementEntry(BankReconciliationBankTransViewModel data)
        {
            try
            {
                var bankStatementEntryDTO = new List<BankStatementEntryDTO>();
                if (data != null && !string.IsNullOrEmpty(data.Narration))
                {
                    bankStatementEntryDTO.Add(new BankStatementEntryDTO()
                    {
                        BankStatementEntryIID = data.BankStatementEntryID ?? 0,
                        Balance = data.Balance,
                        BankStatementID = data.BankStatementID ?? 0,
                        ChequeDate = !string.IsNullOrEmpty(data.ChequeDate) && data.ChequeDate != "-" ? DateTime.ParseExact(data.ChequeDate, "dd/MM/yyyy", null) : (DateTime?)null,
                        ChequeNo = data.ChequeNo,
                        Credit = data.BankCreditAmount ?? 0,
                        Debit = data.BankDebitAmount ?? 0,
                        Description = data.Narration,
                        PartyName = data.PartyName,
                        PostDate = !string.IsNullOrEmpty(data.PostDate) && data.PostDate != "-" ? DateTime.ParseExact(data.PostDate, "dd/MM/yyyy", null) : (DateTime?)null,
                        ReferenceNo = data.Reference,
                        SlNO = data.SlNO
                    });

                    List<BankStatementEntryDTO> bankStatementEntryResultDTO = ClientFactory.AccountingServiceClient(CallContext).SaveBankStatementEntry(bankStatementEntryDTO);
                    if (bankStatementEntryResultDTO.Count() != 0)
                        return Json(new { ID = bankStatementEntryResultDTO[0].BankStatementEntryIID, RawHTML = string.Empty });
                }

            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception") ? ex.InnerException?.Message : ex.Message;

                Eduegate.Logger.LogHelper<string>.Fatal($"Exception in SaveBankStatementEntry(BankReconciliationBankTransViewModel data). Error message: {errorMessage}", ex);
                return Json(new
                {
                    ID = 0,
                    RawHTML = string.Empty
                });
            }
            return Json(new
            {
                ID = 0,
                RawHTML = string.Empty
            });
        }

        [HttpPost]
        public async Task<IActionResult> SaveUploadedExcelFiles(List<UploadedFileDetailsViewModel> files, Screens screen)
        {
            try
            {
                int dataFeedTypeID = 0;

                switch (screen)
                {
                    case Screens.BankReconcilation:
                        dataFeedTypeID = (int)DataFeedTypes.BankReconciliationOpening;
                        break;
                    case Screens.AssetManualEntry:
                        dataFeedTypeID = (int)DataFeedTypes.AssetOpening;
                        break;

                    default:
                        dataFeedTypeID = 1;
                        break;
                }

                DataFeedViewModel model = new DataFeedViewModel();

                model.FeedFileName = files[0].ContentFileName;
                model.StatusID = Services.Contracts.Enums.DataFeedStatus.InProcess;
                model.DataFeedTypeID = dataFeedTypeID;

                var dataFeedLogDTO = ClientFactory.DataFeedServiceClient(CallContext).SaveDataFeedLog(DataFeedViewModel.ToDTO(model));

                var dataFeedViewModel = DataFeedViewModel.FromDTO(dataFeedLogDTO, null);

                string returnHTML = await RenderViewToStringAsync("ProcessFeed", dataFeedViewModel, ControllerContext);

                return Json(new { ID = dataFeedViewModel.DataFeedID, RawHTML = returnHTML });
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception") ? ex.InnerException?.Message : ex.Message;

                Eduegate.Logger.LogHelper<string>.Fatal($"Exception in SaveUploadedExcelFiles(). Error message: {errorMessage}", ex);
                return Json(new { ID = 0, RawHTML = string.Empty });
            }
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

                string serverPath = _hostingEnvironment.WebRootPath;// "C:\\PEARL_CORE\\ParentPortal\\wwwroot\\Documents\\DataFeed";
                string dirPath = serverPath + @"/Documents/DataFeed/Reconc/";

                string fileName = dataFeedLogDTO.FileName;
                string fileLocation = dirPath + @"/" + fileName;

                dataFeedLogDTO = ProcessUpload(fileName, fileLocation, ID);
                var dataFeedViewModel = DataFeedViewModel.FromDTO(dataFeedLogDTO, null);
                string returnHTML = await RenderViewToStringAsync("ProcessFeed", dataFeedViewModel, ControllerContext);
                return Ok(new { ID = dataFeedViewModel.DataFeedID, RawHTML = returnHTML });
            }
            catch (Exception ex)
            {
                string message = "Data import was not successful!";
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception") ? ex.InnerException?.Message : ex.Message;

                LogHelper<string>.Fatal($"Exception in ProcessFeed(long ID). Error message: {errorMessage}", ex);
                return Ok(new { IsSuccess = isSuccessful, Message = message });
            }
        }
        [HttpPost]
        public async Task<IActionResult> SaveBankOpeningDetails(long ID, decimal? LedgerClosingBalInput, decimal? BankClosingBalInput, long BankAccountID, string FromDate, string ToDate)
        {
            try
            {
                var entry = ClientFactory.AccountingServiceClient(CallContext).SaveBankOpeningDetails(
                    new BankOpeningParametersDTO()
                    {
                        FeedLogID = ID,
                        LedgerClosingBalance = LedgerClosingBalInput,
                        BankClosingBalInput = BankClosingBalInput,
                        BankAccountID = BankAccountID,
                        SDate = DateTime.ParseExact(FromDate, "dd/MM/yyyy", null),
                        EDate = DateTime.ParseExact(ToDate, "dd/MM/yyyy", null),
                    });
                return Ok(new { ID = entry.BankReconciliationHeadIID, RawHTML = string.Empty });
            }
            catch (Exception ex)
            {
                var fields = " SDate: " + FromDate.ToString() + "EDate: " + ToDate.ToString() + "BankAccountID" + BankAccountID.ToString() + "FeedLogID" + ID;
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception") ? ex.InnerException?.Message : ex.Message;
                errorMessage = errorMessage + fields;
                LogHelper<string>.Fatal($"Exception in SaveBankOpeningDetails() in controller. Error message: {errorMessage}", ex);
                return Ok(new { ID = 0, RawHTML = string.Empty });
            }
        }

        private DataFeedLogDTO ProcessUpload(string fileName, string filePath, long feedLogID)
        {
            try
            {
                return ClientFactory.DataFeedServiceClient(CallContext).UploadFeedFile(fileName, feedLogID, filePath, null);
            }
            catch (Exception ex)
            {
                var fields = " fileName: " + fileName + "filePath: " + filePath + "FeedLogID" + feedLogID;
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception") ? ex.InnerException?.Message : ex.Message;
                errorMessage = errorMessage + fields;
                LogHelper<string>.Fatal($"Exception in SaveBankOpeningDetails(). Error message: {errorMessage}", ex);
                return new DataFeedLogDTO();
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
        public List<BankStatementEntryDTO> GetExtractedBankStatementEntry(List<BankTransDetailViewModel> extractDetail, List<RuleDTO> rulesList)
        {
            var bankStatementEntryDTO = new List<BankStatementEntryDTO>();
            try
            {
                bankStatementEntryDTO = extractDetail
                    .Where(x => !(string.IsNullOrEmpty(x.DebitAmount) && string.IsNullOrEmpty(x.CreditAmount) && string.IsNullOrEmpty(x.Balance)))
                    .Select(x =>
                    {
                        var entry = new BankStatementEntryDTO();
                        //if (x.Description.Contains("01001538,CHEQUE CASHED,CHEQUE DATE 07/04/24"))
                        //{
                        foreach (var rule in rulesList)
                        {
                            var property = entry.GetType().GetProperty(rule.ColumnName);
                            if (property != null && property.CanWrite)
                            {
                                object extractedValue = null;
                                var sourceValue = x.Description;

                                switch (rule.ColumnName)
                                {
                                    case string column when column.Contains("Description", StringComparison.OrdinalIgnoreCase):
                                        extractedValue = sourceValue;
                                        break;
                                    case string column when column.Contains("SlNO", StringComparison.OrdinalIgnoreCase):
                                        extractedValue = x.NO;
                                        break;

                                    case string column when column.Contains("Party", StringComparison.OrdinalIgnoreCase):

                                        extractedValue = ExtractPartyRef(sourceValue, rule.Pattern, rule.Expression);
                                        break;

                                    case string column when column.Contains("ChequeNo", StringComparison.OrdinalIgnoreCase):
                                        extractedValue = ExtractChequeNos(sourceValue, rule.Pattern, rule.Expression);
                                        break;

                                    case string column when column.Contains("Reference", StringComparison.OrdinalIgnoreCase):

                                        extractedValue = ExtractReference(sourceValue, rule.Pattern, rule.Expression);
                                        break;

                                    case string column when column.Contains("ChequeDate", StringComparison.OrdinalIgnoreCase):
                                        string dateStr = null;
                                        try
                                        {
                                            if (!string.IsNullOrEmpty(dateStr))
                                            {
                                                extractedValue = DateTime.ParseExact(ParseDateToString(dateStr), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception") ? ex.InnerException?.Message : ex.Message;
                                            errorMessage = errorMessage + "Description :" + sourceValue + "pattern" + rule.Pattern + " ChequeDate: " + dateStr;
                                            LogHelper<string>.Fatal($"Exception in GetExtractedBankStatementEntry(). Error message: {errorMessage}", ex);
                                            throw ex;
                                        }
                                        break;
                                    case string column when column.Contains("PostDate", StringComparison.OrdinalIgnoreCase):
                                        try
                                        {
                                            if (!string.IsNullOrEmpty(x.PostDate))
                                            {
                                                var parsedDate = ParseDateToString(x.PostDate);
                                                extractedValue = DateTime.ParseExact(parsedDate, "dd/MM/yyyy", null);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception") ? ex.InnerException?.Message : ex.Message;
                                            errorMessage = errorMessage + " postDate: " + x.PostDate + "Description :" + x.Description;
                                            LogHelper<string>.Fatal($"Exception in GetExtractedBankStatementEntry(). Error message: {errorMessage}", ex);
                                            throw ex;
                                        }

                                        break;
                                    default:
                                        if (rule.ColumnDataType?.Equals("decimal", StringComparison.OrdinalIgnoreCase) == true)
                                        {
                                            switch (rule.ColumnName)
                                            {
                                                case string column when column.Contains("Debit", StringComparison.OrdinalIgnoreCase):

                                                    extractedValue = ParseToNegativeDecimal(x.DebitAmount);
                                                    break;

                                                case string column when column.Contains("Credit", StringComparison.OrdinalIgnoreCase):

                                                    extractedValue = ParseToNegativeDecimal(x.CreditAmount);
                                                    break;

                                                case string column when column.Contains("Balance", StringComparison.OrdinalIgnoreCase):
                                                    extractedValue = ParseToNegativeDecimal(x.Balance);
                                                    break;

                                                default:
                                                    extractedValue = ExtractBasedOnRule(sourceValue, rule.Pattern, rule.Expression);
                                                    break;
                                            }
                                        }
                                        else
                                        {
                                            extractedValue = ExtractBasedOnRule(sourceValue, rule.Pattern, rule.Expression);
                                        }
                                        break;
                                }

                                //}

                                if (extractedValue != null && extractedValue != "")
                                {
                                    if (property.PropertyType == typeof(DateTime?) || property.PropertyType == typeof(DateTime))
                                    {
                                        property.SetValue(entry, Convert.ChangeType(extractedValue, typeof(DateTime)));
                                    }
                                    else if (property.PropertyType == typeof(decimal?))
                                    {
                                        property.SetValue(entry, Convert.ChangeType(extractedValue, typeof(decimal)));
                                    }
                                    else if (property.PropertyType == typeof(string))
                                    {
                                        property.SetValue(entry, extractedValue.ToString());
                                    }
                                    else if (property.PropertyType == typeof(int?) || property.PropertyType == typeof(Int64?) || property.PropertyType == typeof(Int32?))
                                    {
                                        if (extractedValue is int intValue)
                                        {

                                            if (property.PropertyType == typeof(int?))
                                            {
                                                property.SetValue(entry, (int?)intValue);
                                            }
                                            else if (property.PropertyType == typeof(Int64?))
                                            {
                                                property.SetValue(entry, (Int64?)intValue);
                                            }
                                            else if (property.PropertyType == typeof(Int32?))
                                            {
                                                property.SetValue(entry, (Int32?)intValue);
                                            }
                                            else if (property.PropertyType == typeof(long?))
                                            {
                                                property.SetValue(entry, (long?)intValue);
                                            }
                                        }
                                        else
                                        {
                                            property.SetValue(entry, null);
                                        }
                                    }
                                    else
                                    {
                                        property.SetValue(entry, extractedValue.ToString());
                                    }
                                }
                            }
                        }
                        //}

                        return entry;
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return bankStatementEntryDTO;
        }

        [HttpPost]
        public JsonResult SaveUploadedPdfFiles(List<UploadedFileDetailsViewModel> files, string FromDate, string ToDate, long BankAccountID)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            string[] formats = { "dd/MM/yy", "dd/MM/yyyy" };

            var bankReconcilationViewModel = new BankReconcilationViewModel();
            var bankReConciliationList = new List<BankReconciliationEntriesViewModel>();
            var dtos = UploadedFileDetailsViewModel.ToDocumentVM(files,
                Eduegate.Framework.Contracts.Common.Enums.EntityTypes.Others);
            try
            {
                foreach (var dto in dtos)
                {
                    if (dto.ReferenceID.HasValue)
                    {
                        var contentData = ClientFactory.ContentServicesClient(CallContext)
                            .ReadContentsById(dto.ReferenceID.Value);
                        dto.ContentData = contentData.ContentData;
                        bankReconcilationViewModel.ContentFileID = contentData.ContentFileIID.ToString();
                        bankReconcilationViewModel.ContentFileName = contentData.ContentFileName.ToString();
                    }
                }
                //DocumentFileViewModel.FromDTO(ClientFactory.DocumentServiceClient(CallContext)
                //    .SaveDocuments(dtos));

                var documentFileDTOs = ClientFactory.DocumentServiceClient(CallContext).SaveDocuments(dtos);
                if (documentFileDTOs.Count() > 0)
                {
                    var extractedHeader = JsonConvert.DeserializeObject<BankTransHeaders>(documentFileDTOs[0].ExtractedData1);

                    var jsonData = JsonConvert.DeserializeObject<Dictionary<string, List<Dictionary<string, string>>>>(documentFileDTOs[0].ExtractedData2);


                    var extractDetail = new List<BankTransDetailViewModel>();

                    var rulesList = GetBankRules(BankAccountID);
                    var index = 0;
                    if (rulesList.Count > 0)
                    {

                        var dataMatchingRules = rulesList
                        .Where(x => x.RuleTypeID == (int?)Eduegate.Services.Contracts.Enums.RuleTypes.BankDataMatching)
                        .OrderBy(x => x.RuleOrder)
                        .ToList();

                        if (dataMatchingRules.Any())
                        {
                            foreach (var table in jsonData.Values)
                            {
                                var transactions = table
                                    .Select(item =>
                                    {
                                        int nonNullValueCount = 0;
                                        foreach (var kvp in item)
                                        {
                                            if (!string.IsNullOrEmpty(kvp.Value))
                                            {
                                                nonNullValueCount = nonNullValueCount + 1;
                                            }
                                        }

                                        if (nonNullValueCount > 3)
                                        {
                                            index = index + 1;
                                            var transaction = new BankTransDetailViewModel();
                                            var transactionType = transaction.GetType();

                                            foreach (var rule in dataMatchingRules)
                                            {
                                                if (rule.ColumnName == "NO")
                                                {
                                                    var property = transactionType.GetProperty(rule.ColumnName);
                                                    if (property != null && property.CanWrite)
                                                    {
                                                        property.SetValue(transaction, index);
                                                    }
                                                }
                                                else
                                                {
                                                    var matchingValue = item.FirstOrDefault(kvp =>
                                                        kvp.Key.Contains(rule.Pattern, StringComparison.OrdinalIgnoreCase)).Value;

                                                    if (matchingValue != null)
                                                    {
                                                        var property = transactionType.GetProperty(rule.ColumnName);
                                                        if (property != null && property.CanWrite)
                                                        {
                                                            property.SetValue(transaction, matchingValue);
                                                        }
                                                    }
                                                }
                                            }

                                            return transaction;
                                        }

                                        return null; // Skip this item if condition is not met
                                    })
                                    .Where(transaction => transaction != null) // Filter out null values
                                    .ToList();

                                extractDetail.AddRange(transactions);
                            }
                        }
                        else
                        {
                            throw new Exception("Please set 'BankDataMatching' ruleset");
                        }

                    }
                    else
                        throw new Exception("Please set 'BankDataMatching' & 'BankExtractionMatching' ruleset");

                    var rules = rulesList
                     .Where(r => r.RuleTypeID == (int?)Eduegate.Services.Contracts.Enums.RuleTypes.BankExtractionMatching)
                     .OrderBy(r => r.RuleOrder)
                     .ToList();
                    if (rules.Count() == 0)
                        throw new Exception("Please set Bank Extraction Matching details");


                    var openingBalanceData = new BankStatementEntryDTO();
                    var closingBalanceData = new BankStatementEntryDTO();

                    var firstRecord = extractDetail
                         .FirstOrDefault();
                    var lastRecord = extractDetail
                       .LastOrDefault();

                    var firstRecordDate = DateTime.ParseExact(ParseDateToString(firstRecord.PostDate), "dd/MM/yyyy", null);
                    var lastRecordDate = DateTime.ParseExact(ParseDateToString(lastRecord.PostDate), "dd/MM/yyyy", null);

                    var bankStatementEntries = GetExtractedBankStatementEntry(extractDetail, rules).ToList();

                    if (bankStatementEntries != null && bankStatementEntries.Count > 0)
                    {
                        if (firstRecordDate.Date < lastRecordDate.Date)
                        {
                            openingBalanceData = bankStatementEntries.Where(x => x.PostDate == firstRecordDate && x.Description.ToLower() == firstRecord.Description.ToLower()).FirstOrDefault();
                            closingBalanceData = bankStatementEntries.Where(x => x.PostDate == lastRecordDate && x.Description.ToLower() == lastRecord.Description.ToLower()).FirstOrDefault();
                            bankReconcilationViewModel.BankStatementEntries = bankStatementEntries.OrderBy(x => x.SlNO).ThenBy(x => x.PostDate).ToList();
                        }
                        else
                        {
                            openingBalanceData = bankStatementEntries.Where(x => x.PostDate == lastRecordDate && x.Description.ToLower() == lastRecord.Description.ToLower()).FirstOrDefault();
                            closingBalanceData = bankStatementEntries.Where(x => x.PostDate == firstRecordDate && x.Description.ToLower() == firstRecord.Description.ToLower()).FirstOrDefault();
                            bankReconcilationViewModel.BankStatementEntries = bankStatementEntries.OrderByDescending(x => x.SlNO).ThenBy(x => x.PostDate).ToList();
                        }
                    }

                    var openingBalance = openingBalanceData.Balance - openingBalanceData.Credit + openingBalanceData.Debit;
                    var closingBalance = closingBalanceData.Balance;

                    var totalDebit = bankReconcilationViewModel.BankStatementEntries
                    .Sum(x => (x.Debit));

                    var totalCredit = bankReconcilationViewModel.BankStatementEntries
                        .Sum(x => (x.Credit));

                    //var calculatedClosingBalance = openingBalance + totalCredit -totalDebit ;
                    //if (calculatedClosingBalance == closingBalance)
                    //{                     
                    bankReconcilationViewModel.BankOpeningBalance = openingBalance;
                    bankReconcilationViewModel.BankClosingBalance = closingBalance;

                    var bankStatement = ClientFactory.AccountingServiceClient(CallContext).SaveBankStatement(
                        new BankStatementDTO()
                        {
                            BankStatementIID = bankReconcilationViewModel.BankStatementID.HasValue ? bankReconcilationViewModel.BankStatementID.Value : 0,
                            ContentFileID = bankReconcilationViewModel.ContentFileID,
                            ContentFileName = bankReconcilationViewModel.ContentFileName,
                            ExtractedTextData = bankReconcilationViewModel.ContentFileData,
                            BankStatementEntries = bankReconcilationViewModel.BankStatementEntries
                        });
                    bankReconcilationViewModel.BankStatementID = bankStatement.BankStatementIID;
                    bankReconcilationViewModel.BankReconciliationBankTrans = FillBankStatementData(bankStatement.BankStatementEntries);


                    var transactionDTO = new BankReconciliationDTO
                    {
                        //IBAN = extractedHeader.BankTransHeader[0].IBAN,
                        SDate = DateTime.ParseExact(FromDate, "dd/MM/yyyy", null),
                        EDate = DateTime.ParseExact(ToDate, "dd/MM/yyyy", null),
                        BankAccountID = BankAccountID
                    };

                    bankReconcilationViewModel.FromDateString = FromDate;
                    bankReconcilationViewModel.ToDateString = ToDate;
                    bankReconcilationViewModel.FromDate = DateTime.ParseExact(FromDate, "dd/MM/yyyy", null);
                    bankReconcilationViewModel.ToDate = DateTime.ParseExact(ToDate, "dd/MM/yyyy", null);
                    var ledgerData = FillLedgerData(transactionDTO);
                    bankReconcilationViewModel.BankReconciliationLedgerTrans = ledgerData.BankReconciliationLedgerTrans;
                    bankReconcilationViewModel.LedgerOpeningBalance = ledgerData?.LedgerOpeningBalance ?? 0;
                    bankReconcilationViewModel.LedgerClosingBalance = ledgerData?.LedgerClosingBalance ?? 0;
                    bankReconcilationViewModel.BankAccountID = ledgerData?.BankAccountID ?? 0;
                    bankReconcilationViewModel.BankName = ledgerData?.BankName;
                    bankReconcilationViewModel.ContentFileData = documentFileDTOs[0].ExtractedData2;

                    var bankStatementEntryDTO = new List<BankStatementEntryDTO>();
                    bankReconcilationViewModel = GettingMatchedAndUnMatchedLedgerEntries(bankReconcilationViewModel);

                    bankReconcilationViewModel.BankReconciliationMatchingLedgerEntries = bankReconcilationViewModel.BankReconciliationLedgerTrans
                    .Where(ledgerTrans => !bankReconcilationViewModel.BankReconciliationMatchedEntries.Any(matchedEntry =>
                     matchedEntry.AccountID == ledgerTrans.AccountID &&
                     matchedEntry.Particulars == ledgerTrans.Narration &&
                     matchedEntry.Reference == ledgerTrans.Reference &&
                     matchedEntry.LedgerDebitAmount == ledgerTrans.LedgerDebitAmount &&
                     matchedEntry.LedgerCreditAmount == ledgerTrans.LedgerCreditAmount))
                    .ToList();

                    bankReconcilationViewModel.BankReconciliationMatchingBankTransEntries = bankReconcilationViewModel.BankReconciliationBankTrans
                  .Where(bankTrans => !bankReconcilationViewModel.BankReconciliationMatchedEntries.Any(matchedEntry =>
                   matchedEntry.BankStatementEntryID == bankTrans.BankStatementEntryID &&
                   matchedEntry.BankDescription == bankTrans.Narration))
                  .ToList();

                    bankReconcilationViewModel.BankReconciliationHeadIID = long.Parse(SaveBankReconciliationEntries(bankReconcilationViewModel));

                }

            }
            catch (Exception ex)
            {
                var exceptionMessage = ex.Message;
                var fields = " FromDate: " + FromDate.ToString() + "ToDate: " + ToDate.ToString();
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception") ? ex.InnerException?.Message : ex.Message;
                errorMessage = errorMessage + fields;
                Eduegate.Logger.LogHelper<string>.Fatal($"Exception in SaveUploadedPdfFiles(List<UploadedFileDetailsViewModel> files, string FromDate, string ToDate, long BankAccountID). Error message: {errorMessage}", ex);
                return Json(new { Success = false, Data = bankReconcilationViewModel, Message = ex.Message });
            }
            return Json(new { Success = true, Data = bankReconcilationViewModel, Message = "Success" });
        }


        public BankReconcilationViewModel FillLedgerData(BankReconciliationDTO transactionDTO)
        {
            var bankReconcilationViewModel = new BankReconcilationViewModel();
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");
            try
            {
                var entry = ClientFactory.AccountingServiceClient(CallContext).FillBankTransctions(transactionDTO);

                var bankReconciliationLedgerTrans = (from s in entry.BankReconciliationTransactionDtos
                                                     select new BankReconciliationLedgerTransViewModel
                                                     {
                                                         AccountID = s.AccountID,
                                                         AccountName = s.AccountName,
                                                         PostDate = s.TransDate.HasValue ? s.TransDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                                                         LedgerCreditAmount = s.Credit,
                                                         LedgerDebitAmount = s.Debit,
                                                         Narration = s.Narration,
                                                         ChequeNo = s.ChequeNo,
                                                         PartyName = s.PartyName,
                                                         Reference = s.Reference,
                                                         ChequeDate = s.ChequeDate.HasValue ? s.ChequeDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                                                         TranHeadID = s.TranHeadID,
                                                         TranTailID = s.TranTailID,
                                                     }).ToList();

                if (entry != null && entry.BankReconciliationTransactionDtos.Count() > 0)
                {
                    bankReconcilationViewModel.LedgerOpeningBalance = entry?.BankReconciliationTransactionDtos[0]?.OpeningBalance;
                    bankReconcilationViewModel.LedgerClosingBalance = entry?.BankReconciliationTransactionDtos[0]?.ClosingBalance;
                }
                bankReconcilationViewModel.BankAccountID = entry?.BankAccountID;
                bankReconcilationViewModel.BankName = entry?.BankName;
                bankReconcilationViewModel.BankReconciliationLedgerTrans = bankReconciliationLedgerTrans;
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception") ? ex.InnerException?.Message : ex.Message;
                LogHelper<string>.Fatal($"Exception in FillLedgerData(). Error message: {errorMessage}", ex);
                return new BankReconcilationViewModel();
            }

            return bankReconcilationViewModel;
        }
        public static decimal ParseToNegativeDecimal(string input)
        {

            if (string.IsNullOrEmpty(input))
                return 0;
            input = input.Replace(",", "").Trim();


            if (input == "-")
                return 0;
            if (input.StartsWith("(") && input.EndsWith(")"))
            {

                input = input.Substring(1, input.Length - 2);


                if (decimal.TryParse(input, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal value))
                {
                    return -value;
                }
            }
            else if (input.EndsWith("-"))
            {

                input = input.Substring(0, input.Length - 2);


                if (decimal.TryParse(input, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal value))
                {
                    return -value;
                }
            }
            else
            {

                if (decimal.TryParse(input, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal value))
                {
                    return value;
                }
            }


            return 0;
        }

        private string ParseDateToString(string postDate)
        {
            try
            {
                postDate = postDate.Trim();
                var postDateString = postDate;
                //var match = Regex.Match(postDate, @"^(\d{2})(\d{2})(?=[/-])");
                var match = Regex.Match(postDate, @"^(\d{3,})([/-])");

                if (match.Success)
                {
                    
                    string lastTwoDigits = match.Groups[1].Value[^2..]; 
                    string separator = match.Groups[2].Value;

                    postDate= lastTwoDigits + separator + postDate[(match.Length)..];
                }
               
                //var match = Regex.Match(postDate, @"-?\d*(?=(\d{2}/\d{2}/\d{2}))");

                //if (match.Success)
                //{
                //    if (!string.IsNullOrEmpty(match.Value))
                //    {
                //        postDate = postDate.Replace(match.Value, "").TrimStart('-').Trim();
                //    }
                //}

                DateTime parsedDate;
                //if (postDate == "32/12/24")
                //{

                //}
                //if (DateTime.TryParseExact(postDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
                //{
                //    return parsedDate.ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                //}
                if (DateTime.TryParseExact(postDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
                {
                    return parsedDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                }

                else if (DateTime.TryParseExact(postDate, "dd/MM/yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
                {
                    return parsedDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                }

                else if (DateTime.TryParseExact(postDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
                {
                    return parsedDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                }

                else if (DateTime.TryParseExact(postDate, "dd-MM-yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
                {
                    return parsedDate.ToString("dd/MM/yyyy", null);
                }
                else if (DateTime.TryParseExact(postDate, "dd-MMM-yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
                {
                    return parsedDate.ToString("dd/MM/yyyy", null);
                }
                else if (DateTime.TryParseExact(postDate, "dd-MMM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
                {
                    return parsedDate.ToString("dd/MM/yyyy", null);
                }
                else
                {
                    throw new System.FormatException($"Invalid date format: {postDate}");
                }
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception") ? ex.InnerException?.Message : ex.Message;
                errorMessage = errorMessage + " postDate: " + postDate;
                LogHelper<string>.Fatal($"Exception in ParseDateToString(). Error message: {errorMessage}", ex);
                throw ex;
            };
        }
        public static decimal ConvertStringToDecimal(string input)
        {

            input = input.Trim();

            bool isNegative = false;


            if (input.StartsWith("(") && input.EndsWith(")"))
            {
                isNegative = true;
                input = input.Substring(1, input.Length - 2);
            }


            decimal parsedValue = 0;
            if (decimal.TryParse(input, NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out parsedValue))
            {
                return isNegative ? -parsedValue : parsedValue;
            }

            return parsedValue;
        }
        public BankReconcilationViewModel GettingMatchedAndUnMatchedLedgerEntries(BankReconcilationViewModel bankReconcilationViewModel)
        {
            var unMatchedLedgerEntriesList = new List<BankReconciliationUnMatchedWithLedgerEntriesViewModel>();
            var matchedList = new List<BankReconciliationMatchedEntriesViewModel>();
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            try
            {
                foreach (var bankTrans in bankReconcilationViewModel.BankStatementEntries)
                {
                    bool matchFound = false;
                    foreach (var ledgerTrans in bankReconcilationViewModel.BankReconciliationLedgerTrans)
                    {
                        matchFound = false;

                        //if (Math.Abs(Math.Abs(bankTrans.Debit.Value) - Math.Abs(bankTrans.Credit.Value)) == Math.Abs(Math.Abs(ledgerTrans.LedgerDebitAmount.Value) - Math.Abs(ledgerTrans.LedgerCreditAmount.Value)))

                        if (bankTrans.Debit == ledgerTrans.LedgerCreditAmount && bankTrans.Credit.Value == ledgerTrans.LedgerDebitAmount.Value)
                        {

                            var chequeNo = string.Empty;

                            if (!string.IsNullOrEmpty(ledgerTrans.ChequeNo) && ExtractDetails(bankTrans.Description, ledgerTrans.ChequeNo))
                            {
                                if (string.IsNullOrEmpty(bankTrans.ChequeNo))
                                    bankTrans.ChequeNo = ledgerTrans.ChequeNo;
                                matchFound = true;
                            }
                            if (!string.IsNullOrEmpty(ledgerTrans.PartyName) && ExtractDetails(bankTrans.Description, ledgerTrans.PartyName))
                            {
                                if (string.IsNullOrEmpty(bankTrans.PartyName))
                                    bankTrans.PartyName = ledgerTrans.PartyName;
                                matchFound = true;
                            }
                            if (!string.IsNullOrEmpty(ledgerTrans.Reference) && ExtractDetails(bankTrans.Description, ledgerTrans.Reference))
                            {
                                if (string.IsNullOrEmpty(bankTrans.ReferenceNo))
                                    bankTrans.ReferenceNo = ledgerTrans.Reference;
                                matchFound = true;
                            }
                            if (!string.IsNullOrEmpty(ledgerTrans.ChequeDate) && ExtractDetails(bankTrans.Description, ledgerTrans.ChequeDate))
                            {
                                if (!bankTrans.ChequeDate.HasValue)
                                    bankTrans.ChequeDate = ExtractAndFormatDate(ledgerTrans.ChequeDate);
                                matchFound = true;
                            }
                            if (matchFound == false)
                                matchFound = CheckForCommonLetters(bankTrans.Description, ledgerTrans.Narration);

                            if (matchFound)
                            {
                                if (!matchedList.Any(x => x.Particulars == ledgerTrans.Narration && x.BankDescription == bankTrans.Description))
                                {
                                    var bankReconciliationMatchedEntry = new BankReconciliationMatchedEntriesViewModel
                                    {
                                        BankDebitAmount = bankTrans.Debit,
                                        BankCreditAmount = bankTrans.Credit,
                                        Particulars = ledgerTrans.Narration,
                                        BankDescription = bankTrans.Description,
                                        PostDate = bankTrans.PostDate?.ToString(dateFormat, CultureInfo.InvariantCulture),
                                        TransDate = ledgerTrans.PostDate,
                                        LedgerDebitAmount = ledgerTrans.LedgerDebitAmount,
                                        LedgerCreditAmount = ledgerTrans.LedgerCreditAmount,
                                        TranHeadID = ledgerTrans.TranHeadID,
                                        TranTailID = ledgerTrans.TranTailID,
                                        ChequeDate = ledgerTrans.ChequeDate,
                                        ChequeNo = ledgerTrans.ChequeNo,
                                        PartyName = ledgerTrans.PartyName,
                                        Reference = ledgerTrans.Reference,
                                        AccountID = ledgerTrans.AccountID,
                                        BankStatementEntryID = bankTrans.BankStatementEntryIID
                                    };
                                    bankReconciliationMatchedEntry.MatchedBankEntries = new List<MatchedBankEntries>();
                                    bankReconciliationMatchedEntry.MatchedBankEntries.Add(new MatchedBankEntries
                                    {
                                        BankDebitAmount = bankTrans.Debit,
                                        BankCreditAmount = bankTrans.Credit,
                                        BankDescription = bankTrans.Description,
                                        PostDate = bankTrans.PostDate?.ToString(dateFormat, CultureInfo.InvariantCulture),
                                        ChequeDate = bankTrans.ChequeDate?.ToString(dateFormat, CultureInfo.InvariantCulture),
                                        ChequeNo = bankTrans.ChequeNo,
                                        PartyName = bankTrans.PartyName,
                                        Reference = bankTrans.ReferenceNo,
                                        BankStatementEntryID = bankTrans.BankStatementEntryIID
                                    });
                                    bankReconciliationMatchedEntry.MatchedLedgerEntries = new List<MatchedLedgerEntries>();
                                    bankReconciliationMatchedEntry.MatchedLedgerEntries.Add(new MatchedLedgerEntries
                                    {
                                        Particulars = ledgerTrans.Narration,
                                        TransDate = ledgerTrans.PostDate,
                                        LedgerDebitAmount = ledgerTrans.LedgerDebitAmount,
                                        LedgerCreditAmount = ledgerTrans.LedgerCreditAmount,
                                        TranHeadID = ledgerTrans.TranHeadID,
                                        TranTailID = ledgerTrans.TranTailID,
                                        ChequeDate = ledgerTrans.ChequeDate,
                                        ChequeNo = ledgerTrans.ChequeNo,
                                        PartyName = ledgerTrans.PartyName,
                                        Reference = ledgerTrans.Reference,
                                        AccountID = ledgerTrans.AccountID
                                    });
                                    matchedList.Add(bankReconciliationMatchedEntry);
                                    break;
                                }

                            }
                        }
                    }

                    if (!matchFound)
                    {
                        if (!string.IsNullOrEmpty(bankTrans.Description) && !(bankTrans.Credit == 0 && bankTrans.Debit == 0 && bankTrans.Balance == 0))
                        {
                            unMatchedLedgerEntriesList.Add(new BankReconciliationUnMatchedWithLedgerEntriesViewModel
                            {
                                BankDebitAmount = bankTrans.Debit,
                                BankCreditAmount = bankTrans.Credit,
                                Particulars = bankTrans.Description,
                                PostDate = bankTrans.PostDate?.ToString(dateFormat, CultureInfo.InvariantCulture),
                                LedgerDebitAmount = 0,
                                LedgerCreditAmount = 0,
                                ReconciliationDebitAmount = bankTrans.Credit == 0 && bankTrans.Debit == 0 && bankTrans.Balance != 0 ? bankTrans.Balance < 0 ? bankTrans.Balance : 0 : bankTrans.Credit,
                                ReconciliationCreditAmount = bankTrans.Credit == 0 && bankTrans.Debit == 0 && bankTrans.Balance != 0 ? bankTrans.Balance > 0 ? bankTrans.Balance : 0 : bankTrans.Debit,
                                ChequeDate = bankTrans.ChequeDate?.ToString(dateFormat, CultureInfo.InvariantCulture),
                                ChequeNo = bankTrans.ChequeNo,
                                PartyName = bankTrans.PartyName,
                                Reference = bankTrans.ReferenceNo,
                                AccountID = 0,
                                BankStatementEntryID = bankTrans.BankStatementEntryIID,

                            });
                        }
                    }

                }
                bankReconcilationViewModel.BankReconciliationMatchedEntries = matchedList;
                bankReconcilationViewModel.BankReconciliationUnMatchedWithBankEntries = GettingUnMatchedBankEntries(bankReconcilationViewModel);

                bankReconcilationViewModel.BankReconciliationUnMatchedWithLedgerEntries = unMatchedLedgerEntriesList;
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception") ? ex.InnerException?.Message : ex.Message;
                LogHelper<string>.Fatal($"Exception in GettingMatchedAndUnMatchedLedgerEntries(). Error message: {errorMessage}", ex);
                return new BankReconcilationViewModel();
            }
            return bankReconcilationViewModel;
        }
        private List<BankReconciliationUnMatchedWithBankEntriesViewModel> GettingUnMatchedBankEntries(BankReconcilationViewModel bankReconcilationViewModel)
        {
            var unMatchedBankEntriesList = new List<BankReconciliationUnMatchedWithBankEntriesViewModel>();
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            try
            {
                foreach (var ledgerTrans in bankReconcilationViewModel.BankReconciliationLedgerTrans)
                {
                    bool matchFound = false;

                    foreach (var bankTrans in bankReconcilationViewModel.BankStatementEntries)
                    {
                        matchFound = false;
                        //if (Math.Abs(Math.Abs(bankTrans.Debit.Value) - Math.Abs(bankTrans.Credit.Value)) == Math.Abs(Math.Abs(ledgerTrans.LedgerDebitAmount.Value) - Math.Abs(ledgerTrans.LedgerCreditAmount.Value)))

                        if (bankTrans.Debit == ledgerTrans.LedgerCreditAmount && bankTrans.Credit.Value == ledgerTrans.LedgerDebitAmount.Value)
                        {
                            if (!string.IsNullOrEmpty(ledgerTrans.ChequeNo) && ExtractDetails(bankTrans.Description, ledgerTrans.ChequeNo))
                            {
                                if (string.IsNullOrEmpty(bankTrans.ChequeNo))
                                    bankTrans.ChequeNo = ledgerTrans.ChequeNo;
                                matchFound = true;
                            }
                            if (!string.IsNullOrEmpty(ledgerTrans.PartyName) && ExtractDetails(bankTrans.Description, ledgerTrans.PartyName))
                            {
                                if (string.IsNullOrEmpty(bankTrans.PartyName))
                                    bankTrans.PartyName = ledgerTrans.PartyName;
                                matchFound = true;
                            }
                            if (!string.IsNullOrEmpty(ledgerTrans.Reference) && ExtractDetails(bankTrans.Description, ledgerTrans.Reference))
                            {
                                if (string.IsNullOrEmpty(bankTrans.ReferenceNo))
                                    bankTrans.ReferenceNo = ledgerTrans.Reference;
                                matchFound = true;
                            }
                            if (!string.IsNullOrEmpty(ledgerTrans.ChequeDate) && ExtractDetails(bankTrans.Description, ledgerTrans.ChequeDate))
                            {
                                if (!bankTrans.ChequeDate.HasValue)
                                    bankTrans.ChequeDate = ExtractAndFormatDate(ledgerTrans.ChequeDate);
                                matchFound = true;
                            }
                            if (matchFound != false)
                                matchFound = CheckForCommonLetters(bankTrans.Description, ledgerTrans.Narration);

                            if (matchFound)
                            {
                                if (!bankReconcilationViewModel.BankReconciliationMatchedEntries.Any(x => x.Particulars == ledgerTrans.Narration && x.BankDescription == bankTrans.Description))
                                {
                                    var bankReconciliationMatchedEntry = new BankReconciliationMatchedEntriesViewModel
                                    {
                                        BankDebitAmount = bankTrans.Debit,
                                        BankCreditAmount = bankTrans.Credit,
                                        Particulars = ledgerTrans.Narration,
                                        BankDescription = bankTrans.Description,
                                        PostDate = bankTrans.PostDate?.ToString(dateFormat, CultureInfo.InvariantCulture),
                                        TransDate = ledgerTrans.PostDate,
                                        LedgerDebitAmount = ledgerTrans.LedgerDebitAmount,
                                        LedgerCreditAmount = ledgerTrans.LedgerCreditAmount,
                                        TranHeadID = ledgerTrans.TranHeadID,
                                        TranTailID = ledgerTrans.TranTailID,
                                        ChequeDate = ledgerTrans.ChequeDate,
                                        ChequeNo = ledgerTrans.ChequeNo,
                                        PartyName = ledgerTrans.PartyName,
                                        Reference = ledgerTrans.Reference,
                                        AccountID = ledgerTrans.AccountID,
                                        BankStatementEntryID = bankTrans.BankStatementEntryIID
                                    };
                                    bankReconciliationMatchedEntry.MatchedBankEntries = new List<MatchedBankEntries>();
                                    bankReconciliationMatchedEntry.MatchedBankEntries.Add(new MatchedBankEntries
                                    {
                                        BankDebitAmount = bankTrans.Debit,
                                        BankCreditAmount = bankTrans.Credit,
                                        BankDescription = bankTrans.Description,
                                        PostDate = bankTrans.PostDate?.ToString(dateFormat, CultureInfo.InvariantCulture),
                                        ChequeDate = bankTrans.ChequeDate?.ToString(dateFormat, CultureInfo.InvariantCulture),
                                        ChequeNo = bankTrans.ChequeNo,
                                        PartyName = bankTrans.PartyName,
                                        Reference = bankTrans.ReferenceNo,
                                        BankStatementEntryID = bankTrans.BankStatementEntryIID
                                    });
                                    bankReconciliationMatchedEntry.MatchedLedgerEntries = new List<MatchedLedgerEntries>();
                                    bankReconciliationMatchedEntry.MatchedLedgerEntries.Add(new MatchedLedgerEntries
                                    {
                                        Particulars = ledgerTrans.Narration,
                                        TransDate = ledgerTrans.PostDate,
                                        LedgerDebitAmount = ledgerTrans.LedgerDebitAmount,
                                        LedgerCreditAmount = ledgerTrans.LedgerCreditAmount,
                                        TranHeadID = ledgerTrans.TranHeadID,
                                        TranTailID = ledgerTrans.TranTailID,
                                        ChequeDate = ledgerTrans.ChequeDate,
                                        ChequeNo = ledgerTrans.ChequeNo,
                                        PartyName = ledgerTrans.PartyName,
                                        Reference = ledgerTrans.Reference,
                                        AccountID = ledgerTrans.AccountID
                                    });
                                    bankReconcilationViewModel.BankReconciliationMatchedEntries.Add(bankReconciliationMatchedEntry);
                                    break;

                                }
                            }
                        }

                    }

                    if (!matchFound)
                    {
                        if (!string.IsNullOrEmpty(ledgerTrans.Narration) && !(ledgerTrans.LedgerDebitAmount == 0 && ledgerTrans.LedgerCreditAmount == 0))
                        {
                            unMatchedBankEntriesList.Add(new BankReconciliationUnMatchedWithBankEntriesViewModel
                            {
                                BankDebitAmount = 0,
                                BankCreditAmount = 0,
                                Particulars = ledgerTrans.Narration,
                                TransDate = ledgerTrans.PostDate,
                                LedgerDebitAmount = ledgerTrans.LedgerDebitAmount,
                                LedgerCreditAmount = ledgerTrans.LedgerCreditAmount,
                                BankDescription = string.Empty,
                                ReconciliationDebitAmount = ledgerTrans.LedgerCreditAmount,
                                ReconciliationCreditAmount = ledgerTrans.LedgerDebitAmount,
                                TranHeadID = ledgerTrans.TranHeadID,
                                TranTailID = ledgerTrans.TranTailID,
                                ChequeDate = ledgerTrans.ChequeDate,
                                ChequeNo = ledgerTrans.ChequeNo,
                                PartyName = ledgerTrans.PartyName,
                                Reference = ledgerTrans.Reference,
                                BankStatementEntryID = 0
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception") ? ex.InnerException?.Message : ex.Message;
                LogHelper<string>.Fatal($"Exception in GettingUnMatchedBankEntries(). Error message: {errorMessage}", ex);
                return new List<BankReconciliationUnMatchedWithBankEntriesViewModel>();
            }
            return unMatchedBankEntriesList;
        }

        public static bool HasCommonWords(string description1, string description2)
        {
            var check = false;
            HashSet<string> words1 = new HashSet<string>(description1
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Where(word => word.Length > 3)
                .Select(word => word.ToLower()));

            HashSet<string> words2 = new HashSet<string>(description2
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Where(word => word.Length > 3)
                .Select(word => word.ToLower()));


            return words1.Intersect(words2).Any();
        }
        bool HasMatchingDigits(BankReconciliationLedgerTransViewModel ledgerTrans, BankReconciliationBankTransViewModel bankTrans)
        {
            //var digits1 = new string(narration1.Where(char.IsDigit).ToArray());
            //var digits2 = new string(narration2.Where(char.IsDigit).ToArray());
            //return digits1.Contains(digits2) || digits2.Contains(digits1);

            if (!string.IsNullOrEmpty(ledgerTrans.ChequeNo))
            {

                if (bankTrans.Narration.Contains(ledgerTrans.ChequeNo) || (!string.IsNullOrEmpty(bankTrans.ChequeNo) && bankTrans.ChequeNo.Contains(ledgerTrans.ChequeNo)))
                {
                    return true;
                }

            }
            else if (bankTrans.Narration.Contains(ledgerTrans.PartyName) || (!string.IsNullOrEmpty(bankTrans.PartyName) && bankTrans.PartyName.Contains(ledgerTrans.PartyName)))
            {
                return true;
            }
            else if (bankTrans.Reference.Contains(ledgerTrans.Reference) || (!string.IsNullOrEmpty(bankTrans.Reference) && bankTrans.PartyName.Contains(ledgerTrans.Reference)))
            {
                return true;
            }
            return false;
        }

        private void SaveBankOpeningDetails(BankOpeningParametersDTO transactionDTO)
        {
            try
            {
                var entry = ClientFactory.AccountingServiceClient(CallContext).SaveBankOpeningDetails(transactionDTO);
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception") ? ex.InnerException?.Message : ex.Message;
                LogHelper<string>.Fatal($"Exception in SaveBankOpeningDetails(). Error message: {errorMessage}", ex);
                throw ex;
            }
        }

        public List<BankReconciliationBankTransViewModel> FillBankStatementData(List<BankStatementEntryDTO> extractDetail)
        {
            var bankReconciliationBankTransViewModel = new List<BankReconciliationBankTransViewModel>();
            try
            {
                var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
                if (extractDetail.Any())
                {
                    foreach (var x in extractDetail)
                    {
                        bankReconciliationBankTransViewModel.Add(new BankReconciliationBankTransViewModel()
                        {
                            Narration = x.Description,
                            PostDate = x.PostDate.HasValue ? x.PostDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                            BankCreditAmount = x.Credit,
                            BankDebitAmount = x.Debit,
                            Balance = x.Balance,
                            ChequeNo = x.ChequeNo,
                            PartyName = x.PartyName,
                            Reference = x.ReferenceNo,
                            ChequeDate = x.ChequeDate.HasValue ? x.ChequeDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                            BankStatementID = x.BankStatementID,
                            BankStatementEntryID = x.BankStatementEntryIID,
                            SlNO = x.SlNO
                        });

                    }
                }
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception") ? ex.InnerException?.Message : ex.Message;
                LogHelper<string>.Fatal($"Exception in FillBankStatementData(). Error message: {errorMessage}", ex);
                return new List<BankReconciliationBankTransViewModel>();
            }
            return bankReconciliationBankTransViewModel;
        }
        public List<RuleDTO> GetBankRules(long? bankAccountID)
        {
            var bankReconciliationBankTransViewModel = new List<BankReconciliationBankTransViewModel>();
            try
            {
                var entry = ClientFactory.AccountingServiceClient(CallContext).GetBankRules(bankAccountID);
                return entry;
            }
            catch (Exception ex)
            {
                var fields = "BankAccountID" + bankAccountID.ToString();
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception") ? ex.InnerException?.Message : ex.Message;
                errorMessage = errorMessage + fields;
                LogHelper<string>.Fatal($"Exception in GetBankRules() in controller. Error message: {errorMessage}", ex);
                return null;
            }
        }

        public static string ExtractChequeNos(string description)
        {
            string pattern = @"(\d{3,})";
            string chequeNo = string.Empty;

            if (description.IndexOf("CHEQUE", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                MatchCollection matches = Regex.Matches(description, pattern);
                foreach (Match match in matches)
                {
                    chequeNo = match.Value;
                    break;
                }
            }
            return chequeNo;
        }
        private static string ExtractChequeNos(string description, string pattern, string expression)
        {
            string normalizedPattern = pattern.Replace(@"\\", @"\");
            //string normalizedPattern = Regex.Unescape(pattern);
            if (!string.IsNullOrEmpty(expression) &&
                description.IndexOf(expression, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                var match = Regex.Match(description, normalizedPattern);
                return match.Success ? match.Value : string.Empty;
            }
            return string.Empty;
        }
        public static string ExtractPartyRef(string description, string pattern, string expression)
        {
            string normalizedPattern = !string.IsNullOrEmpty(pattern) ? Regex.Unescape(pattern) : string.Empty;
            string normalizedExpression = !string.IsNullOrEmpty(expression) ? Regex.Unescape(expression) : string.Empty;

            if (!string.IsNullOrEmpty(normalizedPattern))
            {
                var match = Regex.Match(description, normalizedPattern, RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    return match.Value.Trim();
                }
            }

            if (!string.IsNullOrEmpty(normalizedExpression))
            {
                var match = Regex.Match(description, normalizedExpression, RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    return match.Value.Trim();
                }
            }

            return string.Empty;
        }

        public static string ExtractReference(string description, string pattern, string expression)
        {

            string normalizedPattern = !string.IsNullOrEmpty(pattern) ? Regex.Unescape(pattern) : string.Empty;
            string normalizedExpression = !string.IsNullOrEmpty(expression) ? Regex.Unescape(expression) : string.Empty;


            if (!string.IsNullOrEmpty(normalizedExpression) &&
                description.IndexOf(normalizedExpression, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                var matches = Regex.Matches(description, normalizedPattern, RegexOptions.IgnoreCase);

                if (matches.Count > 1)
                {
                    return matches[1].Value.Trim();
                }
            }

            if (!string.IsNullOrEmpty(normalizedPattern))
            {
                var match = Regex.Match(description, normalizedPattern, RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    return match.Value.Trim();
                }
            }

            return string.Empty;
        }
        public static bool ExtractDetails(string description, string searchvalue)
        {
            if (description.IndexOf(searchvalue, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                Match match = Regex.Match(description, searchvalue);
                if (match.Success)
                {

                    return true;
                }
            }

            return false;
        }
        public static DateTime? ExtractAndFormatDate(string input)
        {

            string pattern = @"(?:CHEQUE \s*[\n,]*)?(\d{2}[-/]\d{2}[-/]\d{2,4})";
            var match = Regex.Match(input, pattern, RegexOptions.IgnoreCase);

            if (match.Success)
            {
                string dateStr = match.Groups[1].Value;

                string[] dateFormats = { "dd/MM/yy", "dd/MM/yyyy", "dd-MM-yyyy", "dd-MM-yy" };
                if (DateTime.TryParseExact(dateStr, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
                {
                    if (date.Year < 100)
                    {
                        int year = date.Year < 50 ? 2000 + date.Year : 1900 + date.Year;
                        date = new DateTime(date.Year, date.Month, date.Day);
                    }
                    return date;
                }

            }

            return (DateTime?)null;
        }

        public static string ExtractAndFormatDate(string input, string rawPattern)
        {

            string pattern = rawPattern.Replace(@"\\", @"\");
            //string pattern = Regex.Unescape(rawPattern);
            try
            {
                var match = Regex.Match(input, pattern, RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    string dateStr = match.Groups[1].Value;
                    string[] dateFormats = { "dd/MM/yy", "dd/MM/yyyy", "dd-MM-yyyy", "dd-MM-yy" };

                    if (DateTime.TryParseExact(dateStr, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
                    {
                        // Handle two-digit years
                        if (date.Year < 100)
                        {
                            int year = date.Year < 50 ? 2000 + date.Year : 1900 + date.Year;
                            date = new DateTime(year, date.Month, date.Day);
                        }
                        return date.ToString("dd/MM/yyyy");
                    }
                }
            }
            catch (Exception ex)
            {
                var fields = "input" + input + "rawPattern" + rawPattern;
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception") ? ex.InnerException?.Message : ex.Message;
                errorMessage = errorMessage + fields;
                LogHelper<string>.Fatal($"Exception in ExtractAndFormatDate(string input, string rawPattern) in controller. Error message: {errorMessage}", ex);
                return null;
            }
            return null;
        }

        public static bool CheckForCommonLetters(string text1, string text2)
        {
            var minCommonLetters = 4;
            text1 = RemoveNonAlphanumeric(text1.ToLower());
            text2 = RemoveNonAlphanumeric(text2.ToLower());
            var substringsText1 = GetSubstrings(text1, minCommonLetters);
            var substringsText2 = GetSubstrings(text2, minCommonLetters);
            var commonSubstrings = substringsText1.Intersect(substringsText2).ToList();

            return commonSubstrings.Any();
        }

        private static HashSet<string> GetSubstrings(string text, int minLength)
        {
            var substrings = new HashSet<string>();
            for (int i = 0; i <= text.Length - minLength; i++)
            {
                for (int j = minLength; j <= text.Length - i; j++)
                {
                    substrings.Add(text.Substring(i, j));
                }
            }
            return substrings;
        }

        private static string RemoveNonAlphanumeric(string text)
        {
            return new string(text.Where(char.IsLetterOrDigit).ToArray());
        }

        public static string ExtractBasedOnRule(string description, string pattern, string expression)
        {
            if (!string.IsNullOrEmpty(expression) &&
                description.IndexOf(expression, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                var match = Regex.Match(description, pattern);
                return match.Success ? match.Value : string.Empty;
            }
            return string.Empty;
        }

        [HttpGet]
        public ActionResult Configure(long IID = 0)
        {
            return View(IID);
        }

        [HttpPost]
        public ActionResult SaveBankReconciliationMatchedEntry([FromBody] BankReconcilationViewModel bankReconcilationViewModel)//[FromBody] BankReconcilationViewModel bankReconcilationViewModel)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            string[] formats = { "dd/MM/yy", "dd/MM/yyyy" };
            DateTime parsedDate;

            try
            {               
                // BankReconcilationViewModel bankReconcilationViewModel = new BankReconcilationViewModel();

                //if (!string.IsNullOrEmpty(bankReconciliationString))
                //{
                //    bankReconcilationViewModel = JsonConvert.DeserializeObject<BankReconcilationViewModel>(bankReconciliationString);
                //}

                var bankReconciliationHeadDTO = new BankReconciliationHeadDTO();
                bankReconciliationHeadDTO.BankReconciliationDetailDtos = new List<BankReconciliationDetailDTO>();
                var allocationDtos = new List<BankReconciliationDetailDTO>();
                var matchingDtos = new List<BankReconciliationMatchingEntryDTO>();
                bankReconciliationHeadDTO.BankReconciliationHeadIID = bankReconcilationViewModel.BankReconciliationHeadIID;
                foreach (var alloc in bankReconcilationViewModel.BankReconciliationMatchedEntries)
                {
                    if (!string.IsNullOrEmpty(alloc.Particulars))
                    {
                        var bankReconciliationDetail = (new BankReconciliationDetailDTO()
                        {
                            TranHeadID = alloc.TranHeadID,
                            TranTailID = alloc.TranTailID,
                            AccountID = alloc.AccountID,
                            ReconciliationDate = !string.IsNullOrEmpty(alloc.PostDate) && alloc.PostDate != "-" ? DateTime.ParseExact(alloc.PostDate, "dd/MM/yyyy", null) : !string.IsNullOrEmpty(alloc.TransDate) && alloc.TransDate != "-" ? DateTime.ParseExact(alloc.TransDate, "dd/MM/yyyy", null) : (DateTime?)null,
                            Remarks = alloc.Particulars,
                            Amount = alloc.BankDebitAmount - alloc.BankCreditAmount,
                            BankReconciliationMatchedStatusID = (short?)Eduegate.Services.Contracts.Enums.BankReconciliationMatchedStatuses.Matched,
                            ChequeDate = alloc.ChequeDate,
                            ChequeNo = alloc.ChequeNo,
                            PartyName = alloc.PartyName,
                            Reference = alloc.Reference,
                            BankStatementEntryID = alloc.BankStatementEntryID,
                            ReferenceGroupNO = alloc.ReferenceGroupNo,
                            ReferenceGroupName = alloc.ReferenceGroupName
                        });

                        foreach (var allocLedge in alloc.MatchedLedgerEntries)
                        {
                            matchingDtos.Add(new BankReconciliationMatchingEntryDTO()
                            {
                                TranHeadID = allocLedge.TranHeadID,
                                TranTailID = allocLedge.TranTailID,
                                AccountID = allocLedge.AccountID,
                                ReconciliationDate = string.IsNullOrEmpty(allocLedge.TransDate) && allocLedge.TransDate != "-" ? DateTime.ParseExact(allocLedge.TransDate, "dd/MM/yyyy", null) : (DateTime?)null,
                                Remarks = allocLedge.Particulars,
                                Amount = allocLedge.LedgerDebitAmount - allocLedge.LedgerCreditAmount,
                                ChequeDate = !string.IsNullOrEmpty(allocLedge.ChequeDate) && allocLedge.ChequeDate != "-" ? DateTime.ParseExact(allocLedge.ChequeDate, "dd/MM/yyyy", null) : (DateTime?)null,
                                ChequeNo = allocLedge.ChequeNo,
                                PartyName = allocLedge.PartyName,
                                Reference = allocLedge.Reference,
                                ReferenceGroupName = allocLedge.ReferenceGroupName,
                                ReferenceGroupNO = allocLedge.ReferenceGroupNo

                            });
                        }

                        foreach (var allocBank in alloc.MatchedBankEntries)
                        {
                            matchingDtos.Add(new BankReconciliationMatchingEntryDTO()
                            {
                                ReconciliationDate = string.IsNullOrEmpty(allocBank.PostDate) && allocBank.PostDate != "-" ? DateTime.ParseExact(allocBank.PostDate, "dd/MM/yyyy", null) : (DateTime?)null,
                                Remarks = allocBank.BankDescription,
                                Amount = allocBank.BankDebitAmount - allocBank.BankCreditAmount,
                                ChequeDate = !string.IsNullOrEmpty(allocBank.ChequeDate) && allocBank.ChequeDate != "-" ? DateTime.ParseExact(allocBank.ChequeDate, "dd/MM/yyyy", null) : (DateTime?)null,
                                ChequeNo = allocBank.ChequeNo,
                                PartyName = allocBank.PartyName,
                                Reference = allocBank.Reference,
                                ReferenceGroupName = allocBank.ReferenceGroupName,
                                ReferenceGroupNO = allocBank.ReferenceGroupNo,
                                BankStatementEntryID = allocBank.BankStatementEntryID
                            });
                        }
                        bankReconciliationDetail.BankReconciliationMatchingEntry.AddRange(matchingDtos);
                        allocationDtos.Add(bankReconciliationDetail);
                        matchingDtos = new List<BankReconciliationMatchingEntryDTO>();
                    }
                }
                bankReconciliationHeadDTO.BankReconciliationDetailDtos.AddRange(allocationDtos);

                var result = ClientFactory.AccountingServiceClient(CallContext).SaveBankReconciliationMatchedEntry(bankReconciliationHeadDTO);

                if (result != null && result.BankReconciliationHeadIID != 0)
                {
                    return Json(new { IsError = false, Response = "Saved successfully!", ID = result });
                }
                else
                {
                    return Json(new { IsError = true, Response = "Saving failed!", ID = result.BankReconciliationHeadIID });
                }
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception")
                    ? ex.InnerException?.Message : ex.Message;

                Eduegate.Logger.LogHelper<string>.Fatal($"BankReconciliation  entry saving failed. Error message: {errorMessage}", ex);

                return Json(new { IsError = true, Response = "Saving failed!", ID = 0 });
            }
        }
        private string SaveBankReconciliationEntries(BankReconcilationViewModel bankReconcilationViewModel)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            string[] formats = { "dd/MM/yy", "dd/MM/yyyy" };
            DateTime parsedDate;

            try
            {
                var bankReconciliationHeadDTO = new BankReconciliationHeadDTO()
                {
                    BankReconciliationHeadIID = bankReconcilationViewModel.BankReconciliationHeadIID,
                    BankStatementID = bankReconcilationViewModel.BankStatementID,
                    BankAccountID = bankReconcilationViewModel.BankAccountID,

                    FromDate = bankReconcilationViewModel.FromDate,

                    ToDate = bankReconcilationViewModel.ToDate,

                    OpeningBalanceAccount = bankReconcilationViewModel.LedgerOpeningBalance,

                    OpeningBalanceBankStatement = bankReconcilationViewModel.BankOpeningBalance,

                    ClosingBalanceAccount = bankReconcilationViewModel.LedgerClosingBalance,

                    ClosingBalanceBankStatement = bankReconcilationViewModel.BankClosingBalance,

                    BankReconciliationStatusID = bankReconcilationViewModel.BankReconciliationStatusID==1? (short?)Eduegate.Services.Contracts.Enums.BankReconciliationStatuses.Draft : (short?)Eduegate.Services.Contracts.Enums.BankReconciliationStatuses.Posted

                };

                var allocationDtos = new List<BankReconciliationDetailDTO>();
                var matchingDtos = new List<BankReconciliationMatchingEntryDTO>();



                foreach (var alloc in bankReconcilationViewModel.BankReconciliationUnMatchedWithLedgerEntries)
                {

                    bool success = DateTime.TryParseExact(alloc.PostDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate);
                    allocationDtos.Add(new BankReconciliationDetailDTO()
                    {
                        //ReconciliationDetailIID=
                        BankReconciliationHeadID = bankReconcilationViewModel.BankReconciliationHeadIID,
                        TranHeadID = alloc.TranHeadID,
                        TranTailID = alloc.TranTailID,

                        //SlNo
                        AccountID = alloc.AccountID,
                        ReconciliationDate = parsedDate,
                        Remarks = alloc.Particulars,
                        Amount = alloc.BankDebitAmount - alloc.BankCreditAmount,
                        BankReconciliationMatchedStatusID = (short?)Eduegate.Services.Contracts.Enums.BankReconciliationMatchedStatuses.MisMatchWithLedger,
                        ChequeDate = alloc.ChequeDate,
                        ChequeNo = alloc.ChequeNo,
                        PartyName = alloc.PartyName,
                        Reference = alloc.Reference,
                        BankStatementEntryID = alloc.BankStatementEntryID,
                        BankReconciliationMatchingEntry = new List<BankReconciliationMatchingEntryDTO>()

                    });
                }
                foreach (var alloc in bankReconcilationViewModel.BankReconciliationUnMatchedWithBankEntries)
                {

                    allocationDtos.Add(new BankReconciliationDetailDTO()
                    {

                        TranHeadID = alloc.TranHeadID,
                        TranTailID = alloc.TranTailID,
                        AccountID = alloc.AccountID,
                        ReconciliationDate = !string.IsNullOrEmpty(alloc.PostDate) && alloc.PostDate != "-" ? DateTime.ParseExact(alloc.PostDate, "dd/MM/yyyy", null) : !string.IsNullOrEmpty(alloc.TransDate) && alloc.TransDate != "-" ? DateTime.ParseExact(alloc.TransDate, "dd/MM/yyyy", null) : (DateTime?)null,
                        Remarks = alloc.Particulars,
                        Amount = alloc.LedgerDebitAmount - alloc.LedgerCreditAmount,
                        BankReconciliationMatchedStatusID = (short?)Eduegate.Services.Contracts.Enums.BankReconciliationMatchedStatuses.MisMatchWithBank,
                        ChequeDate = alloc.ChequeDate,
                        ChequeNo = alloc.ChequeNo,
                        PartyName = alloc.PartyName,
                        Reference = alloc.Reference,
                        BankReconciliationMatchingEntry= new List<BankReconciliationMatchingEntryDTO>()
                    });
                }

                foreach (var alloc in bankReconcilationViewModel.BankReconciliationMatchedEntries)
                {
                    if (!string.IsNullOrEmpty(alloc.Particulars))
                    {
                        var bankReconciliationDetail = (new BankReconciliationDetailDTO()
                        {
                            TranHeadID = alloc.TranHeadID,
                            TranTailID = alloc.TranTailID,
                            AccountID = alloc.AccountID,
                            ReconciliationDate = !string.IsNullOrEmpty(alloc.PostDate) && alloc.PostDate != "-" ? DateTime.ParseExact(alloc.PostDate, "dd/MM/yyyy", null) : !string.IsNullOrEmpty(alloc.TransDate) && alloc.TransDate != "-" ? DateTime.ParseExact(alloc.TransDate, "dd/MM/yyyy", null) : (DateTime?)null,
                            Remarks = alloc.Particulars,
                            Amount = alloc.BankDebitAmount - alloc.BankCreditAmount,
                            BankReconciliationMatchedStatusID = (short?)Eduegate.Services.Contracts.Enums.BankReconciliationMatchedStatuses.Matched,
                            ChequeDate = alloc.ChequeDate,
                            ChequeNo = alloc.ChequeNo,
                            PartyName = alloc.PartyName,
                            Reference = alloc.Reference,
                            BankStatementEntryID = alloc.BankStatementEntryID,
                            ReferenceGroupNO = alloc.ReferenceGroupNo,
                            ReferenceGroupName = alloc.ReferenceGroupName
                        });

                        foreach (var allocLedge in alloc.MatchedLedgerEntries)
                        {
                            matchingDtos.Add(new BankReconciliationMatchingEntryDTO()
                            {
                                TranHeadID = allocLedge.TranHeadID,
                                TranTailID = allocLedge.TranTailID,
                                AccountID = allocLedge.AccountID,
                                ReconciliationDate = string.IsNullOrEmpty(allocLedge.TransDate) && allocLedge.TransDate != "-" ? DateTime.ParseExact(allocLedge.TransDate, "dd/MM/yyyy", null) : (DateTime?)null,
                                Remarks = allocLedge.Particulars,
                                Amount = allocLedge.LedgerDebitAmount - allocLedge.LedgerCreditAmount,
                                ChequeDate = !string.IsNullOrEmpty(allocLedge.ChequeDate) && allocLedge.ChequeDate != "-" ? DateTime.ParseExact(allocLedge.ChequeDate, "dd/MM/yyyy", null) : (DateTime?)null,
                                ChequeNo = allocLedge.ChequeNo,
                                PartyName = allocLedge.PartyName,
                                Reference = allocLedge.Reference,
                                ReferenceGroupName = allocLedge.ReferenceGroupName,
                                ReferenceGroupNO = allocLedge.ReferenceGroupNo

                            });
                        }
                        foreach (var allocBank in alloc.MatchedBankEntries)
                        {
                            matchingDtos.Add(new BankReconciliationMatchingEntryDTO()
                            {
                                ReconciliationDate = string.IsNullOrEmpty(allocBank.PostDate) && allocBank.PostDate != "-" ? DateTime.ParseExact(allocBank.PostDate, "dd/MM/yyyy", null) : (DateTime?)null,
                                Remarks = allocBank.BankDescription,
                                Amount = allocBank.BankDebitAmount - allocBank.BankCreditAmount,
                                ChequeDate = !string.IsNullOrEmpty(allocBank.ChequeDate) && allocBank.ChequeDate != "-" ? DateTime.ParseExact(allocBank.ChequeDate, "dd/MM/yyyy", null) : (DateTime?)null,
                                ChequeNo = allocBank.ChequeNo,
                                PartyName = allocBank.PartyName,
                                Reference = allocBank.Reference,
                                ReferenceGroupName = allocBank.ReferenceGroupName,
                                ReferenceGroupNO = allocBank.ReferenceGroupNo,
                                BankStatementEntryID = allocBank.BankStatementEntryID
                            });
                        }
                        if (bankReconciliationDetail.ReconciliationDate== null || bankReconciliationDetail.ReconciliationDate== (DateTime?)null)
                        {
                            bankReconciliationDetail.ReconciliationDate = matchingDtos.Select(x => x.ReconciliationDate).FirstOrDefault();
                        }
                        bankReconciliationDetail.BankReconciliationMatchingEntry.AddRange(matchingDtos);
                        allocationDtos.Add(bankReconciliationDetail);
                        matchingDtos = new List<BankReconciliationMatchingEntryDTO>();
                    }
                }
                bankReconciliationHeadDTO.BankReconciliationDetailDtos.AddRange(allocationDtos);

                var result = ClientFactory.AccountingServiceClient(CallContext).SaveBankReconciliationEntry(bankReconciliationHeadDTO);

                if (result != null && result != "0")
                {
                    return result;
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception")
                    ? ex.InnerException?.Message : ex.Message;

                Eduegate.Logger.LogHelper<string>.Fatal($"BankReconciliation  entry saving failed. Error message: {errorMessage}", ex);

                return "0";
            }
        }

        [HttpPost]
        public ActionResult SaveBankReconciliationEntry([FromBody] BankReconcilationViewModel bankReconcilationViewModel)
        {
            try
            {

                var result = SaveBankReconciliationEntries(bankReconcilationViewModel);

                if (result != null && result != "0")
                {
                    return Json(new { IsError = false, Response = "Saved successfully!", ID = result });
                }
                else
                {
                    return Json(new { IsError = true, Response = "Saving failed!", ID = "0" });
                }
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception")
                    ? ex.InnerException?.Message : ex.Message;

                Eduegate.Logger.LogHelper<string>.Fatal($"BankReconciliation  entry saving failed. Error message: {errorMessage}", ex);

                return Json(new { IsError = true, Response = "Saving failed!", ID = "0" });
            }
        }

        public ActionResult ExcelUploadWindow(string viewName = null)
        {
            ViewBag.ViewName = viewName;

            return View();
        }

        //private void SaveAssetOpeningDetails(BankOpeningParametersDTO transactionDTO)
        //{
        //    try
        //    {
        //        var entry = ClientFactory.AccountingServiceClient(CallContext).SaveBankOpeningDetails(transactionDTO);
        //    }
        //    catch (Exception ex)
        //    {
        //        var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception") ? ex.InnerException?.Message : ex.Message;
        //        LogHelper<string>.Fatal($"Exception in SaveBankOpeningDetails(). Error message: {errorMessage}", ex);
        //        throw ex;
        //    }
        //}

        [HttpPost]
        public IActionResult SaveAssetOpeningEntryDetails(long feedLogID)
        {
            try
            {
                var headID = ClientFactory.FixedAssetServiceClient(CallContext).SaveAssetOpeningEntryDetails(feedLogID);

                return Ok(new { ID = headID, RawHTML = string.Empty });
            }
            catch (Exception ex)
            {
                var fields = "FeedLogID" + feedLogID;
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception") ? ex.InnerException?.Message : ex.Message;
                errorMessage = errorMessage + fields;
                LogHelper<string>.Fatal($"Exception in SaveAssetOpeningEntryDetails() in DocManagement controller. Error message: {errorMessage}", ex);
                return Ok(new { ID = 0, RawHTML = string.Empty });
            }
        }

    }
}
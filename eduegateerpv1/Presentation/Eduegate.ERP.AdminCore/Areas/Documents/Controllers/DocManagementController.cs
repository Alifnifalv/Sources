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
using Eduegate.Services.Contracts.Budgeting;
using Eduegate.Web.Library.Budgeting.Budget;
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
                LogHelper<DocManagementController>.Fatal(ex.Message.ToString(), ex);
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

                SaveUploadedFile(imageInfoList.FirstOrDefault());
                isSavedSuccessfully = true;
            }
            catch (Exception ex)
            {
                LogHelper<DocManagementController>.Fatal(ex.Message.ToString(), ex);
                return Json(new { Success = isSavedSuccessfully, Message = (ex.Message) });
            }

            return Json(new { Success = isSavedSuccessfully, FileInfo = imageInfoList });
        }

        private void SaveUploadedFile(ContentFileDTO content)
        {
            using (var dbContext = new dbContentContext())
            {

                if (content != null)
                {
                    var fileName = content.ContentFileName;
                    var contentData = content.ContentData;

                    string serverPath = "C:\\PEARL_CORE\\ParentPortal\\wwwroot\\Documents\\DataFeed";
                    string dirPath = $"{serverPath}\\Reconc";
                    string strFilePath = dirPath + "\\" + fileName;
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
        //[HttpPost]
        //public JsonResult SaveUploadedExcelFiles(List<UploadedFileDetailsViewModel> files)
        //{
        //    var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
        //    string[] formats = { "dd/MM/yy", "dd/MM/yyyy" };

        //    var bankReconcilationViewModel = new BankReconcilationViewModel();
        //    var bankReConciliationList = new List<BankReconciliationEntriesViewModel>();
        //    var dtos = UploadedFileDetailsViewModel.ToDocumentVM(files,
        //        Eduegate.Framework.Contracts.Common.Enums.EntityTypes.Others);

        //    foreach (var dto in dtos)
        //    {
        //        if (dto.ReferenceID.HasValue)
        //        {
        //            var contentData = ClientFactory.ContentServicesClient(CallContext)
        //                .ReadContentsById(dto.ReferenceID.Value);
        //            dto.ContentData = contentData.ContentData;
        //            bankReconcilationViewModel.ContentFileID = contentData.ContentFileIID.ToString();
        //            bankReconcilationViewModel.ContentFileName = contentData.ContentFileName.ToString();
        //        }
        //    }
        //    //DocumentFileViewModel.FromDTO(ClientFactory.DocumentServiceClient(CallContext)
        //    //    .SaveDocuments(dtos));

        //    var documentFileDTOs = ClientFactory.DocumentServiceClient(CallContext).SaveDocuments(dtos);
        //    if (documentFileDTOs.Count() > 0)
        //    {
        //        var extractedHeader = JsonConvert.DeserializeObject<BankTransHeaders>(documentFileDTOs[0].ExtractedData1);
        //        var extractDetail = JsonConvert.DeserializeObject<BankTransDetails>(documentFileDTOs[0].ExtractedData2);


        //        var transactionDTO = new BankReconciliationDTO
        //        {
        //            IBAN = extractedHeader.BankTransHeader[0].IBAN,
        //            // AccountNo = extractedHeader.BankTransHeader[0].AccountType,

        //            SDate = DateTime.ParseExact(extractedHeader.BankTransHeader[0].FromDate, "dd/MM/yyyy", null),
        //            EDate = DateTime.ParseExact(extractedHeader.BankTransHeader[0].ToDate, "dd/MM/yyyy", null)
        //        };

        //        bankReconcilationViewModel.FromDateString = extractedHeader.BankTransHeader[0].FromDate;
        //        bankReconcilationViewModel.ToDateString = extractedHeader.BankTransHeader[0].ToDate;
        //        bankReconcilationViewModel.FromDate = DateTime.ParseExact(extractedHeader.BankTransHeader[0].FromDate, "dd/MM/yyyy", null);
        //        bankReconcilationViewModel.ToDate = DateTime.ParseExact(extractedHeader.BankTransHeader[0].ToDate, "dd/MM/yyyy", null);
        //        var ledgerData = FillLedgerData(transactionDTO);
        //        bankReconcilationViewModel.BankReconciliationLedgerTrans = ledgerData.BankReconciliationLedgerTrans;
        //        bankReconcilationViewModel.LedgerOpeningBalance = ledgerData?.LedgerOpeningBalance ?? 0;
        //        bankReconcilationViewModel.LedgerClosingBalance = ledgerData?.LedgerClosingBalance ?? 0;
        //        bankReconcilationViewModel.BankAccountID = ledgerData?.BankAccountID ?? 0;
        //        bankReconcilationViewModel.BankName = ledgerData?.BankName;
        //        bankReconcilationViewModel.ContentFileData = documentFileDTOs[0].ExtractedData2;


        //        bankReconcilationViewModel.BankReconciliationBankTrans = FillBankStatementData(extractDetail.BankTransDetail);



        //        bankReconcilationViewModel.BankReconciliationBankTrans.RemoveAll(x => string.IsNullOrWhiteSpace(x.Narration)
        //                      && x.BankDebitAmount == null && x.Balance == null && x.PostDate == null
        //                      && x.BankCreditAmount == null);

        //        var openingBalanceData = bankReconcilationViewModel.BankReconciliationBankTrans
        //                             .Where(x => x.PostDate != null)
        //                             .OrderBy(x => x.PostDate)
        //                             .FirstOrDefault();
        //        var closingBalanceData = bankReconcilationViewModel.BankReconciliationBankTrans
        //             .Where(x => x.PostDate != null)
        //             .OrderByDescending(x => x.PostDate)
        //             .FirstOrDefault();
        //        var openingBalance = (openingBalanceData.Balance ?? 0) - ((openingBalanceData.BankDebitAmount ?? 0) - (openingBalanceData.BankCreditAmount ?? 0));
        //        var closingBalance = (closingBalanceData.Balance ?? 0);

        //        var totalDebit = bankReconcilationViewModel.BankReconciliationBankTrans
        //             .Where(x => x.BankDebitAmount != null).Sum(x => (x.BankDebitAmount ?? 0));
        //        var totalCredit = bankReconcilationViewModel.BankReconciliationBankTrans
        //           .Where(x => x.BankCreditAmount != null).Sum(x => (x.BankCreditAmount ?? 0));
        //        string description = "";
        //        if (openingBalance + (totalDebit - totalCredit) == closingBalance)
        //        {
        //            description = "The self-check process is successful : ";
        //            var bankStatementEntryDTO = new List<BankStatementEntryDTO>();

        //            bankStatementEntryDTO = (from x in bankReconcilationViewModel.BankReconciliationBankTrans
        //                                     select new BankStatementEntryDTO()
        //                                     {
        //                                         ChequeNo = x.ChequeNo,
        //                                         PartyName = x.Partyref,
        //                                         Description = x.Narration,
        //                                         Debit = x.BankDebitAmount,
        //                                         Credit = x.BankCreditAmount,
        //                                         PostDate = DateTime.ParseExact(ParseDateToString(x.PostDate), "dd/MM/yyyy", null),
        //                                         SlNO = x.SlNO
        //                                     }).ToList();

        //            var bankStatementID = ClientFactory.AccountingServiceClient(CallContext).SaveBankStatement(
        //                new BankStatementDTO()
        //                {
        //                    BankStatementIID = bankReconcilationViewModel.BankStatementID.HasValue ? bankReconcilationViewModel.BankStatementID.Value : 0,
        //                    ContentFileID = bankReconcilationViewModel.ContentFileID,
        //                    ContentFileName = bankReconcilationViewModel.ContentFileName,
        //                    ExtractedTextData = bankReconcilationViewModel.ContentFileData,
        //                    BankStatementEntries = bankStatementEntryDTO
        //                });
        //            bankReconcilationViewModel.BankStatementID = long.Parse(bankStatementID);
        //        }
        //        else
        //        {
        //            description = "The self-check process is not successful : ";
        //        }
        //        description = description + "Extracted PDF Rows count " + extractDetail.BankTransDetail.Count() + " Opening Balance : " + openingBalance +
        //                                " Closing Balance : " + closingBalance + " Total Debit : " + totalDebit + " Total Credit :" + totalCredit;

        //        bankReconcilationViewModel.BankOpeningBalance = openingBalance;
        //        bankReconcilationViewModel.BankClosingBalance = closingBalance;
        //        bankReconcilationViewModel.SelfCheckingString = description;

        //        bankReconcilationViewModel = GettingMatchedAndUnMatchedLedgerEntries(bankReconcilationViewModel);
        //        bankReconcilationViewModel.BankReconciliationManualEntry =
        //        [
        //            new BankReconciliationManualEntry()
        //            {
        //                AccountID = 0,
        //                BankCreditAmount = 0,
        //                BankDebitAmount = 0,
        //                BankDescription = "-",
        //                CheqDate = "-",
        //                ChequeNo = "-",
        //                LedgerCreditAmount = 0,
        //                LedgerDebitAmount = 0,
        //                Particulars = "-",
        //                PostDate = "-",
        //                ReconciliationCreditAmount = 0,
        //                ReconciliationDebitAmount = 0,
        //                TransDate = "-",
        //                VoucherRef = "-",
        //                TranHeadID = 0,
        //                TranTailID = 0,
        //            },
        //        ];


        //    }
        //    return Json(new { Success = true, Data = bankReconcilationViewModel });
        //}

        [HttpPost]
        public async Task<IActionResult> SaveUploadedExcelFiles(List<UploadedFileDetailsViewModel> files)
        {
            try
            {
                DataFeedViewModel model = new DataFeedViewModel();
                model.FeedFileName = files[0].ContentFileName;

                model.StatusID = Services.Contracts.Enums.DataFeedStatus.InProcess;
                model.DataFeedTypeID = 1;
                var dataFeedLogDTO = ClientFactory.DataFeedServiceClient(CallContext).SaveDataFeedLog(DataFeedViewModel.ToDTO(model));

                var dataFeedViewModel = DataFeedViewModel.FromDTO(dataFeedLogDTO, null);
                string returnHTML = await RenderViewToStringAsync("ProcessFeed", dataFeedViewModel, ControllerContext);
                return Json(new { ID = dataFeedViewModel.DataFeedID, RawHTML = returnHTML });
            }
            catch (Exception ex)
            {
                LogHelper<DocManagementController>.Fatal(ex.Message.ToString(), ex);
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
                //Upload file to server first and then process the file
                //Services.Contracts.Enums.DataFeedTypes dataFeedType = (Services.Contracts.Enums.DataFeedTypes)Enum.Parse(typeof(Services.Contracts.Enums.DataFeedTypes), uploadFileType);
                //string fileLocation =  System.IO.Path.Combine(documentPhysicalPath, "DataFeed",
                //        CallContext.LoginID.ToString(), model.DataFeedTypeID.ToString());

                string serverPath = "C:\\PEARL_CORE\\ParentPortal\\wwwroot\\Documents\\DataFeed";
                string dirPath = $"{serverPath}\\Reconc";
                string fileName = "BankReconciliationMay2024.xlsx";
                string fileLocation = $"{dirPath}\\BankReconciliationMay2024.xlsx";

                //TODO: dll code changed so Request.Form.Files.ToList() not support
                //var uploadedFiles = Utilities.DataExportImport.Upload.UploadToServer(Request.Form.Files.ToList(), fileLocation);

                // var uploadedFiles = Utilities.DataExportImport.Upload.UploadToServer(Request.Form.Files.ToList(), fileLocation);

                dataFeedLogDTO = ProcessUpload(fileName, fileLocation, ID);
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
        [HttpPost]
        public async Task<IActionResult> SaveBankOpeningDetails(long ID, decimal? LedgerClosingBalInput, decimal? BankClosingBalInput, long BankAccountID, string FromDate, string ToDate)
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
        //[HttpPost]
        //public async Task<IActionResult> ProcessFeed(long ID)
        //{
        //    bool isSuccessful = false;

        //    try
        //    {
        //        var dfsc = ClientFactory.DataFeedServiceClient(CallContext);
        //        var dataFeedLogDTO = dfsc.GetDataFeedLogByID(ID);
        //        DataFeedViewModel model = DataFeedViewModel.FromDTO(dataFeedLogDTO, null);
        //        //Upload file to server first and then process the file
        //        //Services.Contracts.Enums.DataFeedTypes dataFeedType = (Services.Contracts.Enums.DataFeedTypes)Enum.Parse(typeof(Services.Contracts.Enums.DataFeedTypes), uploadFileType);
        //        string fileLocation = System.IO.Path.Combine(documentPhysicalPath, "DataFeed",
        //                CallContext.LoginID.ToString(), model.DataFeedTypeID.ToString());
        //        var uploadedFiles = Utilities.DataExportImport.Upload.UploadToServer(Request.Form.Files.ToList(), fileLocation);

        //        dataFeedLogDTO = ProcessUpload(uploadedFiles.FirstOrDefault(), model, ID);

        //        var dataFeedViewModel = DataFeedViewModel.FromDTO(dataFeedLogDTO, null);
        //        string returnHTML = await RenderViewToStringAsync("ProcessFeed", dataFeedViewModel, ControllerContext);
        //        return Ok(new { ID = dataFeedViewModel.DataFeedID, RawHTML = returnHTML });
        //    }
        //    catch (Exception ex)
        //    {
        //        string message = "Data import was not successful!";
        //        Eduegate.Logger.LogHelper<DataFeedController>.Fatal(message, ex);
        //        return Ok(new { IsSuccess = isSuccessful, Message = message });
        //    }
        //}

        private DataFeedLogDTO ProcessUpload(string fileName, string filePath, long feedLogID)
        {
            //using (var fileStream = System.IO.File.OpenRead(filePath))
            //{
            return ClientFactory.DataFeedServiceClient(CallContext).UploadFeedFile(fileName, feedLogID, filePath, null);
            //}
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
        [HttpPost]
        public JsonResult SaveUploadedPdfFiles(List<UploadedFileDetailsViewModel> files, string FromDate, string ToDate, long BankAccountID)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            string[] formats = { "dd/MM/yy", "dd/MM/yyyy" };

            var bankReconcilationViewModel = new BankReconcilationViewModel();
            var bankReConciliationList = new List<BankReconciliationEntriesViewModel>();
            var dtos = UploadedFileDetailsViewModel.ToDocumentVM(files,
                Eduegate.Framework.Contracts.Common.Enums.EntityTypes.Others);

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
                var extractDetail = JsonConvert.DeserializeObject<BankTransDetails>(documentFileDTOs[0].ExtractedData2);


                var transactionDTO = new BankReconciliationDTO
                {
                    IBAN = extractedHeader.BankTransHeader[0].IBAN,
                    SDate = DateTime.ParseExact(FromDate, "dd/MM/yyyy", null),
                    EDate = DateTime.ParseExact(ToDate, "dd/MM/yyyy", null),
                    BankAccountID = BankAccountID
                };

                bankReconcilationViewModel.FromDateString = extractedHeader.BankTransHeader[0].FromDate;
                bankReconcilationViewModel.ToDateString = extractedHeader.BankTransHeader[0].ToDate;
                bankReconcilationViewModel.FromDate = DateTime.ParseExact(extractedHeader.BankTransHeader[0].FromDate, "dd/MM/yyyy", null);
                bankReconcilationViewModel.ToDate = DateTime.ParseExact(extractedHeader.BankTransHeader[0].ToDate, "dd/MM/yyyy", null);
                var ledgerData = FillLedgerData(transactionDTO);
                bankReconcilationViewModel.BankReconciliationLedgerTrans = ledgerData.BankReconciliationLedgerTrans;
                bankReconcilationViewModel.LedgerOpeningBalance = ledgerData?.LedgerOpeningBalance ?? 0;
                bankReconcilationViewModel.LedgerClosingBalance = ledgerData?.LedgerClosingBalance ?? 0;
                bankReconcilationViewModel.BankAccountID = ledgerData?.BankAccountID ?? 0;
                bankReconcilationViewModel.BankName = ledgerData?.BankName;
                bankReconcilationViewModel.ContentFileData = documentFileDTOs[0].ExtractedData2;


                bankReconcilationViewModel.BankReconciliationBankTrans = FillBankStatementData(extractDetail.BankTransDetail);



                bankReconcilationViewModel.BankReconciliationBankTrans.RemoveAll(x => string.IsNullOrWhiteSpace(x.Narration)
                              && x.BankDebitAmount == null && x.Balance == null && x.PostDate == null
                              && x.BankCreditAmount == null);

                var openingBalanceData = bankReconcilationViewModel.BankReconciliationBankTrans
                                     .Where(x => x.PostDate != null)
                                     .OrderBy(x => x.PostDate)
                                     .FirstOrDefault();
                var closingBalanceData = bankReconcilationViewModel.BankReconciliationBankTrans
                     .Where(x => x.PostDate != null)
                     .OrderByDescending(x => x.PostDate)
                     .FirstOrDefault();
                var openingBalance = (openingBalanceData.Balance ?? 0) - ((openingBalanceData.BankDebitAmount ?? 0) - (openingBalanceData.BankCreditAmount ?? 0));
                var closingBalance = (closingBalanceData.Balance ?? 0);

                var totalDebit = bankReconcilationViewModel.BankReconciliationBankTrans
                     .Where(x => x.BankDebitAmount != null).Sum(x => (x.BankDebitAmount ?? 0));
                var totalCredit = bankReconcilationViewModel.BankReconciliationBankTrans
                   .Where(x => x.BankCreditAmount != null).Sum(x => (x.BankCreditAmount ?? 0));
                string description = "";
                //if (openingBalance + (totalDebit - totalCredit) == closingBalance)
                //{
                description = "The self-check process is successful : ";
                var bankStatementEntryDTO = new List<BankStatementEntryDTO>();

                bankStatementEntryDTO = (from x in bankReconcilationViewModel.BankReconciliationBankTrans
                                         select new BankStatementEntryDTO()
                                         {
                                             ChequeNo = x.ChequeNo,
                                             PartyName = x.Partyref,
                                             Description = x.Narration,
                                             Debit = x.BankDebitAmount,
                                             Credit = x.BankCreditAmount,
                                             PostDate = DateTime.ParseExact(ParseDateToString(x.PostDate), "dd/MM/yyyy", null),
                                             SlNO = x.SlNO
                                         }).ToList();

                var bankStatementID = ClientFactory.AccountingServiceClient(CallContext).SaveBankStatement(
                    new BankStatementDTO()
                    {
                        BankStatementIID = bankReconcilationViewModel.BankStatementID.HasValue ? bankReconcilationViewModel.BankStatementID.Value : 0,
                        ContentFileID = bankReconcilationViewModel.ContentFileID,
                        ContentFileName = bankReconcilationViewModel.ContentFileName,
                        ExtractedTextData = bankReconcilationViewModel.ContentFileData,
                        BankStatementEntries = bankStatementEntryDTO
                    });
                bankReconcilationViewModel.BankStatementID = long.Parse(bankStatementID);
                //}
                //else
                //{
                //    description = "The self-check process is not successful : ";
                //}
                description = description + "Extracted PDF Rows count " + extractDetail.BankTransDetail.Count() + " Opening Balance : " + openingBalance +
                                        " Closing Balance : " + closingBalance + " Total Debit : " + totalDebit + " Total Credit :" + totalCredit;

                bankReconcilationViewModel.BankOpeningBalance = openingBalance;
                bankReconcilationViewModel.BankClosingBalance = closingBalance;
                bankReconcilationViewModel.SelfCheckingString = description;

                bankReconcilationViewModel = GettingMatchedAndUnMatchedLedgerEntries(bankReconcilationViewModel);
                bankReconcilationViewModel.BankReconciliationManualEntry =
                [
                    new BankReconciliationManualEntry()
                    {
                        AccountID = 0,
                        BankCreditAmount = 0,
                        BankDebitAmount = 0,
                        BankDescription = "-",
                        CheqDate = "-",
                        ChequeNo = "-",
                        LedgerCreditAmount = 0,
                        LedgerDebitAmount = 0,
                        Particulars = "-",
                        PostDate = "-",
                        ReconciliationCreditAmount = 0,
                        ReconciliationDebitAmount = 0,
                        TransDate = "-",
                        VoucherRef = "-",
                        TranHeadID = 0,
                        TranTailID = 0,
                    },
                ];


            }
            return Json(new { Success = true, Data = bankReconcilationViewModel });
        }

        public BankReconcilationViewModel FillLedgerData(BankReconciliationDTO transactionDTO)
        {
            var bankReconcilationViewModel = new BankReconcilationViewModel();
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

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
                                                     Partyref = s.Partyref,
                                                     ChequeDate = s.ChequeDate.HasValue ? s.ChequeDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                                                     TranHeadID = s.TranHeadID,
                                                     TranTailID = s.TranTailID,
                                                 }).ToList();


            bankReconcilationViewModel.LedgerOpeningBalance = entry.BankReconciliationTransactionDtos[0]?.OpeningBalance;
            bankReconcilationViewModel.LedgerClosingBalance = entry.BankReconciliationTransactionDtos[0]?.ClosingBalance;

            bankReconcilationViewModel.BankAccountID = entry?.BankAccountID;
            bankReconcilationViewModel.BankName = entry.BankName;
            bankReconcilationViewModel.BankReconciliationLedgerTrans = bankReconciliationLedgerTrans;
            return bankReconcilationViewModel;
        }

        private string ParseDateToString(string postDate)
        {

            postDate = postDate.Trim();

            DateTime parsedDate;


            if (DateTime.TryParseExact(postDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
            {
                return parsedDate.ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
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
            else
            {
                throw new FormatException($"Invalid date format: {postDate}");
            }
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

            foreach (var bankTrans in bankReconcilationViewModel.BankReconciliationBankTrans)
            {
                bool matchFound = false;


                foreach (var ledgerTrans in bankReconcilationViewModel.BankReconciliationLedgerTrans)
                {
                    if (Math.Abs(Math.Abs(bankTrans.BankDebitAmount.Value) - Math.Abs(bankTrans.BankCreditAmount.Value)) == Math.Abs(Math.Abs(ledgerTrans.LedgerDebitAmount.Value) - Math.Abs(ledgerTrans.LedgerCreditAmount.Value))
                        && (
                       HasMatchingDigits(ledgerTrans, bankTrans) || HasCommonWords(bankTrans.Narration, ledgerTrans.Narration)))
                    //                  if (bankTrans.BankDebitAmount.HasValue
                    //&& ledgerTrans.LedgerDebitAmount.HasValue
                    //&& Math.Abs(double.Parse(bankTrans.BankDebitAmount.Value.ToString())) == 1678.24
                    //&& Math.Abs(double.Parse(ledgerTrans.LedgerCreditAmount.Value.ToString())) == 1678.24)
                    {
                        if (Math.Abs(Math.Abs(bankTrans.BankDebitAmount.Value) - Math.Abs(bankTrans.BankCreditAmount.Value)) == Math.Abs(Math.Abs(ledgerTrans.LedgerDebitAmount.Value) - Math.Abs(ledgerTrans.LedgerCreditAmount.Value)))
                        {
                            matchFound = true;
                        }
                        if (bankTrans.Narration == "DEBIT INTEREST" && ledgerTrans.Narration == "amount of interest on Overdraft")
                        {
                            matchFound = true;
                        }
                        matchedList.Add(new BankReconciliationMatchedEntriesViewModel
                        {
                            BankDebitAmount = bankTrans.BankDebitAmount,
                            BankCreditAmount = bankTrans.BankCreditAmount,
                            Particulars = ledgerTrans.Narration,
                            BankDescription = bankTrans.Narration,
                            PostDate = bankTrans.PostDate,
                            TransDate = ledgerTrans.PostDate,
                            LedgerDebitAmount = ledgerTrans.LedgerDebitAmount,
                            LedgerCreditAmount = ledgerTrans.LedgerCreditAmount,
                            TranHeadID = ledgerTrans.TranHeadID,
                            TranTailID = ledgerTrans.TranTailID,
                        });
                        matchFound = true;
                        break;
                    }
                }

                if (!matchFound)
                {
                    if (!string.IsNullOrEmpty(bankTrans.Narration) && !(bankTrans.BankCreditAmount == 0 && bankTrans.BankDebitAmount == 0))
                    {
                        unMatchedLedgerEntriesList.Add(new BankReconciliationUnMatchedWithLedgerEntriesViewModel
                        {
                            BankDebitAmount = bankTrans.BankDebitAmount,
                            BankCreditAmount = bankTrans.BankCreditAmount,

                            Particulars = bankTrans.Narration,
                            PostDate = bankTrans.PostDate,

                            LedgerDebitAmount = 0,
                            LedgerCreditAmount = 0,
                            ReconciliationDebitAmount = bankTrans.BankCreditAmount,
                            ReconciliationCreditAmount = bankTrans.BankDebitAmount,
                        });
                    }
                }

            }
            bankReconcilationViewModel.BankReconciliationUnMatchedWithBankEntries = GettingUnMatchedBankEntries(bankReconcilationViewModel);
            bankReconcilationViewModel.BankReconciliationMatchedEntries = matchedList;
            bankReconcilationViewModel.BankReconciliationUnMatchedWithLedgerEntries = unMatchedLedgerEntriesList;

            return bankReconcilationViewModel;
        }
        public List<BankReconciliationUnMatchedWithBankEntriesViewModel> GettingUnMatchedBankEntries(BankReconcilationViewModel bankReconcilationViewModel)
        {
            var unMatchedBankEntriesList = new List<BankReconciliationUnMatchedWithBankEntriesViewModel>();
            foreach (var ledgerTrans in bankReconcilationViewModel.BankReconciliationLedgerTrans)
            {
                bool matchFound = false;


                foreach (var bankTrans in bankReconcilationViewModel.BankReconciliationBankTrans)
                {
                    if (Math.Abs(bankTrans.BankDebitAmount.Value) - Math.Abs(bankTrans.BankCreditAmount.Value) == Math.Abs(ledgerTrans.LedgerDebitAmount.Value) - Math.Abs(ledgerTrans.LedgerCreditAmount.Value) && (
                       HasMatchingDigits(ledgerTrans, bankTrans) || HasCommonWords(bankTrans.Narration, ledgerTrans.Narration)))
                    {
                        matchFound = true;
                        break;
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
                            PostDate = ledgerTrans.PostDate,
                            LedgerDebitAmount = ledgerTrans.LedgerDebitAmount,
                            LedgerCreditAmount = ledgerTrans.LedgerCreditAmount,
                            BankDescription = string.Empty,
                            ReconciliationDebitAmount = ledgerTrans.LedgerCreditAmount,
                            ReconciliationCreditAmount = ledgerTrans.LedgerDebitAmount,
                            TranHeadID = ledgerTrans.TranHeadID,
                            TranTailID = ledgerTrans.TranTailID,
                        });
                    }
                }
            }
            return unMatchedBankEntriesList;
        }

        public static bool HasCommonWords(string description1, string description2)
        {
            var check = false;
            if (description1 == "amount of interest on Overdraft" || description1 == "DEBIT INTEREST")
            {
                check = true;
            }
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
            else if (bankTrans.Narration.Contains(ledgerTrans.Partyref) || (!string.IsNullOrEmpty(bankTrans.Partyref) && bankTrans.Partyref.Contains(ledgerTrans.Partyref)))
            {
                return true;
            }
            return false;
        }

        private void SaveBankOpeningDetails(BankOpeningParametersDTO transactionDTO)
        {
            var entry = ClientFactory.AccountingServiceClient(CallContext).SaveBankOpeningDetails(transactionDTO);
        }
        public List<BankReconciliationBankTransViewModel> FillBankStatementData(List<BankTransDetailViewModel> extractDetail)
        {
            var bankReconciliationBankTransViewModel = new List<BankReconciliationBankTransViewModel>();

            if (extractDetail.Any())
            {
                foreach (var x in extractDetail)
                {
                    bankReconciliationBankTransViewModel.Add(new BankReconciliationBankTransViewModel()
                    {
                        Narration = x.Description,
                        PostDate = x.PostDate,
                        BankCreditAmount = decimal.TryParse(x.CreditAmount, out decimal creditAmount) ? creditAmount : 0,
                        BankDebitAmount = decimal.TryParse(x.DebitAmount, out decimal debitAmount) ? debitAmount : 0,
                        Balance = ConvertStringToDecimal(x.Balance),
                        ChequeNo = ExtractChequeNos(x.Description),
                        Partyref = ExtractPartyRef(x.Description)

                    });

                }
            }
            return bankReconciliationBankTransViewModel;
        }

        public static string ExtractChequeNos(string description)
        {
            string pattern = @"^\d+";
            var chequeNo = string.Empty;
            if (description.IndexOf("CHEQUE", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                Match match = Regex.Match(description, pattern);
                if (match.Success)
                {

                    chequeNo = match.Value;
                }
            }

            return chequeNo;
        }

        public static string ExtractPartyRef(string description)
        {
            string pattern = @"P\d{4}";
            Match match = Regex.Match(description, pattern);
            if (match.Success)
            {
                return match.Value;
            }
            return string.Empty;
        }

        [HttpGet]
        public ActionResult Configure(long IID = 0)
        {
            return View(IID);
        }

        [HttpPost]
        public ActionResult SaveBankReconciliationEntry([FromBody] BankReconcilationViewModel bankReconcilationViewModel)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            string[] formats = { "dd/MM/yy", "dd/MM/yyyy" };
            DateTime parsedDate;


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

                BankReconciliationStatusID = (short?)Eduegate.Services.Contracts.Enums.BankReconciliationStatuses.Draft

            };

            var allocationDtos = new List<BankReconciliationDetailDTO>();
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
                    BankReconciliationMatchedStatusID = (short?)Eduegate.Services.Contracts.Enums.BankReconciliationMatchedStatuses.MisMatchWithLedger
                });
            }
            foreach (var alloc in bankReconcilationViewModel.BankReconciliationUnMatchedWithBankEntries)
            {

                allocationDtos.Add(new BankReconciliationDetailDTO()
                {

                    TranHeadID = alloc.TranHeadID,
                    TranTailID = alloc.TranTailID,
                    AccountID = alloc.AccountID,
                    ReconciliationDate = !string.IsNullOrEmpty(alloc.PostDate) && alloc.PostDate != "-" ? DateTime.ParseExact(alloc.PostDate, "dd/MM/yyyy", null) : !string.IsNullOrEmpty(alloc.TransDate) && alloc.TransDate!= "-" ? DateTime.ParseExact(alloc.TransDate, "dd/MM/yyyy", null) : (DateTime?)null,
                    Remarks = alloc.Particulars,
                    Amount = alloc.LedgerDebitAmount - alloc.LedgerCreditAmount,
                    BankReconciliationMatchedStatusID = (short?)Eduegate.Services.Contracts.Enums.BankReconciliationMatchedStatuses.MisMatchWithBank
                });
            }

            foreach (var alloc in bankReconcilationViewModel.BankReconciliationMatchedEntries)
            {
                if (!string.IsNullOrEmpty(alloc.Particulars))
                {
                    allocationDtos.Add(new BankReconciliationDetailDTO()
                    {
                        TranHeadID = alloc.TranHeadID,
                        TranTailID = alloc.TranTailID,
                        AccountID = alloc.AccountID,
                        ReconciliationDate = !string.IsNullOrEmpty(alloc.PostDate) && alloc.PostDate != "-" ? DateTime.ParseExact(alloc.PostDate, "dd/MM/yyyy", null) : !string.IsNullOrEmpty(alloc.TransDate) && alloc.TransDate != "-" ? DateTime.ParseExact(alloc.TransDate, "dd/MM/yyyy", null) : (DateTime?)null,
                        Remarks = alloc.Particulars,
                        Amount = alloc.BankDebitAmount - alloc.BankCreditAmount,
                        BankReconciliationMatchedStatusID = (short?)Eduegate.Services.Contracts.Enums.BankReconciliationMatchedStatuses.Matched
                    });
                }
            }
            bankReconciliationHeadDTO.BankReconciliationDetailDtos.AddRange(allocationDtos);
            try
            {
                var result = ClientFactory.AccountingServiceClient(CallContext).SaveBankReconciliationEntry(bankReconciliationHeadDTO);

                if (result != null)
                {
                    return Json(new { IsError = false, Response = "Saved successfully!" });
                }
                else
                {
                    return Json(new { IsError = true, Response = "Saving failed!" });
                }
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception")
                    ? ex.InnerException?.Message : ex.Message;

                Eduegate.Logger.LogHelper<string>.Fatal($"Budgeting entry saving failed. Error message: {errorMessage}", ex);

                return Json(new { IsError = true, Response = "Saving failed!" });
            }
        }
    }
}
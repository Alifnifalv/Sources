using Eduegate.ERP.Admin.Controllers;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.Supports;
using Eduegate.Web.Library.School.Fees;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Web.Library.ViewModels.Catalog;
using Eduegate.Web.Library.ViewModels.Supports;
using Microsoft.AspNetCore.Mvc;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.School.Students;
using System.Globalization;
using Eduegate.Framework.Contracts.Common.Enums;
using Microsoft.IdentityModel.Tokens;
using Eduegate.Domain.Setting;
using Eduegate.Web.Library.Support.CustomerSupport;

namespace Eduegate.ERP.Admin.Areas.Supports.Controllers
{
    [Area("Supports")]
    public class TicketController : BaseSearchController
    {
        // GET: Supports/Ticket
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList((Eduegate.Services.Contracts.Enums.SearchView)(int)Eduegate.Infrastructure.Enums.SearchView.Ticket);

            return View(new SearchListViewModel()
            {
                ControllerName = "Supports/" + Eduegate.Infrastructure.Enums.SearchView.Ticket.ToString(),
                ViewName = Eduegate.Infrastructure.Enums.SearchView.Ticket,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                IsCommentLink = true,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                FilterColumns = metadata.QuickFilterColumns,
                HasFilters = metadata.HasFilters,
                UserValues = metadata.UserValues,
                IsChild = false,
                HasChild = true
            });
        }

        [HttpGet]
        public async Task<IActionResult> ChildList(long ID)
        {
            // call the service and get the meta data
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList(Eduegate.Services.Contracts.Enums.SearchView.TicketDescription);

            return PartialView("_ChildList", new SearchListViewModel
            {
                ControllerName = "TicketDescription",
                ViewName = Eduegate.Infrastructure.Enums.SearchView.TicketDescription,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                //IsMultilineEnabled = metadata.MultilineEnabled,
                IsDetailedView = false,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                FilterColumns = metadata.QuickFilterColumns,
                RuntimeFilter = Convert.ToString("TicketIID=" + ID.ToString()).Trim(), // child view must have this column
                IsChild = true,
                ParentViewName = Eduegate.Infrastructure.Enums.SearchView.Ticket,
                IsEditableLink = false
            });
        }

        [HttpGet]
        public Task<IActionResult> SearchData(int currentPage = 1, string orderBy = "", string runtimeFilter = "")
        {
            var view = Eduegate.Infrastructure.Enums.SearchView.Ticket;
            if (runtimeFilter.Trim().IsNotNullOrEmpty())
            {
                view = Eduegate.Infrastructure.Enums.SearchView.Ticket;
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
            return base.SearchSummaryData(Eduegate.Infrastructure.Enums.SearchView.TicketSummary);
        }

        [HttpGet]
        public ActionResult DetailedView(long IID = 0)
        {
            ViewBag.IID = IID;
            return View();
        }

        [HttpGet]
        public ActionResult Create(long ID = 0)
        {
            var viewModel = new CRUDViewModel();
            viewModel.Name = "Ticket";
            viewModel.ListActionName = "Ticket";

            TicketViewModel ticketVM = new TicketViewModel() { DueDateFrom = DateTime.Now.ToLongDateString() };
            //ticketVM.TicketProductSKUs = new List<TicketProductViewModel>();
            ticketVM.ActionTab = new ActionTabViewModel();

            //ticketVM.TicketProductSKUs.Add(new TicketProductViewModel());
            ticketVM.Document = new DocumentViewViewModel();

            ticketVM.Status = new KeyValueViewModel()
            {
                Key = Convert.ToString((int)Eduegate.Services.Contracts.Enums.TicketStatuses.Open),
                Value = Convert.ToString(Eduegate.Services.Contracts.Enums.TicketStatuses.Open)
            };

            viewModel.ViewModel = ticketVM;
            viewModel.DetailViewModel = new TicketProductViewModel();
            viewModel.IID = ID;
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "DocumentType", Url = "Mutual/GetLookUpData?lookType=SupportDocumentType" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Employee", Url = "Employee/GetEmployeesByRoleID?roleID=3" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Manager", Url = "Mutual/GetLookUpData?lookType=Manager" }); // Manager
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Status", Url = "Mutual/GetLookUpData?lookType=TicketStatus" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Priority", Url = "Mutual/GetLookUpData?lookType=TicketPriority" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Action", Url = "Mutual/GetLookUpData?lookType=TicketAction" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Reason", Url = "Mutual/GetLookUpData?lookType=TicketReason" });

            // Sub Actions
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "RefundType", Url = "Mutual/GetLookUpData?lookType=TicketAction&optionalId=1" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "GiveItemToCollectItem", Url = "Mutual/GetLookUpData?lookType=TicketAction&optionalId=2" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "GiveItemToDirectReplacement", Url = "Mutual/GetLookUpData?lookType=TicketAction&optionalId=3" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Notify", Url = "Mutual/GetLookUpData?lookType=TicketAction&optionalId=4" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "IssueType", Url = "Mutual/GetLookUpData?lookType=TicketAction&optionalId=5" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "AmountReceived", Url = "Mutual/GetLookUpData?lookType=BooleanType" });


            TempData["viewModel"] = viewModel;
            return RedirectToAction("Create", "CRUD", new { area = "" });
        }

        [HttpGet]
        public ActionResult Edit(long ID = 0)
        {
            return RedirectToAction("Create", new { ID = ID });
        }

        [HttpGet]
        public ActionResult Get(int ID = 0)
        {
            var vm = TicketViewModel.FromDTO(ClientFactory.SupportServiceClient(CallContext).GetTicket(ID));

            if (vm.IsNotNull())
            {
                if (vm.TicketProductSKUs.IsNotNull())
                    vm.TicketProductSKUs.Add(new TicketProductViewModel());
            }

            return Json(vm);
        }

        [HttpPost]
        public JsonResult Save(TicketViewModel vm)
        {
            var updatedVM = TicketViewModel.FromDTO(ClientFactory.SupportServiceClient(CallContext).SaveTicket(TicketViewModel.ToDTO(vm)));

            if (updatedVM.IsNotNull())
            {
                if (updatedVM.TicketProductSKUs.IsNotNull())
                    updatedVM.TicketProductSKUs.Add(new TicketProductViewModel());
            }

            return Json(updatedVM);
        }

        public ActionResult CaseManagement()
        {
            ViewBag.CallContext = CallContext;
            Helpers.PortalWebHelper.GetDefaultSettingsToViewBag(CallContext, this.ViewBag);
            return View();
        }

        [HttpGet]
        public ActionResult GetFeeDueDetailsByID(long studentFeeDueID)
        {
            var feeTypeList = new List<FeePaymentFeeTypeViewModel>();
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat", 0, "dd/MM/yyyy");

            var feeDueData = ClientFactory.SchoolServiceClient(CallContext).GetStudentFeeDueDetailsByID(studentFeeDueID);

            foreach (var feeType in feeDueData.FeeDueFeeTypeMap)
            {
                feeTypeList.Add(new FeePaymentFeeTypeViewModel()
                {
                    InvoiceNo = feeType.InvoiceNo,
                    Amount = feeType.Amount - (feeType.CollectedAmount ?? 0) - (feeType.CreditNoteAmount ?? 0),
                    FeePeriodID = feeType.FeePeriodID,
                    StudentFeeDueID = feeType.StudentFeeDueID,
                    FeeDueFeeTypeMapsID = feeType.FeeDueFeeTypeMapsIID,
                    FeeMaster = feeType.FeeMaster.Value,
                    FeePeriod = feeType.FeePeriodID.HasValue ? feeType.FeePeriod.Value : null,
                    FeeMasterID = feeType.FeeMasterID,
                    IsExternal = feeType.IsExternal,
                    InvoiceDateString = feeType.InvoiceDate.HasValue ? Convert.ToDateTime(feeType.InvoiceDate).ToString(dateFormat) : "NA",
                    InvoiceDate = feeType.InvoiceDate == null ? (DateTime?)null : Convert.ToDateTime(feeType.InvoiceDate),
                    FeeMonthly = (from split in feeType.FeeDueMonthlySplit
                                  where split.Amount - (split.CollectedAmount ?? 0) - (split.CreditNoteAmount ?? 0) != 0
                                  select new FeeAssignMonthlySplitViewModel()
                                  {
                                      Amount = split.Amount - (split.CollectedAmount ?? 0) - (split.CreditNoteAmount ?? 0),
                                      CreditNote = split.CreditNoteAmount.HasValue ? split.CreditNoteAmount.Value : 0,
                                      Balance = 0,
                                      NowPaying = split.Amount - (split.CollectedAmount ?? 0) - (split.CreditNoteAmount ?? 0),
                                      OldNowPaying = split.Amount - (split.CollectedAmount ?? 0) - (split.CreditNoteAmount ?? 0),
                                      MonthID = split.MonthID,
                                      Year = split.Year,
                                      FeeDueMonthlySplitID = split.FeeDueMonthlySplitIID,
                                      IsRowSelected = true,
                                      MonthName = split.MonthID == 0 ? null : new DateTime(split.Year, split.MonthID, 1).ToString("MMM")
                                  }).ToList(),
                });
            }

            ViewBag.FeeTypes = feeTypeList;

            return View("FeeOutstadingDetails");
        }

        #region Not in use
        [HttpPost]
        public ActionResult FeeDueTicketGeneration([FromBody] TicketDTO ticket)
        {
            var returnMessage = string.Empty;
            bool isError = false;

            var mailSubject = string.Empty;
            var mailDescription = string.Empty;

            try
            {
                var dueDetail = ClientFactory.SchoolServiceClient(CallContext).GetStudentFeeDueDetailsByID(ticket.ReferenceID.Value);

                ticket.LoginID = dueDetail?.ParentLoginID;
                ticket.CustomerEmailID = dueDetail?.ParentEmailID;

                var ticketData = ClientFactory.SupportServiceClient(CallContext).GenerateTicketByReference(ticket);

                mailSubject = ticket.Subject + " - " + ticketData.TicketNo;
                mailDescription = "<p>Dear Parent,</p>  <p>We have raised a Ticket for you.</p>  <p>Please note your reference number for this ticket is <b>" + ticketData.TicketNo + "</b></p>  <p>Note:- Interactions are tracked via the Ticket number. We request you not to change the subject line in future correspondence.</p>  <p>We assure you the best of our services.</p>  <p>You can check the parent portal for more details.</p>  <p>Use the below link<br /> https://parent.pearlschool.org/</p>  <p><br /> Warm Regards,<br /> Podar Pearl School</p> ";

                returnMessage = "Ticket Generated Successfully!";
                isError = false;
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                returnMessage = "Ticket Generation failed!";
                isError = true;
            }

            Email_Ticket(ticket.CustomerEmailID, mailSubject, mailDescription);

            if (isError == true)
            {
                return Json(new { IsError = false, Response = returnMessage });
            }
            else
            {
                return Json(new { IsError = true, Response = returnMessage });
            }
        }

        public void Email_Ticket(string emailID, string emailSubject, string receiptBody)
        {
            var settingBL = new Domain.Setting.SettingBL(CallContext);

            //string receiptBody = settingBL.GetSettingValue<string>("SALESORDER_EMAILBODY_CONTENT");

            string hostDet = settingBL.GetSettingValue<string>("HOST_NAME");

            string defaultMail = settingBL.GetSettingValue<string>("DEFAULT_MAIL_ID");

            var emailBody = receiptBody;

            //var emailSubject = mailSubject;

            string mailMessage = new Eduegate.Domain.Notification.EmailNotificationBL(CallContext).PopulateBody(emailID, emailBody);
            var mailParameters = new Dictionary<string, string>();

            if (emailBody != "")
            {
                if (hostDet.ToLower() == "live")
                {
                    new Eduegate.Domain.Notification.EmailNotificationBL(CallContext).SendMail(emailID, emailSubject, mailMessage, EmailTypes.Ticketing, mailParameters);
                }
                else
                {
                    new Eduegate.Domain.Notification.EmailNotificationBL(CallContext).SendMail(defaultMail, emailSubject, mailMessage, EmailTypes.Ticketing, mailParameters);
                }
            }
        }

        [HttpPost]
        public ActionResult GenerateAndSendFeeDueMailReportToParent([FromBody] MailFeeDueStatementReportDTO gridData)
        {
            var dateFormat = new SettingBL(null).GetSettingValue<string>("DateFormat", "dd/MM/yyyy");
            var currentDate = DateTime.Now.Date;
            gridData.AsOnDate = currentDate.ToString(dateFormat, CultureInfo.InvariantCulture);

            try
            {
                var dataPass = ClientFactory.SchoolServiceClient(CallContext).SendFeeDueMailReportToParent(gridData);

                ClientFactory.ReportGenerationServiceClient(CallContext).SendFeeDueMailReportToParent(dataPass);

                return Json(new { IsError = false, Response = "Fee due statement Successfully sent!" });
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception")
                    ? ex.InnerException?.Message : ex.Message;

                Eduegate.Logger.LogHelper<string>.Fatal($"Fee due statement Sending failed. Error message: {errorMessage}", ex);

                return Json(new { IsError = true, Response = errorMessage });
            }
        }
        #endregion

        [HttpGet]
        public JsonResult SendFeeDueMailReportToParent(long? studentID, string reportName)
        {
            var result = ClientFactory.SupportServiceClient(CallContext).SendFeeDueMailReportToParent(studentID, reportName);

            if (result.operationResult == OperationResult.Success)
            {
                return Json(new { IsError = false, Response = "Fee due statement Successfully sent!" });
            }
            else
            {
                return Json(new { IsError = true, Response = "Fee due statement sending failed!" });
            }
        }

        [HttpGet]
        public JsonResult SendProformaInvoiceToParent(long? studentID, string reportName)
        {
            var result = ClientFactory.SupportServiceClient(CallContext).SendProformaInvoiceToParent(studentID, reportName);

            if (result.operationResult == OperationResult.Success)
            {
                return Json(new { IsError = false, Response = "Proforma invoice Successfully sent!" });
            }
            else
            {
                return Json(new { IsError = true, Response = "Proforma invoice sending failed!" });
            }
        }

        public JsonResult GetTicketFeeDueDetails(long studentID)
        {
            var ticketFeeDueMaps = new List<TicketingFeeDueMapViewModel>();

            var feeDueDetails = ClientFactory.SchoolServiceClient(CallContext).GetStudentFeeDetails(studentID);

            if (feeDueDetails != null)
            {
                foreach (var typeMap in feeDueDetails.StudentFeeDueTypes)
                {
                    ticketFeeDueMaps.Add(new TicketingFeeDueMapViewModel()
                    {
                        TicketFeeDueMapIID = 0,
                        StudentFeeDueID = typeMap.StudentFeeDueID,
                        InvoiceNo = typeMap.InvoiceNo,
                        InvoiceDateString = typeMap.InvoiceStringDate,
                        InvoiceDate = typeMap.InvoiceDate,
                        FeeMaster = typeMap.FeeMaster != null ? typeMap.FeeMaster?.Value : null,
                        DueAmount = typeMap.NowPaying,
                    });
                }
            }

            if (feeDueDetails == null)
            {
                return Json(new { IsError = true, Response = "Something went wrong, try again later!" });
            }
            else
            {
                return Json(new { IsError = false, Response = ticketFeeDueMaps });
            }
        }

        public JsonResult GetSupportActionsByReferenceTypeID(int ticketReferenceTypeID)
        {
            var result = ClientFactory.SupportServiceClient(CallContext).GetSupportActionsByReferenceTypeID(ticketReferenceTypeID);
            return Json(result);
        }

        public JsonResult GetSupportSubCategoriesByCategoryID(int? supportCategoryID)
        {
            var result = ClientFactory.SupportServiceClient(CallContext).GetSupportSubCategoriesByCategoryID(supportCategoryID);
            return Json(result);
        }

    }
}
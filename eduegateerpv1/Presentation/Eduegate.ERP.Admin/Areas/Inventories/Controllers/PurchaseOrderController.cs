using Eduegate.ERP.Admin.Controllers;
using Eduegate.ERP.Admin.Helpers;
using Eduegate.Framework.Extensions;
//using Eduegate.Framework.Web.Library.Common;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Notifications;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Web.Library.ViewModels.Inventory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Eduegate.ERP.Admin.Areas.Inventories.Controllers
{
    public class PurchaseOrderController : BaseSearchController
    {
        private string dateTimeFormat = ConfigurationExtensions.GetAppConfigValue("DateTimeFormat");

        // GET: Inventories/PurchaseOrder
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            var metadata = ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Infrastructure.Enums.SearchView.PurchaseOrder);
            return View(new SearchListViewModel
            {
                ControllerName = "Inventories/" + Infrastructure.Enums.SearchView.PurchaseOrder,
                ViewName = Infrastructure.Enums.SearchView.PurchaseOrder,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                IsCommentLink = true,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,

                InfoBar = @"<li class='status-label-mobile'>
                                                        <div class='right status-label'>
                                                            <div class='status-label-color'><label class='status-color-label  green'></label>Complete</div>
                                                            <div class='status-label-color'><label class='status-color-label red'></label>Fail</div>
                                                            <div class='status-label-color'><label class='status-color-label yellow'></label>Others</div>
                                                        </div>
                                                    </li>",

                FilterColumns = metadata.QuickFilterColumns,
                HasFilters = metadata.HasFilters,
                IsChild = false,
                HasChild = true,
                UserValues = metadata.UserValues
            });
        }
        [HttpGet]
        public ActionResult ChildList(long ID)
        {
            // call the service and get the meta data
            var metadata = ClientFactory.SearchServiceClient(CallContext).SearchList(Services.Contracts.Enums.SearchView.PurchaseSKU);

            return PartialView("_ChildList", new SearchListViewModel
            {
                ControllerName = "ProductSKU",
                ViewName = Infrastructure.Enums.SearchView.PurchaseSKU,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                //IsMultilineEnabled = metadata.MultilineEnabled,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                FilterColumns = metadata.QuickFilterColumns,
                RuntimeFilter = Convert.ToString("HeadIID=" + ID.ToString()).Trim(), // child view must have this column
                IsChild = true,
                ParentViewName = Infrastructure.Enums.SearchView.PurchaseOrder,
                IsEditableLink = false
            });
        }

        [HttpGet]
        public JsonResult SearchData(int currentPage = 1, string orderBy = "", string runtimeFilter = "")
        {
            var view = Infrastructure.Enums.SearchView.PurchaseOrder;
            if (runtimeFilter.Trim().IsNotNullOrEmpty())
            {
                view = Infrastructure.Enums.SearchView.PurchaseSKU;
                runtimeFilter = runtimeFilter + " AND CompanyID = " + CallContext.CompanyID.ToString();
            }
            else
            {
                runtimeFilter = " CompanyID = " + CallContext.CompanyID.ToString();
            }
            return base.SearchData(view, currentPage, orderBy, runtimeFilter);

        }

        [HttpGet]
        public JsonResult SearchSummaryData()
        {
            return base.SearchSummaryData(Infrastructure.Enums.SearchView.PurchaseOrderSummary);
        }

        [HttpGet]
        public ActionResult DetailedView(long IID = 0)
        {
            ViewBag.IID = IID;
            return View();
        }


        public ActionResult Create(long ID = 0)
        {
            var vm = new PurchaseOrderViewModel()
            {
                DetailViewModel = new List<PurchaseOrderDetailViewModel>() { new PurchaseOrderDetailViewModel() { } },
                MasterViewModel = new PurchaseOrderMasterViewModel()
                {
                    //Allocations = new BranchWiseAllocationViewModel(),
                    DocumentReferenceTypeID = DocumentReferenceTypes.PurchaseOrder,
                    TransactionDate = DateTime.Now.ToString(dateTimeFormat),
                    Validity = DateTime.Now.ToString(dateTimeFormat),
                    //Currency = KeyValueViewModel.ToViewModel(ClientFactory.ReferenceDataServiceClient(CallContext).GetDefaultCurrencyWithName()),
                    DocumentStatus = new KeyValueViewModel() { Key = "1", Value = Convert.ToString(Eduegate.Services.Contracts.Enums.DocumentStatuses.Draft) },
                    DocumentType = new KeyValueViewModel() { Key = "20", Value = Convert.ToString(Eduegate.Services.Contracts.Enums.DocumentReferenceTypes.PurchaseOrder) }
                }
            };

            //vm.MasterViewModel
            //vm.MasterViewModel.Allocations.Allocations = new List<QuantityAllocationViewModel>() { new QuantityAllocationViewModel() };
            //vm.MasterViewModel.DocumentReferenceTypeID = DocumentReferenceTypes.PurchaseOrder;
            return Json(vm, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult Get(long ID = 0, bool partialCalculation = false)
        {
            try
            {
                var vm = TransactionViewModel.FromDTOToPurchaseOrderVM(ClientFactory.TransactionServiceClient(CallContext).GetTransaction(ID, partialCalculation, false));
                if (vm != null)
                {
                    if ((vm.DetailViewModel.IsNull() || vm.DetailViewModel.Count == 0) && partialCalculation)
                    {
                        return Json(new { IsError = true, UserMessage = "No items available to create purchase invoice." }, JsonRequestBehavior.AllowGet);
                    }

                    vm.DetailViewModel.Add(new PurchaseOrderDetailViewModel());
                }
                return Json(vm, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<PurchaseOrderController>.Fatal(ex.Message.ToString(), ex);
                return Json(new { IsError = true, UserMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Save(PurchaseOrderViewModel vm)
        {
            try
            {
                var updatedVM = new PurchaseOrderViewModel();

                if (vm != null)
                {
                    var result = ClientFactory.TransactionServiceClient(CallContext).SaveTransactions(TransactionViewModel.ToDTOFromPurchaseOrderVM(vm));
                    updatedVM = TransactionViewModel.FromDTOToPurchaseOrderVM(result);

                    // once TransactionStatus completed we should not allow any operation on any transaction..
                    if (updatedVM.MasterViewModel.IsTransactionCompleted)
                    {
                        return Json(new { IsError = true, UserMessage = "Cannot update the trasaction, it's already processed.", data = vm }, JsonRequestBehavior.AllowGet);
                    }
                    if (updatedVM.MasterViewModel.IsError == true)
                    {
                        return Json(new { IsError = true, UserMessage = PortalWebHelper.GetErrorMessage(updatedVM.MasterViewModel.ErrorCode), data = vm }, JsonRequestBehavior.AllowGet);
                    }
                }

                var emailNotificationTypeDetail = ClientFactory.NotificationServiceClient(CallContext).GetEmailNotificationType(Services.Contracts.Enums.EmailNotificationTypes.OrderNotificationForSupplier);

                if (emailNotificationTypeDetail != null)
                {
                    // Add notification to send email to supplier
                    AddOrderNotification(updatedVM.MasterViewModel.TransactionHeadIID, updatedVM.MasterViewModel.Supplier.Key, emailNotificationTypeDetail, updatedVM.MasterViewModel.TransactionNo, updatedVM.MasterViewModel.DeliveryDate);
                }

                return Json(updatedVM, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<PurchaseOrderController>.Fatal(ex.Message.ToString(), ex);
                return Json(new { IsError = true, UserMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        private string AddOrderNotification(long orderID, string supplierID, EmailNotificationTypeDTO emailNotificationTypeDetail, string transactionNumber, string deliveryDate)
        {
            if (string.IsNullOrEmpty(supplierID))
            {
                throw new Exception("No supplier infor to send email.");
            }

            var notificationDTO = new EmailNotificationDTO();
            notificationDTO.EmailNotificationType = Eduegate.Services.Contracts.Enums.EmailNotificationTypes.OrderNotificationForSupplier; //TODO: this should be based on payment status
            notificationDTO.AdditionalParameters = new List<Eduegate.Services.Contracts.Commons.KeyValueParameterDTO>();
            notificationDTO.FromEmailID = ConfigurationExtensions.GetAppConfigValue("EmailFrom").ToString();

            // Get supplier's emailid
            var supplierDetail = ClientFactory.SupplierServiceClient(CallContext).GetSupplier(supplierID);
            string websiteUrl = string.Empty;
            var domainsetting = ClientFactory.SettingServiceClient(CallContext).GetSettingDetailByCompany("DOMAINNAME", (long)CallContext.CompanyID);

            if (domainsetting.IsNotNull())
            {
                websiteUrl = domainsetting.SettingValue + ": ";
            }


            notificationDTO.Subject = string.Concat(websiteUrl, transactionNumber, " | ", supplierDetail.FirstName, " ", supplierDetail.LastName, " ");

            // Add OrderID, ReportName, Attachment flag
            notificationDTO.AdditionalParameters.Add(new Eduegate.Services.Contracts.Commons.KeyValueParameterDTO() { ParameterName = Eduegate.Services.Contracts.Constants.EmailNotificationType.OrderNotificationForSupplier.Keys.HeadID, ParameterValue = orderID.ToString() });
            notificationDTO.AdditionalParameters.Add(new Eduegate.Services.Contracts.Commons.KeyValueParameterDTO() { ParameterName = Eduegate.Services.Contracts.Constants.EmailNotificationType.OrderNotificationForSupplier.Keys.ReportName, ParameterValue = "Something" });
            notificationDTO.AdditionalParameters.Add(new Eduegate.Services.Contracts.Commons.KeyValueParameterDTO() { ParameterName = Eduegate.Services.Contracts.Constants.EmailNotificationType.OrderNotificationForSupplier.Keys.Attachment, ParameterValue = "true" });
            notificationDTO.AdditionalParameters.Add(new Eduegate.Services.Contracts.Commons.KeyValueParameterDTO() { ParameterName = Eduegate.Services.Contracts.Constants.EmailNotificationType.OrderNotificationForSupplier.Keys.ReturnFile, ParameterValue = "true" });

            // Picking supplier emails from contacts
            if (supplierDetail.Contacts.IsNotNull() && supplierDetail.Contacts.Count > 0)
            {

                var supplierEmailIds = string.Empty;

                foreach (var contact in supplierDetail.Contacts)
                {
                    supplierEmailIds = supplierEmailIds + (contact.AlternateEmailID1.IsNotNullOrEmpty() ? contact.AlternateEmailID1 + "," : string.Empty) + (contact.AlternateEmailID2.IsNotNullOrEmpty() ? contact.AlternateEmailID2 + "," : string.Empty);
                }

                supplierEmailIds = supplierEmailIds.TrimEnd(',');

                notificationDTO.ToEmailID = supplierEmailIds;
                notificationDTO.AdditionalParameters.Add(new Eduegate.Services.Contracts.Commons.KeyValueParameterDTO() { ParameterName = Eduegate.Services.Contracts.Constants.EmailNotificationType.OrderNotificationForSupplier.Keys.ErrorMessage, ParameterValue = "" });
            }
            else
            {
                notificationDTO.ToEmailID = ConfigurationExtensions.GetAppConfigValue("EmailFrom").ToString();
                notificationDTO.AdditionalParameters.Add(new Eduegate.Services.Contracts.Commons.KeyValueParameterDTO() { ParameterName = Eduegate.Services.Contracts.Constants.EmailNotificationType.OrderNotificationForSupplier.Keys.ErrorMessage, ParameterValue = string.Concat("Could not Find Email Address for Supplier: ", supplierDetail.FirstName, " (supplierID: ", supplierID, ")") });
            }
            //notificationDTO.AdditionalParameters.Add(new Eduegate.Services.Contracts.Commons.KeyValueParameterDTO()
            //{
            //    // Eduegate.sa.com : {PO#} {Payement terms} {Delivery Date}
            //    ParameterName = Eduegate.Services.Contracts.Constants.EmailNotificationType.Subject,
            //    ParameterValue = emailNotificationTypeDetail.IsNotNull() ? string.Concat(websiteUrl, transactionNumber, " | ", supplierDetail.FirstName, " ", supplierDetail.LastName, " ") : string.Empty,
            //});

            // add notification
            //var notificationReponse = new NotificationServiceClient(null).SaveEmailData(notificationDTO);
            var notificationReponse = Task<EmailNotificationDTO>.Factory.StartNew(() => ClientFactory.NotificationServiceClient(null).SaveEmailData(notificationDTO));
            return notificationReponse.ToString();
        }

    }
}
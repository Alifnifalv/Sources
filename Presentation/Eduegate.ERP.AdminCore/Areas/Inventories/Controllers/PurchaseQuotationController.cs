using Eduegate.ERP.Admin.Controllers;
using Eduegate.ERP.Admin.Helpers;
using Eduegate.Framework.Extensions;
//using Eduegate.Framework.Web.Library.Common;
using Eduegate.Services.Client.Factory;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Web.Library.ViewModels.Inventory.Purchase;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Eduegate.Domain;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Domain.Repository;

namespace Eduegate.ERP.Admin.Areas.Inventories.Controllers
{
    [Area("Inventories")]
    public class PurchaseQuotationController : BaseSearchController
    {
        private string dateTimeFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateTimeFormat");

        // GET: Inventories/PurchaseQuotation
        public ActionResult Index()
        {
            return View();
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
            var vm = new PurchaseQuotationViewModel()
            {
                DetailViewModel = new List<PurchaseQuotationDetailViewModel>() { new PurchaseQuotationDetailViewModel() { } },
                MasterViewModel = new PurchaseQuotationMasterViewModel()
                {
                    TransactionDate = DateTime.Now.ToString(dateTimeFormat),
                    DocumentStatus = new KeyValueViewModel() { Key = "3", Value = Convert.ToString(Eduegate.Services.Contracts.Enums.DocumentStatuses.Approved) }
                }
            };

            return Json(vm);
        }

        [HttpGet]
        public ActionResult Edit(long ID = 0)
        {
            return RedirectToAction("Create", new { ID = ID });
        }

        [HttpGet]
        public ActionResult Get(long ID = 0)
        {
            var vm = TransactionViewModel.FromDTOToPurchaseQuotationVM(ClientFactory.TransactionServiceClient(CallContext).GetTransaction(ID, false, false));

            if (vm != null)
                vm.DetailViewModel.Add(new PurchaseQuotationDetailViewModel());

            return Json(vm);
        }

        [HttpPost]
        public JsonResult Save(string model)
        {
            try
            {
                var jsonData = JObject.Parse(model);
                var vm = JsonConvert.DeserializeObject<PurchaseQuotationViewModel>(jsonData.SelectToken("data").ToString());

                var updatedVM = new PurchaseQuotationViewModel();

                if (vm != null)
                {
                    var result = ClientFactory.TransactionServiceClient(CallContext).SaveTransactions(TransactionViewModel.ToDTOFromPurchaseQuotationVM(vm));

                    //Save to Mapping Table 
                    ClientFactory.TransactionServiceClient(CallContext).SaveRFQMappingData(result);

                    if (vm.MasterViewModel.SendMailNotification)
                    {
                        var purposeMsg = "Please review the request and provide us with your quotation at your earliest convenience.";
                        new TransactionBL(CallContext).SendTransactionMailToSupplier(result, purposeMsg);
                    }

                    updatedVM = TransactionViewModel.FromDTOToPurchaseQuotationVM(result);
                    updatedVM.DetailViewModel.Add(new PurchaseQuotationDetailViewModel());
                }

                // once TransactionStatus completed we should not allow any operation on any transaction..
                if (updatedVM.MasterViewModel.IsTransactionCompleted)
                {
                    return Json(new { IsError = true, UserMessage = "Cannot update the trasaction, it's already processed.", data = vm });
                }

                if (updatedVM.MasterViewModel.IsError == true)
                {
                    return Json(new { IsError = true, UserMessage = PortalWebHelper.GetErrorMessage(updatedVM.MasterViewModel.ErrorCode), data = vm });
                }

                return Json(updatedVM);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<PurchaseQuotationController>.Fatal(ex.Message.ToString(), ex);
                return Json(new { IsError = true, UserMessage = ex.Message });
            }
        }

        public JsonResult GetProductsByPurchaseRequestID([FromBody] List<KeyValueViewModel> RequestIDs)
        {
            var request_IDs = new List<long>();
            if (RequestIDs != null && RequestIDs.Any())
                request_IDs = RequestIDs.Select(w => long.Parse(w.Key)).ToList();
            var result = ClientFactory.TransactionServiceClient(CallContext).GetProductsByPurchaseRequestID(request_IDs);
            return Json(result);
        }

        #region Quotation Compare Screen
        public JsonResult FillQuotationItemList([FromBody] List<KeyValueViewModel> QuotationIDs)
        {
            var quotation_IDs = new List<long>();
            if (QuotationIDs != null && QuotationIDs.Any())
                quotation_IDs = QuotationIDs.Select(w => long.Parse(w.Key)).ToList();
            var result = ClientFactory.TransactionServiceClient(CallContext).FillQuotationItemList(quotation_IDs);
            return Json(result);
        }        

        public JsonResult FillQuotationsByRFQ(string rfqHeadIID)
        {
            var result = ClientFactory.TransactionServiceClient(CallContext).FillQuotationsByRFQ(rfqHeadIID);
            return Json(result);
        }

        #endregion

        #region Bid Opening Screen
        public JsonResult FillBidLookUpByRFQ(string rfqHeadIID)
        {
            var result = ClientFactory.TransactionServiceClient(CallContext).FillBidLookUpByRFQ(rfqHeadIID); 
            return Json(result);
        } 
        
        public JsonResult FillBidItemList(string bidApprovalIID)
        {
            var result = ClientFactory.TransactionServiceClient(CallContext).FillBidItemList(bidApprovalIID); 
            return Json(result);
        }
        #endregion


        public JsonResult GetEmployeeDetailsByEmployeeIID(long employeeIID)
        {
            var result = new Eduegate.Domain.Repository.Payroll.EmployeeRepository().GetEmployeeDetailsByEmployeeIID(employeeIID);
            return Json(result);
        }   
        
        public JsonResult GetBidUserDetailsByAuthenticationID(long authendicationID)
        {
            var result = new Eduegate.Domain.Repository.SupplierRepository().GetBidUserDetailsByAuthenticationID(authendicationID);
            return Json(result);
        }

        public JsonResult GetSupplierRemarkList([FromBody] List<KeyValueViewModel> QuotationIDs)
        {
            var quotation_IDs = new List<long>();
            if (QuotationIDs != null && QuotationIDs.Any())
                quotation_IDs = QuotationIDs.Select(w => long.Parse(w.Key)).ToList();
            var result = new Eduegate.Domain.Repository.SupplierRepository().GetSupplierRemarkList(quotation_IDs);
            return Json(result);
        }
    }
}
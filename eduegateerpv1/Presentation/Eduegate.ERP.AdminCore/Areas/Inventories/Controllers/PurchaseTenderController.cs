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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Eduegate.ERP.Admin.Areas.Inventories.Controllers
{
    [Area("Inventories")]
    public class PurchaseTenderController : BaseSearchController
    {
        private string dateTimeFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateTimeFormat");

        // GET: Inventories/PurchaseTender
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
            var vm = new PurchaseTenderViewModel()
            {
                DetailViewModel = new List<PurchaseTenderDetailViewModel>() { new PurchaseTenderDetailViewModel() { } },
                MasterViewModel = new PurchaseTenderMasterViewModel()
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
            var vm = TransactionViewModel.FromDTOToPurchaseTenderVM(ClientFactory.TransactionServiceClient(CallContext).GetTransaction(ID, false, false));

            if (vm != null)
                vm.DetailViewModel.Add(new PurchaseTenderDetailViewModel());

            return Json(vm);
        }

        [HttpPost]
        public JsonResult Save(string model)
        {
            try
            {
                var jsonData = JObject.Parse(model);
                var vm = JsonConvert.DeserializeObject<PurchaseTenderViewModel>(jsonData.SelectToken("data").ToString());

                var updatedVM = new PurchaseTenderViewModel();

                if (vm != null)
                {
                    var result = ClientFactory.TransactionServiceClient(CallContext).SaveTransactions(TransactionViewModel.ToDTOFromPurchaseTenderVM(vm));
                    updatedVM = TransactionViewModel.FromDTOToPurchaseTenderVM(result);
                    updatedVM.DetailViewModel.Add(new PurchaseTenderDetailViewModel());
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
                Eduegate.Logger.LogHelper<PurchaseTenderController>.Fatal(ex.Message.ToString(), ex);
                return Json(new { IsError = true, UserMessage = ex.Message });
            }
        }

    }
}
using Eduegate.ERP.Admin.Controllers;
using Eduegate.ERP.Admin.Helpers;
using Eduegate.Framework.Extensions;
//using Eduegate.Framework.Web.Library.Common;
using Eduegate.Services.Client.Factory;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Web.Library.ViewModels.Inventory;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Eduegate.Domain;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Eduegate.ERP.Admin.Areas.Inventories.Controllers
{
    [Area("Inventories")]
    public class OrderChangeRequestController : BaseSearchController
    {
        private string dateTimeFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateTimeFormat");
        // GET: Inventories/Replacement
        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> List()
        {
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Infrastructure.Enums.SearchView.OrderChangeRequest);

            return View(new SearchListViewModel
            {
                ControllerName = "Inventories/" + Infrastructure.Enums.SearchView.OrderChangeRequest,
                ViewName = Infrastructure.Enums.SearchView.OrderChangeRequest,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                UserViews = metadata.UserViews,
                SortColumns = metadata.SortColumns,
                IsMultilineEnabled = false,
                //IsMultilineEnabled = metadata.MultilineEnabled,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                //                InfoBar = @"<li class='status-label-mobile'>
                //                                <div class='right status-label'>
                //                                    <div class='status-label-color'><label class='status-color-label orange'></label>Draft</div>
                //                                    <div class='status-label-color'><label class='status-color-label  green'></label>Active</div>
                //                                    <div class='status-label-color'><label class='status-color-label red'></label>In Active</div>
                //                                </div>
                //                            </li>",
                HasFilters = metadata.HasFilters,
                FilterColumns = metadata.QuickFilterColumns,
                UserValues = metadata.UserValues
            });
        }

        [HttpGet]
        public Task<IActionResult> SearchData(int currentPage = 1, string orderBy = "")
        {
            return base.SearchData(Infrastructure.Enums.SearchView.OrderChangeRequest, currentPage, orderBy);
        }

        [HttpGet]
        public Task<IActionResult> SearchSummaryData()
        {
            return base.SearchSummaryData(Infrastructure.Enums.SearchView.OrderChangeRequestSummary);
        }

        public ActionResult Create(long ID = 0)
        {
            var vm = new OrderChangeRequestViewModel()
            {
                DetailViewModel = new List<OrderChangeRequestDetailViewModel>() { new OrderChangeRequestDetailViewModel() { } },
                MasterViewModel = new OrderChangeRequestMasterViewModel()
                {
                    TransactionDate = DateTime.Now.ToString(dateTimeFormat),
                    Currency = KeyValueViewModel.ToViewModel(ClientFactory.ReferenceDataServiceClient(CallContext).GetDefaultCurrencyWithName()),
                    DocumentStatus = new KeyValueViewModel() { Key = "1", Value = Convert.ToString(Eduegate.Services.Contracts.Enums.DocumentStatuses.Draft) },
                },
            };

            return Json(vm);
        }


        [HttpPost]
        public JsonResult Save(string model)
        {
            var jsonData = JObject.Parse(model);
            var vm = JsonConvert.DeserializeObject<OrderChangeRequestViewModel>(jsonData.SelectToken("data").ToString());

            //return Json(vm);
            try
            {
                if (vm.DetailViewModel != null && vm.DetailViewModel.Count > 0)
                {
                    foreach (var transactionDetail in vm.DetailViewModel)
                    {
                        if (transactionDetail.ChildGrid != null && transactionDetail.ChildGrid.Count > 0)
                        {
                            foreach (var child in transactionDetail.ChildGrid)
                            {
                                if (child.Quantity > transactionDetail.Quantity)
                                {
                                    return Json(new { IsError = true, UserMessage = "Quantity mismatch!", data = vm });
                                }
                            }
                        }
                    }
                }
                var updatedVM = new OrderChangeRequestViewModel();
                if (vm != null)
                {
                    var child = ClientFactory.TransactionServiceClient(CallContext).SaveTransactions(TransactionViewModel.ToDTOFromOrderChangeRequestViewModel(vm));
                    var parent = ClientFactory.TransactionServiceClient(CallContext).GetTransaction(vm.MasterViewModel.ReferenceTransactionHeaderID.Value, false, false);
                    updatedVM = TransactionViewModel.ToOrderChangeRequestViewModel(parent, child);
                }

                if (updatedVM.MasterViewModel.IsError == true)
                {
                    return Json(new { IsError = true, UserMessage = PortalWebHelper.GetErrorMessage(updatedVM.MasterViewModel.ErrorCode), data = vm });
                }
                return Json(updatedVM);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<OrderChangeRequestController>.Fatal(ex.Message.ToString(), ex);
                return Json(new { IsError = true, UserMessage = ex.Message });
            }

        }

        [HttpGet]
        public ActionResult Get(long ID = 0)
        {
            try
            {
                var child = ClientFactory.TransactionServiceClient(CallContext).GetTransaction(ID, false, false);
                var parent = ClientFactory.TransactionServiceClient(CallContext).GetTransaction((long)child.TransactionHead.ReferenceHeadID, false, false);
                var vm = TransactionViewModel.ToOrderChangeRequestViewModel(parent, child);

                if (vm.MasterViewModel.IsError == true)
                {
                    return Json(new { IsError = true, UserMessage = PortalWebHelper.GetErrorMessage(vm.MasterViewModel.ErrorCode), data = vm });
                }
                return Json(vm);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<OrderChangeRequestController>.Fatal(ex.Message.ToString(), ex);
                return Json(new { IsError = true, UserMessage = ex.Message });
            }
        }

        [HttpGet]
        public ActionResult Edit(long ID = 0)
        {
            return RedirectToAction("Create", new { ID = ID });
        }

    }
}
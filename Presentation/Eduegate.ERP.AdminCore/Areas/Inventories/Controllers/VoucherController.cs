using Eduegate.ERP.Admin.Controllers;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Client.Factory;
using Eduegate.Web.Library.ViewModels;
using System;
using Microsoft.AspNetCore.Mvc;
using Eduegate.Domain;

namespace Eduegate.ERP.Admin.Areas.Inventories.Controllers
{
    [Area("Inventories")]
    public class VoucherController : BaseSearchController
    {
        // GET: Voucher

        private string dateTimeFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateTimeFormat");
        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> List()
        {
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Infrastructure.Enums.SearchView.Voucher);
            ViewBag.VoucherAdminAccess = ClientFactory.UserServiceClient(CallContext).HasClaimAccessByResourceID("VOUCHERADMIN", CallContext.LoginID.Value);

            return View(new SearchListViewModel
            {
                ControllerName = Infrastructure.Enums.SearchView.Voucher.ToString(),
                ViewName = Infrastructure.Enums.SearchView.Voucher,
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
        public Task<IActionResult> SearchData(int currentPage = 1, string orderBy = "", string runtimeFilter = "")
        {
            runtimeFilter = " CompanyId = " + CallContext.CompanyID.ToString();
            return base.SearchData(Infrastructure.Enums.SearchView.Voucher, currentPage, orderBy, runtimeFilter);
        }

        [HttpGet]
        public Task<IActionResult> SearchSummaryData()
        {
            return base.SearchSummaryData(Infrastructure.Enums.SearchView.VoucherSummary);
        }

        [HttpGet]
        public ActionResult Create(long ID = 0)
        {
            var viewModel = new CRUDViewModel();
            viewModel.Name = "Voucher";
            viewModel.ListActionName = "Voucher";
            viewModel.ViewModel = new VoucherViewModel();
            viewModel.IID = ID;
            //viewModel.Urls.Add(new UrlViewModel() { LookUpName = "VoucherType", Url = "Mutual/GetLookUpData?lookType=VoucherType" });
            //viewModel.Urls.Add(new UrlViewModel() { LookUpName = "VoucherStatus", Url = "Mutual/GetLookUpData?lookType=VoucherStatus" });
            ////viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Customer", Url = "Mutual/GetLookUpData?lookType=Customer" });
            //viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Customer", Url = "Mutual/GetLazyLookUpData?lookType=Customer&lookupName=LookUps.Customer&searchText=" });
            //TempData["viewModel"] = Newtonsoft.Json.JsonConvert.SerializeObject(viewModel);
            //return RedirectToAction("Create", "CRUD", new { });
            return Json(viewModel);
        }

        [HttpGet]
        public ActionResult Edit(long ID = 0)
        {
            return RedirectToAction("Create", new { ID = ID });
        }

        [HttpGet]
        public ActionResult Get(long ID = 0)
        {
            var dto = ClientFactory.VoucherServiceClient(CallContext).GetVoucher(ID.ToString());
            return Json(VoucherViewModel.FromDTO(dto));
        }

        [HttpPost]
        public JsonResult Save([FromBody] VoucherViewModel vm)
        {
            try
            {
                var updatedDTO = ClientFactory.VoucherServiceClient(CallContext).SaveVoucher(VoucherViewModel.ToDTO(vm));
                if (updatedDTO.IsError == true)
                {
                    return Json(new { IsError = true, UserMessage = "Expiry Date should not be previous date", data = vm });
                }
                return Json(VoucherViewModel.FromDTO(updatedDTO));
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<VoucherController>.Fatal(ex.Message.ToString(), ex);
                return Json(new { IsError = true, UserMessage = ex.Message });
            }
        }


        [HttpGet]
        public ActionResult DetailedView(long IID = 0)
        {
            ViewBag.IID = IID;
            return View("DetailedView");
        }

        [HttpGet]
        public string CreateVoucher()
        {
            return ClientFactory.VoucherServiceClient(CallContext).CreateVoucher(); ;
        }
    }
}
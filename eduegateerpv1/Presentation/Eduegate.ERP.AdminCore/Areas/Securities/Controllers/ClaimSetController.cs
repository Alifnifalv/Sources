using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Eduegate.ERP.Admin.Controllers;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts.Search;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Web.Library.ViewModels.Security;
using Eduegate.Services.Client.Factory;
using System.Threading.Tasks;

namespace Eduegate.ERP.Admin.Areas.Securities.Controllers
{
    [Area("Securities")]
    public class ClaimSetController : BaseSearchController
    {
        // GET: Securities/CalimSet
        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> List()
        {
            // call the service and get the meta data
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Eduegate.Infrastructure.Enums.SearchView.ClaimSet);
            return View(new SearchListViewModel
            {
                ControllerName = "/Securities/ClaimSet",
                ViewName = Eduegate.Infrastructure.Enums.SearchView.ClaimSet,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                //IsMultilineEnabled = metadata.MultilineEnabled,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                //InfoBar = ""
                HasFilters = metadata.HasFilters,
                FilterColumns = metadata.QuickFilterColumns,
                UserValues = metadata.UserValues
            });
        }

        [HttpGet]
        public Task<IActionResult> SearchData(int currentPage = 1, string orderBy = "", string runtimeFilter="")
        {
            if (new Domain.Setting.SettingBL(null).GetSettingValue<bool>("IsMultiCompany", true))
                runtimeFilter = " CompanyId = " + CallContext.CompanyID.ToString();
            return base.SearchData(Eduegate.Infrastructure.Enums.SearchView.ClaimSet, currentPage, orderBy, runtimeFilter);
        }

        [HttpGet]
        public Task<IActionResult> SearchSummaryData()
        {
            return base.SearchSummaryData(Eduegate.Infrastructure.Enums.SearchView.ClaimSetSummary);
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
            viewModel.Name = "/Securities/ClaimSet";
            viewModel.ListActionName = "/Securities/ClaimSet";
            viewModel.DisplayName = Eduegate.Globalization.ResourceHelper.GetValue("CLAIMSET_TITLE") + " Maintenance";
            var claimTypes = ClientFactory.ReferenceDataServiceClient(CallContext).GetLookUpData(Eduegate.Services.Contracts.Enums.LookUpTypes.ClaimType, string.Empty,0 ,0);
            var vm = new ClaimSetViewModel();
            vm.Claims = Eduegate.Web.Library.ViewModels.Security.SecurityClaimViewModel2.GetSecurityVM(claimTypes, new List<Eduegate.Services.Contracts.Admin.ClaimDetailDTO>());
            //viewModel.ViewModel = vm;
            //viewModel.IID = ID;
            //viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Claims", Url = "Mutual/GetLookUpData?lookType=Claims" });
            //viewModel.Urls.Add(new UrlViewModel() { LookUpName = "ClaimSets", Url = "Mutual/GetLookUpData?lookType=ClaimSets" });
            return Json(vm);
            //TempData["viewModel"] = Newtonsoft.Json.JsonConvert.SerializeObject(viewModel);
            //return RedirectToAction("Create", "CRUD", new { area = "" });
        }

        [HttpGet]
        public ActionResult Edit(long ID = 0)
        {
            return RedirectToAction("Create", new { ID = ID });
        }

        [HttpGet]
        public ActionResult Get(long ID = 0)
        {
            var claimTypes = ClientFactory.ReferenceDataServiceClient(CallContext).GetLookUpData(Eduegate.Services.Contracts.Enums.LookUpTypes.ClaimType, string.Empty, 0, 0);
            var vm = ClaimSetViewModel.FromDTO(ClientFactory.SecurityServiceClient(CallContext).GetClaimSet(ID), claimTypes);
            return Json(vm);
        }

        [HttpPost]
        public JsonResult Save([FromBody] ClaimSetViewModel vm)
        {
            var claimTypes = ClientFactory.ReferenceDataServiceClient(CallContext).GetLookUpData(Eduegate.Services.Contracts.Enums.LookUpTypes.ClaimType, string.Empty, 0, 0);
            var updatedVM = ClaimSetViewModel.FromDTO(ClientFactory.SecurityServiceClient(CallContext).SaveClaimSet(ClaimSetViewModel.ToDTO(vm)), claimTypes);
            return Json(updatedVM);
        }
    }
}
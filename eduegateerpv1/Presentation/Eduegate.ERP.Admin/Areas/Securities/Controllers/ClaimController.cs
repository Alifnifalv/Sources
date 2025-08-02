using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eduegate.ERP.Admin.Controllers;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts.Search;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Web.Library.ViewModels.Common;
using Eduegate.Web.Library.ViewModels.Security;
using Eduegate.Services.Client.Factory;

namespace Eduegate.ERP.Admin.Areas.Securities.Controllers
{
    public class ClaimController : BaseSearchController
    {
        // GET: Securities/Claim
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            var metadata = ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Eduegate.Infrastructure.Enums.SearchView.Claim);

            return View(new SearchListViewModel
            {
                ControllerName = "Securities/" + Eduegate.Infrastructure.Enums.SearchView.Claim.ToString(),
                ViewName = Eduegate.Infrastructure.Enums.SearchView.Claim,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                //IsMultilineEnabled = metadata.MultilineEnabled,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                //                InfoBar = @"<li class='status-label-mobile'>
                //                                        <div class='right status-label'>
                //                                            <div class='status-label-color'><label class='status-color-label  green'></label>Active</div>
                //                                            <div class='status-label-color'><label class='status-color-label red'></label>In Active</div>
                //                                        </div>
                //                                    </li>"
                HasFilters = metadata.HasFilters,
                FilterColumns = metadata.QuickFilterColumns,
                UserValues = metadata.UserValues,
            });
        }

        [HttpGet]
        public JsonResult SearchData(int currentPage = 1, string orderBy = "", String runtimeFilter="")
        {
            if (Eduegate.Framework.Extensions.ConfigurationExtensions.GetAppConfigValueWithDefault<bool>("IsMultiCompany", true))
                runtimeFilter = " CompanyId = " + CallContext.CompanyID.ToString();

            return base.SearchData(Eduegate.Infrastructure.Enums.SearchView.Claim, currentPage, orderBy, runtimeFilter);
        }

        [HttpGet]
        public JsonResult SearchSummaryData()
        {
            return base.SearchSummaryData(Eduegate.Infrastructure.Enums.SearchView.ClaimSummary);
        }

        [HttpGet]
        public ActionResult DetailedView(long IID = 0)
        {
            ViewBag.IID = IID;
            return View();
        }

        public ActionResult Create(long ID = 0)
        {
            var viewModel = new CRUDViewModel();
            viewModel.Name = "Claim";
            viewModel.ListActionName = "Claim";
            viewModel.DisplayName = Eduegate.Globalization.ResourceHelper.GetValue("CLAIM_TITLE") + " Maintenance";
            viewModel.ViewModel = new ClaimViewModel();
            //viewModel.IID = ID;
            //viewModel.Urls.Add(new UrlViewModel() { LookUpName = "ClaimType", Url = "Mutual/GetLookUpData?lookType=ClaimType" });

            //TempData["viewModel"] = viewModel;
            //return RedirectToAction("Create", "CRUD", new { area = "" });
            return Json(viewModel.ViewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Edit(long ID = 0)
        {
            return RedirectToAction("Create", new { ID = ID });
        }

        [HttpGet]
        public ActionResult Get(long ID = 0)
        {
            var vm = ClaimViewModel.FromDTO(ClientFactory.SecurityServiceClient(CallContext).GetClaim(ID));
            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(ClaimViewModel vm)
        {
            var updatedVM = ClaimViewModel.FromDTO(ClientFactory.SecurityServiceClient(CallContext).SaveClaim(ClaimViewModel.ToDTO(vm)));
            return Json(updatedVM, JsonRequestBehavior.AllowGet);
        }
    }
}
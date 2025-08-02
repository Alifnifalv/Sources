using Eduegate.ERP.Admin.Controllers;
using Eduegate.Services.Client.Factory;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Web.Mvc;

namespace Eduegate.ERP.Admin.Areas.Inventories.Controllers
{
    public class CustomerGroupController : BaseSearchController
    {
        // GET: CustomerGroup
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            // call the service and get the meta data
            var metadata = ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Infrastructure.Enums.SearchView.CustomerGroup);

            return View(new SearchListViewModel
            {
                ControllerName = Infrastructure.Enums.SearchView.CustomerGroup.ToString(),
                ViewName = Infrastructure.Enums.SearchView.CustomerGroup,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                UserViews = metadata.UserViews,
                SortColumns = metadata.SortColumns,
                IsMultilineEnabled = false,
                //IsMultilineEnabled = metadata.MultilineEnabled,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                //                InfoBar = @"<li class='status-label-mobile'>
                //                                        <div class='right status-label'>
                //                                            <div class='status-label-color'><label class='status-color-label orange'></label>Draft</div>
                //                                            <div class='status-label-color'><label class='status-color-label  green'></label>Active</div>
                //                                            <div class='status-label-color'><label class='status-color-label red'></label>In Active</div>
                //                                        </div>
                //                                    </li>"
                FilterColumns = metadata.QuickFilterColumns,
                HasFilters = metadata.HasFilters,
                UserValues = metadata.UserValues
            });
        }

        [HttpGet]
        public JsonResult SearchData(int currentPage = 1, string orderBy = "")
        {
            return base.SearchData(Infrastructure.Enums.SearchView.CustomerGroup, currentPage, orderBy);
        }

        [HttpGet]
        public JsonResult SearchSummaryData()
        {
            return base.SearchSummaryData(Infrastructure.Enums.SearchView.CustomerGroupSummary);
        }

        [HttpGet]
        public ActionResult Create(long ID = 0)
        {
            var viewModel = new CRUDViewModel();
            viewModel.Name = "CustomerGroup";
            viewModel.ListActionName = "CustomerGroup";
            viewModel.ViewModel = new CustomerGroupViewModel();
            viewModel.IID = ID;
            TempData["viewModel"] = viewModel;
            return RedirectToAction("Create", "CRUD");
        }

        [HttpGet]
        public ActionResult Edit(long ID = 0)
        {
            return RedirectToAction("Create", new { ID = ID });
        }

        [HttpGet]
        public ActionResult DetailedView(long IID = 0)
        {
            ViewBag.IID = IID;
            return View("DetailedView");
        }

        [HttpGet]
        public ActionResult Get(long ID = 0)
        {
            return Json(CustomerGroupViewModel.FromDTO(ClientFactory.CustomerServiceClient(CallContext).GetCustomerGroup(ID.ToString())), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(CustomerGroupViewModel vm)
        {
            if (ModelState.IsValid)
            {
                return Json(CustomerGroupViewModel.FromDTO(ClientFactory.CustomerServiceClient(CallContext).SaveCustomerGroup(CustomerGroupViewModel.ToDTO(vm))), JsonRequestBehavior.AllowGet);
            }
            {
                throw new Exception("Validation failed.");
            }
        }

    }
}
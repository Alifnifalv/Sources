using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eduegate.ERP.Admin.Controllers;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Web.Library.ViewModels.Common;
using Eduegate.Web.Library.ViewModels.HR;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Client.Factory;

namespace Eduegate.ERP.Admin.Areas.HR.Controllers
{
    public class ArchivedJobController : BaseSearchController
    {
        // GET: HR/ArchivedJob
        public ActionResult Index()
        {
            return View(); 
        }

        public ActionResult List()
        {
            var metadata = ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Eduegate.Infrastructure.Enums.SearchView.ArchivedJobs);
            var hasAccess = ClientFactory.UserServiceClient(CallContext).HasClaimAccessByResourceID("EDITACCESS", CallContext.LoginID.Value);

            return View(new SearchListViewModel
            {
                ViewName = Eduegate.Infrastructure.Enums.SearchView.ArchivedJobs,
                ControllerName = "HR/ArchivedJob",
                HeaderList = metadata.Columns,
                SummaryHeaderList = new List<Services.Contracts.Search.ColumnDTO>(),
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                //IsMultilineEnabled = metadata.MultilineEnabled,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
//                InfoBar = @"<li class='status-label-mobile'>
//                            <div class='right status-label'>
//                                <div class='status-label-color'><label class='status-color-label purple'></label>Submitted by Dept</div>
//                                <div class='status-label-color'><label class='status-color-label yellow'></label>Visa company updated by Personnel</div>
//                                <div class='status-label-color'><label class='status-color-label lightblue'></label>Approved by management</div>
//                                <br/>
//                                <div class='status-label-color'><label class='status-color-label orange'></label>Updated in Passport</div>
//                                <div class='status-label-color'><label class='status-color-label blue'></label>Candidate joins and Joining memo send</div>
//                                <div class='status-label-color'><label class='status-color-label green'></label>Completed</div>
//                            </div>
//                        </li>",
                HasFilters = metadata.HasFilters,
                FilterColumns = metadata.QuickFilterColumns,
                UserValues = metadata.UserValues,
                IsEditableLink = hasAccess ? true: false,
            });
        }

        [HttpGet]
        public JsonResult SearchData(int currentPage = 1, string orderBy = "", string runtimeFilter = "")
        {
            var filter = " DepartmentId in (";

            var claims = ClientFactory.SecurityServiceClient(CallContext).GetClaimsByType(CallContext.LoginID.Value, Eduegate.Services.Contracts.Enums.ClaimType.Department);

            foreach (var claim in claims)
            {
                filter = string.Concat(filter, claim.ResourceName, ",");
            }

            filter = string.Concat(filter, "-1)");

            filter = filter + " and JobIID in (";

            claims = ClientFactory.SecurityServiceClient(CallContext).GetClaimsByType(CallContext.LoginID.Value, Eduegate.Services.Contracts.Enums.ClaimType.JobOpening);

            foreach (var claim in claims)
            {
                filter = string.Concat(filter, claim.ResourceName, ",");
            }

            filter = string.Concat(filter, "-1)");
            return base.SearchData(Eduegate.Infrastructure.Enums.SearchView.ArchivedJobs, currentPage, orderBy, filter);
        }

        [HttpGet]
        public JsonResult SearchSummaryData()
        {
            return base.SearchSummaryData(Eduegate.Infrastructure.Enums.SearchView.JobOpeningSummary);
        }

        [HttpGet]
        public ActionResult DetailedView(string IID = "")
        {
            ViewBag.IID = IID;
            return View();
        }

        [HttpGet]
        public ActionResult Create(string ID = "")
        {
            var viewModel = new CRUDViewModel();
            viewModel.Name = "ArchivedJob";
            viewModel.ListActionName = "HR/ArchivedJob";
            viewModel.ListButtonDisplayName = "Job Opening List";
            viewModel.DisplayName = "Create/Edit Archived Job";
            viewModel.ViewModel = new JobOpeningViewModel() { CreatedDate = DateTime.Now.ToString() };
            viewModel.IID = 0;
            viewModel.IID2 = ID;
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "Department", Url = "Mutual/GetLookUpData?lookType=DBDepartment" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "TypeOfJob", Url = "Mutual/GetLookUpData?lookType=TypeOfJob" });
            viewModel.Urls.Add(new UrlViewModel() { LookUpName = "JobStatus", Url = "Mutual/GetLookUpData?lookType=JobOpeningStatus" });
            TempData["viewModel"] = viewModel;
            return RedirectToAction("Create", "CRUD", new { area = "" });
        }

        [HttpGet]
        public ActionResult Edit(string ID = "")
        {
            return RedirectToAction("Create", new { ID = ID });
        }

        [HttpGet]
        public ActionResult Get(string ID = null)
        {
            var dto = ClientFactory.EmployementServiceClient(CallContext).GetJobOpening(ID);
            var cultureData = ClientFactory.ReferenceDataServiceClient(CallContext).GetCultureList();
            return Json(new JobOpeningViewModel().ToVM(dto, cultureData), JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Save(JobOpeningViewModel vm)
        {
            var cultureData = ClientFactory.ReferenceDataServiceClient(CallContext).GetCultureList();
            var cultureVM = CultureDataInfoViewModel.FromDTO(cultureData);
            var dto = ClientFactory.EmployementServiceClient(CallContext).SaveJobOpening(JobOpeningViewModel.ToDTO(vm, cultureVM));
            return Json(new JobOpeningViewModel().ToVM(dto, cultureData), JsonRequestBehavior.AllowGet);
        }
    }
}
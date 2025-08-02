using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Eduegate.ERP.Admin.Controllers;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Web.Library.ViewModels.Common;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Client.Factory;
using System.Threading.Tasks;

namespace Eduegate.ERP.Admin.Areas.HR.Controllers
{
    [Area("HR")]
    public class JobProfileController : BaseSearchController
    {
        // GET: HR/JobProfile
        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> List()
        {
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Eduegate.Infrastructure.Enums.SearchView.JobProfile);
            var hasAccess = ClientFactory.UserServiceClient(CallContext).HasClaimAccessByResourceID("EDITACCESS", CallContext.LoginID.Value);
            ViewBag.HasEditAccess = hasAccess;

            return View(new SearchListViewModel
            {
                ViewName = Eduegate.Infrastructure.Enums.SearchView.JobProfile,
                ControllerName = "HR/JobProfile",
                HeaderList = metadata.Columns,
                SummaryHeaderList = new List<Services.Contracts.Search.ColumnDTO>(),
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = true,
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
                IsEditableLink = false,
                UserValues = metadata.UserValues
            });
        }

        [HttpGet]
        public Task<IActionResult> SearchData(int currentPage = 1, string orderBy = "", string runtimeFilter = "")
        {
            var filter = " DepartmentId in (";

            var claims = ClientFactory.SecurityServiceClient(CallContext).GetClaimsByType(CallContext.LoginID.Value, Eduegate.Services.Contracts.Enums.ClaimType.Department);

            foreach (var claim in claims)
            {
                filter = string.Concat(filter, claim.ResourceName, ",");
            }

            filter = string.Concat(filter, "-1) AND (STATUS IS NULL OR STATUS = 'ACTIVE')");

            filter = filter + " and JobIID in (";

            claims = ClientFactory.SecurityServiceClient(CallContext).GetClaimsByType(CallContext.LoginID.Value, Eduegate.Services.Contracts.Enums.ClaimType.JobOpening);

            foreach (var claim in claims)
            {
                filter = string.Concat(filter, claim.ResourceName, ",");
            }

            filter = string.Concat(filter, "-1)");

            return base.SearchData(Eduegate.Infrastructure.Enums.SearchView.JobProfile, currentPage, orderBy, filter);
        }

        [HttpGet]
        public Task<IActionResult> SearchSummaryData()
        {
            return base.SearchSummaryData(Eduegate.Infrastructure.Enums.SearchView.JobProfileSummary);
        }

        [HttpGet]
        public ActionResult DetailedView(string IID = "")
        {
            var hasAccess = ClientFactory.UserServiceClient(CallContext).HasClaimAccessByResourceID("EDITACCESS", CallContext.LoginID.Value);
            ViewBag.HasEditAccess = hasAccess;
            ViewBag.IID = IID;
            return View();
        }

        [HttpGet]
        public JsonResult MoveToActive(string id)
        {
            try
            {
                return Json(ClientFactory.EmployementServiceClient(CallContext).JobProfileStatusChange(id, "ACTIVE"));
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<BaseSearchController>.Fatal(ex.Message.ToString(), ex);
                return Json(ex.Message);
            }
        }

        [HttpGet]
        public JsonResult MoveToArchive(string id)
        {
            try
            {
                return Json(ClientFactory.EmployementServiceClient(CallContext).JobProfileStatusChange(id, "ARCHIVE"));
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<BaseSearchController>.Fatal(ex.Message.ToString(), ex);
                return Json(ex.Message);
            }
        }

        [HttpGet]
        public JsonResult MoveToSelected(string id)
        {
            try
            {
                return Json(ClientFactory.EmployementServiceClient(CallContext).JobProfileStatusChange(id, "SELECTED"));
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<BaseSearchController>.Fatal(ex.Message.ToString(), ex);
                return Json(ex.Message);
            }
        }
    }
}
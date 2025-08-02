using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eduegate.ERP.Admin.Controllers;


using Eduegate.Web.Library.ViewModels;
using Eduegate.Services.Client.Factory;

namespace Eduegate.ERP.Admin.Areas.HR.Controllers
{
    public class JobProfileSelectedController : BaseSearchController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            var metadata = ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Eduegate.Infrastructure.Enums.SearchView.JobProfile);
            var hasAccess = ClientFactory.UserServiceClient(CallContext).HasClaimAccessByResourceID("EDITACCESS", CallContext.LoginID.Value);
            ViewBag.HasEditAccess = hasAccess;

            return View(new SearchListViewModel
            {
                ViewName = Eduegate.Infrastructure.Enums.SearchView.JobProfile,
                ControllerName = "HR/JobProfileSelected",
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
        public JsonResult SearchData(int currentPage = 1, string orderBy = "", string runtimeFilter = "")
        {
            var filter = " DepartmentId in (";

            var claims = ClientFactory.SecurityServiceClient(CallContext).GetClaimsByType(CallContext.LoginID.Value, Eduegate.Services.Contracts.Enums.ClaimType.Department);

            foreach (var claim in claims)
            {
                filter = string.Concat(filter, claim.ResourceName, ",");
            }

            filter = string.Concat(filter, "-1) AND STATUS = 'SELECTED'");

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
        public JsonResult SearchSummaryData()
        {
            return base.SearchSummaryData(Eduegate.Infrastructure.Enums.SearchView.JobProfileSummary);
        }

        [HttpGet]
        public ActionResult DetailedView(string IID = "")
        {
            ViewBag.IID = IID;
            return View();
        }      
    }
}
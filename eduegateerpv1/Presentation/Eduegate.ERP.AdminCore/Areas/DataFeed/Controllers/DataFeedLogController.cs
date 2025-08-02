using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Eduegate.ERP.Admin.Controllers;
using Eduegate.Framework.Extensions;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Services.Client.Factory;

namespace Eduegate.ERP.Admin.Areas.DataFeed.Controllers
{
    [Area("DataFeed")]
    public class DataFeedLogController : BaseSearchController
    {
        // GET: DataFeed/DataFeedLog
        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> List()
        {
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Eduegate.Infrastructure.Enums.SearchView.DataFeedLog);

            return View(new SearchListViewModel
            {
                ControllerName = "DataFeed/" + Eduegate.Infrastructure.Enums.SearchView.DataFeedLog.ToString(),
                ViewName = Eduegate.Infrastructure.Enums.SearchView.DataFeedLog,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                IsEditableLink = false,
                //IsMultilineEnabled = metadata.MultilineEnabled,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                //InfoBar = ""
                FilterColumns = metadata.QuickFilterColumns,
                HasFilters = metadata.HasFilters,
                UserValues = metadata.UserValues,
                IsChild = false,
                HasChild = true,
            });
        }

        [HttpGet]
        public async Task<IActionResult> ChildList(long ID)
        {
            // call the service and get the meta data
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList(Services.Contracts.Enums.SearchView.DataFeedDetail);

            return PartialView("_ChildList", new SearchListViewModel
            {
                ControllerName = "DataFeedDetail",
                ViewName = Eduegate.Infrastructure.Enums.SearchView.DataFeedDetail,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                //IsMultilineEnabled = metadata.MultilineEnabled,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                FilterColumns = metadata.QuickFilterColumns,
                RuntimeFilter = Convert.ToString("DataFeedLogIID=" + ID.ToString()).Trim(), // child view must have this column
                IsChild = true,
                ParentViewName = Eduegate.Infrastructure.Enums.SearchView.DataFeedLog,
                IsEditableLink = false
            });
        }

        [HttpGet]
        public Task<IActionResult> SearchData(int currentPage = 1, string orderBy = "", String runtimeFilter = "")
        {
            //TODO: create runtime filter if supplier is marketplace(right now its on BL)
            //return base.SearchData(Eduegate.Infrastructure.Enums.SearchView.DataFeed, currentPage, orderBy);
            //runtimeFilter = " CompanyId = " + CallContext.CompanyID.ToString();
            //return base.SearchData(Eduegate.Infrastructure.Enums.SearchView.DataFeedLog, currentPage, orderBy, runtimeFilter);

            var view = Eduegate.Infrastructure.Enums.SearchView.DataFeedLog;
            var pageSize = 0;

            if (runtimeFilter.Trim().IsNotNullOrEmpty())
            {
                pageSize = int.MaxValue;
                view = Eduegate.Infrastructure.Enums.SearchView.DataFeedDetail;
                runtimeFilter = runtimeFilter + " AND CompanyID = " + CallContext.CompanyID.ToString();
            }
            else
            {
                runtimeFilter = " CompanyID = " + CallContext.CompanyID.ToString();
            }

            return base.SearchData(view, currentPage, orderBy, runtimeFilter, pageSize);

        }

        [HttpGet]
        public ActionResult DetailedView(long IID = 0)
        {
            ViewBag.IID = IID;
            return View("DetailedView");
        }
    }
}
using Eduegate.ERP.Admin.Controllers;
using Eduegate.Infrastructure.Enums;
using Eduegate.Services.Client.Factory;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Web.Mvc;

namespace Eduegate.ERP.Admin.Areas.Frameworks.Controllers
{
    public class SearchController : BaseSearchController
    {
        [HttpGet]
        //[OutputCache(CacheProfile = "CacheEduegate")]
        public ActionResult List(SearchView view, bool isSortableList = false, string parameters = null)
        {
            // call the service and get the meta data
            var metadata = ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)view);
            ViewBag.HasExportAccess = ClientFactory.UserServiceClient(CallContext).HasClaimAccessByResourceID("DATAEXPORTACCESS", CallContext.LoginID.Value);
            ViewBag.HasSortableFeature = ClientFactory.UserServiceClient(CallContext).HasClaimAccessByResourceID("SORTABLEGRIDFEATURE", CallContext.LoginID.Value);
            Helpers.PortalWebHelper.GetDefaultSettingsToViewBag(CallContext, this.ViewBag);

            return View(new SearchListViewModel
            {
                ControllerName = "Frameworks/Search",
                ViewName = view,
                ViewTitle =  string.IsNullOrEmpty(metadata.ViewTitle) ? metadata.ViewName : metadata.ViewTitle,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = metadata.MultilineEnabled,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                FilterColumns = metadata.QuickFilterColumns,
                HasFilters = metadata.HasFilters,
                UserValues = metadata.UserValues,
                EnabledSortableGrid = isSortableList,
                ViewFullPath = metadata.ViewFullPath,
                ViewActions = metadata.ViewActions,

                HasChild = metadata.HasChild,
                ChildView = metadata.ChildView,
                ChildFilterField = metadata.ChildFilterField,
                ActualControllerName = metadata.ControllerName,
                IsRowClickForMultiSelect = metadata.IsRowClickForMultiSelect,
                IsMasterDetail = metadata.IsMasterDetail,
                IsEditableLink = metadata.IsEditable,
                IsGenericCRUDSave = metadata.IsGenericCRUDSave,
                IsReloadSummarySmartViewAlways = metadata.IsReloadSummarySmartViewAlways,
                JsControllerName = metadata.JsControllerName,
                RuntimeFilter = parameters
            });
        }

        [HttpGet]
        public JsonResult SearchData(SearchView view, int currentPage = 1, string orderBy = "", string runtimeFilter = "", int pageSize = 0)
        {
            return base.SearchData(view, currentPage, orderBy, runtimeFilter.Split(';')[0], pageSize);
        }

        [HttpGet]
        public new JsonResult SearchSummaryData(SearchView view)
        {
            return base.SearchSummaryData((SearchView)((int)view) + 1);
        }

        [HttpGet]
        //[OutputCache(CacheProfile = "CacheEduegate")]
        public ActionResult ChildList(SearchView view, long ID)
        {
            // call the service and get the meta data
            var metadata = ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)view);

            return PartialView("_ChildList", new SearchListViewModel
            {
                ControllerName = "Frameworks/Search",
                ViewName = view,
                ViewFullPath = metadata.ViewFullPath,
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                //IsMultilineEnabled = metadata.MultilineEnabled,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                FilterColumns = metadata.QuickFilterColumns,
                RuntimeFilter = Convert.ToString(metadata.ChildFilterField + "=" + ID.ToString()).Trim(), // child view must have this column
                IsChild = true,
                ParentViewName = SearchView.Product,
                IsEditableLink = false
            });
        }
    }
}
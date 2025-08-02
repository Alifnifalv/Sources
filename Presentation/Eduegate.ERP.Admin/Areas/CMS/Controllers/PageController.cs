using Eduegate.ERP.Admin.Controllers;
using Eduegate.Framework.Web.Library.PageRenderer;
using Eduegate.Services.Client.Factory;
using Eduegate.Web.Library.ViewModels;
using System.Web.Mvc;

namespace Eduegate.ERP.Admin.Areas.CMS
{
    public class PageController : BaseSearchController
    {
        // GET: Page
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult List()
        {
            // call the service and get the meta data
            var metadata = ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Eduegate.Infrastructure.Enums.SearchView.Page);

            return View(new SearchListViewModel
            {
                ViewName = Eduegate.Infrastructure.Enums.SearchView.Page,
                ControllerName = "Page",
                HeaderList = metadata.Columns,
                SummaryHeaderList = metadata.SummaryColumns,
                SortColumns = metadata.SortColumns,
                UserViews = metadata.UserViews,
                IsMultilineEnabled = false,
                //IsMultilineEnabled = metadata.MultilineEnabled,
                IsCategoryColumnEnabled = metadata.RowCategoryEnabled,
                //                InfoBar = @"<li class='status-label-mobile'>
                //                                        <div class='right status-label'>
                //                                            <div class='status-label-color'><label class='status-color-label orange'></label>Draft</div>
                //                                            <div class='status-label-color'><label class='status-color-label  green'></label>Active</div>
                //                                            <div class='status-label-color'><label class='status-color-label red'></label>In Active</div>
                //                                        </div>
                //                                    </li>" ,
                HasFilters = metadata.HasFilters,       
                FilterColumns = metadata.QuickFilterColumns,
                UserValues = metadata.UserValues
            });
        }

        [HttpGet]
        public JsonResult SearchData(int currentPage = 1, string orderBy = "", string runtimeFilter = "")
        {
            runtimeFilter = " CompanyId = " + CallContext.CompanyID.ToString();
            return base.SearchData(Eduegate.Infrastructure.Enums.SearchView.Page, currentPage, orderBy, runtimeFilter);
        }

        [HttpGet]
        public JsonResult SearchSummaryData()
        {
            return base.SearchSummaryData(Eduegate.Infrastructure.Enums.SearchView.PageSummary);
        }

        [HttpGet]
        public ActionResult Get(long ID = 0)
        {
            return Json(PageViewModel.FromDTO(ClientFactory.PageRenderServiceClient(CallContext).GetPageInfo(ID, "")), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetBolierPlateMaps(long pageID, string parameter = "")
        {
            return Json(PageViewModel.FromDTO(ClientFactory.PageRenderServiceClient(CallContext).GetPageInfo(pageID, parameter)), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(PageViewModel vm)
        {
            var updatedVM = PageViewModel.FromDTO(ClientFactory.PageRenderServiceClient(CallContext).SavePage(PageViewModel.ToDTO(vm)));
            return Json(updatedVM, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult DetailedView(long IID = 0)
        {
            ViewBag.IID = IID;
            return View("DetailedView");
        }

        [HttpGet]
        public JsonResult GetBoilerPlateParameters(long boilerPlateID)
        {
            return Json(new { BoilerPlateGridViewModel = BoilerPlateGridViewModel.FromDTO(
                ClientFactory.BoilerPlateServiceClient(CallContext).GetBoilerPlate(boilerPlateID.ToString())) }, JsonRequestBehavior.AllowGet);
        }
    }
}
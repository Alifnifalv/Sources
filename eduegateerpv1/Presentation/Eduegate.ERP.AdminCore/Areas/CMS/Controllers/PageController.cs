using Eduegate.ERP.Admin.Controllers;
using Eduegate.Framework;
using Eduegate.Services.Client.Factory;
using Eduegate.Web.Library.PageRenderer;
using Eduegate.Web.Library.ViewModels;
using Microsoft.AspNetCore.Mvc;



namespace Eduegate.ERP.Admin.Areas.CMS
{
    [Area("CMS")]
    public class PageController : BaseSearchController
    {
        // GET: Page
        public ActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> List()
        {
            // call the service and get the meta data
            var metadata = await ClientFactory.SearchServiceClient(CallContext).SearchList((Services.Contracts.Enums.SearchView)(int)Eduegate.Infrastructure.Enums.SearchView.Page);

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
        public Task<IActionResult> SearchData(int currentPage = 1, string orderBy = "", string runtimeFilter = "")
        {
            runtimeFilter = " CompanyId = " + CallContext.CompanyID.ToString();
            return base.SearchData(Eduegate.Infrastructure.Enums.SearchView.Page, currentPage, orderBy, runtimeFilter);
        }

        [HttpGet]
        public Task<IActionResult> SearchSummaryData()
        {
            return base.SearchSummaryData(Eduegate.Infrastructure.Enums.SearchView.PageSummary);
        }

        [HttpGet]
        public ActionResult Get(long ID = 0)
        {
            return Json(PageViewModel.FromDTO(ClientFactory.PageRenderServiceClient(CallContext).GetPageInfo(ID, "")));
        }

        [HttpGet]
        public ActionResult GetBolierPlateMaps(long pageID, string parameter = "")
        {
            return Json(PageViewModel.FromDTO(ClientFactory.PageRenderServiceClient(CallContext).GetPageInfo(pageID, parameter)));
        }

        [HttpPost]
        public JsonResult Save([FromBody] PageViewModel vm)
        {
            var updatedVM = PageViewModel.FromDTO(ClientFactory.PageRenderServiceClient(CallContext).SavePage(PageViewModel.ToDTO(vm)));
            return Json(updatedVM);
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
            return Json(new
            {
                BoilerPlateGridViewModel = BoilerPlateGridViewModel.FromDTO(
                ClientFactory.BoilerPlateServiceClient(CallContext).GetBoilerPlate(boilerPlateID.ToString()))
            });
        }

        //public class pageController : Controller
        //{
        //    private readonly string _clientId;
        //    private readonly string _clientSecret;
        //    private readonly string _authority;
        //    private readonly string _resource;
        //    private readonly string _redirectUri;

        //    public pageController()
        //    {
        //        _clientId = "27722e62-988d-4f64-a8cc-62b49eef9286";
        //        _clientSecret = "08e5f5f3-49cd-475d-a96c-2d892b031fd4";
        //        _authority = "https://login.microsoftonline.com/74371ba3-a603-4a16-ae30-9741f9381eaa";
        //        _resource = "https://analysis.windows.net/powerbi/api";
        //        _redirectUri = "https://localhost:5001/redirect";
        //    }

        //    [HttpGet]
        //    public async Task<IActionResult> GetAccessToken()
        //    {
        //        var authenticationContext = new AuthenticationContext(_authority);
        //        var credential = new ClientCredential(_clientId, _clientSecret);
        //        var authenticationResult = await authenticationContext.AcquireTokenAsync(_resource, credential);
        //        var accessToken = authenticationResult.AccessToken;

        //        return Ok(accessToken);
        //    }

        //}

        [HttpGet]
        public JsonResult GetPowerBIDataUsingPageID(long? pageID)
        {
            var result = ClientFactory.PageRenderServiceClient(CallContext).GetPowerBIDataUsingPageID(pageID);
            return Json(result);
        }

    }
}
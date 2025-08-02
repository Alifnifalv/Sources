using AppOwnsData.Models;
using AppOwnsData.Services;
using Eduegate.ERP.Admin.Controllers;
using Eduegate.Framework;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.CRM;
using Eduegate.Web.Library.PageRenderer;
using Eduegate.Web.Library.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Rest;
using System.Text.Json;



namespace Eduegate.ERP.Admin.Areas.CMS
{
    [Area("CMS")]
    public class PageController : BaseSearchController
    {
        private readonly PbiEmbedService pbiEmbedService;
        private readonly IOptions<AzureAd> azureAd;

        public PageController(PbiEmbedService pbiEmbedService, IOptions<AzureAd> azureAd)
        {
            this.pbiEmbedService = pbiEmbedService;
            this.azureAd = azureAd;
        }


        //private readonly PowerBIService _powerBIService;

        //public PageController(PowerBIService powerBIService)
        //{
        //    _powerBIService = powerBIService;
        //}
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




        //[HttpGet]
        //public async Task<IActionResult> GetEmbedInfo(long? pageID)
        //{
        //    var result = ClientFactory.PageRenderServiceClient(CallContext).GetPowerBIDataUsingPageID(pageID);

        //    try
        //    {
        //        var embedToken = await _powerBIService.GetEmbedToken();

        //        var embedInfo = new
        //        {
        //            EmbedToken = embedToken.Token,
        //            EmbedUrl = $"https://app.powerbi.com/reportEmbed?reportId={result.ReportID}&workspaceId={result.WorkspaceID}",
        //            ReportId = result.ReportID
        //        };

        //        return Json(embedInfo);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}
   
        [HttpGet]
        public async Task<string> GetEmbedInfoAsync(long? pageID)
        {
            try
            {
                var result = ClientFactory.PageRenderServiceClient(CallContext).GetPowerBIDataUsingPageID(pageID);



                EmbedParams embedParams = await pbiEmbedService.GetEmbedParams(new Guid(result.WorkspaceID), new Guid(result.ReportID));
                return JsonSerializer.Serialize<EmbedParams>(embedParams);
            }
            catch (HttpOperationException exc)
            {
                HttpContext.Response.StatusCode = (int)exc.Response.StatusCode;
                var message = string.Format("Status: {0} ({1})\r\nResponse: {2}\r\nRequestId: {3}", exc.Response.StatusCode, (int)exc.Response.StatusCode, exc.Response.Content, exc.Response.Headers["RequestId"].FirstOrDefault());
                return message;
            }
            catch (Exception ex)
            {
                HttpContext.Response.StatusCode = 500;
                return ex.Message + "\n\n" + ex.StackTrace;
            }
        }


    }
}
using Microsoft.AspNetCore.Mvc;
using Eduegate.Framework.PageRendererEngine.ViewModels;
using Eduegate.Services.Client.Factory;
using Eduegate.Application.Mvc;

namespace Eduegate.ERP.Admin.Areas.Frameworks.Controllers
{
    [Area("Frameworks")]
    public class DashboardController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult EntityDashboard(long pageID, long referenceID)
        {
            ViewBag.CallContext = CallContext;
            
            return View(new PageRedererViewModel(PageEngineViewModel.FromDTO(
                    ClientFactory.PageRenderServiceClient(CallContext).GetPageInfo(pageID, string.Empty), "referenceID=" + referenceID.ToString(), true)));
        }
    }
}
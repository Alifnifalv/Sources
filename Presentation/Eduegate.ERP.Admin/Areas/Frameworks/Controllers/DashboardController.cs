using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eduegate.Framework.PageRendererEngine.ViewModels;
using Eduegate.Frameworks.Mvc.Controllers;
using Eduegate.Services.Client.Factory;

namespace Eduegate.ERP.Admin.Areas.Frameworks.Controllers
{
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
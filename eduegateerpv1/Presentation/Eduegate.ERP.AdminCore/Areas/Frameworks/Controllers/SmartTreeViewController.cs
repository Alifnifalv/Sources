using Eduegate.Application.Mvc;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Web.Library.ViewModels.Frameworks;
using Microsoft.AspNetCore.Mvc;

namespace Eduegate.ERP.Admin.Areas.Frameworks.Controllers
{
    [Area("Frameworks")]
    public class SmartTreeViewController : BaseController
    {
        // GET: Frameworks/SmartTreeView
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowView(SmartViewType type)
        {
            return View(new SmartTreeViewViewModel() { SmartViewType = type });
        }

        public JsonResult GetSmartTreeView(SmartViewType type, long? parentID = null, string searchText = "")
        {
            var vm = SmartTreeViewViewModel.ToVM(
                ClientFactory.SmartViewServiceClient(CallContext)
                             .GetSmartTreeView(type, parentID, searchText));
            return Json(vm);
        }

        public ActionResult ProductSmartView(string categoryID = null)
        {
            var productTree = ClientFactory.SmartViewServiceClient(CallContext).GetProductTree(categoryID);
            return PartialView(productTree);
        }
    }
}
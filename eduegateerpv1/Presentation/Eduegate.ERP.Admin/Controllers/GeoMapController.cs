using System.Web.Mvc;

namespace Eduegate.ERP.Admin.Controllers
{
    public class GeoMapController : Controller
    {
        // GET: GeoMap
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreatePolyline()
        {
            return View();
        }
    }
}
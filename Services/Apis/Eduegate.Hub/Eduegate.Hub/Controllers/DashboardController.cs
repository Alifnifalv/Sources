using Microsoft.AspNetCore.Mvc;

namespace Eduegate.Hub.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

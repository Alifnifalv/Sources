using Microsoft.AspNetCore.Mvc;
using Eduegate.Application.Mvc;

namespace Eduegate.ERP.Admin.Areas.Schools.Controllers
{
    [Area("Schools")]
    public class CertificateTemplateController : BaseController
    {
        // GET: Schools/CertificateTemplate
        public ActionResult Index()
        {
            return View();
        }
    }
}
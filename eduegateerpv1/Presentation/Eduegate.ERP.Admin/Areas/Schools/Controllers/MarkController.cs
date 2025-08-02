using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eduegate.ERP.Admin.Controllers;
using Eduegate.Services.Client.Factory;
using Eduegate.Web.Library.School.Attendences;
using Eduegate.Web.Library.ViewModels;


namespace Eduegate.ERP.Admin.Areas.Schools.Controllers
{
    public class MarkController : Controller
    {
        // GET: Schools/Mark
        public ActionResult Index()
        {
            return View();
        }
    }
}
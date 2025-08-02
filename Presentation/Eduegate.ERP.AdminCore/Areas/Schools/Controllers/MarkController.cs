using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Eduegate.ERP.Admin.Controllers;
using Eduegate.Services.Client.Factory;
using Eduegate.Web.Library.School.Attendences;
using Eduegate.Web.Library.ViewModels;


namespace Eduegate.ERP.Admin.Areas.Schools.Controllers
{
    [Area("Schools")]
    public class MarkController : Controller
    {
        // GET: Schools/Mark
        public ActionResult Index()
        {
            return View();
        }
    }
}
using Eduegate.Web.Library.Chat.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace Eduegate.ERP.Admin.Controllers
{
    public class ChatPortalController : Controller
    {
        // GET: ChartPortal
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AdminPortal(string parameters)
        {
            return View(new UserViewModel());
        }
    }
}
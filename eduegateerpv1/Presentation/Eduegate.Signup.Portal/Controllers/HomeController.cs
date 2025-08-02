using Eduegate.Frameworks.Mvc.Controllers;
using Eduegate.Signup.Portal.ViewModel;
using Eduegate.Services.Client.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eduegate.Signup.Portal.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            if (getcookieloginid() == true)
            {
            }
            else
            {
                //return RedirectToAction("login", "account");
            }
            return View();
        }

        public ActionResult Test()
        {
            if (getcookieloginid() == true)
            {
            }
            else
            {
                //return RedirectToAction("login", "account");
            }
            return View();
        }

        public ActionResult Index2()
        {
            if (getcookieloginid() == true)
            {

            }
            else
            {
                //return RedirectToAction("login", "account");
            }
            return View();
        }

       
        private bool getcookieloginid()
        {
            String SessionID = "";
            try
            {
                SessionID = Session["ExamCandidate"] != null ? Session["ExamCandidate"].ToString() : null;

                if (SessionID != null || SessionID != "")
                {
                    if (int.Parse(SessionID) == 0)
                    {
                        SessionID = null;
                    }
                }
            }
            catch { }
            String getcookieID = SessionID;
            if (SessionID == null || SessionID == "")
            {
                try
                {
                    HttpCookie SessionLoginID = (HttpCookie)Request.Cookies["ExamCandidateCookie"];

                    if (SessionLoginID != null)
                    {
                        for (int i = 0; i < SessionLoginID.Values.Count; i++)
                        {
                            getcookieID = SessionLoginID.Values[0].ToString();
                            break;
                        }
                    }
                }
                catch { }
                Session["ExamCandidate"] = getcookieID;
            }
            if (getcookieID != null && getcookieID != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
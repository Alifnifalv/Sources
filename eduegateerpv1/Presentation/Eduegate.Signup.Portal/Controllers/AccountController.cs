using System;
using System.Web;
using System.Web.Mvc;

namespace Eduegate.Signup.Portal.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            if (getcookieloginid() == true)
            {
                return RedirectToAction("Contact", "Home");
            }
            else
            {

            }
            return View();
        }

        public ActionResult ResetPassword()
        {
            return View();
        }

        private bool getcookieloginid()
        {
            String SessionID = "";
            try
            {
                SessionID = Session["ExamCandidate"] != null ? Session["ExamCandidate"].ToString() : null;
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
            if(getcookieID!=null && getcookieID != "")
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
using System;
using System.Web;
using System.Web.Mvc;
using Eduegate.Services.Contracts.OnlineExam.Exam;

namespace Eduegate.OnlineExam.Portal.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            if (getcookieloginid() == true)
            {
                return RedirectToAction("index", "Home");
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

        //public ActionResult SignOut()
        //{
        //    Session["ExamCandidate"] = null;
        //    Session.Contents.Clear();
        //    Session.Clear();
        //    Session.RemoveAll();
        //    Session.Abandon();

        //    /* FormsAuthentication.SignOut();
        //     this.Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
        //     this.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //     this.Response.Cache.SetNoStore();          
        //     */
        //    Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
        //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //    Response.Cache.SetNoStore();

        //    try
        //    {
        //        HttpCookie SessionID = (HttpCookie)Request.Cookies["ExamCandidateCookie"];
        //        SessionID.Values.Clear();
        //        SessionID.Value = null;
        //        SessionID.Expires = DateTime.Now.AddYears(-1);
        //        Response.Cookies.Add(SessionID);

        //        if (Request.Cookies["ExamCandidateCookie"] != null)
        //        {
        //            Response.Cookies["ExamCandidateCookie"].Expires = DateTime.Now.AddYears(-1);
        //        }

        //    }
        //    catch { }

        //    return View();//return RedirectToAction("Login", "Account");
        //}

        [AllowAnonymous]
        public ActionResult Signout()
        {
            Session["ExamCandidate"] = null;
            Session.Contents.Clear();
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("login", "account");
        }

        // GET: Login
        [HttpPost]
        public ActionResult Login(FormCollection Collection)
        {
            try
            {
                string UserName = Collection["UserName"]; UserName = UserName.Trim(' ');
                string Password = Collection["Password"]; Password = Password.Trim(' ');
                string remindme = Collection["remindme"];
                
                var candidate = Services.Client.Factory.ClientFactory.SchoolServiceClient(null).GetCandidateDetails(UserName, Password);

                var redirectTo = LoginCommon(candidate, remindme);// this will return where to redirect (action,controller)

                string msg = "";
                ViewBag.error = null;
                if (candidate.CandidateIID == 0 || candidate.CandidateIID == null)
                {
                    msg = "invalid";
                    ViewBag.error = "invalid user name or password...!";
                }
                else
                {
                    return RedirectToAction("index", "Home", new { msg });
                }

            }
            catch (Exception ex) { ViewBag.error = "Error try again later"; }

            return View();
        }

        public Tuple<string, string> LoginCommon(CandidateDTO Login, string remindme)
        {
            if (Login.CandidateIID == 0 || Login.CandidateIID !=null)
            {
                Session["ExamCandidate"] = Login.CandidateIID;
                if (remindme == "on")
                {
                    HttpCookie SessionLoginID = new HttpCookie("ExamCandidateCookie");
                    SessionLoginID.Values.Add(Login.CandidateIID.ToString(), Login.CandidateIID.ToString());
                    //Set Cookie to expire in 1 month
                    SessionLoginID.Expires = DateTime.Now.AddMonths(1);
                    Response.Cookies.Add(SessionLoginID);

                }
                else
                {

                }
            }
            else
            {
                ViewBag.error = "Invalid User...!";
            }

            return Tuple.Create("Login", "Account");// RedirectToAction("Login", "settings");
        }


        private string GenerateRandomOTPCode(int ID, String User)
        {

            String RandomLetter = "";
            try
            {
                Random rnd = new Random();
                int First = rnd.Next(100000, 999999);
                int Second = rnd.Next(ID);
                int Third = First + Second;
                int num = rnd.Next(0, 26); // Zero to 25
                                           //  char let = (char)('A' + num);
                RandomLetter = Third.ToString();
            }
            catch
            {
                RandomLetter = "538794";
            }

            return RandomLetter;
        }
        private DateTime DateTimeNow()
        {

            DateTime todaysDate = DateTime.Now;
            var gmTime = todaysDate.ToUniversalTime();
            var indianTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            var gmTimeConverted = TimeZoneInfo.ConvertTime(gmTime, indianTimeZone);
            DateTime DateTimevar = Convert.ToDateTime(gmTimeConverted);
            String DateTimeString = DateTimevar.ToString("dd-MMM-yyy hh:mm:ss tt");
            DateTime ResultDateTime = Convert.ToDateTime(DateTimeString);

            return ResultDateTime;
        }
        private DateTime OTPDateTimeNow()
        {

            DateTime todaysDate = DateTime.Now.AddMinutes(5);
            var gmTime = todaysDate.ToUniversalTime();
            var indianTimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            var gmTimeConverted = TimeZoneInfo.ConvertTime(gmTime, indianTimeZone);
            DateTime DateTimevar = Convert.ToDateTime(gmTimeConverted);
            String DateTimeString = DateTimevar.ToString("dd-MMM-yyy hh:mm:ss tt");
            DateTime ResultDateTime = Convert.ToDateTime(DateTimeString);

            return ResultDateTime;
        }

    }
}
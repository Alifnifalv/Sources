using System;
using System.Linq;
using System.Text;
using System.Web;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.OnlineExam.Exam;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eduegate.OnlineExam.Portal.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public IActionResult Login()
        {
            if (GetCookieLoginId() == true)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        public ActionResult ResetPassword()
        {
            return View();
        }

        private bool GetCookieLoginId()
        {
            string SessionID = "";

            if (HttpContext.Session.TryGetValue("ExamCandidate", out byte[] candidateValue))
            {
                SessionID = Encoding.UTF8.GetString(candidateValue);
            }


            if (string.IsNullOrEmpty(SessionID))
            {
                string cookieLoginID = Request.Cookies["ExamCandidateCookie"];

                if (!string.IsNullOrEmpty(cookieLoginID))
                {
                    HttpContext.Session.SetString("ExamCandidate", cookieLoginID);
                    return true;
                }
            }
            else
            {
                HttpContext.Session.Remove("ExamCandidate");
            }

            return false;
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
        public IActionResult Signout()
        {
            HttpContext.Session.Remove("ExamCandidate");
            HttpContext.Session.Clear();
            HttpContext.SignOutAsync();

            return RedirectToAction("login", "account");
        }

        // GET: Login
        [HttpPost]
        public IActionResult Login(IFormCollection Collection)
        {
            try
            {
                string UserName = Collection["UserName"];
                UserName = UserName.Trim();
                string Password = Collection["Password"];
                Password = Password.Trim();
                string remindme = Collection["remindme"];

                var candidate = ClientFactory.SchoolServiceClient(null).GetCandidateDetails(UserName, Password);

                var redirectTo = LoginCommon(candidate, remindme); // this will return where to redirect (action, controller)

                string msg = "";
                ViewData["error"] = null;
                if (candidate.CandidateIID == 0)
                {
                    msg = "invalid";
                    ViewData["error"] = "Invalid user name or password...!";
                }
                else
                {
                    ViewBag.CandidateDetails = candidate;
                    return RedirectToAction("Home", "Home", new { msg });
                }

            }
            catch (Exception ex)
            {
                ViewData["error"] = "Error, please try again later";
            }

            return View();
        }


        public Tuple<string, string> LoginCommon(CandidateDTO Login, string remindme)
        {
            if (Login.CandidateIID != 0)
            {
                HttpContext.Session.SetString("ExamCandidate", Login.CandidateIID.ToString());

                if (remindme == "on")
                {
                    CookieOptions option = new CookieOptions
                    {
                        Expires = DateTime.Now.AddMonths(1),
                        IsEssential = true // To make the cookie essential
                    };

                    Response.Cookies.Append("ExamCandidateCookie", Login.CandidateIID.ToString(), option);
                }
            }
            else
            {
                ViewBag.error = "Invalid User...!";
            }

            return Tuple.Create("Login", "Account");
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
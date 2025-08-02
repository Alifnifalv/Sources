using Microsoft.AspNetCore.Mvc;
using Eduegate.Application.Mvc;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Client.Factory;
using Eduegate.Framework.Enums;
using Eduegate.Domain;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Security.Claims;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Services.Contracts.Settings;
using Eduegate.Web.Library.SignUp.Common;
using System.Threading.Tasks;
using System;
using Eduegate.Framework;

namespace Eduegate.Signup.Portal.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            var userDTO = new UserDTO();
            var CompanyID = "1";

            if (CallContext.LoginID == null)
            {
                return Redirect("~/Account/Login");
            }
            else
            {
                if (CallContext.UserRole.IsNullOrEmpty())
                {
                    userDTO = ClientFactory.AccountServiceClient(CallContext).GetUserDetails(CallContext.EmailID);

                    if (userDTO.IsNotNull())
                        ResetCookies(userDTO, int.Parse(CompanyID));
                }

                return View();
            }            
        }

        public JsonResult GetUserDetails()
        {
            var userDetail = ClientFactory.AccountServiceClient(CallContext).GetUserDetails(CallContext.EmailID);

            if (userDetail == null)
            {
                return Json(new { IsError = true, Response = "There are some issue with you login credential, please check with the administrator." });
            }
            else
            {
                if (!string.IsNullOrEmpty(userDetail.ProfileFile))
                {
                    userDetail.ProfileFile = string.Format("{0}/{1}/{2}", new Domain.Setting.SettingBL().GetSettingValue<string>("ImageHostUrl"), EduegateImageTypes.UserProfile, userDetail.ProfileFile);
                }

                return Json(new { IsError = false, Response = userDetail });
            }
        }

        public ActionResult Conference()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetParentStudents()
        {
            var studentsDetail = ClientFactory.SchoolServiceClient(CallContext).GetStudentsSiblings(CallContext.LoginID.HasValue ? CallContext.LoginID.Value : 0);

            if (studentsDetail == null)
            {
                return Json(new { IsError = true, Response = "There are some issue !" });
            }
            else
            {
                return Json(new { IsError = false, Response = studentsDetail });
            }
        }

        #region Portal redirect from parent portal
        public ActionResult Meeting(long? loginID)
        {
            var userDTO = new UserDTO();

            loginID = loginID.HasValue ? loginID.Value : 0;

            var loginData = new AccountBL(null).GetLoginDetailByLoginID(loginID.Value);

            if (loginData != null)
            {
                userDTO = ClientFactory.AccountServiceClient(CallContext).GetUserDetails(loginData.LoginEmailID);
                userDTO.UserSettings = new AccountBL(CallContext).GetUserSettings(userDTO.UserID);
                userDTO.CompanyID = 1;

                if (userDTO.IsNotNull())
                    ResetCookies(userDTO, userDTO.CompanyID.Value);
                else
                    return Json(new { IsSuccess = false });

                var result = FillContextAndClaim(loginData);

                return Redirect("Index");
            }
            else
            {
                return Redirect("~/Account/Login");
            }
        }

        public async Task<IActionResult> FillContextAndClaim(Eduegate.Domain.Entity.Models.Login login)
        {
            var claims = new List<Claim>
                            {
                                new System.Security.Claims.Claim(ClaimTypes.Name, login.LoginEmailID),
                                new System.Security.Claims.Claim("FullName", login.LoginEmailID),
                                new System.Security.Claims.Claim(ClaimTypes.UserData,
                                JsonConvert.SerializeObject(new CallContext() {
                                    EmailID = login.LoginEmailID,
                                    LoginID = login.LoginIID,
                                    UserId  = login.LoginUserID,
                                })),
                            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return null;
        }
        #endregion Portal redirect

        public ActionResult UserProfile()
        {
            return View();
        }
    }
}
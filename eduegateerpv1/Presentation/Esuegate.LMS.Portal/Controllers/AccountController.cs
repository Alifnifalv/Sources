using Eduegate.Services.Contracts;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Eduegate.Application.Mvc;
using Eduegate.Domain.Entity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Eduegate.Domain;
using Newtonsoft.Json;
using System.Security.Claims;
using Eduegate.Framework;
using System.Text.RegularExpressions;
using Eduegate.Web.Library.SignUp.Common;

namespace Eduegate.Signup.Portal.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        public dbEduegateERPContext db = new dbEduegateERPContext();
        private static string ServiceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        private static string AccountService = string.Concat(ServiceHost, Eduegate.Framework.Helper.Constants.ACCOUNT_SERVICE);

        //TODO: Check if this is needed
        //private ApplicationSignInManager _signInManager;
        //private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            var VM = new LoginViewModel();
            var userDTO = new UserDTO();
            VM.CompanyID = "1";
            try
            {
                if (CallContext.LoginID != null)
                {
                    userDTO = ClientFactory.AccountServiceClient(CallContext).GetUserDetails(CallContext.EmailID);
                    if (CallContext.UserRole.IsNullOrEmpty())
                    {
                        return RedirectPermanent("~/Account/Login");
                    }
                    else
                    {
                        return RedirectPermanent("~/Home/MyWards");
                    }
                }
            }
            catch { }
            return View("Login", VM);
        }

        [AllowAnonymous]
        [HttpGet]
        public JsonResult LoginCheck(string emailID, string password, string appID)
        {
            bool isvalid = ClientFactory.AccountServiceClient(CallContext).Login(emailID, password, appID);
            // pass the user settings if it is valid
            List<SettingDTO> userSettings = null;

            if (isvalid)
            {
                userSettings = ClientFactory.SettingServiceClient(CallContext).GetAllUserSettingDetail();
            }

            return Json(new { IsSuccess = isvalid, UserSettings = userSettings });
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var userDTO = new UserDTO();
            byte appID = (byte)ApplicationType.ParentPortal;// get the Application Id
            bool isSuccess = false;
            model.CompanyID = "1";
            bool isvalid = false;

            if (model.IsNotNull())
            {
                if (ModelState.IsValid)
                {
                    if (!IsEmail(model.Email))
                    {
                        var uerInfo = ClientFactory.AccountServiceClient(CallContext).GetUserData(new UserDTO() { LoginUserID = model.Email });
                        userDTO.LoginEmailID = uerInfo?.LoginEmailID;
                        model.Email = userDTO.LoginEmailID;
                    }

                    if (!string.IsNullOrEmpty(model.Email))
                        isvalid = ClientFactory.AccountServiceClient(CallContext).Login(model.Email, model.Password, appID.ToString());

                    if (isvalid)
                    {
                        userDTO = ClientFactory.AccountServiceClient(CallContext).GetUserDetails(model.Email);
                        userDTO.UserSettings = new AccountBL(CallContext).GetUserSettings(userDTO.UserID);

                        if (userDTO.IsNotNull())
                            ResetCookies(userDTO, int.Parse(model.CompanyID));
                        else
                            return Json(new { IsSuccess = false });

                        ClientFactory.UserServiceClient(CallContext).ResetForceLogout(long.Parse(userDTO.LoginID));

                        isSuccess = true;

                        var claims = new List<System.Security.Claims.Claim>
                            {
                                new System.Security.Claims.Claim(ClaimTypes.Name, model.Email),
                                new System.Security.Claims.Claim("FullName", model.Email),
                                new System.Security.Claims.Claim(ClaimTypes.UserData,
                                JsonConvert.SerializeObject(new CallContext() {
                                    EmailID = userDTO.LoginEmailID,
                                    //MobileNumber = userDTO.MobileNumber,
                                    //UserClaims = JsonConvert.SerializeObject(userDTO.UserClaims),
                                    LoginID = long.Parse(userDTO.LoginID),
                                    UserId  = userDTO.UserID.ToString(),
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
                    }
                }
            }

            List<SettingDTO> userSettings = null;

            if (isSuccess)
            {
                CallContext.LoginID = int.Parse(userDTO.LoginID);
                try
                {
                    userSettings = ClientFactory.SettingServiceClient(CallContext).GetAllUserSettingDetail();
                }
                catch (Exception ex)
                {
                    Eduegate.Logger.LogHelper<AccountController>.Fatal(ex.Message, ex);
                    throw;
                }
            }

            return Ok(new { IsSuccess = isSuccess, UserSettings = userSettings });
        }

        private bool IsEmail(string input)
        {
            // Define a regular expression pattern for email validation
            string emailPattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";

            // Use Regex.IsMatch to check if the input matches the email pattern
            return Regex.IsMatch(input, emailPattern);
        }

        [AllowAnonymous]
        public async Task<bool> Signout()
        {
            if (CallContext.LoginID.HasValue)
            {
                Framework.CacheManager.MemCacheManager<bool>.ClearAll();

                Framework.CacheManager.MemCacheManager<object>.ClearAll();
            }

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            RemoveCallContext();
            return true;
        }

        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public ActionResult ExternalLogin(string provider, string returnUrl)
        //{
        //    // Request a redirect to the external login provider
        //    return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Redirect to a different page or perform other actions after signing out
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

    }
}
using Eduegate.ERP.Admin.Models;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.Settings;
using Eduegate.Services.Contracts.Notifications;
using Eduegate.Domain.Entity.Models;
using System.Net.Mail;
using System.Net;
using Eduegate.Domain.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Eduegate.Application.Mvc;
using Eduegate.Domain.Entity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Domain;
using Newtonsoft.Json;
using System.Security.Claims;
using Eduegate.Framework;
using System.Text.RegularExpressions;
using Eduegate.Services.Contracts.Enums;

namespace Eduegate.ERP.School.Portal.Controllers
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

        //public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        //{
        //    UserManager = userManager;
        //    SignInManager = signInManager;
        //}

        //TODO: Check if this is needed
        //public ApplicationSignInManager SignInManager
        //{
        //    get
        //    {
        //        return null;// _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
        //    }
        //    private set
        //    {
        //        _signInManager = value;
        //    }
        //}

        //TODO: Check if this is needed
        //public ApplicationUserManager UserManager
        //{
        //    get
        //    {
        //        return null; // _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //    }
        //    private set
        //    {
        //        _userManager = value;
        //    }
        //}

        [AllowAnonymous]
        public ActionResult onlineadmissionenquiry()
        {
            return View();
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
                        return Redirect("~/Home/AnonymousDashbaord");
                    }
                    else
                    {
                        return Redirect("~/Home/MyWards");
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

        //[AllowAnonymous]
        //[HttpPost]
        //public JsonResult Login(LoginViewModel model)
        //{
        //    var userDTO = new UserDTO();
        //    byte appID = (byte)ApplicationType.ParentPortal;// get the Application Id
        //    bool isSuccess = false;
        //    model.CompanyID = "1";
        //    bool isvalid = false;
        //    if (model.IsNotNull())
        //    {
        //        if (ModelState.IsValid)
        //        {

        //            if (model.LoginType != "Email")
        //            {

        //                userDTO.LoginEmailID = ClientFactory.AccountServiceClient(CallContext).GetUserData(new UserDTO() { LoginUserID = model.LoginUserID }).LoginEmailID;
        //                model.Email = userDTO.LoginEmailID;
        //            }

        //            if (!string.IsNullOrEmpty(model.Email))
        //                isvalid = ClientFactory.AccountServiceClient(CallContext).Login(model.Email, model.Password, appID.ToString());

        //            if (isvalid)
        //            {
        //                userDTO = ClientFactory.AccountServiceClient(CallContext).GetUserDetails(model.Email);

        //                if (userDTO.IsNotNull())
        //                    ResetCookies(userDTO, int.Parse(model.CompanyID));
        //                else
        //                    return Json(new { IsSuccess = false });

        //                isSuccess = true;
        //            }
        //        }
        //    }

        //    List<SettingDTO> userSettings = null;

        //    if (isSuccess)
        //    {
        //        CallContext.LoginID = int.Parse(userDTO.LoginID);

        //        userSettings = ClientFactory.SettingServiceClient(CallContext).GetAllUserSettingDetail();
        //    }

        //    return Json(new { IsSuccess = isSuccess, UserSettings = userSettings });
        //}

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

        //TODO: Check later
        // GET: /Account/VerifyCode
        //[AllowAnonymous]
        //public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        //{
        //    // Require that the user has already logged in via username/password or external login
        //    if (!await SignInManager.HasBeenVerifiedAsync())
        //    {
        //        return View("Error");
        //    }
        //    return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        //}

        //TODO: Check later
        // POST: /Account/VerifyCode
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    // The following code protects for brute force attacks against the two factor codes. 
        //    // If a user enters incorrect codes for a specified amount of time then the user account 
        //    // will be locked out for a specified amount of time. 
        //    // You can configure the account lockout settings in IdentityConfig
        //    var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
        //    switch (result)
        //    {
        //        case SignInStatus.Success:
        //            return RedirectToLocal(model.ReturnUrl);
        //        case SignInStatus.LockedOut:
        //            return View("Lockout");
        //        case SignInStatus.Failure:
        //        default:
        //            ModelState.AddModelError("", "Invalid code.");
        //            return View(model);
        //    }
        //}

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            Common result = new Common();
            bool success = false;
            string strMessagType = "";

            try            // edited on 27feb2020 for redirecting to new page
            {


                if (ModelState.IsValid)
                {
                    ClientFactory.AccountServiceClient(CallContext).RegisterUser(new UserDTO()
                    {
                        LoginEmailID = model.Email,
                        Password = model.Password,
                        UserName = model.Email,
                        StatusID = 1
                    });

                    return View("NewRegisterConfirmation");
                }
                else
                {
                    return View(model);
                }

            }

            catch (Exception exception)
            {
                ModelState.AddModelError("Email", "Email ID already exist!");
                return View(model);
            }
        }

        //TODO: Check later
        // GET: /Account/ConfirmEmail
        //[AllowAnonymous]
        //public async Task<ActionResult> ConfirmEmail(string userId, string code)
        //{
        //    if (userId == null || code == null)
        //    {
        //        return View("Error");
        //    }
        //    var result = await UserManager.ConfirmEmailAsync(userId, code);
        //    return View(result.Succeeded ? "ConfirmEmail" : "Error");
        //}

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult EMailOTPTemplate()
        {
            return View(new ForgotPasswordViewModel());
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View(new ForgotPasswordViewModel());
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                //TODO: Check later
                //var user = await UserManager.FindByNameAsync(model.Email);
                //if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                //{
                //    // Don't reveal that the user does not exist or is not confirmed
                //    return View("ForgotPasswordConfirmation");
                //}

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult LoginConfirmation()
        {
            return View();
        }
        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        //// GET: /Account/ResetPassword
        //[AllowAnonymous]
        //public ActionResult ResetPassword(string code)
        //{
        //    return code == null ? View("Error") : View();
        //}

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            Common result = new Common();
            bool success = false;
            string strMessagType = "";

            try
            {
                bool isUserAvailable = ClientFactory.AccountServiceClient(CallContext).IsUserAvailable(model.Email);
                if (isUserAvailable)
                {
                    result = ClientFactory.AccountServiceClient(CallContext).ResetPassword(new UserDTO()
                    {
                        LoginEmailID = model.Email,
                        Password = model.Password,
                        UserName = model.Email,
                        StatusID = 1
                    });
                    //if ((Eduegate.Framework.Helper.Enums.Common)result == Eduegate.Framework.Helper.Enums.Common.Success)
                    //    return View("ResetPasswordConfirmation");
                    if (result.ToString() == "Success")
                    {
                        strMessagType = "Success";
                        success = true;
                        return View("ResetPasswordConfirmation");
                    }
                    else
                    {
                        strMessagType = "Email ID not exist!";
                    }
                }
                else
                {
                    strMessagType = "Email ID not exist!";
                    success = false;
                    var newmodel = new Web.Library.ViewModels.PasswordResetViewModel();
                    ModelState.AddModelError("Email", strMessagType);
                    return View(newmodel);
                    //return Json(new { IsError = !success, Reset = result, Message = "Email ID not exist!", MessageType = strMessagType });

                }
            }
            catch (Exception exception)
            {
                strMessagType = "Error";
                Eduegate.Logger.LogHelper<AccountController>.Fatal(exception.Message.ToString(), exception);
                return Json(new { IsError = !success, Reset = result, UserMessage = exception.Message.ToString(), MessageType = strMessagType });
            }

            return Json(new { IsError = !success, Reset = result, Message = "Reset Password Successfully!", MessageType = strMessagType });



        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //TODO: Check later
        // GET: /Account/SendCode
        //[AllowAnonymous]
        //public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        //{
        //    var userId = await SignInManager.GetVerifiedUserIdAsync();
        //    if (userId == null)
        //    {
        //        return View("Error");
        //    }
        //    var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
        //    var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
        //    return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        //}

        //TODO: Check later
        // POST: /Account/SendCode
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> SendCode(SendCodeViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View();
        //    }

        //    // Generate the token and send it
        //    if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
        //    {
        //        return View("Error");
        //    }
        //    return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        //}

        //TODO: Check later
        // GET: /Account/ExternalLoginCallback
        //[AllowAnonymous]
        //public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        //{
        //    var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
        //    if (loginInfo == null)
        //    {
        //        return RedirectToAction("Login");
        //    }

        //    // Sign in the user with this external login provider if the user already has a login
        //    var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
        //    switch (result)
        //    {
        //        case SignInStatus.Success:
        //            return RedirectToLocal(returnUrl);
        //        case SignInStatus.LockedOut:
        //            return View("Lockout");
        //        case SignInStatus.RequiresVerification:
        //            return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
        //        case SignInStatus.Failure:
        //        default:
        //            // If the user does not have an account, then prompt the user to create an account
        //            ViewBag.ReturnUrl = returnUrl;
        //            ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
        //            return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
        //    }
        //}

        //TODO: Check later
        // POST: /Account/ExternalLoginConfirmation
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        return RedirectToAction("Index", "Manage");
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        // Get the information about the user from the external login provider
        //        var info = await AuthenticationManager.GetExternalLoginInfoAsync();
        //        if (info == null)
        //        {
        //            return View("ExternalLoginFailure");
        //        }
        //        var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
        //        var result = await UserManager.CreateAsync(user);
        //        if (result.Succeeded)
        //        {
        //            result = await UserManager.AddLoginAsync(user.Id, info.Login);
        //            if (result.Succeeded)
        //            {
        //                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
        //                return RedirectToLocal(returnUrl);
        //            }
        //        }
        //        AddErrors(result);
        //    }

        //    ViewBag.ReturnUrl = returnUrl;
        //    return View(model);
        //}

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Redirect to a different page or perform other actions after signing out
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        //TODO: Check later
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        if (_userManager != null)
        //        {
        //            _userManager.Dispose();
        //            _userManager = null;
        //        }

        //        if (_signInManager != null)
        //        {
        //            _signInManager.Dispose();
        //            _signInManager = null;
        //        }
        //    }

        //    base.Dispose(disposing);
        //}

        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {

            var model = new Eduegate.Web.Library.ViewModels.PasswordResetViewModel();
            model.Code = code;
            return View(model);
        }

        //to generate and save OTP
        [AllowAnonymous]
        public JsonResult OTPGenerate(string email)
        {
            string returnvalue = "0";

            var emaildata = new EmailNotificationDTO();

            var fromEmail = new Domain.Setting.SettingBL().GetSettingValue<string>("EmailFrom").ToString();

            var ccEmail = new Domain.Setting.SettingBL().GetSettingValue<string>("EmailID").ToString();
            try
            {
                emaildata.TemplateName = "ForgetPassword";
                emaildata.Subject = "OTP";
                emaildata.ToEmailID = email;// "shahana.softop@gmail.com";
                emaildata.FromEmailID = fromEmail;
                emaildata.ToBCCEmailID = ccEmail;
                emaildata.AdditionalParameters = new List<Eduegate.Services.Contracts.Commons.KeyValueParameterDTO>();
                emaildata.AdditionalParameters.Add(new Services.Contracts.Commons.KeyValueParameterDTO() { ParameterName = "User", ParameterValue = "", ParameterObject = null });
                //emaildata.AdditionalParameters.Add(new Eduegate.Services.Contracts.Commons.KeyValueParameterDTO() { ParameterName = Eduegate.Services.Contracts.Constants.EmailNotificationType.ForgetPassword.Keys.ResetPasswordLink, ParameterValue = "0"});
                emaildata.AdditionalParameters = null;
                //AccountRepository.GetEmailDetails("");
                Login login = db.Logins.Where(s => s.LoginEmailID == email).FirstOrDefault();

                if (login != null)
                {
                    string GetOTP = GenerateOTP();
                    emaildata.Subject = "OTP:-" + GetOTP;
                    login.LastOTP = GetOTP;
                    db.SaveChanges();
                    //EmailProcess.ProcessEmail(emaildata);
                    string subject = "OTP - Don't share your code with others ";
                    string emailBody = @"<strong style='font-size:1.2em;' font-family:'Times New Roman;'><b>Here is your One Time Password</b></strong><br />
                                        <strong style='font-size:1em;' font-family:'Times New Roman;'>To Validate Your Email Address</strong><br />
                                        <strong style='font-size:3rem;'>" + GetOTP + @"</strong><br /><strong style='font-size:0.6em;' font-family:'Times New Roman;'>Don't share this code with others</strong>";
                    
                    string mailMessage = PopulateBodyForOTP(email, "Welcome", "Here is your One Time Password", "Don't share this code with others", "Please note : do not reply to this email as it is a computer generated email", GetOTP);

                    var hostDet = new Domain.Setting.SettingBL(CallContext).GetSettingValue<string>("HOST_NAME");

                    string defaultMail = new Domain.Setting.SettingBL(CallContext).GetSettingValue<string>("DEFAULT_MAIL_ID");

                    var mailParameters = new Dictionary<string, string>();

                    if (hostDet.ToLower() == "live")
                    {
                        new Eduegate.Domain.Notification.EmailNotificationBL(CallContext).SendMail(email, subject, mailMessage, EmailTypes.OTPGeneration, mailParameters);
                    }
                    else
                    {
                        new Eduegate.Domain.Notification.EmailNotificationBL(CallContext).SendMail(defaultMail, subject, mailMessage, EmailTypes.OTPGeneration, mailParameters);
                    }

                    returnvalue = "1";
                }
                else
                {
                    returnvalue = "Invalid Account..!";
                }
            }
            catch (Exception ex) { returnvalue = ex.Message; }

            return Json(returnvalue);
        }

        [AllowAnonymous]
        public JsonResult OTPforVerifyEmail(string email)
        {
            string returnvalue = "0";

            var listFileNames = new List<string>();

            var emaildata = new EmailNotificationDTO();
            try
            {
                Login login = db.Logins.Where(s => s.LoginEmailID == email).FirstOrDefault();

                if (login == null)
                {
                    string GetOTP = GenerateOTP();
                    string subject = "OTP - Don't share your code with others ";
                    string emailBody = @"<strong style='font-size:1.2em;' font-family:'Times New Roman;'><b>Here is your One Time Password</b></strong><br />
                                        <strong style='font-size:1em;' font-family:'Times New Roman;'>To Validate Your Email Address</strong><br />
                                        <strong style='font-size:3rem;'>" + GetOTP + @"</strong><br /><strong style='font-size:0.6em;' font-family:'Times New Roman;'>Don't share this code with others</strong>";
                    
                    string mailMessage = PopulateBodyForOTP(email, "Welcome", "Here is your One Time Password", "Don't share this code with others", "Please note : do not reply to this email as it is a computer generated email", GetOTP);

                    var hostDet = new Eduegate.Domain.Setting.SettingBL(CallContext).GetSettingValue<string>("HOST_NAME");

                    string defaultMail = new Eduegate.Domain.Setting.SettingBL(CallContext).GetSettingValue<string>("DEFAULT_MAIL_ID");

                    var mailParameters = new Dictionary<string, string>();

                    if (hostDet.ToLower() == "live")
                    {
                        new Eduegate.Domain.Notification.EmailNotificationBL(CallContext).SendMail(email, subject, mailMessage, EmailTypes.OTPGeneration, mailParameters);
                    }
                    else
                    {
                        new Eduegate.Domain.Notification.EmailNotificationBL(CallContext).SendMail(defaultMail, subject, mailMessage, EmailTypes.OTPGeneration, mailParameters);
                    }

                    returnvalue = GetOTP;
                }
            }
            catch (Exception ex)
            {
                returnvalue = ex.Message;
            }

            return Json(returnvalue);
        }

        public JsonResult SendMail(string email, string subject, string msg)
        {
            try
            {
                var mailHost = new Domain.Setting.SettingBL(null).GetSettingValue<string>("EMAILHOST_OTP").ToString();
                var mailPortNumber = new Domain.Setting.SettingBL(null).GetSettingValue<string>("SMTPPORT_OTP").ToString();
                var SMTPUserName = new Domain.Setting.SettingBL(null).GetSettingValue<string>("SMTPUSERNAME_OTP").ToString();
                var SMTPPassword = new Domain.Setting.SettingBL(null).GetSettingValue<string>("SMTPPASSWORD_OTP").ToString();
                var fromEmail = new Domain.Setting.SettingBL(null).GetSettingValue<string>("EMAILFROM_OTP").ToString();
                var mailName = new Domain.Setting.SettingBL(null).GetSettingValue<string>("EMAILUSER_OTP").ToString();
                var senderEmailID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("EMAILID_OTP").ToString();

                if (string.IsNullOrEmpty(mailHost))
                {
                    mailHost = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DEFAULT_EMAILHOST").ToString();
                }

                if (string.IsNullOrEmpty(mailPortNumber))
                {
                    mailPortNumber = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DEFAULT_SMTPPORT").ToString();
                }

                if (string.IsNullOrEmpty(SMTPUserName))
                {
                    SMTPUserName = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DEFAULT_SMTPUSERNAME").ToString();
                }

                if (string.IsNullOrEmpty(SMTPPassword))
                {
                    SMTPPassword = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DEFAULT_SMTPPASSWORD").ToString();
                }

                if (string.IsNullOrEmpty(fromEmail))
                {
                    fromEmail = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DEFAULT_EMAILFROM").ToString();
                }

                if (string.IsNullOrEmpty(mailName))
                {
                    mailName = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DEFAULT_EMAILUSER").ToString();
                }

                if (string.IsNullOrEmpty(senderEmailID))
                {
                    senderEmailID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DEFAULT_EMAILID").ToString();
                }

                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                                      | SecurityProtocolType.Tls11
                                                      | SecurityProtocolType.Tls12;
                }

                SmtpClient ss = new SmtpClient();
                ss.Host = mailHost;//"smtpout.secureserver.net";// "smtp.gmail.com";//"smtp.zoho.com";//
                ss.Port = string.IsNullOrEmpty(mailPortNumber) ? 587 : int.Parse(mailPortNumber);// 587;//465;//;
                ss.Timeout = 20000;
                ss.DeliveryMethod = SmtpDeliveryMethod.Network;
                ss.UseDefaultCredentials = false;
                ss.EnableSsl = true;
                ss.Credentials = new NetworkCredential(SMTPUserName, SMTPPassword);//elcguide@gmail.com elcguide!@#$

                MailMessage mailMsg = new MailMessage(SMTPUserName, email, subject, msg);
                mailMsg.From = new MailAddress(fromEmail, mailName);
                mailMsg.IsBodyHtml = true;
                mailMsg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                ss.Send(mailMsg);
            }
            catch (Exception ex)
            {
                var errorMessage = string.Empty;
                if (ex.Message.ToLower().Contains("inner exception"))
                {
                    errorMessage = ex.InnerException.Message;
                }
                else
                {
                    errorMessage = ex.Message;
                }

                Eduegate.Logger.LogHelper<string>.Fatal("OTP_Mail Error" + errorMessage, ex);

                return Json(ex.Message);
            }

            return Json("ok");
        }

        private string PopulateBody(string Name, string htmlMessage)
        {
            string body = string.Empty;

            // Get the file path
            string filePath = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Views/External/emailtemplate_otp.html");

            // Read the content of the file
            body = System.IO.File.ReadAllText(filePath);

            body = body.Replace("{CUSTOMERNAME}", Name);
            body = body.Replace("{HTMLMESSAGE}", htmlMessage);
            body = body.Replace("{YEAR}", DateTime.Now.Year.ToString());

            return body;
        }

        private string PopulateBodyForOTP(string Name, string content_01, string content_02, string content_03, string content_04, string content_otp)
        {
            string body = string.Empty;


            string clientName = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CLIENTNAME");

            string clientMailLogo = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CLIENT_MAILLOGO");

            string clientWebsite = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CLIENT_WEBSITE");

            string poweredBy = new Domain.Setting.SettingBL(null).GetSettingValue<string>("POWEREDBYCOMPANYWEBSITE");

            string clientFB = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CLIENT_FBSITE");

            string fbLogo = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CLIENT_FBLOGO");

            string linkedInSite = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CLIENT_LINKEDINSITE");

            string linkedInLogo = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CLIENT_LINKEDINLOGO");

            string clientInsta = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CLIENT_INSTASITE");

            string instaLogo = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CLIENT_INSTALOGO");

            string clientYoutube = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CLIENT_YOUTUBESITE");

            string youtubeLogo = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CLIENT_YOUTUBELOGO");


            string clientWhatsappLink = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CLIENT_WHATSAPPLINK");

            string whatsappLogo = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CLIENT_WHATSAPPLOGO");

            string clientPlayStoreLink = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CLIENT_PLAYSTORELINK");

            string playstoreLogo = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CLIENT_PLAYSTORELOGO");

            string clientAppStoreLink = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CLIENT_APPSTORELINK");

            string appstoreLogo = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CLIENT_APPSTORELOGO");

            string clientDescription = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CLIENT_DESCRIPTION");

            string mailingAddress = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CLIENT_MAILINGADDRESS");

            string disclosure = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CLIENT_DISCLOSURE");

            string headerImage = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CLIENT_HEADERIMAGE");

            string footerLogo = new Domain.Setting.SettingBL(null).GetSettingValue<string>("CLIENT_FOOTERLOGO");



            // Get the file path
            string filePath = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Views/External/emailtemplate_otp.html");

            // Read the content of the file
            body = System.IO.File.ReadAllText(filePath);

            body = body.Replace("{content_01}", content_01);
            body = body.Replace("{content_02}", content_02);
            body = body.Replace("{content_03}", content_03);
            body = body.Replace("{content_04}", content_04);
            body = body.Replace("{content_otp}", content_otp);
            body = body.Replace("{YEAR}", DateTime.Now.Year.ToString());

            body = body.Replace("{CLIENT_MAILLOGO}", clientMailLogo);
            body = body.Replace("{CLIENT_WEBSITE}", clientWebsite);
            body = body.Replace("{POWEREDBYCOMPANYWEBSITE}", poweredBy);
            body = body.Replace("{CLIENT_NAME}", clientName);
            body = body.Replace("{CLIENT_FBSITE}", clientFB);
            body = body.Replace("{CLIENT_FBLOGO}", fbLogo);
            body = body.Replace("{CLIENT_LINKEDINSITE}", linkedInSite);
            body = body.Replace("{CLIENT_LINKEDINLOGO}", linkedInLogo);
            body = body.Replace("{CLIENT_INSTASITE}", clientInsta);
            body = body.Replace("{CLIENT_INSTALOGO}", instaLogo);
            body = body.Replace("{CLIENT_YOUTUBESITE}", clientYoutube);
            body = body.Replace("{CLIENT_YOUTUBELOGO}", youtubeLogo);
            body = body.Replace("{CLIENT_WHATSAPPLINK}", clientWhatsappLink);
            body = body.Replace("{CLIENT_WHATSAPPLOGO}", whatsappLogo);
            body = body.Replace("{CLIENT_PLAYSTORELINK}", clientPlayStoreLink);
            body = body.Replace("{CLIENT_PLAYSTORELOGO}", playstoreLogo);
            body = body.Replace("{CLIENT_DESCRIPTION}", clientDescription);
            body = body.Replace("{CLIENT_MAILINGADDRESS}", mailingAddress);
            body = body.Replace("{CLIENT_DISCLOSURE}", disclosure);
            body = body.Replace("{CLIENT_HEADERIMAGE}", headerImage);
            body = body.Replace("{CLIENT_FOOTERLOGO}", footerLogo);

            body = body.Replace("{CUSTOMERNAME}", Name);

            return body;
        }

        public class ParameterObject
        {
            public string bannerFullPath { get; set; }
        }

        //Verify OTP
        [AllowAnonymous]
        public JsonResult VerifyOTP(string OTP, string email)
        {
            string returnvalue = "0";

            try
            {
                Login login = db.Logins.Where(s => s.LoginEmailID == email && s.LastOTP == OTP).FirstOrDefault();
                if (login != null)
                {
                    returnvalue = "1";
                }
                else
                {
                    returnvalue = "0";
                }
            }
            catch (Exception ex) { returnvalue = ex.Message; }

            return Json(returnvalue);
        }

        //Generate OTP
        public static string GenerateOTP()
        {
            var chars1 = "1234567890";
            var stringChars1 = new char[6];
            var random1 = new Random();

            for (int i = 0; i < stringChars1.Length; i++)
            {
                stringChars1[i] = chars1[random1.Next(chars1.Length)];
            }

            var str = new string(stringChars1);
            return str;
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult ResetPasswordSubmit(Eduegate.Web.Library.ViewModels.PasswordResetViewModel model)
        {
            Common result = new Common();
            bool success = false;
            string strMessagType = "";

            try
            {
                UserDTO userDTO = new UserDTO();
                userDTO.LoginUserID = model.Code;
                userDTO.PasswordSalt = Eduegate.Framework.Security.PasswordHash.CreateHash(model.Password);
                userDTO.Password = Eduegate.Framework.Security.StringCipher.Encrypt(model.Password, userDTO.PasswordSalt);
                userDTO.LoginID = CallContext.LoginID.ToString();
                result = ClientFactory.AccountServiceClient(CallContext).ResetPassword(userDTO);
                if (result.ToString() == "Success")
                {
                    strMessagType = "Success";
                    success = true;
                }
                else
                {
                    strMessagType = "Error";
                }
            }
            catch (Exception exception)
            {
                strMessagType = "Error";
                Eduegate.Logger.LogHelper<AccountController>.Fatal(exception.Message.ToString(), exception);
                return Json(new { IsError = !success, Reset = result, UserMessage = exception.Message.ToString(), MessageType = strMessagType });
            }

            return Json(new { IsError = !success, Reset = result, Message = "Reset Password Successfully!", MessageType = strMessagType });
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        //TODO: Check later
        //private IAuthenticationManager AuthenticationManager
        //{
        //    get
        //    {
        //        return null;// HttpContext.GetOwinContext().Authentication;
        //    }
        //}

        //TODO: Check later
        //private void AddErrors(IdentityResult result)
        //{
        //    foreach (var error in result.Errors)
        //    {
        //        ModelState.AddModelError("", error);
        //    }
        //}

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : UnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            //public override void ExecuteResult(ControllerContext context)
            //{
            //    var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
            //    if (UserId != null)
            //    {
            //        properties.Dictionary[XsrfKey] = UserId;
            //    }
            //    //context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            //}

            public void ExecuteResult(ControllerContext context)
            {
                var properties = new Microsoft.AspNetCore.Authentication.AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Items[XsrfKey] = UserId;
                }
                //context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
                context.HttpContext.ChallengeAsync(LoginProvider, properties);
            }

        }
        #endregion
    }
}
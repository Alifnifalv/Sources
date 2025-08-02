using Eduegate.ERP.Admin.Models;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Settings;
using System.Net;
using System.Net.Mail;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Eduegate.Application.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Newtonsoft.Json;
using Eduegate.Domain;
using Eduegate.Framework;
using System.Threading.Tasks;
using System.Linq;
using System.Text.RegularExpressions;

namespace Eduegate.ERP.Admin.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private static string ServiceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        private static string AccountService = string.Concat(ServiceHost, Eduegate.Framework.Helper.Constants.ACCOUNT_SERVICE);

        //TODO: Check if this is needed
        //private ApplicationSignInManager _signInManager;
        //private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        //TODO: Check if this is needed
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
        //        return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
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
        //        return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //    }
        //    private set
        //    {
        //        _userManager = value;
        //    }
        //}


        [AllowAnonymous]
        public ActionResult Login()
        {
            var VM = new LoginViewModel();
            VM.Companies = Eduegate.Web.Library.CompanyViewModel.FromDTO(ClientFactory.ReferenceDataServiceClient(CallContext).GetCompanies());
            var firstCompany = VM.Companies.FirstOrDefault();

            if (firstCompany != null)
                VM.CompanyID = firstCompany.Key;
            else
                VM.CompanyID = "1";

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
            int appID = (int)ApplicationType.ERP; // get the Application Id
            bool isSuccess = false;
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
                    {
                        isvalid = ClientFactory.AccountServiceClient(CallContext).Login(model.Email, model.Password, appID.ToString());
                    }

                    if (isvalid)
                    {
                        userDTO = ClientFactory.AccountServiceClient(CallContext).GetUserDetails(model.Email);
                        userDTO.UserSettings = new AccountBL(CallContext).GetUserSettings(userDTO.UserID);

                        if (userDTO.IsNotNull())
                            ResetCookies(userDTO, int.Parse(model.CompanyID));
                        else
                            return Ok(new { IsSuccess = false });

                        ClientFactory.UserServiceClient(CallContext).ResetForceLogout(long.Parse(userDTO.LoginID));
                        isSuccess = true;

                        var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Name, model.Email),
                                new Claim("FullName", model.Email),
                                new Claim(ClaimTypes.UserData,
                                JsonConvert.SerializeObject(new CallContext() {
                                    //BranchID = userDTO.Branch?.BranchIID,
                                    EmailID = userDTO.LoginEmailID,
                                    //MobileNumber = userDTO.MobileNumber,
                                    //UserClaims = JsonConvert.SerializeObject(userDTO.UserClaims),
                                    LoginID = long.Parse(userDTO.LoginID),
                                    UserId   = userDTO.UserID.ToString(),
                                    EmployeeID = userDTO.Employee?.EmployeeIID,
                                    AcademicYearID = userDTO.AcademicYearID,
                                    SchoolID = userDTO.SchoolID
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
            }

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            RemoveCallContext();
            return true;
        }

        [AllowAnonymous]
        public JsonResult SendMail(string email, string subject, string msg, string mailname)
        {
            string email_id = email;
            string mail_body = msg;
            try
            {
                var hostEmail = new Domain.Setting.SettingBL().GetSettingValue<string>("SMTPUserName").ToString();
                var hostPassword = new Domain.Setting.SettingBL().GetSettingValue<string>("SMTPPassword").ToString();

                SmtpClient ss = new SmtpClient();
                ss.Host = new Domain.Setting.SettingBL().GetSettingValue<string>("EmailHost").ToString();//"smtpout.secureserver.net";// "smtp.gmail.com";//"smtp.zoho.com";//
                ss.Port = new Domain.Setting.SettingBL().GetSettingValue<int>("smtpPort");// 587;//465;//;
                ss.Timeout = 20000;
                ss.DeliveryMethod = SmtpDeliveryMethod.Network;
                ss.UseDefaultCredentials = true;
                ss.EnableSsl = true;
                ss.Credentials = new NetworkCredential(hostEmail, hostPassword);//elcguide@gmail.com elcguide!@#$

                MailMessage mailMsg = new MailMessage(hostEmail, email, subject, msg);
                mailMsg.From = new MailAddress(hostEmail, mailname);
                mailMsg.IsBodyHtml = true;
                mailMsg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                ss.Send(mailMsg);

            }
            catch (Exception ex)
            {

                //lb_error.Visible = true;
                return Json(ex.Message);
            }

            return Json("ok");
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


        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //TODO: Check later
        // POST: /Account/Register
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Register(RegisterViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
        //        var result = await UserManager.CreateAsync(user, model.Password);

        //        if (result.Succeeded)
        //        {
        //            await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

        //            // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
        //            // Send an email with this link
        //            // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
        //            // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
        //            // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

        //            return RedirectToAction("Index", "Home");
        //        }

        //        AddErrors(result);
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}

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


        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //TODO: Check later
        // POST: /Account/ForgotPassword
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await UserManager.FindByNameAsync(model.Email);
        //        if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
        //        {
        //            // Don't reveal that the user does not exist or is not confirmed
        //            return View("ForgotPasswordConfirmation");
        //        }

        //        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
        //        // Send an email with this link
        //        // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
        //        // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
        //        // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
        //        // return RedirectToAction("ForgotPasswordConfirmation", "Account");
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}

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

        //TODO: Check later
        // POST: /Account/ResetPassword
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }
        //    var user = await UserManager.FindByNameAsync(model.Email);
        //    if (user == null)
        //    {
        //        // Don't reveal that the user does not exist
        //        return RedirectToAction("ResetPasswordConfirmation", "Account");
        //    }
        //    var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
        //    if (result.Succeeded)
        //    {
        //        return RedirectToAction("ResetPasswordConfirmation", "Account");
        //    }
        //    AddErrors(result);
        //    return View();
        //}

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

        private void ResetCookies(UserDTO user, int companyID)
        {
            base.CallContext.EmailID = user.LoginEmailID;
            base.CallContext.UserRole = string.Join(",", user.Roles);
            //base.CallContext.UserClaims = string.Join(",", user.UserClaims);
            base.CallContext.LoginID = user.UserID;
            base.CallContext.CompanyID = companyID;
            base.CallContext.AcademicYearID = user.AcademicYearID;
            base.CallContext.SchoolID = user.SchoolID;
            base.CallContext.CustomerID = user.Customer != null ? user.Customer.CustomerIID : (long?)null;
            base.CallContext.EmployeeID = user.Employee != null ? user.Employee.EmployeeIID : (long?)null;
            base.CallContext.SupplierID = user.Supplier != null ? user.Supplier.SupplierIID : (long?)null;
            ResetCallContext(base.CallContext);
        }

        public void RemoveCallContext()
        {
            Framework.CallContext _CallContext = new Framework.CallContext();
            Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.Remove("LOOKUP_School_" + base.CallContext.LoginID.Value.ToString());
            _CallContext.LoginID = null;
            _CallContext.EmailID = null;
            _CallContext.UserClaims = null;
            _CallContext.GUID = null;
            _CallContext.CompanyID = null;
            _CallContext.CustomerID = (long?)null;
            _CallContext.SupplierID = (long?)null;
            _CallContext.EmployeeID = (long?)null;
            _CallContext.AcademicYearID = null;
            _CallContext.SchoolID = null;
            ResetCallContext(_CallContext);
        }

        [AllowAnonymous]
        public ActionResult ResetPassword()
        {
            return View(new Eduegate.Web.Library.ViewModels.PasswordResetViewModel());
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ResetSchoolAcadamicYear(int? academicYearID, short? schoolID)
        {
            //TODO: This is temporary fix to clear caches, need actual fix
            Framework.CacheManager.MemCacheManager<object>.ClearAll();
            //Framework.CacheManager.MemCacheManager<List<KeyValueDTO>>.ClearAll();

            Common result = new Common();
            bool success = true;
            string strMessagType = "";

            try
            {
                base.CallContext.AcademicYearID = academicYearID;
                base.CallContext.SchoolID = schoolID;
                ResetCallContext(base.CallContext);

                var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Name, base.CallContext.EmailID),
                                new Claim("FullName", base.CallContext.EmailID),
                                new Claim(ClaimTypes.UserData,
                                JsonConvert.SerializeObject(base.CallContext))
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
            catch (Exception exception)
            {
                strMessagType = "Error";
                Eduegate.Logger.LogHelper<AccountController>.Fatal(exception.Message.ToString(), exception);
                return Json(new { IsError = !success, Reset = result, UserMessage = exception.Message.ToString(), MessageType = strMessagType });
            }

            return Json(new { IsError = !success, Reset = result, Message = "Successfully Modified!", MessageType = strMessagType });
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
                //userDTO.PasswordSalt = Eduegate.Framework.Security.PasswordHash.CreateHash(model.Password);
                //userDTO.Password = Eduegate.Framework.Security.StringCipher.Encrypt(model.Password, userDTO.PasswordSalt);
                userDTO.Password = model.Password;
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

        public ActionResult SchoolSelection()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Face(IFormFile photo)
        {
            if (photo == null || photo.Length == 0)
            {
                return BadRequest("No photo received");
            }

            // Validate content type (optional):
            if (!photo.ContentType.StartsWith("image/"))
            {
                return BadRequest("Invalid photo format");
            }

            // Create unique filename and path:
            string filename = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);
            string filePath = Path.Combine("uploads", filename);

            // Save the photo file:
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                photo.CopyToAsync(stream).Wait();
            }

            // Process the saved photo (optional):
            // ... (e.g., extract face data, resize image, etc.)

            return Ok("Photo saved successfully");
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        //TODO: Check later
        //private IAuthenticationManager AuthenticationManager
        //{
        //    get
        //    {
        //        return HttpContext.GetOwinContext().Authentication;
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
            //    var properties = new Microsoft.AspNetCore.Authentication.AuthenticationProperties { RedirectUri = RedirectUri };
            //    if (UserId != null)
            //    {
            //        properties.Items[XsrfKey] = UserId;
            //    }
            //    //context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            //    context.HttpContext.ChallengeAsync(LoginProvider, properties);
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
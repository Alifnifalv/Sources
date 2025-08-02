using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Eduegate.ERP.Admin.Models;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Framework.Extensions;
using Eduegate.Frameworks.Mvc.Controllers;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.Settings;
using System.Collections.Generic;
using Eduegate.Services.Contracts.Notifications;
using Eduegate.Domain.Entity.Models;
using System.Net.Mail;
using System.Net;
using System.IO;
using Eduegate.Domain.Repository;

namespace Eduegate.ERP.School.Portal.Controllers
{

    [Authorize]
    public class AccountController : BaseController
    {
        public dbEduegateERPContext db = new dbEduegateERPContext();
        private static string ServiceHost { get { return ConfigurationExtensions.GetAppConfigValue("ServiceHost"); } }
        private static string AccountService = string.Concat(ServiceHost, Eduegate.Framework.Helper.Constants.ACCOUNT_SERVICE);

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return null;// _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return null; // _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

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
                        return RedirectPermanent("~/Home/AnonymousDashbaord");
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

            return Json(new { IsSuccess = isvalid, UserSettings = userSettings }, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult Login(LoginViewModel model)
        {
            var userDTO = new UserDTO();
            byte appID = (byte)ApplicationType.ParentPortal;
                ; // get the Application Id
            bool isSuccess = false;
            model.CompanyID = "1";
            bool isvalid = false;
            if (model.IsNotNull())
            {
                if (ModelState.IsValid)
                {

                    if (model.LoginType != "Email")
                    {

                        userDTO.LoginEmailID = ClientFactory.AccountServiceClient(CallContext).GetUserData(new UserDTO() { LoginUserID = model.LoginUserID }).LoginEmailID;
                        model.Email = userDTO.LoginEmailID;
                    }

                    if (!string.IsNullOrEmpty(model.Email))
                        isvalid = ClientFactory.AccountServiceClient(CallContext).Login(model.Email, model.Password, appID.ToString());

                    if (isvalid)
                    {
                        userDTO = ClientFactory.AccountServiceClient(CallContext).GetUserDetails(model.Email);

                        if (userDTO.IsNotNull())
                            ResetCookies(userDTO, int.Parse(model.CompanyID));
                        else
                            return Json(new { IsSuccess = false }, JsonRequestBehavior.AllowGet);

                        isSuccess = true;
                    }
                }
            }

            List<SettingDTO> userSettings = null;

            if (isSuccess)
            {
                CallContext.LoginID = int.Parse(userDTO.LoginID);

                userSettings = ClientFactory.SettingServiceClient(CallContext).GetAllUserSettingDetail();
            }

            return Json(new { IsSuccess = isSuccess, UserSettings = userSettings }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SendMail(string email, string subject, string msg, string mailname, String maildomain)
        {
            string email_id = email;
            string mail_body = msg;
            try
            {
                var hostEmail = ConfigurationExtensions.GetAppConfigValue("SMTPUserName").ToString();
                var hostPassword = ConfigurationExtensions.GetAppConfigValue("SMTPPassword").ToString();

                SmtpClient ss = new SmtpClient();
                ss.Host = ConfigurationExtensions.GetAppConfigValue("EmailHost").ToString();//"smtpout.secureserver.net";// "smtp.gmail.com";//"smtp.zoho.com";//
                ss.Port = ConfigurationExtensions.GetAppConfigValue<int>("smtpPort");// 587;//465;//;
                ss.Timeout = 20000;
                ss.DeliveryMethod = SmtpDeliveryMethod.Network;
                ss.UseDefaultCredentials = true;
                ss.EnableSsl = true;
                ss.Credentials = new NetworkCredential(hostEmail, hostPassword);//elcguide@gmail.com elcguide!@#$

                MailMessage mailMsg = new MailMessage(maildomain, email, subject, msg);
                mailMsg.From = new MailAddress(maildomain, mailname);
                mailMsg.IsBodyHtml = true;
                mailMsg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                ss.Send(mailMsg);

            }
            catch (Exception ex)
            {

                //lb_error.Visible = true;
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }


            return Json("ok", JsonRequestBehavior.AllowGet);
        }
        private String PopulateBody(String Name, String htmlMessage)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath("~/emailtemplate.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{CUSTOMERNAME}", Name);
            body = body.Replace("{HTMLMESSAGE}", htmlMessage);
            body = body.Replace("{YEAR}", DateTime.Now.Year.ToString());
            return body;
        }
        private String PopulateBodyForOTP(String Name, String content_01, String content_02, String content_03, String content_04, String content_otp)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(System.Web.Hosting.HostingEnvironment.MapPath("~/emailtemplate_otp.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{content_01}", content_01);
            body = body.Replace("{content_02}", content_02);
            body = body.Replace("{content_03}", content_03);
            body = body.Replace("{content_04}", content_04);
            body = body.Replace("{content_otp}", content_otp);
            body = body.Replace("{YEAR}", DateTime.Now.Year.ToString());
            return body;
        }
        [AllowAnonymous]
        public bool Signout()
        {
            RemoveCallContext();
            return true;
        }



        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

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

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

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
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

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
                    //return Json(new { IsError = !success, Reset = result, Message = "Email ID not exist!", MessageType = strMessagType }, JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception exception)
            {
                strMessagType = "Error";
                Eduegate.Logger.LogHelper<AccountController>.Fatal(exception.Message.ToString(), exception);
                return Json(new { IsError = !success, Reset = result, UserMessage = exception.Message.ToString(), MessageType = strMessagType }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { IsError = !success, Reset = result, Message = "Reset Password Successfully!", MessageType = strMessagType }, JsonRequestBehavior.AllowGet);



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

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        private void ResetCookies(UserDTO user, int companyID)
        {
            base.CallContext.EmailID = user.LoginEmailID;
            base.CallContext.UserRole = string.Join(",", user.Roles);
            //base.CallContext.UserClaims = string.Join(",", user.UserClaims);
            base.CallContext.LoginID = user.UserID;
            base.CallContext.CompanyID = companyID;

            base.CallContext.CustomerID = user.Customer != null ? user.Customer.CustomerIID : (long?)null;
            base.CallContext.EmployeeID = user.Employee != null ? user.Employee.EmployeeIID : (long?)null;
            base.CallContext.SupplierID = user.Supplier != null ? user.Supplier.SupplierIID : (long?)null;
            base.CallContext.UserRole = user.RoleName;
            ResetCallContext(base.CallContext);
        }

        public void RemoveCallContext()
        {
            Framework.CallContext _CallContext = new Framework.CallContext();
            _CallContext.LoginID = null;
            _CallContext.EmailID = null;
            _CallContext.UserClaims = null;
            _CallContext.GUID = null;
            _CallContext.CompanyID = null;
            _CallContext.CustomerID = (long?)null;
            _CallContext.SupplierID = (long?)null;
            _CallContext.EmployeeID = (long?)null;
            base.CallContext.UserRole = null;
            ResetCallContext(_CallContext);
        }

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

            /*
             notificationDTO.AdditionalParameters.Add(new Eduegate.Services.Contracts.Commons.KeyValueParameterDTO() { ParameterName = Eduegate.Services.Contracts.Constants.EmailNotificationType.OrderNotificationForSupplier.Keys.HeadID, ParameterValue = transaction.HeadIID.ToString() });
                notificationDTO.AdditionalParameters.Add(new Eduegate.Services.Contracts.Commons.KeyValueParameterDTO() { ParameterName = Eduegate.Services.Contracts.Constants.EmailNotificationType.OrderNotificationForSupplier.Keys.ReportName, ParameterValue = "NoReport" });
                notificationDTO.AdditionalParameters.Add(new Eduegate.Services.Contracts.Commons.KeyValueParameterDTO() { ParameterName = Eduegate.Services.Contracts.Constants.EmailNotificationType.OrderNotificationForSupplier.Keys.Attachment, ParameterValue = "fales" });
                notificationDTO.AdditionalParameters.Add(new Eduegate.Services.Contracts.Commons.KeyValueParameterDTO() { ParameterName = Eduegate.Services.Contracts.Constants.EmailNotificationType.OrderNotificationForSupplier.Keys.ReturnFile, ParameterValue = "false" });

                if (supplierDetail.Login.LoginEmailID.IsNotNull())
                {
                    notificationDTO.ToEmailID = supplierDetail.Login.LoginEmailID;
                    notificationDTO.AdditionalParameters.Add(new Eduegate.Services.Contracts.Commons.KeyValueParameterDTO() { ParameterName = Eduegate.Services.Contracts.Constants.EmailNotificationType.OrderNotificationForSupplier.Keys.ErrorMessage, ParameterValue = "" });
                }
                else
                {
                    notificationDTO.ToEmailID = ConfigurationExtensions.GetAppConfigValue("EmailFrom").ToString();
                    notificationDTO.AdditionalParameters.Add(new Eduegate.Services.Contracts.Commons.KeyValueParameterDTO() { ParameterName = Eduegate.Services.Contracts.Constants.EmailNotificationType.OrderNotificationForSupplier.Keys.ErrorMessage, ParameterValue = string.Concat("Could not Find Email Address for Supplier: ", supplierDetail.FirstName, " (supplierID: ", transaction.SupplierID, ")") });
                }
             */
            // var supplierDetail = new Eduegate.TransactionEgine.ClientFactory.ClientFactory(null).GetSupplier(transaction.SupplierID.ToString());

            var hostUser = ConfigurationExtensions.GetAppConfigValue("EmailUser").ToString();
            var emaildata = new EmailNotificationDTO();

            var fromEmail = ConfigurationExtensions.GetAppConfigValue("EmailFrom").ToString();

            var ccEmail = ConfigurationExtensions.GetAppConfigValue("EmailID").ToString();
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
                    String Subject = "OTP - Don't share your code with others ";
                    String emailBody = @"<strong style='font-size:1.2em;' font-family:'Times New Roman;'><b>Here is your One Time Password</b></strong><br />
                                        <strong style='font-size:1em;' font-family:'Times New Roman;'>To Validate Your Email Address</strong><br />
                                        <strong style='font-size:3rem;'>" + GetOTP + @"</strong><br /><strong style='font-size:0.6em;' font-family:'Times New Roman;'>Don't share this code with others</strong>";
                    String replaymessage = PopulateBodyForOTP(email, "Welcome", "Here is your One Time Password", "Don't share this code with others", "Please note : do not reply to this email as it is a computer generated email", GetOTP);

                    MutualRepository mutualRepository = new MutualRepository();
                    var hostDet = mutualRepository.GetSettingData("HOST_NAME").SettingValue;

                    string defaultMail = mutualRepository.GetSettingData("DEFAULT_MAIL_ID").SettingValue;

                    if (hostDet == "Live")
                    {
                        SendMail(email, Subject, replaymessage, hostUser, fromEmail);
                    }
                    else
                    {
                        SendMail(defaultMail, Subject, replaymessage, hostUser, fromEmail);
                    }
                    
                    returnvalue = "1";
                }
                else
                {
                    returnvalue = "Invalid Account..!";
                }
            }
            catch (Exception ex) { returnvalue = ex.Message; }

            return Json(returnvalue, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public JsonResult OTPforVerifyEmail(string email)
        {
            string returnvalue = "0";

            var hostUser = ConfigurationExtensions.GetAppConfigValue("EmailUser").ToString();
            var fromEmail = ConfigurationExtensions.GetAppConfigValue("EmailFrom").ToString();

            var emaildata = new EmailNotificationDTO();
            try
            {

                Login login = db.Logins.Where(s => s.LoginEmailID == email).FirstOrDefault();

                if (login == null)
                {
                    string GetOTP = GenerateOTP();
                    //emaildata.Subject = "OTP:-" + GetOTP;
                    //login.LastOTP = GetOTP;
                    // db.SaveChanges();
                    //EmailProcess.ProcessEmail(emaildata);
                    String Subject = "OTP - Don't share your code with others ";
                    String emailBody = @"<strong style='font-size:1.2em;' font-family:'Times New Roman;'><b>Here is your One Time Password</b></strong><br />
                                        <strong style='font-size:1em;' font-family:'Times New Roman;'>To Validate Your Email Address</strong><br />
                                        <strong style='font-size:3rem;'>" + GetOTP + @"</strong><br /><strong style='font-size:0.6em;' font-family:'Times New Roman;'>Don't share this code with others</strong>";
                    String replaymessage = PopulateBodyForOTP(email, "Welcome", "Here is your One Time Password", "Don't share this code with others", "Please note : do not reply to this email as it is a computer generated email", GetOTP);

                    MutualRepository mutualRepository = new MutualRepository();
                    var hostDet = mutualRepository.GetSettingData("HOST_NAME").SettingValue;

                    string defaultMail = mutualRepository.GetSettingData("DEFAULT_MAIL_ID").SettingValue;

                    if (hostDet == "Live")
                    {
                        SendMail(email, Subject, replaymessage, hostUser, fromEmail);
                    }
                    else
                    {
                        SendMail(defaultMail, Subject, replaymessage, hostUser, fromEmail);
                    }

                    returnvalue = GetOTP;
                }

            }
            catch (Exception ex) { returnvalue = ex.Message; }

            return Json(returnvalue, JsonRequestBehavior.AllowGet);
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

            return Json(returnvalue, JsonRequestBehavior.AllowGet);
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

            var str = new String(stringChars1);
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
                return Json(new { IsError = !success, Reset = result, UserMessage = exception.Message.ToString(), MessageType = strMessagType }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { IsError = !success, Reset = result, Message = "Reset Password Successfully!", MessageType = strMessagType }, JsonRequestBehavior.AllowGet);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return null;// HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
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

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                //context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}
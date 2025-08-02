using Eduegate.Vendor.PortalCore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Eduegate.Application.Mvc;
using Eduegate.Domain;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Services.Contracts.Settings;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using Eduegate.Framework;
using System.Security.Claims;
using Eduegate.Domain.Entity;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Vendor;
using Microsoft.AspNetCore.Authorization;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Notifications;
using System.Linq;
using Eduegate.Domain.Mappers;
using Eduegate.Framework.Contracts.Common.Enums;
using Microsoft.Reporting.Map.WebForms.BingMaps;

namespace Eduegate.Vendor.PortalCore.Controllers
{
    public class AccountController : BaseController
    {
        public dbEduegateERPContext db = new dbEduegateERPContext();
        private static string ServiceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        private static string AccountService = string.Concat(ServiceHost, Eduegate.Framework.Helper.Constants.ACCOUNT_SERVICE);


        public AccountController()
        {
        }

        public IActionResult ForgetPassword()
        {
            return View();
        }

        public IActionResult NewLogin() //Registration
        {
            return View();
        } 
        
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var userDTO = new UserDTO();
            byte appID = 0;
            bool isSuccess = false;
            model.CompanyID = "1";
            bool isvalid = false;

            if (model.IsNotNull())
            {
                if (ModelState.IsValid)
                {

                    if (!string.IsNullOrEmpty(model.Email))
                        isvalid = ClientFactory.AccountServiceClient(CallContext).Login(model.Email, model.Password, appID.ToString());

                    if (model.Email != null)
                    {
                        var userInfo = ClientFactory.AccountServiceClient(CallContext).GetUserData(new UserDTO()
                        {
                            LoginUserID = model.LoginUserID,
                        });

                        if (userInfo.LoginUserID == null)
                        {
                            isvalid = false;
                        }
                        else
                        {
                            if (userInfo.LoginEmailID != model.Email || userInfo.LoginUserID != model.LoginUserID)
                            {
                                isvalid = false;
                            }
                        }
                    }

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
                                    SupplierID = userDTO.Supplier?.SupplierIID,
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
                CallContext.SupplierID = userDTO?.Supplier?.SupplierIID;

                try
                {
                    userSettings = ClientFactory.SettingServiceClient(CallContext).GetAllUserSettingDetail();
                }
                catch (Exception ex)
                {
                    Eduegate.Logger.LogHelper<AccountController>.Fatal(ex.Message, ex);
                    throw;
                }
                return RedirectToAction("Home", "Home");
            }
            else
            {
                if(model.LoginUserID != null)
                {
                    ViewBag.ErrorMessage = "Invalid Vendor CR,Email or password !";
                }
                return View(model);
            }
        }

        [AllowAnonymous]
        public IActionResult OTPGenerate([FromBody] LoginDTO loginDTO) 
        {
            if (loginDTO.ConfirmPassword != loginDTO.Password)
            {
                loginDTO.IsError = true;
                loginDTO.ReturnMessage =
                    "The confirmed password does not match the original password. Please ensure they are the same";
            }
            else
            {
                try
                {
                    var otpGen = new LoginMapper().ResetPasswordOTPGenerate(loginDTO.LoginEmailID);

                    if (otpGen.operationResult == OperationResult.Success)
                    {
                        loginDTO.IsError = false;
                        loginDTO.ReturnMessage = "OTP Generated Successfully";
                    }
                }
                catch (Exception ex) { loginDTO.ReturnMessage = ex.Message; }
            }

            return Ok(loginDTO);

        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult ResetPasswordSubmit([FromBody] LoginDTO loginDTO)
        {
            Common result = new Common();
            bool success = false;
            string strMessagType = "Error";
            var returnMsg = "Something went wrong !";

            UserDTO userDTO = new UserDTO();
            userDTO = ClientFactory.AccountServiceClient(CallContext).GetUserDetails(loginDTO.LoginEmailID);

            if (loginDTO.LastOTP == null)
            {
                returnMsg = "Please enter OTP to reset password !";
            }
            else if (loginDTO.ConfirmPassword != loginDTO.Password)
            {
                returnMsg = "The confirmed password does not match the original password. Please ensure they are the same";
            }
            else if(loginDTO.LastOTP != userDTO.LastOTP)
            {
                returnMsg = "Incorrect OTP!";
            }
            else
            {
                try
                {
                        userDTO.Password = loginDTO.Password;
                        result = ClientFactory.AccountServiceClient(CallContext).ResetPassword(userDTO);
                        if (result.ToString() == "Success")
                        {
                            strMessagType = "Success";
                            success = true;
                            returnMsg = "Reset Password Successfully!";
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
            }

            return Json(new { IsError = !success, Reset = result, Message = returnMsg, MessageType = strMessagType });
        }

        [HttpPost]
        public IActionResult Register([FromBody] VendorRegisterDTO registrationDTO)
        {
            if (registrationDTO.ConfirmPassword != registrationDTO.Password)
            {
                registrationDTO.IsError = true;
                registrationDTO.ReturnMessage = 
                    "The confirmed password does not match the original password. Please ensure they are the same";
            }
            else
            {
                var result = new SupplierBL(CallContext).RegisterVendor(registrationDTO);
            }
            return Ok(registrationDTO);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}

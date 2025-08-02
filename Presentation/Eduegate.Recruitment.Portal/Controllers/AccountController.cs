using Eduegate.Application.Mvc;
using Eduegate.Domain;
using Eduegate.Domain.Entity;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Recruitment.Portal.Models;
using Eduegate.Services.Contracts.Jobs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Security.Claims;

namespace Eduegate.Recruitment.Portal.Controllers
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

        public IActionResult Register()
        {
            return View();
        }  
        
        public IActionResult Login()
        {
            return View();
        } 
        
        public IActionResult MailResponsePage()
        {
            return View();
        }


        [HttpPost]
        public IActionResult RegisterUser([FromBody] RegisterUserDTO registrationDTO)
        {
            var result = new RecruitmentBL(CallContext).RegisterUser(registrationDTO);
            return Ok(registrationDTO);
        }

        [HttpPost]
        public async Task<IActionResult> LoginValidate([FromBody] RegisterUserDTO loginDTO)
        {
            if (loginDTO.Password != null)
            {
                loginDTO = new RecruitmentBL(CallContext).LoginValidate(loginDTO);

                if (loginDTO.IsError == false)
                {
                    var claims = new List<System.Security.Claims.Claim>
                            {
                                new System.Security.Claims.Claim(ClaimTypes.Name, loginDTO.EmailID),
                                new System.Security.Claims.Claim("FullName", loginDTO.EmailID),
                                new System.Security.Claims.Claim(ClaimTypes.UserData,
                                JsonConvert.SerializeObject(new CallContext() {
                                    EmailID = loginDTO.EmailID,
                                    LoginID = loginDTO.LoginID,
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
            else
            {
                loginDTO.IsError = true;
                loginDTO.ReturnMessage = "Please enter the password !";
            }

            return Ok(loginDTO);
        }


        [AllowAnonymous]
        public IActionResult OTPGenerate([FromBody] RegisterUserDTO loginDTO)
        {

            try
            {
                var otpGen = new RecruitmentBL(CallContext).GenerateAndSendMailOTP(loginDTO);

                if (otpGen.operationResult == OperationResult.Success)
                {
                    loginDTO.IsError = false;
                    loginDTO.ReturnMessage = "OTP Generated Successfully! please check your mail.";
                }
            }
            catch (Exception ex) { loginDTO.ReturnMessage = ex.Message; }

            return Ok(loginDTO);
        }


        [HttpPost]
        public IActionResult ValidateOTP([FromBody] RegisterUserDTO loginDTO) 
        {
            var result = new Services.Contracts.Commons.OperationResultDTO();

            if (loginDTO.OTP != null)
            {
                result = new RecruitmentBL(CallContext).OTPValidate(loginDTO);

                if(result.operationResult == OperationResult.Success)
                {
                    return Json(new { IsError = false, UserMessage = result.Message });
                }
                else
                {
                    return Json(new { IsError = true, UserMessage = result.Message });
                }
            }
            else
            {
                return Json(new { IsError = true, UserMessage = "Please enter the OTP !" });
            }
        }

        public ActionResult JobInterviewApplicantResponse(bool? response, long? interviewID, long? applicantID)
        {
            try
            {
                new RecruitmentBL(CallContext).JobInterviewApplicantResponse(response, interviewID, applicantID);
                return RedirectToAction("MailResponsePage");
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<AccountController>.Fatal(ex.Message.ToString(), ex);
                return Json(new { IsError = true, UserMessage = ex.Message });
            }
        }

        [AllowAnonymous]
        public IActionResult UpdateIsActiveStatusAndSendMail([FromBody] RegisterUserDTO loginDTO) 
        {

            try
            {
                var otpGen = new RecruitmentBL(CallContext).UpdateIsActiveStatus(loginDTO);

                if (otpGen.operationResult == OperationResult.Success)
                {
                    loginDTO.IsError = false;
                    loginDTO.ReturnMessage = "User registration has been successfully completed.";
                }
            }
            catch (Exception ex) { loginDTO.ReturnMessage = ex.Message; }

            return Ok(loginDTO);
        } 
        
        [AllowAnonymous]
        public IActionResult ResetLoginPassword([FromBody] RegisterUserDTO loginDTO)  
        {
            try
            {
                var result = new RecruitmentBL(CallContext).ResetLoginPassword(loginDTO);

                if (result.operationResult == OperationResult.Success)
                {
                    return Json(new { IsError = false, UserMessage = result.Message });
                }
                else
                {
                    return Json(new { IsError = true, UserMessage = result.Message });
                }
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<AccountController>.Fatal(ex.Message.ToString(), ex);
                return Json(new { IsError = true, UserMessage = ex.Message });
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}

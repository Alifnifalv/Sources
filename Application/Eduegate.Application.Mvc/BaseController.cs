using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Eduegate.Domain.Entity;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts;
using System.Security.Claims;
using System.Linq;
using System.Threading.Tasks;
using Eduegate.Framework.Security;
using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;

namespace Eduegate.Application.Mvc
{
    public class BaseController : Controller
    {
        protected CallContext CallContext { get; set; }
        protected dbEduegateERPContext _dbContext;
        protected IBackgroundJobClient _backgroundJobs;

        private string siteCookieKey = new Domain.Setting.SettingBL().GetSettingValue<string>("SiteCookieKey", 0, "www.eduegateerp.com");

        private string callContextKey = new Domain.Setting.SettingBL().GetSettingValue<string>("ContextKey", 0, "cce");

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var callContext = HttpContext.User.Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.UserData).Value;

                if (!string.IsNullOrEmpty(callContext))
                {
                    CallContext = JsonConvert.DeserializeObject<CallContext>(callContext);
                }

                if (CallContext == null)
                {
                    CallContext = new CallContext();
                }

                if (!CallContext.CompanyID.HasValue)
                {
                    CallContext.CompanyID = 1;
                }

                if (string.IsNullOrEmpty(CallContext.LanguageCode))
                {
                    if (context.HttpContext.Request.Query.Keys.Contains("language"))
                    {
                        CallContext.LanguageCode = context.HttpContext.Request.Query["language"].ToString();
                    }
                }

                if (string.IsNullOrEmpty(CallContext.LanguageCode))
                {
                    CallContext.LanguageCode = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
                }

                ViewBag.LoginID = CallContext.LoginID.ToString();
            }

            return base.OnActionExecutionAsync(context, next);
        }

        public BaseController()
        {
            CallContext = new CallContext();
        }

        protected void ResetCookies(UserDTO user, int companyID)
        {
            CallContext.EmailID = user.LoginEmailID;
            CallContext.UserRole = user.Roles.Count > 0 ? string.Join(",", user.Roles.Select(i => i.RoleName)) : user.RoleName;
            //base.CallContext.UserClaims = string.Join(",", user.UserClaims);
            CallContext.LoginID = user.UserID;
            CallContext.CompanyID = companyID;
            CallContext.CustomerID = user.Customer != null ? user.Customer.CustomerIID : (long?)null;
            CallContext.EmployeeID = user.Employee != null ? user.Employee.EmployeeIID : (long?)null;
            CallContext.SupplierID = user.Supplier != null ? user.Supplier.SupplierIID : (long?)null;
            CallContext.SchoolID = user.SchoolID;
            CallContext.AcademicYearID = user.AcademicYearID;

            Framework.CacheManager.MemCacheManager<string>.ClearAll();

            ResetCallContext(CallContext);
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
            _CallContext.SchoolID = (short?)null;
            _CallContext.AcademicYearID = (int?)null;

            ResetCallContext(_CallContext);
        }

        protected void ResetCallContext(CallContext callContext = null)
        {
            //try
            //{
            //    DecryptCookies();
            //    //CallContext.IPAddress = Request.Headers["REMOTE_ADDR"] ?? Request.UserHostAddress;
            //    //CallContext.LanguageCode = Response.Cookies.AllKeys.Contains("_uiculture") ? Response.Cookies["_uiculture"].Value : (Request.Cookies.AllKeys.Contains("_uiculture") ? Request.Cookies["_uiculture"].Value : null);
            //    CallContext.CurrencyCode = CallContext.CurrencyCode.IsNotNullOrEmpty() ? CallContext.CurrencyCode : new Domain.Setting.SettingBL().GetSettingValue<string>("DEFAULT_CURRENCY_CODE");
            //    CallContext.CompanyID = CallContext.CompanyID.IsNotNull() ? CallContext.CompanyID : int.Parse(new Domain.Setting.SettingBL().GetSettingValue<string>("CompanyID"));
            //    // CallContext.SiteID = CallContext.SiteID.IsNotNull() ? CallContext.SiteID : new Domain.Setting.SettingBL().GetSettingValue<string>("SiteID");
            //    CallContext.EmployeeID = CallContext.EmployeeID.IsNotNull() ? CallContext.EmployeeID : null;
            //    CallContext.CustomerID = CallContext.CustomerID.IsNotNull() ? CallContext.CustomerID : null;
            //    CallContext.SupplierID = CallContext.SupplierID.IsNotNull() ? CallContext.SupplierID : null;
            //    CallContext.SchoolID = CallContext.SchoolID.IsNotNull() ? CallContext.SchoolID : null;
            //    CallContext.AcademicYearID = CallContext.AcademicYearID.IsNotNull() ? CallContext.AcademicYearID : null;
            //}
            //catch (Exception ex) 
            //{

            //}

            //DecryptCookies();

            if (callContext == null)
            {
                callContext = new CallContext();
            }


            if (callContext.IsNotNull())
            {
                CallContext.CompanyID = callContext.CompanyID;
                CallContext.EmailID = callContext.EmailID;
                CallContext.LoginID = callContext.LoginID;
                CallContext.GUID = callContext.GUID;
                CallContext.CurrencyCode = callContext.CurrencyCode;
                CallContext.UserId = callContext.UserId;
                CallContext.ReturnUrl = callContext.ReturnUrl;
                CallContext.SupplierID = callContext.SupplierID;
                CallContext.EmployeeID = callContext.EmployeeID;
                CallContext.CustomerID = callContext.CustomerID;
                CallContext.SchoolID = callContext.SchoolID;
                CallContext.AcademicYearID = callContext.AcademicYearID;
                CallContext.LanguageCode = callContext.LanguageCode;
                CallContext.UserRole = callContext.UserRole;
                CallContext.SiteID = callContext.SiteID;
            }

            EncryptCookies();
        }

        private void DecryptCookies()
        {
            //if (string.IsNullOrEmpty(Request.Headers.Authorization.Parameter))
            //{
            //    CallContext = JsonConvert.DeserializeObject<CallContext>(StringCipher.Decrypt(Request.Headers.Authorization.Parameter, siteCookieKey));
            //}

            //if (CallContext.IsNull())
            //    _context = new CallContext();
            try
            {
                if (Request.Cookies.ContainsKey(callContextKey))
                {
                    CallContext = JsonConvert.DeserializeObject<CallContext>(StringCipher.Decrypt(Request.Cookies[callContextKey], siteCookieKey));
                }

                if (CallContext == null)
                {
                    CallContext = new CallContext();
                }
            }

            catch (Exception ex)
            {
                //throw ex;
            }
        }

        private void EncryptCookies()
        {
            Response.Cookies.Append(callContextKey, StringCipher.Encrypt(JsonConvert.SerializeObject(CallContext), siteCookieKey), new CookieOptions
            {
                Expires = DateTime.Now.AddYears(1)
            });
            //Request.Headers.Authorization.Parameter = StringCipher.Encrypt(XmlSerializerHelper.ToXml<CallContext>(CallContext, Encoding.Default), siteCookieKey);
        }

        public async void UpdateCookiesByContext(CallContext context)
        {
            // Update the language code in the cookie
            var updatedCallContext = new CallContext()
            {
                // Update all necessary fields of the CallContext
                CompanyID = CallContext.CompanyID,
                EmailID = CallContext.EmailID,
                LoginID = CallContext.LoginID,
                GUID = CallContext.GUID,
                CurrencyCode = CallContext.CurrencyCode,
                UserId = CallContext.UserId,
                ReturnUrl = CallContext.ReturnUrl,
                SupplierID = CallContext.SupplierID,
                EmployeeID = CallContext.EmployeeID,
                CustomerID = CallContext.CustomerID,
                SchoolID = CallContext.SchoolID,
                AcademicYearID = CallContext.AcademicYearID,
                LanguageCode = CallContext.LanguageCode,
            };

            // Serialize the updated CallContext and store it back in the cookie
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, context.EmailID),
                    //new Claim("FullName", model.Email),
                    new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(updatedCallContext)),
                };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties { IsPersistent = true };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties
            );
        }

    }
}
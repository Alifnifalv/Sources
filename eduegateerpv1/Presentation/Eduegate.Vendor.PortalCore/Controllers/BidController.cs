using Eduegate.Application.Mvc;
using Eduegate.Domain;
using Eduegate.Services.Contracts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Eduegate.Services.Client.Factory;
using Eduegate.Framework.Extensions;
using Eduegate.Framework;
using Eduegate.Framework.Enums;
using Eduegate.Services.Contracts.Settings;
using Eduegate.Web.Library.ViewModels.Inventory.Purchase;
using Eduegate.Services.Contracts.School.Transports;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Setting;
using Eduegate.Services.Contracts.Catalog;
namespace Eduegate.Vendor.PortalCore.Controllers
{
    public class BidController : BaseController
    {
        private readonly HttpClient _httpClient;

        public BidController()
        {
            _httpClient = new HttpClient();
        }

        public IActionResult BidLogin()
        {
            return View();
        }

        public IActionResult BidHome()
        {
            return View();
        }  
        
        public IActionResult BidUsersList() 
        {
            return PartialView();
        }

        public JsonResult GetBidUserDetails()
        {
            var userDetail = new SupplierBL(CallContext).GetBidUserDetails((long)CallContext.LoginID);

            return Json(new { IsError = false, Response = userDetail });
        }

        [HttpPost]
        public async Task<IActionResult> BidLoginValidate([FromBody] TenderAuthenticationDTO bidLoginDTO)
        {
            if (bidLoginDTO.Password != null)
            {
                bidLoginDTO = new SupplierBL(CallContext).BidLoginValidate(bidLoginDTO);

                if (bidLoginDTO.IsError == false)
                {
                    var claims = new List<System.Security.Claims.Claim>
                            {
                                new System.Security.Claims.Claim(ClaimTypes.Name, bidLoginDTO.EmailID),
                                new System.Security.Claims.Claim("FullName", bidLoginDTO.EmailID),
                                new System.Security.Claims.Claim(ClaimTypes.UserData,
                                JsonConvert.SerializeObject(new CallContext() {
                                    EmailID = bidLoginDTO.EmailID,
                                    LoginID = bidLoginDTO.LoginID,
                                    UserId  = bidLoginDTO.UserID.ToString(),
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
                bidLoginDTO.IsError = true;
                bidLoginDTO.ReturnMessage = "Please enter the password !";
            }

            return Ok(bidLoginDTO);
        }


        public JsonResult GetBidUserListByTenderID(long tenderID)
        {
            var usersList = new SupplierBL(CallContext).GetBidUserListByTenderID(tenderID);

            return Json(new { IsError = false, Response = usersList });
        }

        public JsonResult GetTenderList()
        {
            var usersList = new SupplierBL(CallContext).GetTenderList((long)CallContext.LoginID);

            return Json(new { IsError = false, Response = usersList });
        }

        public IActionResult RFQComparison(long? tenderIID)
        {
            var model = new RFQComparisonViewModel();

            model.TenderIID = tenderIID;

           return View(model);
        }

        public JsonResult GetQuotationListByTenderID(long? tenderIID) 
        {
            var data = new TransactionBL(CallContext).GetQuotationListByTenderID(tenderIID);

            return Json(new { IsError = false, Response = data });
        }

        [HttpPost]
        public ActionResult OpenAndUpdateTenderLog(long iid) 
        {
            var result = new SupplierBL(CallContext).OpenAndUpdateTenderLog(iid);
            return Json(result);
        }

        [HttpPost]
        public JsonResult SaveBidApprovalItemList([FromBody] TransactionDTO dto) 
        {
            var result = new TransactionDTO();

            try
            {
                dto.TransactionHead.CreatedBy = (int?)CallContext.LoginID;
                result = new TransactionBL(CallContext).SaveBidApprovalItemList(dto);
            }
            catch (Exception ex)
            {
                result.IsError = true;
                result.ReturnMessage = ex.Message; // Store the exception message
            }

            return Json(result);
        }
    }
}

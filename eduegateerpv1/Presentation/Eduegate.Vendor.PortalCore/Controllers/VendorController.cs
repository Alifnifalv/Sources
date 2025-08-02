using Eduegate.Application.Mvc;
using Eduegate.Domain;
using Eduegate.Services.Contracts.Vendor;
using Microsoft.AspNetCore.Mvc;
using Eduegate.Vendor.PortalCore.Models;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using Eduegate.Services.Contracts;
using Eduegate.Domain.Mappers;
using Eduegate.Web.Library.ViewModels.Inventory.Purchase;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Client.Factory;
using System.Data.Entity.Infrastructure;
using Eduegate.Domain.Setting;
using System.Linq;
using System;
using System.Security.Cryptography;
using Eduegate.Services.Contracts.CommonDTO;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Domain.Repository;
using static System.Net.Mime.MediaTypeNames;


namespace Eduegate.Vendor.PortalCore.Controllers
{
    public class VendorController : BaseController
    {

        public IActionResult _ToastMessage()
        {
            return View();
        }

        public JsonResult GetQuotationList()
        {
            var result = new SupplierBL(CallContext).GetQuotationListByLoginID((long)CallContext.LoginID);

            return Json(result);
        }

        [HttpPost]
        public JsonResult SubmitContactPerson([FromBody] ContactDTO contactDTO)
        {
            bool success = false;
            string strMessagType = "Error";
            var returnMsg = "Something went wrong !";

            try
            {
                var result = new SupplierBL(CallContext).RegisterContactPerson(contactDTO);
                if (result.ToString() == "Success")
                {
                    strMessagType = "Success";
                    success = true;
                    result = "Successfully saved";
                }
                else
                {
                    strMessagType = "Error";
                    success = false;
                }

                returnMsg = result;
            }
            catch (Exception exception)
            {
                strMessagType = "Error";
                Eduegate.Logger.LogHelper<AccountController>.Fatal(exception.Message.ToString(), exception);
                return Json(new { IsError = !success, Reset = "", UserMessage = exception.Message.ToString(), MessageType = strMessagType });
            }

            return Json(new { IsError = !success, Reset = "", Message = returnMsg, MessageType = strMessagType });
        }

        public JsonResult GetVendorContactList()
        {
            var result = new SupplierBL(CallContext).GetVendorContactListByLoginID((long)CallContext.LoginID);

            return Json(result);
        }

        public JsonResult GetRFQItemListByIID(long iid)
        {
            var result = new SupplierBL(CallContext).GetRFQItemListByIID(iid);

            return Json(result);
        }

        [HttpPost]
        public JsonResult SaveSupplierQuotation([FromBody] TransactionDTO dto)
        {
            var result = new TransactionDTO();

            long headIID = 0;

            try
            {
                var settingData = new SettingBL().GetSettingValue<string>("QUOTATION_DOC_TYP_ID");

                dto.TransactionHead.TransactionDate = DateTime.Now;
                dto.TransactionHead.DocumentTypeID = int.Parse(settingData);
                dto.TransactionHead.DocumentStatusID = dto.TransactionHead.DocumentStatusID.HasValue ? dto.TransactionHead.DocumentStatusID : 1;
                dto.TransactionHead.ReferenceHeadID = dto.TransactionHead.ReferenceHeadID;
                dto.TransactionHead.SupplierID = CallContext.SupplierID;

                var saveTran = new TransactionDTO();

                if (dto.TransactionHead.HeadIID == 0)
                {
                    saveTran = ClientFactory.TransactionServiceClient(CallContext).SaveTransactions(dto);
                    headIID = saveTran.TransactionHead.HeadIID;
                }
                else
                {
                    var resultID = new TransactionBL(CallContext).UpdateQuotation(dto);
                    headIID = resultID;
                }

                if (headIID != 0)
                {
                    result = new SupplierBL(CallContext).GetRFQItemListByIID(headIID);
                    result.IsError = false;
                    result.ReturnMessage = "Saved Successfully";

                    new TransactionBL(CallContext).MailQTSubmission(headIID);
                }
            }
            catch (Exception ex)
            {
                result.IsError = true;
                result.ReturnMessage = ex.Message; // Store the exception message
            }

            return Json(result);
        }


        public JsonResult GetSupplierData()
        {
            var result = new SupplierBL(CallContext).GetSupplier((long)CallContext.SupplierID);

            return Json(result);
        }

        [HttpPost]
        public JsonResult SaveSupplierData([FromBody] SupplierDTO supplierDto)
        {
            var result = new SupplierBL(CallContext).SupplierToEntity(supplierDto);

            return Json(result);
        }
    }
}

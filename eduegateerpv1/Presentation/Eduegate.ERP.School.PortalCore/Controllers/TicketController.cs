using Eduegate.Application.Mvc;
using Eduegate.Domain;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Infrastructure.Enums;
using Eduegate.Services.Client.Factory;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Framework;
using Eduegate.Services.Contracts.Supports;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Web.Library.ViewModels.Supports;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Eduegate.ERP.School.ParentPortal.Controllers
{
    public class TicketController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllTickets()
        {
            var loginID = CallContext?.LoginID;
            var ticketList = ClientFactory.SupportServiceClient(CallContext).GetAllTicketsByLoginID(loginID);

            if (ticketList.Count == 0)
            {
                return Json(new { IsError = true, Response = "There are some issue !" });
            }
            else
            {
                return Json(new { IsError = false, Response = ticketList });
            }
        }

        [HttpPost]
        public ActionResult SaveTicketCommunication([FromBody] TicketCommunicationDTO ticketCommunicationDTO)
        {
            var result = ClientFactory.SupportServiceClient(CallContext).SaveTicketCommunication(ticketCommunicationDTO);

            if (result.operationResult == OperationResult.Error)
            {
                return Json(new { IsError = true, Response = "There are some issue !" });
            }
            else
            {
                return Json(new { IsError = false, Response = result.Message });
            }
        }

        [HttpPost]
        public ActionResult GenerateTicket([FromBody] TicketingViewModel ticket)
        {
            var currentDate = DateTime.Now;
            var dateFormat = new Domain.Setting.SettingBL(CallContext).GetSettingValue<string>("DateFormat");

            var highPriorityID = new Domain.Setting.SettingBL(CallContext).GetSettingValue<string>("TICKET_PRIORITY_ID_HIGH", 1);
            var feeActionID = new Domain.Setting.SettingBL(CallContext).GetSettingValue<string>("TICKET_ACTION_ID_FEE", 1);
            var newStatusID = new Domain.Setting.SettingBL(CallContext).GetSettingValue<string>("TICKET_STATUS_ID_NEW", 1);

            byte? referenceTypeID = null;

            switch (ticket.DocumentTypeID)
            {
                case (byte)TicketDocumentTypes.CustomerSupport:
                    referenceTypeID = (byte)TicketReferenceTypes.CustomerSupport;
                    break;
                case (byte)TicketDocumentTypes.FeeOutStandingSupport:
                    referenceTypeID = (byte)TicketReferenceTypes.FeeDue;
                    break;
                case (byte)TicketDocumentTypes.FeeCollectionSupport:
                    referenceTypeID = (byte)TicketReferenceTypes.FeeCollection;
                    break;
                case (byte)TicketDocumentTypes.ComplaintsSupport:
                    referenceTypeID = (byte)TicketReferenceTypes.Complaint;
                    break;
                case (byte)TicketDocumentTypes.EnquirySupport:
                    referenceTypeID = (byte)TicketReferenceTypes.Enquiry;
                    break;
                default:
                    referenceTypeID = null;
                    break;
            }

            var parentDet = new AccountBL(CallContext).GetParentDetailsByLoginID(CallContext.LoginID);

            var ticketVM = new TicketingViewModel()
            {
                TicketIID = 0,
                DocumentType = ticket.DocumentTypeID.ToString(),
                Subject = ticket.Subject,
                Description1 = ticket.Description1,
                LoginID = CallContext?.LoginID,
                Priority = highPriorityID,
                Action = feeActionID,
                Status = newStatusID,
                DueDateFromString = currentDate.Date.ToString(dateFormat),
                ReferenceTypeID = referenceTypeID,
                IsSendMail = true,
                Parent = parentDet != null ? new KeyValueViewModel()
                {
                    Key = parentDet.ParentIID.ToString(),
                    Value = parentDet.ParentCode + " - " + parentDet.FatherFirstName + " " + (parentDet.FatherMiddleName != null ? parentDet.FatherMiddleName + " " : "") + parentDet.FatherLastName,
                } : new KeyValueViewModel(),
            };

            var crudSave = ClientFactory.FrameworkServiceClient(CallContext).SaveCRUDData(new CRUDDataDTO() { ScreenID = (long)Screens.Ticket, Data = ticketVM.AsDTOString(ticketVM.ToDTO(CallContext)) });

            if (crudSave.IsError == true)
            {
                return Json(new { IsError = true, Response = crudSave.ErrorMessage });
            }
            else
            {
                return Json(new { IsError = false, Response = crudSave.ErrorMessage });
            }
        }

    }
}
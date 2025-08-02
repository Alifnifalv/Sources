using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eduegate.Domain.Mappers;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Services.Contracts.Notifications;
using Eduegate.Services.Contracts.Supports;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Domain.Entity.Supports.Models;
using Eduegate.Domain.Mappers.School.Students;
using Eduegate.Services.Contracts.School.Students;
using System.Globalization;
using Eduegate.Domain.Mappers.School.Fees;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Domain.Mappers.CustomerSupport;
using Eduegate.Domain.Report;
using Eduegate.Domain.Setting;

namespace Eduegate.Domain.Support
{
    public class SupportBL
    {
        private CallContext _callContext;
        private static SupportRepository supportRepository = new SupportRepository();

        public SupportBL(CallContext context)
        {
            _callContext = context;
        }

        public TicketDTO GetTicket(int ticketID)
        {
            return TicketMapper.Mapper(_callContext).ToDTO(new SupportRepository().GetTicket(ticketID));
        }

        public TicketDTO SaveTicket(TicketDTO ticketDTO)
        {
            var dbTicket = new SupportRepository().GetTicket(ticketDTO.TicketIID);

            Ticket ticket = new SupportRepository().SaveTicket(TicketMapper.Mapper(_callContext).ToEntity(ticketDTO));

            // Add Ticket No# as comment to selected order
            if (ticketDTO.TicketIID == default(long) || (dbTicket.IsNotNull() && ticket.TicketStatusID != dbTicket.TicketStatusID))
            {
                var comment = new CommentDTO();
                comment.CommentText = string.Concat("Ticket No# ", ticket.TicketNo, " | Ticket Status: ", ticketDTO.TicketStatus);
                comment.EntityType = EntityTypes.Transaction;
                comment.ReferenceID = Convert.ToInt64(ticket.HeadID);
                comment = new MutualBL(_callContext).SaveComment(comment);

                // Add Email notificatio for customer/product manager
                AddEmailNotification(ticket);
            }

            // change Transaction status as per action selected
            if (ticketDTO.TicketIID == default(long) || (dbTicket.IsNotNull() && (Eduegate.Framework.Enums.SupportActions)dbTicket.ActionID != (Eduegate.Framework.Enums.SupportActions)ticketDTO.ActionID))
            {
                ActionProcessing(ticket);
            }
            Entity.Models.DocumentType entity = new MutualBL(_callContext).UpdateLastTransactionNo(Convert.ToInt32(ticket.DocumentTypeID), ticket.TicketNo);

            return TicketMapper.Mapper(_callContext).ToDTO(ticket);
        }

        public bool AddCustomerSupportTicket(CustomerSupportTicketDTO ticketDTO)
        {
            //var cultureID = new UtilityBL().GetLanguageCultureId(_callContext.LanguageCode).CultureID;
            //ticketDTO.CultureID = cultureID;
            var ticket = new TicketDTO();
            ticket.Subject = ticketDTO.Name + " Telephone : " + ticketDTO.Telephone + " Mail: " + ticketDTO.EmailID;
            ticket.Description = ticketDTO.Comments;
            ticket.CreatedBy = _callContext.LoginID.IsNotNull() ? Convert.ToInt32(_callContext.LoginID) : (int?)null;
            var documenttype = new SettingRepository().GetSettingDetail("TICKETDCMNTTYPEID", _callContext.CompanyID ?? 1);
            if (_callContext.LoginID.IsNotNull()) //? new AccountRepository().GetCustomerIDbyLoginID(long.Parse(_callContext.LoginID.ToString())) : null;
            {
                var Customers = new AccountRepository().GetCustomerIDbyLoginID(_callContext.LoginID.HasValue ? (long)_callContext.LoginID : 0);
                ticket.CustomerID = Customers.IsNotNull() ? Customers : (long?)null;
            }
            ticket.DocumentTypeID = int.Parse(documenttype.SettingValue);
            //ticket.CustomerID = Customer.IsNotNull() ? Customer : (long?)null;
            ticket.TicketStatusID = (int)Eduegate.Services.Contracts.Enums.TicketStatuses.Open;
            ticket.ActionID = (int)Eduegate.Services.Contracts.Enums.TicketActions.Refund;
            var lastticketnumber = new MutualBL(_callContext).GetNextTransactionNumber(Convert.ToInt64(ticket.DocumentTypeID));
            Entity.Models.DocumentType entity = new MutualBL(_callContext).UpdateLastTransactionNo(Convert.ToInt32(ticket.DocumentTypeID), lastticketnumber);
            ticket.TicketNo = lastticketnumber;
            new SupportRepository().SaveTicket(TicketMapper.Mapper(_callContext).ToEntity(ticket));
            new SupportRepository().AddCustomerSupportTicket(CustomerSupportTicketMapper.Mapper(_callContext).ToEntity(ticketDTO));
            return true;
            //if(entity != null)
            //{
            //    return null;
            //}
            //return ticketDTO;
        }

        public bool JustAskInsert(JustAskDTO justAskDTO)
        {
            try
            {
                //var cultureID = new UtilityBL().GetLanguageCultureId(_callContext.LanguageCode).CultureID;
                //justAskDTO.CultureID = cultureID;
                var ticket = new TicketDTO();
                ticket.Subject = justAskDTO.Name + " Telephone : " + justAskDTO.Telephone + " Mail: " + justAskDTO.EmailID;
                ticket.Description = justAskDTO.Description;
                ticket.CreatedBy = _callContext.LoginID.IsNotNull() ? Convert.ToInt32(_callContext.LoginID) : (int?)null;
                var documenttype = new SettingRepository().GetSettingDetail("TICKETDCMNTTYPEID", _callContext.CompanyID ?? 1);
                if (_callContext.LoginID.IsNotNull()) //? new AccountRepository().GetCustomerIDbyLoginID(long.Parse(_callContext.LoginID.ToString())) : null;
                {
                    var Customers = new AccountRepository().GetCustomerIDbyLoginID(_callContext.LoginID.HasValue ? (long)_callContext.LoginID : 0);
                    ticket.CustomerID = Customers.IsNotNull() ? Customers : (long?)null;
                }
                ticket.DocumentTypeID = int.Parse(documenttype.SettingValue);
                ticket.TicketStatusID = (int)Eduegate.Services.Contracts.Enums.TicketStatuses.Open;
                ticket.ActionID = (int)Eduegate.Services.Contracts.Enums.TicketActions.Refund;
                var lastticketnumber = new MutualBL(_callContext).GetNextTransactionNumber(Convert.ToInt32(ticket.DocumentTypeID));
                Entity.Models.DocumentType entity = new MutualBL(_callContext).UpdateLastTransactionNo(Convert.ToInt32(ticket.DocumentTypeID), lastticketnumber);
                ticket.TicketNo = lastticketnumber;
                new SupportRepository().SaveTicket(TicketMapper.Mapper(_callContext).ToEntity(ticket));
                new SupportRepository().JustAskInsert(JustAskMapper.Mapper(_callContext).ToEntity(justAskDTO));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool JobInsert(UserJobApplicationDTO dto)
        {
            //var cultureID = new UtilityBL().GetLanguageCultureId(_callContext.LanguageCode).CultureID;
            //dto.CultureID = cultureID;
            return new SupportRepository().JobInsert(UserJobApplicationMapper.Mapper(_callContext).ToEntity(dto));
        }

        public KeyValueDTO GetTicketStatusByStatusID(int statusID)
        {
            //var statuses = new ReferenceDataBL(_callContext).GetLookUpData(Services.Contracts.Enums.LookUpTypes.TicketStatus);
            var status = new SupportRepository().GetTicketStatuses().Where(s => s.TicketStatusID == statusID).FirstOrDefault();
            return new KeyValueDTO() { Key = status?.TicketStatusID.ToString(), Value = status?.StatusName };
        }

        public KeyValueDTO GetTicketActionByActionID(int actionID)
        {
            //var statuses = new ReferenceDataBL(_callContext).GetLookUpData(Services.Contracts.Enums.LookUpTypes.TicketStatus);
            var action = new SupportRepository().GetTicketActions().Where(a => a.SupportActionID == actionID).FirstOrDefault();
            return new KeyValueDTO() { Key = action?.SupportActionID.ToString(), Value = action?.ActionName };
        }


        #region Private Methods
        private void ActionProcessing(Ticket ticket)
        {
            // Update related transaction based on selected ticket action
            switch ((Eduegate.Framework.Enums.SupportActions)ticket.ActionID)
            {
                //case Framework.Enums.SupportActions.Distrubutions:
                //    break;
                //case Framework.Enums.SupportActions.RMA:
                //    break;
                //case Framework.Enums.SupportActions.SC:
                //    break;
                //case Framework.Enums.SupportActions.Warehouse:
                //    break;
                //case Framework.Enums.SupportActions.Showroom:
                //    break;
                //case Framework.Enums.SupportActions.Purchase:
                //    break;
                //case Framework.Enums.SupportActions.Accounts:
                //    break;
                case Framework.Enums.SupportActions.Refund:
                    if (ticket.HeadID.IsNotNull())
                    {
                        // Update Head to InitiateReprocess and Cancelled
                        new TransactionBL(_callContext).UpdateTransactionHead(new Services.Contracts.Catalog.TransactionHeadDTO()
                        {
                            HeadIID = Convert.ToInt64(ticket.HeadID),
                            TransactionStatusID = (byte)Services.Contracts.Enums.TransactionStatus.IntitiateReprecess,
                            DocumentStatusID = (short)Services.Contracts.Enums.DocumentStatuses.Cancelled
                        });
                    }
                    break;
                //case Framework.Enums.SupportActions.DigitalProblem:
                //    break;
                default:
                    break;
            }
        }

        private void AddEmailNotification(Ticket ticket)
        {

            /*
                1. Add Email notification to Customer + Product manager
                2. Send email to notify selection if ticket-action is arrangement
             
             */

            // Step 1
            var notificationDTO = new EmailNotificationDTO();
            notificationDTO.EmailNotificationType = Eduegate.Services.Contracts.Enums.EmailNotificationTypes.SupportTicketAlert;
            notificationDTO.AdditionalParameters = new List<Eduegate.Services.Contracts.Commons.KeyValueParameterDTO>();
            notificationDTO.FromEmailID = "customer.support@blink.com.kw"; // new Domain.Setting.SettingBL().GetSettingValue<string>("EmailFrom").ToString(); // can I add setting entry for this

            notificationDTO.AdditionalParameters.Add(new Eduegate.Services.Contracts.Commons.KeyValueParameterDTO() { ParameterName = Eduegate.Services.Contracts.Constants.EmailNotificationType.SupportTicketNotification.Keys.OrderID, ParameterValue = ticket.HeadID.ToString() });

            notificationDTO.AdditionalParameters.Add(new Eduegate.Services.Contracts.Commons.KeyValueParameterDTO() { ParameterName = Eduegate.Services.Contracts.Constants.EmailNotificationType.SupportTicketNotification.Keys.TicketID, ParameterValue = ticket.TicketIID.ToString() });

            var customerDetail = new AccountBL(_callContext).GetUserDetailsByCustomerID(Convert.ToInt64(ticket.CustomerID), false);
            //var managerDetail = new EmployeeBL(_callContext).GetEmployee(Convert.ToInt64(ticket.ManagerEmployeeID));

            // Send Customer Notification
            if (Convert.ToBoolean(ticket.CustomerNotification))
            {
                notificationDTO.ToEmailID = customerDetail.LoginEmailID;
                // Adding CC and BCC to additional pararms as we do not have fild in NotificationEmailData
                //notificationDTO.AdditionalParameters.Add(new Eduegate.Services.Contracts.Commons.KeyValueParameterDTO() { ParameterName = Eduegate.Services.Contracts.Constants.EmailNotificationType.SupportTicketNotification.Keys.ToBCCEmailID, ParameterValue = managerDetail.Login.LoginEmailID });
            }
            else
            {
                //notificationDTO.ToEmailID = managerDetail.Login.LoginEmailID;
            }


            string websiteUrl = string.Empty;
            var domainsetting = new Domain.Setting.SettingBL().GetSettingDetail("DOMAINNAME");

            if (domainsetting.IsNotNull())
            {
                websiteUrl = domainsetting.SettingValue + ": ";
            }

            notificationDTO.Subject = string.Concat(websiteUrl, "Ticket Status: ", ticket.TicketStatus.StatusName, " | Ticket No# ", ticket.TicketNo);
            var notificationReponse = Task<EmailNotificationDTO>.Factory.StartNew(() => new NotificationBL(_callContext).SaveEmailData(notificationDTO));


            if ((Services.Contracts.Enums.TicketActions)Enum.Parse(typeof(Services.Contracts.Enums.TicketActions), ticket.ActionID.ToString()) == TicketActions.Arrangement)
            {
                // Step 2
                var notifyDTO = new EmailNotificationDTO();
                notifyDTO.EmailNotificationType = Eduegate.Services.Contracts.Enums.EmailNotificationTypes.SupportTicketAlert;
                notifyDTO.AdditionalParameters = new List<Eduegate.Services.Contracts.Commons.KeyValueParameterDTO>();
                notifyDTO.AdditionalParameters.Add(new Eduegate.Services.Contracts.Commons.KeyValueParameterDTO() { ParameterName = Eduegate.Services.Contracts.Constants.EmailNotificationType.SupportTicketNotification.Keys.OrderID, ParameterValue = ticket.HeadID.ToString() });

                notifyDTO.AdditionalParameters.Add(new Eduegate.Services.Contracts.Commons.KeyValueParameterDTO() { ParameterName = Eduegate.Services.Contracts.Constants.EmailNotificationType.SupportTicketNotification.Keys.TicketID, ParameterValue = ticket.TicketIID.ToString() });

                if (ticket.TicketActionDetailMaps.IsNotNull() && ticket.TicketActionDetailMaps.Count > 0 && ticket.TicketActionDetailMaps.First().TicketActionDetailDetailMaps.IsNotNull() && ticket.TicketActionDetailMaps.First().TicketActionDetailDetailMaps.Count > 0)
                {
                    foreach (var actionDetailDetailMap in ticket.TicketActionDetailMaps.First().TicketActionDetailDetailMaps)
                    {
                        switch ((Services.Contracts.Enums.TicketActions)Enum.Parse(typeof(Services.Contracts.Enums.TicketActions), ticket.ActionID.ToString()))
                        {

                            case TicketActions.ArrangmentPM:
                                // Send mail to Product Manager
                                //notificationDTO.AdditionalParameters.Add(new Eduegate.Services.Contracts.Commons.KeyValueParameterDTO() { ParameterName = Eduegate.Services.Contracts.Constants.EmailNotificationType.SupportTicketNotification.Keys.ToBCCEmailID, ParameterValue = managerDetail.Login.LoginEmailID });

                                break;
                            case TicketActions.ArrangementPurchase:
                                // to purchase department
                                break;
                            case TicketActions.ArrangementWarehouse:
                                // To warehouse
                                break;
                            case TicketActions.ArrangementDistribution:
                                // To ditribution department
                                break;
                            default:
                                break;
                        }
                    }
                }


                notifyDTO.Subject = string.Concat(websiteUrl, "Ticket Status: ", ticket.TicketStatus.StatusName, " | Ticket No# ", ticket.TicketNo, " | ", ((Services.Contracts.Enums.TicketActions)ticket.ActionID).ToString());
                var notifyReponse = Task<EmailNotificationDTO>.Factory.StartNew(() => new NotificationBL(_callContext).SaveEmailData(notifyDTO));
            }

            // To test above, uncomment like below and test in sync mode
            //return notificationReponse.ToString();
            //var result  = new NotificationBL(null).SaveEmailData(notificationDTO);
        }
        #endregion

        #region Case Management related
        public OperationResultDTO SendFeeDueMailReportToParent(long? studentID, string reportName)
        {
            var returnData = new OperationResultDTO();

            var reportDateFormat = new SettingBL(null).GetSettingValue<string>("ReportDateFormat", "dd/MM/yyyy");
            var currentDate = DateTime.Now.Date;

            try
            {
                var studentData = StudentMapper.Mapper(_callContext).GetStudentDetailsByStudentID(studentID.Value);

                var gridData = new MailFeeDueStatementReportDTO()
                {
                    StudentID = studentID,
                    ClassID = studentData.ClassID,
                    Class = studentData.ClassName,
                    AdmissionNo = studentData.AdmissionNumber,
                    StudentName = studentData.FirstName + " " + (string.IsNullOrEmpty(studentData.MiddleName) ? "" : studentData.MiddleName + " ") + studentData.LastName,
                    ParentEmailID = studentData.Guardian?.GaurdianEmail,
                    ParentLoginID = studentData.Guardian?.LoginID,
                    SchoolID = studentData.SchoolID,
                    SchoolName = studentData.SchoolName,
                    AcademicYearID = studentData.AcademicYearID,
                    AsOnDate = currentDate.ToString(reportDateFormat, CultureInfo.InvariantCulture),
                    ReportName = reportName,
                };

                var dataPass = MailFeeDueStatementMapper.Mapper(_callContext).SendFeeDueMailReportToParent(gridData);

                new ReportGenerationBL(_callContext).SendFeeDueMailReportToParent(gridData);

                returnData = new OperationResultDTO()
                {
                    operationResult = OperationResult.Success,
                    Message = "Fee due statement Successfully sent!"
                };
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception")
                    ? ex.InnerException?.Message : ex.Message;

                Eduegate.Logger.LogHelper<string>.Fatal($"Fee due statement Sending failed. Error message: {errorMessage}", ex);

                returnData = new OperationResultDTO()
                {
                    operationResult = OperationResult.Error,
                    Message = errorMessage
                };
            }

            return returnData;
        }

        public OperationResultDTO SendProformaInvoiceToParent(long? studentID, string reportName)
        {
            var returnData = new OperationResultDTO();

            try
            {
                var studentData = StudentMapper.Mapper(_callContext).GetStudentDetailsByStudentID(studentID.Value);

                var reportDTO = new MailFeeDueStatementReportDTO()
                {
                    StudentID = studentID,
                    ClassID = studentData.ClassID,
                    Class = studentData.ClassName,
                    AdmissionNo = studentData.AdmissionNumber,
                    StudentName = studentData.FirstName + " " + (string.IsNullOrEmpty(studentData.MiddleName) ? "" : studentData.MiddleName + " ") + studentData.LastName,
                    ParentEmailID = studentData.Guardian?.GaurdianEmail,
                    ParentLoginID = studentData.Guardian?.LoginID,
                    SchoolID = studentData.SchoolID,
                    SchoolName = studentData.SchoolName,
                    AcademicYearID = studentData.AcademicYearID,
                    ReportName = reportName,
                };

                new ReportGenerationBL(_callContext).SendProformaInvoiceToParent(reportDTO);

                returnData = new OperationResultDTO()
                {
                    operationResult = OperationResult.Success,
                    Message = "Proforma invoice successfully sent!"
                };
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception")
                    ? ex.InnerException?.Message : ex.Message;

                Eduegate.Logger.LogHelper<string>.Fatal($"Proforma invoice sending failed. Error message: {errorMessage}", ex);

                returnData = new OperationResultDTO()
                {
                    operationResult = OperationResult.Error,
                    Message = errorMessage
                };
            }

            return returnData;
        }

        #endregion


        public List<TicketDTO> GetAllTicketsByLoginID(long? loginID)
        {
            var result = TicketingMapper.Mapper(_callContext).GetAllTicketsByLoginID(loginID);

            return result;
        }

    }
}
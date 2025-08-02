using Eduegate.Domain.Entity.Supports;
using Eduegate.Domain.Entity.Supports.Models;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Extensions;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.Supports;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Linq;
using Eduegate.Services.Contracts.Enums;
using System.Collections.Generic;
using Eduegate.Domain.Entity;
using System.Globalization;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Domain.Mappers.Notification;
using Eduegate.Services.Contracts.Enums.Notifications;
using Eduegate.Globalization;
using System.Threading.Tasks;

namespace Eduegate.Domain.Mappers.CustomerSupport
{
    public class TicketingMapper : DTOEntityDynamicMapper
    {
        public static TicketingMapper Mapper(CallContext context)
        {
            var mapper = new TicketingMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<TicketDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private TicketDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSupportContext())
            {
                var entity = dbContext.Tickets.Where(x => x.TicketIID == IID)
                    .Include(i => i.Customer)
                    .Include(i => i.DocumentType)
                    .Include(i => i.Action)
                    .Include(i => i.ManagerEmployee)
                    .Include(i => i.TicketStatus)
                    .Include(i => i.TicketProcessingStatus)
                    .Include(i => i.AssingedEmployee)
                    .Include(i => i.Supplier)
                    .Include(i => i.Employee)
                    .Include(i => i.Login)
                    .Include(i => i.TicketCommunications).ThenInclude(i => i.Login)
                    .Include(i => i.TicketFeeDueMaps).ThenInclude(i => i.StudentFeeDue).ThenInclude(i => i.FeeDueFeeTypeMaps).ThenInclude(i => i.FeeMaster)
                    .AsNoTracking()
                    .FirstOrDefault();

                return ToDTO(entity);
            }
        }

        private TicketDTO ToDTO(Ticket entity)
        {
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

            var ticketDTO = new TicketDTO()
            {
                TicketIID = entity.TicketIID,
                TicketNo = entity.TicketNo,
                DocumentTypeID = entity.DocumentTypeID,
                DocumentTypeName = entity.DocumentTypeID.HasValue ? entity.DocumentType?.TransactionTypeName : null,
                Subject = entity.Subject,
                Description = entity.Description,
                Description2 = entity.Description2,
                Source = entity.Source,
                PriorityID = entity.PriorityID,
                ActionID = entity.ActionID,
                TicketStatusID = entity.TicketStatusID,
                TicketStatus = entity.TicketStatusID.HasValue ? entity.TicketStatus?.StatusName : null,
                ManagerEmployeeID = entity.ManagerEmployeeID,
                ManagerEmployee = entity.ManagerEmployeeID.HasValue ? entity.ManagerEmployee?.EmployeeCode + " - " + entity.ManagerEmployee?.FirstName + entity.ManagerEmployee?.MiddleName + entity.ManagerEmployee?.LastName : null,
                CustomerID = entity.CustomerID,
                CustomerName = entity.CustomerID.HasValue ? entity.Customer?.CustomerCode + " - " + entity.Customer?.FirstName + entity.Customer?.MiddleName + entity.Customer?.LastName : null,
                SupplierID = entity.SupplierID,
                EmployeeID = entity.EmployeeID,
                DueDateFrom = entity.DueDateFrom,
                DueDateTo = entity.DueDateTo,
                FromDueDateString = entity.DueDateFrom.HasValue ? entity.DueDateFrom.Value.ToString(dateFormat) : null,
                ToDueDateString = entity.DueDateTo.HasValue ? entity.DueDateTo.Value.ToString(dateFormat) : null,
                HeadID = entity.HeadID,
                CustomerNotification = entity.CustomerNotification,
                CompanyID = entity.CompanyID.HasValue ? (int)entity.CompanyID : 1,
                LoginID = entity.LoginID,
                ReferenceID = entity.ReferenceID,
                ReferenceTypeID = entity.ReferenceTypeID,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedBy = entity.UpdatedBy,
                UpdatedDate = entity.UpdatedDate,
            };

            GetParentDetails(ticketDTO, entity);
            FillCommunicationDetails(ticketDTO, entity);
            FillFeeDueDetails(ticketDTO, entity);
            GetDepartmentByDocumentType(ticketDTO, entity);

            return ticketDTO;
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as TicketDTO;

            var entity = SaveTicket(toDto);

            if (entity != null && entity.TicketIID > 0)
            {
                if (toDto.IsSendMail == true)
                {
                    GenerateAndSendMail(entity, toDto);
                }
            }

            return ToDTOString(ToDTO(entity.TicketIID));
        }

        public Ticket SaveTicket(TicketDTO toDto)
        {
            if (toDto.TicketIID == 0)
            {
                toDto.TicketNo = GetNextTransactionNumber(toDto.DocumentTypeID.Value);
            }

            if (!toDto.LoginID.HasValue)
            {
                toDto.LoginID = GetLoginDetailsByParentID(toDto.ParentID);
            }

            using (var dbContext = new dbEduegateSupportContext())
            {
                var entity = new Ticket()
                {
                    TicketIID = toDto.TicketIID,
                    TicketNo = toDto.TicketNo,
                    DocumentTypeID = toDto.DocumentTypeID,
                    Subject = toDto.Subject,
                    Description = toDto.Description,
                    Description2 = toDto.Description2,
                    Source = toDto.Source,
                    PriorityID = toDto.PriorityID,
                    ActionID = toDto.ActionID,
                    TicketStatusID = toDto.TicketStatusID,
                    ManagerEmployeeID = toDto.ManagerEmployeeID,
                    CustomerID = toDto.CustomerID,
                    SupplierID = toDto.SupplierID,
                    EmployeeID = toDto.EmployeeID,
                    DueDateFrom = toDto.DueDateFrom,
                    DueDateTo = toDto.DueDateTo,
                    HeadID = toDto.HeadID,
                    CustomerNotification = toDto.IsSendMail == true ? toDto.IsSendMail : toDto.CustomerNotification,
                    LoginID = toDto.LoginID,
                    ReferenceID = toDto.ReferenceID,
                    ReferenceTypeID = toDto.ReferenceTypeID,
                    CompanyID = (int)_context.CompanyID,
                    CreatedBy = toDto.TicketIID > 0 ? toDto.CreatedBy : _context.LoginID.HasValue ? (int)_context.LoginID : (int?)null,
                    CreatedDate = toDto.TicketIID > 0 ? toDto.CreatedDate : DateTime.Now,
                    UpdatedBy = toDto.TicketIID > 0 ? (int)_context.LoginID : (int?)null,
                    UpdatedDate = toDto.TicketIID > 0 ? DateTime.Now : (DateTime?)null,
                };

                entity.TicketCommunications = new List<TicketCommunication>();

                foreach (var communication in toDto.TicketCommunications)
                {
                    entity.TicketCommunications.Add(new TicketCommunication()
                    {
                        TicketCommunicationIID = communication.TicketCommunicationIID,
                        TicketID = communication.TicketID,
                        LoginID = communication.LoginID.HasValue ? communication.LoginID : _context.LoginID.HasValue ? _context.LoginID : (long?)null,
                        Notes = communication.Notes,
                        CommunicationDate = communication.TicketCommunicationIID > 0 ? communication.CommunicationDate : DateTime.Now.Date,
                        FollowUpDate = communication.FollowUpDate,
                        CreatedBy = communication.TicketCommunicationIID > 0 ? communication.CreatedBy : _context.LoginID.HasValue ? (int)_context.LoginID : (int?)null,
                        CreatedDate = communication.TicketCommunicationIID > 0 ? communication.CreatedDate : DateTime.Now,
                        UpdatedBy = communication.TicketCommunicationIID > 0 ? (int)_context.LoginID : (int?)null,
                        UpdatedDate = communication.TicketCommunicationIID > 0 ? DateTime.Now : (DateTime?)null,
                    });
                }

                entity.TicketFeeDueMaps = new List<TicketFeeDueMap>();

                foreach (var feeDueMap in toDto.TicketFeeDueMapDTOs)
                {
                    entity.TicketFeeDueMaps.Add(new TicketFeeDueMap()
                    {
                        TicketFeeDueMapIID = feeDueMap.TicketFeeDueMapIID,
                        TicketID = feeDueMap.TicketID,
                        StudentFeeDueID = feeDueMap.StudentFeeDueID,
                        FeeDueAmount = feeDueMap.DueAmount,
                        CreatedBy = feeDueMap.TicketFeeDueMapIID > 0 ? feeDueMap.CreatedBy : _context.LoginID.HasValue ? (int)_context.LoginID : (int?)null,
                        CreatedDate = feeDueMap.TicketFeeDueMapIID > 0 ? feeDueMap.CreatedDate : DateTime.Now,
                        UpdatedBy = feeDueMap.TicketFeeDueMapIID > 0 ? (int)_context.LoginID : (int?)null,
                        UpdatedDate = feeDueMap.TicketFeeDueMapIID > 0 ? DateTime.Now : (DateTime?)null,
                    });
                }

                dbContext.Tickets.Add(entity);

                if (entity.TicketIID == 0)
                {
                    dbContext.Entry(entity).State = EntityState.Added;
                }
                else
                {
                    foreach (var communicationDetail in entity.TicketCommunications)
                    {
                        if (communicationDetail.TicketCommunicationIID == 0)
                        {
                            dbContext.Entry(communicationDetail).State = EntityState.Added;
                        }
                        else
                        {
                            dbContext.Entry(communicationDetail).State = EntityState.Modified;
                        }
                    }

                    foreach (var feeDueMapDetail in entity.TicketFeeDueMaps)
                    {
                        if (feeDueMapDetail.TicketFeeDueMapIID == 0)
                        {
                            dbContext.Entry(feeDueMapDetail).State = EntityState.Added;
                        }
                        else
                        {
                            dbContext.Entry(feeDueMapDetail).State = EntityState.Modified;
                        }
                    }

                    dbContext.Entry(entity).State = EntityState.Modified;
                }
                dbContext.SaveChanges();

                return entity;
            }
        }

        public string GetNextTransactionNumber(int documentTypeID)
        {
            string nextTransactionNumber = string.Empty;

            var documentTypeData = new MetadataRepository().GetDocumentType(documentTypeID);

            if (documentTypeData.IsNotNull())
            {
                nextTransactionNumber = documentTypeData.TransactionNoPrefix;
                nextTransactionNumber += documentTypeData.LastTransactionNo.IsNull() ? "1" : Convert.ToString(documentTypeData.LastTransactionNo + 1);

                documentTypeData.LastTransactionNo = documentTypeData.LastTransactionNo.IsNull() ? 1 : documentTypeData.LastTransactionNo + 1;

                new MetadataRepository().SaveDocumentType(documentTypeData);

                return nextTransactionNumber;
            }
            else return nextTransactionNumber;
        }

        public TicketDTO GenerateTicketByReference(TicketDTO ticketDTO)
        {
            ticketDTO.DueDateFrom = DateTime.Now.Date;

            var entity = SaveTicket(ticketDTO);

            ticketDTO.TicketIID = entity.TicketIID;
            ticketDTO.TicketNo = entity.TicketNo;

            return ticketDTO;
        }

        private void GetParentDetails(TicketDTO ticketDTO, Ticket entity)
        {
            if (entity.LoginID.HasValue)
            {
                using (var dbContext = new dbEduegateERPContext())
                {
                    var parentData = dbContext.Parents.Where(p => p.LoginID == entity.LoginID).FirstOrDefault();

                    ticketDTO.ParentID = parentData?.ParentIID;
                    ticketDTO.ParentName = parentData != null ? parentData.ParentCode + " - " + parentData.FatherFirstName + " " + (string.IsNullOrEmpty(parentData.FatherMiddleName) ? "" : parentData.FatherMiddleName + " ") + parentData.FatherLastName : null;
                }
            }
        }

        private void FillCommunicationDetails(TicketDTO ticketDTO, Ticket entity)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            ticketDTO.TicketCommunications = new List<TicketCommunicationDTO>();

            if (entity.TicketCommunications.Count > 0)
            {
                foreach (var communication in entity.TicketCommunications)
                {
                    var communicationUser = string.Empty;
                    if (communication.LoginID == _context.LoginID)
                    {
                        communicationUser = "Me";
                    }
                    else
                    {
                        communicationUser = "Admin";
                    }

                    ticketDTO.TicketCommunications.Add(new TicketCommunicationDTO()
                    {
                        TicketCommunicationIID = communication.TicketCommunicationIID,
                        TicketID = communication.TicketID,
                        LoginID = communication.LoginID,
                        LoginUserID = communication.LoginID.HasValue ? communication?.Login?.LoginUserID : null,
                        CommunicationUser = communicationUser,
                        Notes = communication.Notes,
                        CommunicationDate = communication.CommunicationDate,
                        CommunicationStringDate = communication.CommunicationDate.HasValue ? communication.CommunicationDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                        FollowUpDate = communication.FollowUpDate,
                        FollowUpStringDate = communication.FollowUpDate.HasValue ? communication.FollowUpDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                        CreatedBy = communication.CreatedBy,
                        CreatedDate = communication.CreatedDate,
                        UpdatedBy = communication.UpdatedBy,
                        UpdatedDate = communication.UpdatedDate,
                    });
                }
            }
        }

        private void FillFeeDueDetails(TicketDTO ticketDTO, Ticket entity)
        {
            ticketDTO.TicketFeeDueMapDTOs = new List<TicketFeeDueMapDTO>();

            if (entity.TicketFeeDueMaps.Count > 0)
            {
                foreach (var feeDueMap in entity.TicketFeeDueMaps)
                {
                    ticketDTO.TicketFeeDueMapDTOs.Add(new TicketFeeDueMapDTO()
                    {
                        TicketFeeDueMapIID = feeDueMap.TicketFeeDueMapIID,
                        TicketID = feeDueMap.TicketID,
                        StudentFeeDueID = feeDueMap.StudentFeeDueID,
                        InvoiceNo = feeDueMap.StudentFeeDue?.InvoiceNo,
                        InvoiceDate = feeDueMap.StudentFeeDue?.InvoiceDate,
                        FeeMasterID = feeDueMap.StudentFeeDue?.FeeDueFeeTypeMaps?.FirstOrDefault()?.FeeMasterID,
                        FeeMaster = feeDueMap.StudentFeeDue?.FeeDueFeeTypeMaps?.FirstOrDefault()?.FeeMaster?.Description,
                        DueAmount = feeDueMap.FeeDueAmount,
                        CreatedBy = feeDueMap.CreatedBy,
                        CreatedDate = feeDueMap.CreatedDate,
                        UpdatedBy = feeDueMap.UpdatedBy,
                        UpdatedDate = feeDueMap.UpdatedDate,
                    });
                }
            }
        }

        public long? GetLoginDetailsByParentID(long? parentID)
        {
            long? loginID = null;
            if (parentID.HasValue)
            {
                using (var dbContext = new dbEduegateERPContext())
                {
                    var parentData = dbContext.Parents.Where(p => p.ParentIID == parentID).AsNoTracking().FirstOrDefault();

                    loginID = parentData?.LoginID;
                }
            }

            return loginID;
        }

        public List<TicketDTO> GetAllTicketsByLoginID(long? loginID)
        {
            var ticketList = new List<TicketDTO>();
            if (loginID.HasValue)
            {
                using (var dbContext = new dbEduegateSupportContext())
                {
                    var tickets = dbContext.Tickets.Where(p => p.LoginID == loginID)
                        .Include(i => i.Customer)
                        .Include(i => i.DocumentType)
                        .Include(i => i.Action)
                        .Include(i => i.ManagerEmployee)
                        .Include(i => i.TicketStatus)
                        .Include(i => i.TicketProcessingStatus)
                        .Include(i => i.AssingedEmployee)
                        .Include(i => i.Supplier)
                        .Include(i => i.Employee)
                        .Include(i => i.Login)
                        .Include(i => i.TicketCommunications).ThenInclude(i => i.Login)
                        .OrderByDescending(o => o.TicketIID)
                        .AsNoTracking().ToList();

                    foreach (var ticket in tickets)
                    {
                        ticketList.Add(ToDTO(ticket));
                    }
                }
            }

            return ticketList;
        }

        #region Mail related code
        public void GenerateAndSendMail(Ticket ticketEntity, TicketDTO ticketDTO)
        {
            if (string.IsNullOrEmpty(ticketDTO.CustomerEmailID))
            {
                if (ticketDTO.ParentID.HasValue)
                {
                    using (var dbContext = new Eduegate.Domain.Entity.School.Models.School.dbEduegateSchoolContext())
                    {
                        ticketDTO.CustomerEmailID = dbContext.Parents
                                .Where(n => n.ParentIID == ticketDTO.ParentID.Value)
                                .AsNoTracking().FirstOrDefault()?.GaurdianEmail;
                    }
                }
                else if (ticketDTO.CustomerID.HasValue)
                {
                    using (var dbContext = new dbEduegateSupportContext())
                    {
                        ticketDTO.CustomerEmailID = dbContext.Customers
                                    .Where(n => n.CustomerIID == ticketDTO.CustomerID.Value)
                                    .AsNoTracking().FirstOrDefault()?.CustomerEmail;
                    }
                }
            }

            Email_Ticket(ticketDTO.CustomerEmailID, ticketEntity.Subject, ticketEntity.TicketNo);
        }

        public void Email_Ticket(string emailID, string emailSubject, string ticketNumber)
        {
            var settingBL = new Domain.Setting.SettingBL(_context);

            //string receiptBody = settingBL.GetSettingValue<string>("TICKETING_EMAILBODY_CONTENT");

            string parentPortal = settingBL.GetSettingValue<string>("CLIENT_PARENT_PORTAL");

            //string regardsName = settingBL.GetSettingValue<string>("CLIENT_DEFAULT_MAIL_REGARD_NAME");

            string hostDet = settingBL.GetSettingValue<string>("HOST_NAME");

            string defaultMail = settingBL.GetSettingValue<string>("DEFAULT_MAIL_ID");

            var emailTemplate = EmailTemplateMapper.Mapper(_context).GetEmailTemplateDetails(EmailTemplates.TicketingMail.ToString());

            var receiptBody = emailTemplate?.EmailTemplate;

            receiptBody = receiptBody.Replace("{REFERENCE_NUMBER}", ticketNumber);
            receiptBody = receiptBody.Replace("{PARENT_PORTAL}", parentPortal);
            //receiptBody = receiptBody.Replace("{MAIL_REGARDS}", regardsName);

            var emailBody = receiptBody;

            string mailMessage = new Eduegate.Domain.Notification.EmailNotificationBL(_context).PopulateBody(emailID, emailBody);
            var mailParameters = new Dictionary<string, string>();

            string ccMailValue = settingBL.GetSettingValue<string>("TICKETING_CC_MAILS", null);
            var ccMailIDs = new List<string>();
            var splitMails = ccMailValue != null ? ccMailValue.Split(",") : null;

            if (splitMails != null)
            {
                foreach (var mail in splitMails)
                {
                    ccMailIDs.Add(mail.Trim());
                }
            }

            if (emailBody != "")
            {
                if (hostDet.ToLower() == "live")
                {
                    new Eduegate.Domain.Notification.EmailNotificationBL(_context).SendMail(emailID, emailSubject, mailMessage, EmailTypes.Ticketing, mailParameters, ccMailIDs);
                }
                else
                {
                    new Eduegate.Domain.Notification.EmailNotificationBL(_context).SendMail(defaultMail, emailSubject, mailMessage, EmailTypes.Ticketing, mailParameters, ccMailIDs);
                }
            }
        }

        #endregion


        public OperationResultDTO SaveTicketCommunication(TicketCommunicationDTO toDTO)
        {
            var result = new OperationResultDTO();

            try
            {
                var communication = new TicketCommunication()
                {
                    TicketCommunicationIID = toDTO.TicketCommunicationIID,
                    TicketID = toDTO.TicketID,
                    LoginID = toDTO.LoginID.HasValue ? toDTO.LoginID : _context.LoginID.HasValue ? _context.LoginID : (long?)null,
                    Notes = toDTO.Notes,
                    CommunicationDate = toDTO.TicketCommunicationIID > 0 ? toDTO.CommunicationDate : DateTime.Now.Date,
                    FollowUpDate = toDTO.FollowUpDate,
                    CreatedBy = toDTO.TicketCommunicationIID > 0 ? toDTO.CreatedBy : _context.LoginID.HasValue ? (int)_context.LoginID : (int?)null,
                    CreatedDate = toDTO.TicketCommunicationIID > 0 ? toDTO.CreatedDate : DateTime.Now,
                    UpdatedBy = toDTO.TicketCommunicationIID > 0 ? (int)_context.LoginID : (int?)null,
                    UpdatedDate = toDTO.TicketCommunicationIID > 0 ? DateTime.Now : (DateTime?)null,
                };

                using (var dbContext = new dbEduegateSupportContext())
                {
                    if (communication.TicketCommunicationIID == 0)
                    {
                        dbContext.Entry(communication).State = EntityState.Added;
                    }
                    else
                    {
                        dbContext.Entry(communication).State = EntityState.Modified;
                    }
                    dbContext.SaveChanges();
                }

                result = new OperationResultDTO()
                {
                    operationResult = OperationResult.Success,
                    Message = "Saved successfully!"
                };
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception")
                    ? ex.InnerException?.Message : ex.Message;

                result = new OperationResultDTO()
                {
                    operationResult = OperationResult.Error,
                    Message = errorMessage
                };

                Eduegate.Logger.LogHelper<string>.Fatal($"Ticket communication saving failed. Error message: {errorMessage}", ex);
            }

            return result;
        }

        private void GetDepartmentByDocumentType(TicketDTO ticketDTO, Ticket entity)
        {
            using (var dbContext = new dbEduegateSupportContext())
            {
                var mapData = dbContext.DocumentDepartmentMaps.Where(p => p.DocumentTypeID == entity.DocumentTypeID)
                    .Include(i => i.Department)
                    .Include(i => i.DocumentType)
                    .AsNoTracking()
                    .FirstOrDefault();

                ticketDTO.DepartmentID = mapData?.DepartmentID;
                ticketDTO.DepartmentName = mapData?.Department?.DepartmentName;
            }
        }


        public OperationResultDTO CustomerFeedback(CustomerFeedbackDTO feedback)
        {
            var returnResult = new OperationResultDTO();

            if (string.IsNullOrEmpty(feedback.Message))
            {
                returnResult.operationResult = OperationResult.Error;
                returnResult.Message = ResourceHelper.GetValue("CustomerFeedbackMessageEmpty", _context.LanguageCode);
                return returnResult;
            }

            try
            {
                using (var dbContext = new Eduegate.Domain.Entity.School.Models.School.dbEduegateSchoolContext())
                {
                    var customerFeedback = new Eduegate.Domain.Entity.School.Models.CustomerFeedBacks()
                    {
                        CreatedBy = _context.LoginID.HasValue ? int.Parse(_context.LoginID.Value.ToString()) : (int?)null,
                        LoginID = _context.LoginID??(long?)null,
                        //CustomerFeedbackTypeID = feedback.FeedbackTypeID.HasValue ? feedback.FeedbackTypeID.Value : (byte)1,
                        //CustomerFeedbackTypeID = 1,
                        Message = feedback.Message,
                        CreatedDate = DateTime.Now
                    };

                    dbContext.CustomerFeedBacks.Add(customerFeedback);
                    dbContext.SaveChanges();
                }

                // Uncomment and adjust the Task.Run section if needed for email notifications
                /*
                Task.Run(() =>
                {
                    try
                    {
                        feedback.Message = string.Concat("Customer Email: ", _context.EmailID, "<br>",
                            "Customer Phone Number: ", _context.MobileNumber, "<br>",
                            "Complaint: ", feedback.Message);

                        var emailTo = new Domain.Setting.SettingBL().GetSettingValue<string>("EmailTo", _context.CompanyID.Value, "customersupport@adcs.ae");
                        var title = new Domain.Setting.SettingBL().GetSettingValue<string>("SupportEmailTitle", _context.CompanyID.Value, "Customer Feedback");
                        var fromEmailDisplayName = new Domain.Setting.SettingBL().GetSettingValue<string>("SupportEmailDisplayName", _context.CompanyID.Value, "Online Webstore");

                        SendEmailNotification(feedback.Message, title, emailTo, null, null, fromEmailDisplayName);
                    }
                    catch (Exception ex)
                    {
                        Logger.LogHelper<CMSBL>.Fatal("CustomerFeedback_TaskRun_" + ex.Message, ex);
                    }
                }).Forget();
                */

                returnResult.operationResult = OperationResult.Success;
                returnResult.Message = ResourceHelper.GetValue("CustomerFeedback", _context.LanguageCode);
            }
            catch (Exception ex)
            {
                Logger.LogHelper<string>.Fatal(ex.Message, ex);
                var errorMessage = ex.Message.Contains("inner") && ex.Message.Contains("exception") ? ex.InnerException?.Message : ex.Message;

                returnResult.operationResult = OperationResult.Error;
                returnResult.Message = errorMessage;
            }

            return returnResult;
        }

    }
}
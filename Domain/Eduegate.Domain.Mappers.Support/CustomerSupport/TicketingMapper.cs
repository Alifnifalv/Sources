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
using Eduegate.Services.Contracts.Enums.Notifications;
using Eduegate.Globalization;
using Eduegate.Domain.Setting;
using Eduegate.Domain.Mappers.Notification.Notifications;
using Eduegate.Domain.Mappers.Notification.Notifications.Helpers;
using Eduegate.Infrastructure.Enums;

namespace Eduegate.Domain.Mappers.Support.CustomerSupport
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
                    .Include(i => i.AssignedEmployee)
                    .Include(i => i.TicketStatus)
                    .Include(i => i.Supplier)
                    .Include(i => i.Employee)
                    .Include(i => i.Login)
                    .Include(i => i.Student)
                    .Include(i => i.SupportCategory)
                    .Include(i => i.SupportSubCategory)
                    .Include(i => i.Department)
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
                Description = !string.IsNullOrEmpty(entity.Description) ? entity.Description.Replace("\n", "<br/>") : entity.Description,
                Description2 = !string.IsNullOrEmpty(entity.Description2) ? entity.Description.Replace("\n", "<br/>") : entity.Description2,
                Source = entity.Source,
                PriorityID = entity.PriorityID,
                ActionID = entity.ActionID,
                TicketStatusID = entity.TicketStatusID,
                TicketStatus = entity.TicketStatusID.HasValue ? entity.TicketStatus?.StatusName : null,
                AssignedEmployeeID = entity.AssignedEmployeeID,
                AssignedEmployeeName = entity.AssignedEmployeeID.HasValue ? entity.AssignedEmployee?.EmployeeCode + " - " + entity.AssignedEmployee?.FirstName + entity.AssignedEmployee?.MiddleName + entity.AssignedEmployee?.LastName : null,
                CustomerID = entity.CustomerID,
                CustomerName = entity.CustomerID.HasValue ? entity.Customer?.CustomerCode + " - " + entity.Customer?.FirstName + entity.Customer?.MiddleName + entity.Customer?.LastName : null,
                SupplierID = entity.SupplierID,
                EmployeeID = entity.EmployeeID,
                DueDateFrom = entity.DueDateFrom,
                DueDateTo = entity.DueDateTo,
                FromDueDateString = entity.DueDateFrom.HasValue ? entity.DueDateFrom.Value.ToString(dateFormat) : null,
                ToDueDateString = entity.DueDateTo.HasValue ? entity.DueDateTo.Value.ToString(dateFormat) : null,
                IsSendCustomerNotification = entity.IsSendCustomerNotification,
                OldIsSendCustomerNotification = entity.IsSendCustomerNotification,
                CompanyID = entity.CompanyID.HasValue ? (int)entity.CompanyID : 1,
                LoginID = entity.LoginID,
                ReferenceID = entity.ReferenceID,
                ReferenceTypeID = entity.ReferenceTypeID,
                TicketTypeID = entity.TicketTypeID,
                SupportCategoryID = entity.SupportCategoryID,
                SupportCategoryName = entity.SupportCategoryID.HasValue ? entity.SupportCategory?.CategoryName : null,
                SupportSubCategoryID = entity.SupportSubCategoryID,
                SupportSubCategoryName = entity.SupportSubCategoryID.HasValue ? entity.SupportSubCategory?.CategoryName : null,
                FacultyTypeID = entity.FacultyTypeID,
                StudentID = entity.StudentID,
                StudentName = entity.StudentID.HasValue && entity.Student != null ? entity.Student?.AdmissionNumber + " - " + entity.Student?.FirstName + " " + (string.IsNullOrEmpty(entity.Student?.MiddleName) ? entity.Student?.MiddleName + " " : "") + entity.Student?.LastName : null,
                DepartmentID = entity.DepartmentID,
                DepartmentName = entity.DepartmentID.HasValue ? entity.Department?.DepartmentName : null,
                StudentSchoolID = entity.Student?.SchoolID,
                StudentAcademicYearID = entity.Student?.AcademicYearID,
                StudentClassID = entity.Student?.ClassID,
                StudentSectionID = entity.Student?.SectionID,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedBy = entity.UpdatedBy,
                UpdatedDate = entity.UpdatedDate,
            };

            GetParentDetails(ticketDTO, entity);
            FillCommunicationDetails(ticketDTO, entity);
            FillFeeDueDetails(ticketDTO, entity);

            if (string.IsNullOrEmpty(ticketDTO.DepartmentName))
            {
                if (ticketDTO.DepartmentID.HasValue)
                {
                    GetDepartmentByEmployee(ticketDTO);
                }
                else
                {
                    GetDepartmentByDocumentType(ticketDTO, entity);
                }
            }

            return ticketDTO;
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as TicketDTO;

            var entity = SaveTicket(toDto);

            if (entity != null && entity.TicketIID > 0)
            {
                if (toDto.IsSendCustomerNotification == true)
                {
                    GenerateAndSendMailToParentOrCustomer(entity, toDto);
                    SendPushNotificationToParentOrCustomer(entity, toDto);
                }
                if (toDto.IsSendMailToAssignedEmployee == true)
                {
                    toDto.StudentSchoolID = toDto.StudentSchoolID.HasValue ? toDto.StudentSchoolID : _context != null && _context.SchoolID != null ? Convert.ToByte(_context.SchoolID) : null;
                    GenerateAndSendMailToEmployee(entity, toDto);
                    SendPushNotificationToAssignedEmployee(entity, toDto);
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
                toDto.LoginID = Setting.Mappers.CommonDataMapper.Mapper(_context).GetLoginDetailsByParentID(toDto.ParentID);
            }

            if (!toDto.DepartmentID.HasValue && toDto.AssignedEmployeeID.HasValue)
            {
                GetDepartmentByEmployee(toDto);
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
                    AssignedEmployeeID = toDto.AssignedEmployeeID,
                    CustomerID = toDto.CustomerID,
                    SupplierID = toDto.SupplierID,
                    EmployeeID = toDto.EmployeeID,
                    DueDateFrom = toDto.DueDateFrom,
                    DueDateTo = toDto.DueDateTo,
                    IsSendCustomerNotification = toDto.IsSendCustomerNotification == true ? toDto.IsSendCustomerNotification : toDto.OldIsSendCustomerNotification,
                    LoginID = toDto.LoginID,
                    ReferenceID = toDto.ReferenceID,
                    ReferenceTypeID = toDto.ReferenceTypeID,
                    TicketTypeID = toDto.TicketTypeID,
                    SupportCategoryID = toDto.SupportCategoryID,
                    SupportSubCategoryID = toDto.SupportSubCategoryID,
                    FacultyTypeID = toDto.FacultyTypeID,
                    StudentID = toDto.StudentID,
                    DepartmentID = toDto.DepartmentID,
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
                        Notes = !string.IsNullOrEmpty(communication.Notes) ? communication.Notes.Replace("\n", "<br/>") : communication.Notes,
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
            ticketDTO.TicketCommunications = ticketDTO.TicketCommunications.Count > 0 ? ticketDTO.TicketCommunications.OrderByDescending(o => o.TicketCommunicationIID).ToList() : ticketDTO.TicketCommunications;
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
                        .Include(i => i.AssignedEmployee)
                        .Include(i => i.TicketStatus)
                        .Include(i => i.Supplier)
                        .Include(i => i.Employee)
                        .Include(i => i.Login)
                        .Include(i => i.Student)
                        .Include(i => i.SupportCategory)
                        .Include(i => i.SupportSubCategory)
                        .Include(i => i.Department)
                        .Include(i => i.TicketCommunications).ThenInclude(i => i.Login)
                        .Include(i => i.TicketFeeDueMaps).ThenInclude(i => i.StudentFeeDue).ThenInclude(i => i.FeeDueFeeTypeMaps).ThenInclude(i => i.FeeMaster)
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
        public void GenerateAndSendMailToParentOrCustomer(Ticket ticketEntity, TicketDTO ticketDTO)
        {
            if (string.IsNullOrEmpty(ticketDTO.CustomerEmailID))
            {
                if (ticketDTO.ParentID.HasValue)
                {
                    using (var dbContext = new Eduegate.Domain.Entity.School.Models.School.dbEduegateSchoolContext())
                    {
                        var parentData = dbContext.Parents
                                .Where(n => n.ParentIID == ticketDTO.ParentID.Value)
                                .AsNoTracking().FirstOrDefault();

                        ticketDTO.CustomerEmailID = parentData?.GaurdianEmail;
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

            GenerateAndSendParentMail(ticketDTO.CustomerEmailID, ticketEntity.Subject, ticketEntity.TicketNo, ticketDTO.StudentSchoolID);
        }

        public void GenerateAndSendParentMail(string emailID, string emailSubject, string ticketNumber, byte? schoolID, string schoolShortName = null)
        {
            string parentPortal = new SettingBL(_context).GetSettingValue<string>("CLIENT_PARENT_PORTAL");

            var emailTemplate = EmailTemplateMapper.Mapper(_context).GetEmailTemplateDetails(EmailTemplates.TicketingMail.ToString());

            var receiptBody = emailTemplate?.EmailTemplate;

            receiptBody = receiptBody.Replace("{REFERENCE_NUMBER}", ticketNumber);
            receiptBody = receiptBody.Replace("{PARENT_PORTAL}", parentPortal);

            var emailBody = receiptBody;

            string mailMessage = new Eduegate.Domain.Notification.EmailNotificationBL(_context).PopulateBody(emailID, emailBody);

            Email_Ticket(emailID, emailSubject, mailMessage, emailBody, schoolID);
        }

        public void Email_Ticket(string emailID, string emailSubject, string mailMessage, string emailBody, byte? schoolID)
        {
            var schoolShortName = string.Empty;
            if (schoolID.HasValue)
            {
                var schoolData = new SettingBL(_context).GetSchoolDetailByID(schoolID.Value);

                schoolShortName = schoolData?.SchoolShortName?.ToLower();
            }

            var mailParameters = new Dictionary<string, string>()
            {
                { "SCHOOL_SHORT_NAME", schoolShortName},
            };

            var ccMailIDs = new List<string>();
            string ccMailValue = new SettingBL(_context).GetSettingValue<string>("TICKETING_CC_MAILS", null);
            var splitMails = ccMailValue != null ? ccMailValue.Split(",") : null;

            if (splitMails != null)
            {
                foreach (var mail in splitMails)
                {
                    ccMailIDs.Add(mail.Trim());
                }
            }

            string defaultMail = new SettingBL(_context).GetSettingValue<string>("DEFAULT_MAIL_ID");
            string hostDet = new SettingBL(_context).GetSettingValue<string>("HOST_NAME");

            if (emailBody != "")
            {
                if (hostDet.ToLower() == "live")
                {
                    new Domain.Notification.EmailNotificationBL(_context).SendMail(emailID, emailSubject, mailMessage, EmailTypes.Ticketing, mailParameters, ccMailIDs);
                }
                else
                {
                    new Domain.Notification.EmailNotificationBL(_context).SendMail(defaultMail, emailSubject, mailMessage, EmailTypes.Ticketing, mailParameters, ccMailIDs);
                }
            }
        }

        public void GenerateAndSendMailToEmployee(Ticket ticketEntity, TicketDTO ticketDTO)
        {
            string emailID = string.Empty;

            if (ticketEntity.AssignedEmployeeID.HasValue)
            {
                using (var dbContext = new dbEduegateSupportContext())
                {
                    var empDet = dbContext.Employees.Where(x => x.EmployeeIID == ticketEntity.AssignedEmployeeID).FirstOrDefault();
                    emailID = empDet?.WorkEmail ?? ticketDTO.AssignedEmployeeEmailID;
                }
            }
            else
            {
                if (ticketDTO.DepartmentID.HasValue)
                {
                    using (var dbContext = new dbEduegateSupportContext())
                    {
                        var depDet = dbContext.TicketDepartments.Where(x => x.DepartmentID == ticketEntity.DepartmentID).FirstOrDefault();
                        emailID = depDet?.SupportEmailID ?? ticketDTO.AssignedEmployeeEmailID;
                    }
                }
                else
                {
                    emailID = ticketDTO.AssignedEmployeeEmailID;
                }
            }

            if (!string.IsNullOrEmpty(emailID))
            {
                GenerateAndSendSupportingEmployeeMail(emailID, ticketEntity.TicketNo, ticketDTO.StudentSchoolID);
            }
        }

        public void GenerateAndSendSupportingEmployeeMail(string emailID, string ticketNumber, byte? schoolID)
        {
            var emailTemplate = EmailTemplateMapper.Mapper(_context).GetEmailTemplateDetails(EmailTemplates.TicketingGenerateMailToEmployee.ToString());

            string emailBody = emailTemplate?.EmailTemplate;
            string emailSubject = emailTemplate?.Subject;

            emailSubject = emailSubject.Replace("{REFERENCE_NUMBER}", ticketNumber);
            emailBody = emailBody.Replace("{REFERENCE_NUMBER}", ticketNumber);

            string mailMessage = new Eduegate.Domain.Notification.EmailNotificationBL(_context).PopulateBody(emailID, emailBody);

            var ccMailIDs = new List<string>();
            string ccMailValue = new SettingBL(_context).GetSettingValue<string>("TICKETING_CC_MAILS", null);
            var splitMails = ccMailValue != null ? ccMailValue.Split(",") : null;

            if (splitMails != null)
            {
                foreach (var mail in splitMails)
                {
                    ccMailIDs.Add(mail.Trim());
                }
            }

            Email_Ticket(emailID, emailSubject, mailMessage, emailBody, schoolID);
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

        private void GetDepartmentByEmployee(TicketDTO ticketDTO)
        {
            using (var dbContext = new dbEduegateSupportContext())
            {
                var empData = dbContext.Employees.Where(p => p.EmployeeIID == ticketDTO.AssignedEmployeeID)
                    .Include(i => i.Department)
                    .AsNoTracking()
                    .FirstOrDefault();

                ticketDTO.DepartmentID = empData?.DepartmentID;
                ticketDTO.DepartmentName = empData?.Department?.DepartmentName;
            }
        }

        public TicketDTO GenerateTicketByDTO(TicketDTO ticketDTO)
        {
            var entity = SaveTicket(ticketDTO);

            ticketDTO.TicketIID = entity.TicketIID;
            ticketDTO.TicketNo = entity.TicketNo;

            if (entity != null && entity.TicketIID > 0)
            {
                if (ticketDTO.IsSendCustomerNotification == true)
                {
                    GenerateAndSendMailToParentOrCustomer(entity, ticketDTO);
                }
            }

            return ticketDTO;
        }

        public List<KeyValueDTO> GetSupportActionsByReferenceTypeID(int ticketReferenceTypeID)
        {
            using (var dbContext = new dbEduegateSupportContext())
            {
                var keyValueList = new List<KeyValueDTO>();

                var entities = dbContext.SupportActions.Where(x => x.TicketReferenceTypeID == ticketReferenceTypeID || x.TicketReferenceTypeID == null)
                    .OrderBy(o => o.SortOrder)
                    .AsNoTracking()
                    .ToList();

                // Custom ordering: non-null TicketReferenceTypeID first, then nulls
                entities = entities
                    .OrderBy(o => o.TicketReferenceTypeID == null ? 1 : 0) // Non-null TicketReferenceTypeID first
                    .ThenBy(o => o.TicketReferenceTypeID) // Order by TicketReferenceTypeID
                    .ToList();

                foreach (var entity in entities)
                {
                    keyValueList.Add(new KeyValueDTO()
                    {
                        Key = entity.SupportActionID.ToString(),
                        Value = entity.ActionName
                    });
                }

                return keyValueList;
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

        private void SendPushNotificationToParentOrCustomer(Ticket ticketEntity, TicketDTO ticketDTO)
        {
            var loginIDs = new List<long?>();

            var title = "Support Ticket Created";
            var message = "A ticket has been raised on your behalf. Your reference number is: " + ticketEntity?.TicketNo;

            var settings = NotificationSetting.GetParentAppSettings();

            long? loginID = null;
            if (ticketDTO.ParentID.HasValue)
            {
                loginID = Setting.Mappers.CommonDataMapper.Mapper(_context).GetLoginDetailsByParentID(ticketDTO.ParentID);
            }
            else if (ticketDTO.StudentID.HasValue)
            {
                loginID = Setting.Mappers.CommonDataMapper.Mapper(_context).GetParentLoginDetailsByStudentID(ticketDTO.StudentID);
            }
            else if (ticketDTO.CustomerID.HasValue)
            {
                loginID = Setting.Mappers.CommonDataMapper.Mapper(_context).GetLoginDetailsByCustomerID(ticketDTO.CustomerID);
            }
            loginIDs.Add(loginID);

            foreach (var login in loginIDs)
            {
                long toLoginID = Convert.ToInt32(login);
                long fromLoginID = _context?.LoginID != null ? (long)_context.LoginID : 2;

                fromLoginID = fromLoginID == toLoginID ? 2 : fromLoginID;

                PushNotificationMapper.SendAndSavePushNotification(toLoginID, fromLoginID, message, title, settings, ticketEntity.TicketIID, (long)Screens.Ticket);
            }
        }

        private void SendPushNotificationToAssignedEmployee(Ticket ticketEntity, TicketDTO ticketDTO)
        {
            var loginIDs = new List<long?>();

            var message = "New ticket created. Please check Ticket No: " + ticketEntity?.TicketNo;
            var title = "Ticket Alert";

            var settings = NotificationSetting.GetEmployeeAppSettings();

            long? loginID = null;
            if (ticketDTO.AssignedEmployeeID.HasValue)
            {
                loginID = Setting.Mappers.CommonDataMapper.Mapper(_context).GetLoginDetailsByEmployeeID(ticketDTO.AssignedEmployeeID);
            }
            loginIDs.Add(loginID);

            foreach (var login in loginIDs)
            {
                long toLoginID = Convert.ToInt32(login);
                long fromLoginID = _context?.LoginID != null ? (long)_context.LoginID : 2;

                PushNotificationMapper.SendAndSavePushNotification(toLoginID, fromLoginID, message, title, settings, ticketEntity.TicketIID);
            }
        }

    }
}
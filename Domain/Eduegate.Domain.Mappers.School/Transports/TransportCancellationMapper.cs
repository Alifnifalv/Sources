using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Mappers.Notification.Notifications;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.Commons;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Enums.Notifications;
using Eduegate.Services.Contracts.Notifications;
using Eduegate.Services.Contracts.School.Students;
using Eduegate.Services.Contracts.School.Transports;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;

namespace Eduegate.Domain.Mappers.School.Transports
{
    public class TransportCancellationMapper : DTOEntityDynamicMapper
    {
        public static TransportCancellationMapper Mapper(CallContext context)
        {
            var mapper = new TransportCancellationMapper();
            mapper._context = context;
            return mapper;
        }
        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<TransportCancellationDTO>(entity);
        }


        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private TransportCancellationDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                string transportFeeDueDetails = null;

                var entity = dbContext.TransportCancelRequests.Where(x => x.RequestIID == IID)
                    .Include(x => x.StudentRouteStopMap).ThenInclude(y => y.Student).ThenInclude(z => z.Class)
                    .Include(x => x.StudentRouteStopMap).ThenInclude(y => y.Student).ThenInclude(e => e.Section)
                    .Include(x => x.StudentRouteStopMap).ThenInclude(y => y.RouteStopMap1)
                    .Include(x => x.StudentRouteStopMap).ThenInclude(y => y.RouteStopMap2)
                    .Include(x => x.StudentRouteStopMap).ThenInclude(y => y.Routes1)
                    .Include(x => x.StudentRouteStopMap).ThenInclude(y => y.Routes11)
                    .AsNoTracking()
                    .FirstOrDefault();

                var transportDueEntity = dbContext.FeeDueFeeTypeMaps
                    .Include(x => x.FeePeriod)
                    .Include(x => x.StudentFeeDue)
                    .Where(x => x.StudentFeeDue.StudentId == entity.StudentRouteStopMap.StudentID
                                && x.StudentFeeDue.IsCancelled == false
                                && x.Status == false
                                && x.FeePeriod.IsTransport == true)
                    .ToList();

                if (transportDueEntity.Count > 0)
                {
                    foreach (var due in transportDueEntity)
                    {
                        transportFeeDueDetails = string.Concat(transportFeeDueDetails, due.FeePeriod.Description + " Amount: " + due.Amount, "</br>");
                    }
                }
                else
                {
                    transportFeeDueDetails = "No dues found against transportation";
                }

                var routeStopMapDTO = new TransportCancellationDTO()
                {
                    RequestIID = entity.RequestIID,
                    StudentRouteStopMapIID = entity.StudentRouteStopMapID,
                    StudentID = entity.StudentRouteStopMap?.Student?.StudentIID,
                    AdmissionNumber = entity.StudentRouteStopMap?.Student?.AdmissionNumber,
                    StudentName = entity.StudentRouteStopMap?.Student?.FirstName + " " + entity.StudentRouteStopMap?.Student?.MiddleName + " " + entity.StudentRouteStopMap?.Student?.LastName,
                    ClassSection = entity.StudentRouteStopMap?.Student?.Class?.ClassDescription + " " + entity.StudentRouteStopMap?.Student?.Section.SectionName,
                    AppliedDate = entity.AppliedDate,
                    ExpectedCancelDate = entity.ExpectedCancelDate,
                    ApprovedBy = entity.ApprovedBy,
                    ApprovedDate = entity.ApprovedDate,
                    Reason = entity.Reason,
                    RemarksBySchool = entity.RemarksBySchool,
                    StatusID = entity.StatusID,
                    PreviousStatusID = entity.StatusID,

                    IsOneWay = entity.StudentRouteStopMap.IsOneWay == true ? true : false,
                    PickupStopMapName = entity.StudentRouteStopMap?.RouteStopMap1.StopName,
                    DropStopMapName = entity.StudentRouteStopMap?.RouteStopMap2.StopName,
                    PickupRouteCode = entity.StudentRouteStopMap?.Routes11.RouteCode,
                    DropStopRouteCode = entity.StudentRouteStopMap?.Routes1.RouteCode,
                    FeeDues = transportFeeDueDetails,

                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    UpdatedBy = entity.UpdatedBy,
                    UpdatedDate = entity.UpdatedDate,
                };

                return routeStopMapDTO;
            }
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as TransportCancellationDTO;
            long retrunIID = 0;

            List<TransportCancelRequest> entity = new List<TransportCancelRequest>();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                //RequestIID is 0, only when Transport Cancell Request from Parent
                if (toDto.RequestIID == 0)
                {
                    var statusID = 1;
                    if (toDto.CheckBoxForSiblings == true) //For Sibling list take and committ
                    {
                        var existingList = dbContext.TransportCancelRequests
                                .AsNoTracking()
                                .Where(x => x.CreatedBy == _context.LoginID && (x.CancelRequest == false || x.CancelRequest == null))
                                .Select(x => x.StudentRouteStopMapID)
                                .ToList();

                        var getStopMapIDs = dbContext.StudentRouteStopMaps
                            .Where(x => x.Student.Parent.LoginID == _context.LoginID && x.IsActive == true && !existingList.Contains(x.StudentRouteStopMapIID))
                            .Include(x => x.Student).ThenInclude(y => y.Parent)
                            .Select(x => x.StudentRouteStopMapIID)
                            .ToList();

                        entity.AddRange(getStopMapIDs.Select(stopMapIID => new TransportCancelRequest
                        {
                            RequestIID = toDto.RequestIID,
                            StudentRouteStopMapID = stopMapIID,
                            AppliedDate = DateTime.Now,
                            StatusID = statusID,
                            ExpectedCancelDate = toDto.ExpectedCancelDate,
                            Reason = toDto.Reason,
                            CreatedBy = (int?)_context.LoginID,
                            CreatedDate = DateTime.Now
                        }));
                    }
                    else
                    {
                        entity.Add(new TransportCancelRequest
                        {
                            RequestIID = toDto.RequestIID,
                            StudentRouteStopMapID = toDto.StudentRouteStopMapIID,
                            AppliedDate = DateTime.Now,
                            StatusID = statusID,
                            ExpectedCancelDate = toDto.ExpectedCancelDate,
                            Reason = toDto.Reason,
                            CreatedBy = (int?)_context.LoginID,
                            CreatedDate = DateTime.Now
                        });
                    }

                    dbContext.TransportCancelRequests.AddRange(entity);
                    dbContext.SaveChanges();

                    retrunIID = entity.FirstOrDefault().RequestIID;

                    if (retrunIID != 0)
                    {
                        SendTransportCancelRequestMail(entity);
                    }
                }
                else
                {
                    var updateEntity = dbContext.TransportCancelRequests.Where(x => x.RequestIID == toDto.RequestIID)
                                                .AsNoTracking().FirstOrDefault();

                    updateEntity.StatusID = toDto.StatusID;
                    updateEntity.ApprovedBy = (int?)_context.LoginID;
                    updateEntity.ApprovedDate = DateTime.Now;
                    updateEntity.RemarksBySchool = toDto.RemarksBySchool;

                    dbContext.Entry(updateEntity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                    var getExistingTransportData = StudentRouteStopMapMapper.Mapper(_context).GetStudentTransportDetails((long)toDto.StudentID).FirstOrDefault();

                    //Update student transport as Inactive and cancel the current transportation
                    if (toDto.StatusID == 2)//Completed
                    {
                        var studTransport = dbContext.StudentRouteStopMaps.FirstOrDefault(x => x.StudentRouteStopMapIID == toDto.StudentRouteStopMapIID && x.IsActive == true);

                        if (studTransport != null)
                        {
                            studTransport.IsActive = false;
                            studTransport.CancelDate = toDto.ExpectedCancelDate;
                            studTransport.Remarks = toDto.RemarksBySchool;
                            studTransport.UpdatedBy = (int?)_context.LoginID;
                            studTransport.UpdatedDate = DateTime.Now;

                            dbContext.Entry(studTransport).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }
                        else
                        {
                            throw new Exception("No active student transportation record found for this student. Please check the student transportation screen for more details.");
                        }
                    }

                    dbContext.SaveChanges();

                    retrunIID = updateEntity.RequestIID;

                    //Mail Send only when status changes
                    if(toDto.PreviousStatusID != updateEntity.StatusID)
                    {
                        SendTransportCancellationRequestStatusMail(updateEntity, getExistingTransportData);
                    }
                }

                return ToDTOString(ToDTO(retrunIID));
            }
        }

        #region Transport Cancellation details By StudentID
        public TransportCancellationDTO GetStudentTransportCancellationDetails(long studentID)
        {
            var cancelledDetails = new TransportCancellationDTO();
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat", 0, "dd/MM/yyyy");

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var transport = dbContext.TransportCancelRequests
                    .Include(x => x.StudentRouteStopMap).ThenInclude(y => y.Student)
                    .Include(x => x.Status)
                    .AsNoTracking()
                    .FirstOrDefault(x => x.StudentRouteStopMap.StudentID == studentID && (x.CancelRequest == false || x.CancelRequest == null));

                if (transport != null)
                {
                    cancelledDetails.RequestIID = transport.RequestIID;
                    cancelledDetails.StudentID = transport.StudentRouteStopMap?.StudentID;
                    cancelledDetails.AppliedDate = transport.AppliedDate;
                    cancelledDetails.ExpectedCancelDate = transport.ExpectedCancelDate;
                    cancelledDetails.StatusID = transport.StatusID;
                    cancelledDetails.RequestStatus = transport.Status?.StatusName;

                    cancelledDetails.AppliedDateString = cancelledDetails.AppliedDate.HasValue ? cancelledDetails.AppliedDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                    cancelledDetails.ExpectedCancelDateString = cancelledDetails.ExpectedCancelDate.HasValue ? cancelledDetails.ExpectedCancelDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
                }

            }
            return cancelledDetails;
        }
        #endregion

        #region Cancell Transport Cancellation Form by Parent 
        public OperationResultDTO RevertTransportCancellation(long RequestIID)
        {
            var result = new OperationResultDTO();

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.TransportCancelRequests
                    .AsNoTracking()
                    .FirstOrDefault(x => x.RequestIID == RequestIID);

                entity.RequestIID = RequestIID;
                entity.CancelRequest = true;
                entity.UpdatedBy = (int?)_context.LoginID;
                entity.UpdatedDate = DateTime.Now;
                entity.CancelRequest = true;

                try
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    dbContext.SaveChanges();

                    result = new OperationResultDTO()
                    {
                        operationResult = Framework.Contracts.Common.Enums.OperationResult.Success,
                        Message = "Cancelled Request Successfully"
                    };
                }
                catch (Exception ex)
                {
                    result = new OperationResultDTO()
                    {
                        operationResult = Framework.Contracts.Common.Enums.OperationResult.Error,
                        Message = ex.Message
                    };
                }

            }
            return result;
        }
        #endregion


        #region Mail : When Transport Cancellation Request
        public bool? SendTransportCancelRequestMail(List<TransportCancelRequest> entity)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            using (var dbContext = new dbEduegateSchoolContext())
            {
                if (entity != null)
                {
                    var emaildata = new EmailNotificationDTO();

                    var transportDepartmentID = new Domain.Setting.SettingBL(null).GetSettingValue<long?>("TRANSPORTATION_DEPARTMENT_ID");

                    var getTransportCoordinators = dbContext.Employees
                        .Include(x => x.Designation)
                        .Where(x => x.DepartmentID == transportDepartmentID && x.Designation.IsTransportNotification == true && x.IsActive == true).ToList();

                    // Fetch entity IDs first to avoid translation issues
                    var studentRouteStopMapIds = entity.Select(a => a.StudentRouteStopMapID).ToList();

                    var studentTransports = dbContext.StudentRouteStopMaps
                        .Where(x => studentRouteStopMapIds.Contains(x.StudentRouteStopMapIID))
                        .ToList();

                    var transportList = new List<StudentTransportDetailDTO>();
                    var EmailIDs = new List<string>();

                    transportList = studentTransports
                        .SelectMany(stud => StudentRouteStopMapMapper.Mapper(_context)
                            .GetStudentTransportDetails((long)stud.StudentID))
                        .Where(detail => detail != null)
                        .ToList();

                    var emailTemplate = EmailTemplateMapper.Mapper(_context).GetEmailTemplateDetails(EmailTemplates.TransportCancellationRequest.ToString());
                    String emailSub = emailTemplate.Subject;
                    var appliedDate = DateTime.Now.ToString(dateFormat, CultureInfo.InvariantCulture);
                    var cancellationDate = entity.FirstOrDefault().ExpectedCancelDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture);

                    EmailIDs = getTransportCoordinators.Select(x => x.WorkEmail).ToList();

                    foreach (var tranfStuds in transportList)
                    {
                        var schoolShortName = tranfStuds.SchoolShortName?.ToLower();

                        var mailParameters = new Dictionary<string, string>()
                            {
                                { "SCHOOL_SHORT_NAME", schoolShortName},
                            };

                        foreach (var email in EmailIDs)
                        {
                            if (email != null)
                            {
                                String emailDetails = "";

                                emailDetails = emailTemplate?.EmailTemplate;
                                //StudentDetails
                                emailDetails = emailDetails.Replace("{AdmissionNumber}", tranfStuds.AdmissionNumber);
                                emailDetails = emailDetails.Replace("{StudentName}", tranfStuds.Name);
                                emailDetails = emailDetails.Replace("{ClassName}", tranfStuds.Class);
                                emailDetails = emailDetails.Replace("{SectionName}", tranfStuds.Section);
                                emailDetails = emailDetails.Replace("{AppliedDate}", appliedDate);
                                emailDetails = emailDetails.Replace("{CancellationDate}", cancellationDate);

                                //TransportDetails
                                emailDetails = emailDetails.Replace("{PickUpRoute}", tranfStuds.PickupRouteCode);
                                emailDetails = emailDetails.Replace("{PickupStopMap}", tranfStuds.PickupStopMapName);
                                emailDetails = emailDetails.Replace("{DropRoute}", tranfStuds.DropStopRouteCode);
                                emailDetails = emailDetails.Replace("{DropStop}", tranfStuds.DropStopMapName);


                                try
                                {
                                    string mailMessage = new Eduegate.Domain.Notification.EmailNotificationBL(_context).PopulateBody(email, emailDetails);

                                    var hostDet = new Domain.Setting.SettingBL(null).GetSettingValue<string>("HOST_NAME");

                                    string defaultMail = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DEFAULT_MAIL_ID");

                                    if (emailDetails != "")
                                    {
                                        if (hostDet.ToLower() == "live")
                                        {
                                            new Eduegate.Domain.Notification.EmailNotificationBL(_context).SendMail(email, emailSub, mailMessage, EmailTypes.TransportCreation, mailParameters);
                                        }
                                        else
                                        {
                                            new Eduegate.Domain.Notification.EmailNotificationBL(_context).SendMail(defaultMail, emailSub, mailMessage, EmailTypes.TransportCreation, mailParameters);
                                        }
                                    }

                                }
                                catch { }
                            }
                        }
                    }
                }
            }

            return true;
        }
        #endregion

        #region Mail : When Transport Cancellation Request Status Changes
        public bool? SendTransportCancellationRequestStatusMail(TransportCancelRequest entity,StudentTransportDetailDTO tranfStuds)
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            using (var dbContext = new dbEduegateSchoolContext())
            {
                if (entity != null)
                {
                    var emaildata = new EmailNotificationDTO();

                    var transportDepartmentID = new Domain.Setting.SettingBL(null).GetSettingValue<long?>("TRANSPORTATION_DEPARTMENT_ID");

                    var getTransportCoordinators = dbContext.Employees
                        .Include(x => x.Designation)
                        .Where(x => x.DepartmentID == transportDepartmentID && x.Designation.IsTransportNotification == true && x.IsActive == true).ToList();

                    var studentTransport = dbContext.StudentRouteStopMaps
                        .Include(x => x.Student).ThenInclude(p => p.Parent)
                        .FirstOrDefault(x => x.StudentRouteStopMapIID == entity.StudentRouteStopMapID);

                    var parentMailID = studentTransport.Student?.Parent?.GaurdianEmail ?? studentTransport.Student.Parent?.FatherEmailID ?? studentTransport.Student.Parent?.MotherEmailID;

                    var CCEmailIDs = new List<string>();

                    var emailTemplate = EmailTemplateMapper.Mapper(_context).GetEmailTemplateDetails(EmailTemplates.TransportCancellationRequestStatus.ToString());

                    String emailSub = emailTemplate.Subject;
                    var cancellationDate = entity.ExpectedCancelDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture);

                    CCEmailIDs = getTransportCoordinators.Select(x => x.WorkEmail).ToList();
                    var status = dbContext.TransportCancellationStatuses.FirstOrDefault(x => x.StatusID == entity.StatusID).StatusName;

                    //Parent Email Main >> Others as CC
                    var transportDueEntity = dbContext.FeeDueFeeTypeMaps
                                            .Include(x => x.FeePeriod)
                                            .Include(x => x.StudentFeeDue)
                                            .Where(x => x.StudentFeeDue != null
                                                && x.StudentFeeDue.StudentId == studentTransport.StudentID
                                                && x.StudentFeeDue.IsCancelled == false
                                                && x.Status == false && x.FeePeriod != null
                                                && x.FeePeriod.IsTransport == true).ToList();

                    if (transportDueEntity?.Sum(x => x.Amount) > 0)
                    {
                        var accountsEmpMailIDs = dbContext.Employees
                            .Include(x => x.Designation)
                            .Where(x => x.BranchID == _context.SchoolID && x.IsActive == true && x.Designation.DesignationCode == "004").Select(x => x.WorkEmail).ToList();

                        if(accountsEmpMailIDs.Count > 0)
                        {
                            CCEmailIDs.AddRange(accountsEmpMailIDs);
                        }
                    }

                    var schoolShortName = tranfStuds.SchoolShortName?.ToLower();

                    var mailParameters = new Dictionary<string, string>()
                            {
                                { "SCHOOL_SHORT_NAME", schoolShortName},
                            };

                        if (parentMailID != null)
                        {
                            String emailDetails = "";
                            emailDetails = emailTemplate?.EmailTemplate;

                            //Subject
                            emailSub = emailSub.Replace("{AdmissionNumber}", tranfStuds.AdmissionNumber);
                            emailSub = emailSub.Replace("{Status}", status);

                            emailDetails = emailDetails.Replace("{AdmissionNumber}", tranfStuds.AdmissionNumber);
                            emailDetails = emailDetails.Replace("{StudentName}", tranfStuds.Name);
                            emailDetails = emailDetails.Replace("{ClassName}", tranfStuds.Class);
                            emailDetails = emailDetails.Replace("{SectionName}", tranfStuds.Section);
                            emailDetails = emailDetails.Replace("{CancellationDate}", cancellationDate);
                            emailDetails = emailDetails.Replace("{Status}", status);

                            //TransportDetails
                            emailDetails = emailDetails.Replace("{PickUpRoute}", tranfStuds.PickupRouteCode);
                            emailDetails = emailDetails.Replace("{PickupStopMap}", tranfStuds.PickupStopMapName);
                            emailDetails = emailDetails.Replace("{DropRoute}", tranfStuds.DropStopRouteCode);
                            emailDetails = emailDetails.Replace("{DropStop}", tranfStuds.DropStopMapName);


                            try
                            {
                                string mailMessage = new Eduegate.Domain.Notification.EmailNotificationBL(_context).PopulateBody(parentMailID, emailDetails);

                                var hostDet = new Domain.Setting.SettingBL(null).GetSettingValue<string>("HOST_NAME");

                                string defaultMail = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DEFAULT_MAIL_ID");

                                if (emailDetails != "")
                                {
                                    if (hostDet.ToLower() == "live")
                                    {
                                        new Eduegate.Domain.Notification.EmailNotificationBL(_context).SendMail(parentMailID, emailSub, mailMessage, EmailTypes.TransportCreation, mailParameters, CCEmailIDs);
                                    }
                                    else
                                    {
                                        new Eduegate.Domain.Notification.EmailNotificationBL(_context).SendMail(defaultMail, emailSub, mailMessage, EmailTypes.TransportCreation, mailParameters, CCEmailIDs);
                                    }
                                }

                            }
                            catch { }
                        }
                }
            }

            return true;
        }
        #endregion
    }
}
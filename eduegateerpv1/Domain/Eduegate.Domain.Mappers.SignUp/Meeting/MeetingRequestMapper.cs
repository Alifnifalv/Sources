using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using System;
using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Eduegate.Domain.Entity.SignUp;
using Eduegate.Domain.Entity.SignUp.Models;
using Eduegate.Services.Contracts.SignUp.Meeting;
using Eduegate.Services.Contracts.SignUp.SignUps;
using System.Collections.Generic;
using System.Globalization;
using Eduegate.Domain.Mappers.SignUp.SignUps;
using Eduegate.Domain.Mappers.Notification.Helpers;
using Eduegate.Domain.Mappers.Notification;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Framework.Contracts.Common.Enums;
using Eduegate.Services.Contracts.Enums.Notifications;
using Eduegate.Services.Contracts.Mutual;

namespace Eduegate.Domain.Mappers.SignUp.Meeting
{
    public class MeetingRequestMapper : DTOEntityDynamicMapper
    {
        public static MeetingRequestMapper Mapper(CallContext context)
        {
            var mapper = new MeetingRequestMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<MeetingRequestDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private MeetingRequestDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSignUpContext())
            {
                var entity = dbContext.MeetingRequests
                    .Where(x => x.MeetingRequestIID == IID)
                    .Include(i => i.Student)
                    .Include(i => i.Parent)
                    .Include(i => i.Faculty)
                    .Include(i => i.School)
                    .Include(i => i.AcademicYear)
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .Include(i => i.RequestedSignupSlotMap)
                    .Include(i => i.ApprovedSignupSlotMap)
                    .Include(i => i.MeetingRequestStatus)
                    .AsNoTracking()
                    .FirstOrDefault();

                return ToDTO(entity);
            }
        }

        private MeetingRequestDTO ToDTO(MeetingRequest entity)
        {
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");
            var timeFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("TimeFormatWithoutSecond");

            var groupDTO = new MeetingRequestDTO()
            {
                MeetingRequestIID = entity.MeetingRequestIID,
                StudentID = entity.StudentID,
                Student = entity.StudentID.HasValue ? new KeyValueDTO()
                {
                    Key = entity.StudentID.ToString(),
                    Value = entity.Student.AdmissionNumber + " - " + entity.Student.FirstName + " " + (string.IsNullOrEmpty(entity.Student.MiddleName) ? null : entity.Student.MiddleName + " ") + entity.Student.LastName,
                } : new KeyValueDTO(),
                ParentID = entity.ParentID,
                ParentName = entity.ParentID.HasValue ? entity.Parent.FatherFirstName + " " + (string.IsNullOrEmpty(entity.Parent.FatherMiddleName) ? null : entity.Parent.FatherMiddleName + " ") + entity.Parent.FatherLastName : null,
                ParentLoginID = entity.ParentID.HasValue ? entity.Parent?.LoginID : null,
                GuardianEmailID = entity.ParentID.HasValue ? entity.Parent?.GaurdianEmail : null,
                FacultyID = entity.FacultyID,
                Faculty = entity.FacultyID.HasValue ? new KeyValueDTO()
                {
                    Key = entity.FacultyID.ToString(),
                    Value = entity.Faculty.EmployeeCode + " - " + entity.Faculty.FirstName + " " + (string.IsNullOrEmpty(entity.Faculty.MiddleName) ? null : entity.Faculty.MiddleName + " ") + entity.Faculty.LastName,
                } : new KeyValueDTO(),
                SchoolID = entity.SchoolID,
                AcademicYearID = entity.AcademicYearID,
                ClassID = entity.ClassID,
                SectionID = entity.SectionID,
                RequestedSignupSlotMapID = entity.RequestedSignupSlotMapID,
                ApprovedSignupSlotMapID = entity.ApprovedSignupSlotMapID,
                MeetingRequestStatusID = entity.MeetingRequestStatusID,
                MeetingRequestStatusName = entity.MeetingRequestStatusID.HasValue ? entity.MeetingRequestStatus?.RequestStatusName : null,
                RequestedDate = entity.RequestedDate,
                RequestedDateString = entity.RequestedDate.HasValue ? entity.RequestedDate.Value.ToString(dateFormat) : null,
                ApprovedDate = entity.ApprovedDate,
                ApprovedDateString = entity.ApprovedDate.HasValue ? entity.ApprovedDate.Value.ToString(dateFormat) : null,
                RequestedSlotTime = entity.RequestedSignupSlotMapID.HasValue ? DateTime.Today.Add(entity.RequestedSignupSlotMap.StartTime.Value).ToString(timeFormat, CultureInfo.InvariantCulture) + " - " + DateTime.Today.Add(entity.RequestedSignupSlotMap.EndTime.Value).ToString(timeFormat, CultureInfo.InvariantCulture) : null,
                ApprovedSlotTime = entity.ApprovedSignupSlotMapID.HasValue ? DateTime.Today.Add(entity.ApprovedSignupSlotMap.StartTime.Value).ToString(timeFormat, CultureInfo.InvariantCulture) + " - " + DateTime.Today.Add(entity.ApprovedSignupSlotMap.EndTime.Value).ToString(timeFormat, CultureInfo.InvariantCulture) : null,
                OldMeetingRequestStatusID = entity.MeetingRequestStatusID,
                RequesterRemark = entity.RequesterRemark,
                FacultyRemark = entity.FacultyRemark,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedBy = entity.UpdatedBy,
                UpdatedDate = entity.UpdatedDate,
            };

            return groupDTO;
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as MeetingRequestDTO;

            var entity = new MeetingRequest();

            var requestStatusID = new Domain.Setting.SettingBL(_context).GetSettingValue<byte>("MEETING_REQUEST_STATUS_REQUESTED", 1);
            var approveStatusID = new Domain.Setting.SettingBL(_context).GetSettingValue<byte>("MEETING_REQUEST_STATUS_APPROVED", 3);
            var rejectStatusID = new Domain.Setting.SettingBL(_context).GetSettingValue<byte>("MEETING_REQUEST_STATUS_REJECTED", 4);
            var cancelStatusID = new Domain.Setting.SettingBL(_context).GetSettingValue<byte>("MEETING_REQUEST_STATUS_CANCEL", 5);

            if (toDto.OldMeetingRequestStatusID == approveStatusID)
            {
                if (toDto.MeetingRequestStatusID != cancelStatusID && toDto.MeetingRequestStatusID != approveStatusID)
                {
                    throw new Exception("The slot is already approved, so only cancellation is allowed.");
                }
            }
            else if (!toDto.ApprovedSignupSlotMapID.HasValue)
            {
                throw new Exception("Approved time is required!");
            }

            if (toDto.OldMeetingRequestStatusID != approveStatusID && toDto.MeetingRequestStatusID == approveStatusID)
            {
                CheckSlotAvailabilityStatus(toDto);
            }

            var requestID = SaveMeetingRequest(toDto);
            entity.MeetingRequestIID = requestID.Value;

            if (entity.MeetingRequestIID > 0)
            {
                if (toDto.OldMeetingRequestStatusID != approveStatusID && toDto.MeetingRequestStatusID == approveStatusID)
                {
                    AssignSlotForParent(toDto);
                }
                else if (toDto.MeetingRequestStatusID == cancelStatusID)
                {
                    CancelApprovedSlotForParent(toDto);
                }

                string messageType = null;
                if (toDto.MeetingRequestStatusID == approveStatusID)
                {
                    messageType = "approved";
                }
                else if (toDto.MeetingRequestStatusID == cancelStatusID)
                {
                    messageType = "cancelled";
                }
                else if (toDto.MeetingRequestStatusID == rejectStatusID)
                {
                    messageType = "rejected";
                }

                if (toDto.IsSendNotification == true)
                {
                    SendPushNotification(toDto, messageType);
                    SendEmailNotification(toDto, messageType);
                }
            }

            return ToDTOString(ToDTO(entity.MeetingRequestIID));
        }

        public long? SaveMeetingRequest(MeetingRequestDTO requestDTO)
        {
            using (var dbContext = new dbEduegateSignUpContext())
            {
                var entity = new MeetingRequest()
                {
                    MeetingRequestIID = requestDTO.MeetingRequestIID,
                    StudentID = requestDTO.StudentID,
                    ParentID = requestDTO.ParentID,
                    FacultyID = requestDTO.FacultyID,
                    SchoolID = requestDTO.SchoolID.HasValue ? requestDTO.SchoolID : _context != null && _context.SchoolID.HasValue ? Convert.ToByte(_context.SchoolID) : (byte?)null,
                    AcademicYearID = requestDTO.AcademicYearID.HasValue ? requestDTO.AcademicYearID : _context != null && _context.AcademicYearID.HasValue ? _context.AcademicYearID : (int?)null,
                    ClassID = requestDTO.ClassID,
                    SectionID = requestDTO.SectionID,
                    RequestedSignupSlotMapID = requestDTO.RequestedSignupSlotMapID,
                    ApprovedSignupSlotMapID = requestDTO.ApprovedSignupSlotMapID,
                    MeetingRequestStatusID = requestDTO.MeetingRequestStatusID,
                    RequestedDate = requestDTO.RequestedDate,
                    ApprovedDate = requestDTO.ApprovedDate,
                    RequesterRemark = requestDTO.RequesterRemark,
                    FacultyRemark = requestDTO.FacultyRemark,
                    CreatedBy = requestDTO.MeetingRequestIID == 0 ? (int)_context.LoginID : requestDTO.CreatedBy,
                    UpdatedBy = requestDTO.MeetingRequestIID > 0 ? (int)_context.LoginID : requestDTO.UpdatedBy,
                    CreatedDate = requestDTO.MeetingRequestIID == 0 ? DateTime.Now : requestDTO.CreatedDate,
                    UpdatedDate = requestDTO.MeetingRequestIID > 0 ? DateTime.Now : requestDTO.UpdatedDate,
                };

                dbContext.MeetingRequests.Add(entity);

                if (entity.MeetingRequestIID == 0)
                {
                    dbContext.Entry(entity).State = EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = EntityState.Modified;
                }
                dbContext.SaveChanges();

                return entity.MeetingRequestIID;
            }
        }

        public List<MeetingRequestDTO> GetMeetingRequestsByParentID(long? parentID)
        {
            var dtos = new List<MeetingRequestDTO>();

            using (var dbContext = new dbEduegateSignUpContext())
            {
                var entities = dbContext.MeetingRequests
                    .Where(x => x.ParentID == parentID)
                    .Include(i => i.Student)
                    .Include(i => i.Parent)
                    .Include(i => i.Faculty)
                    .Include(i => i.School)
                    .Include(i => i.AcademicYear)
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .Include(i => i.RequestedSignupSlotMap)
                    .Include(i => i.ApprovedSignupSlotMap)
                    .Include(i => i.MeetingRequestStatus)
                    .AsNoTracking()
                    .OrderByDescending(o => o.MeetingRequestIID)
                    .ToList();

                foreach (var entity in entities)
                {
                    dtos.Add(ToDTO(entity));
                }
            }

            return dtos;
        }

        public void CheckSlotAvailabilityStatus(MeetingRequestDTO requestDTO)
        {
            using (var dbContext = new dbEduegateSignUpContext())
            {
                var slotAssignedStatusID = new Domain.Setting.SettingBL(_context).GetSettingValue<byte>("SIGNUP_SLOTMAP_STATUSID_ASSIGNED", 2);
                var slotCancelStatusID = new Domain.Setting.SettingBL(_context).GetSettingValue<byte>("SIGNUP_SLOTMAP_STATUSID_CANCEL", 3);
                var slotHoldStatusID = new Domain.Setting.SettingBL(_context).GetSettingValue<byte>("SIGNUP_SLOTMAP_STATUSID_HOLD", 6);

                var entity = dbContext.SignupSlotMaps
                    .Where(x => x.SignupSlotMapIID == requestDTO.ApprovedSignupSlotMapID)
                    .Include(i => i.SignupSlotAllocationMaps)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (entity != null)
                {
                    if (entity.SignupSlotAllocationMaps.Count > 0)
                    {
                        var assignedSlotDet = entity.SignupSlotAllocationMaps.FirstOrDefault(x => x.SlotMapStatusID == slotAssignedStatusID);

                        if (assignedSlotDet != null)
                        {
                            throw new Exception("Selected slot is already assigned to someone. Please select a new one.");
                        }
                    }

                    if (entity.SlotMapStatusID == slotCancelStatusID)
                    {
                        throw new Exception("Slot is already cancelled. Please select an open slot.");
                    }
                    else if (entity.SlotMapStatusID == slotHoldStatusID)
                    {
                        throw new Exception("The selected slot is currently on hold. Please choose a new one or update the status of the current slot.");
                    }
                }
            }
        }

        public void AssignSlotForParent(MeetingRequestDTO requestDTO)
        {
            using (var dbContext = new dbEduegateSignUpContext())
            {
                var slotAssignedStatusID = new Domain.Setting.SettingBL(_context).GetSettingValue<byte>("SIGNUP_SLOTMAP_STATUSID_ASSIGNED", 2);

                var entity = dbContext.SignupSlotMaps
                    .Where(x => x.SignupSlotMapIID == requestDTO.ApprovedSignupSlotMapID)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (entity != null)
                {
                    var slotAllocMapDTO = FillSlotAllocationDTO(requestDTO);

                    var result = SignUpMapper.Mapper(_context).SaveSignupSlotAllocation(slotAllocMapDTO);

                    if (result != null)
                    {
                        SignUpMapper.Mapper(_context).UpdateSignupSlotMapStatus(requestDTO.ApprovedSignupSlotMapID.Value, slotAssignedStatusID);
                    }
                }
                else
                {
                    throw new Exception("Slot assignment failed!");
                }
            }
        }

        public void CancelApprovedSlotForParent(MeetingRequestDTO requestDTO)
        {
            using (var dbContext = new dbEduegateSignUpContext())
            {
                var slotOpenStatusID = new Domain.Setting.SettingBL(_context).GetSettingValue<byte>("SIGNUP_SLOTMAP_STATUSID_OPEN", 1);

                var entity = dbContext.SignupSlotMaps
                    .Where(x => x.SignupSlotMapIID == requestDTO.ApprovedSignupSlotMapID)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (entity != null)
                {
                    var signUpSlotMap = new SignupSlotMapDTO()
                    {
                        SignupSlotMapIID = requestDTO.ApprovedSignupSlotMapID.HasValue ? Convert.ToInt64(requestDTO.ApprovedSignupSlotMapID) : 0,
                        ParentID = requestDTO.ParentID,
                    };

                    var result = SignUpMapper.Mapper(_context).CancelSelectedSignUpSlot(signUpSlotMap);

                    if (result.operationResult == OperationResult.Success)
                    {
                        SignUpMapper.Mapper(_context).UpdateSignupSlotMapStatus(requestDTO.ApprovedSignupSlotMapID.Value, slotOpenStatusID);
                    }
                }
                else
                {
                    throw new Exception("Slot cancellation failed!");
                }
            }
        }

        public SignupSlotAllocationMapDTO FillSlotAllocationDTO(MeetingRequestDTO requestDTO)
        {
            var slotAssignedStatusID = new Domain.Setting.SettingBL(_context).GetSettingValue<byte>("SIGNUP_SLOTMAP_STATUSID_ASSIGNED", 2);

            var slotAllocMapDTO = new SignupSlotAllocationMapDTO()
            {
                SignupSlotAllocationMapIID = 0,
                SignupSlotMapID = requestDTO.ApprovedSignupSlotMapID,
                StudentID = requestDTO.StudentID,
                EmployeeID = (long?)null,
                ParentID = requestDTO.ParentID,
                SchoolID = requestDTO.SchoolID,
                AcademicYearID = requestDTO.AcademicYearID,
                SlotMapStatusID = slotAssignedStatusID
            };

            return slotAllocMapDTO;
        }

        private void SendPushNotification(MeetingRequestDTO reqDTO, string messageType)
        {
            var loginIDs = new List<long?>();

            var title = "Meeting request update";
            var message = "Meeting request status changed.";

            if (messageType == "approved")
            {
                message = "Your meeting request is approved";
            }
            else if (messageType == "cancelled")
            {
                message = "Your meeting request is cancelled";
            }
            else if (messageType == "rejected")
            {
                message = "Your meeting request is rejected";
            }

            var settings = NotificationSetting.GetParentAppSettings();

            FillDetailsForNotification(reqDTO, message, loginIDs);

            foreach (var login in loginIDs)
            {
                long toLoginID = Convert.ToInt32(login);
                long fromLoginID = _context?.LoginID != null ? (long)_context.LoginID : 2;

                PushNotificationMapper.SendAndSavePushNotification(toLoginID, fromLoginID, message, title, settings);
            }
        }

        private void FillDetailsForNotification(MeetingRequestDTO reqDTO, string message, List<long?> loginIDs)
        {
            var approveStatusID = new Domain.Setting.SettingBL(_context).GetSettingValue<byte>("MEETING_REQUEST_STATUS_APPROVED", 3);
            var rejectStatusID = new Domain.Setting.SettingBL(_context).GetSettingValue<byte>("MEETING_REQUEST_STATUS_REJECTED", 4);
            var cancelStatusID = new Domain.Setting.SettingBL(_context).GetSettingValue<byte>("MEETING_REQUEST_STATUS_CANCEL", 5);

            if (reqDTO.OldMeetingRequestStatusID.HasValue)
            {
                if (reqDTO.OldMeetingRequestStatusID.HasValue && reqDTO.OldMeetingRequestStatusID == approveStatusID)
                {
                    if (reqDTO.MeetingRequestStatusID == cancelStatusID)
                    {
                        message = "Sorry, the scheduled meeting has been cancelled.";
                    }
                    else if (reqDTO.MeetingRequestStatusID == rejectStatusID)
                    {
                        message = "Sorry, the scheduled meeting has been rejected.";
                    }
                }
                else if (reqDTO.MeetingRequestStatusID == rejectStatusID)
                {
                    message = "Sorry, the meeting request is rejected.";
                }
                else if (reqDTO.MeetingRequestStatusID == approveStatusID)
                {
                    message = "Your meeting request has been approved!.";

                    if (reqDTO.ParentLoginID.HasValue)
                    {
                        loginIDs.Add(reqDTO.ParentLoginID);
                    }
                    else
                    {
                        var slotMapAlloc = SignUpMapper.Mapper(_context).GetAssignedSlotAllocationDetailBySlotMapID(reqDTO.ApprovedSignupSlotMapID.Value);
                        if (slotMapAlloc != null)
                        {
                            if (slotMapAlloc.StudentID.HasValue)
                            {
                                var loginID = new Eduegate.Domain.School.ParentBL(_context).GetParentLoginIDByStudentID(slotMapAlloc.StudentID);
                                loginIDs.Add(loginID);
                            }
                        }
                    }
                }
            }

            if (loginIDs.Count == 0)
            {
                if (reqDTO.MeetingRequestStatusID == approveStatusID)
                {
                    message = "Your meeting request has been approved!.";

                    if (reqDTO.ParentLoginID.HasValue)
                    {
                        loginIDs.Add(reqDTO.ParentLoginID);
                    }
                    else
                    {
                        if (reqDTO.StudentID.HasValue)
                        {
                            var loginID = new Eduegate.Domain.School.ParentBL(_context).GetParentLoginIDByStudentID(reqDTO.StudentID);
                            loginIDs.Add(loginID);
                        }
                        else if (reqDTO.ClassID.HasValue)
                        {
                            if (reqDTO.SectionID.HasValue)
                            {
                                loginIDs = new Eduegate.Domain.School.ParentBL(_context).GetParentsLoginIDByClassSection(reqDTO.ClassID, reqDTO.SectionID);
                            }
                            else
                            {
                                loginIDs = new Eduegate.Domain.School.ParentBL(_context).GetParentsLoginIDByClassSection(reqDTO.ClassID, null);
                            }
                        }
                    }
                }
            }
        }

        private void SendEmailNotification(MeetingRequestDTO reqDTO, string messageType)
        {
            var parentEmails = new List<string>();

            if (string.IsNullOrEmpty(reqDTO.GuardianEmailID))
            {
                if (reqDTO.ClassID.HasValue)
                {
                    if (reqDTO.SectionID.HasValue)
                    {
                        parentEmails = new Eduegate.Domain.School.ParentBL(_context).GetParentsEmailIDByClassSection(reqDTO.ClassID, reqDTO.SectionID);
                    }
                    else
                    {
                        parentEmails = new Eduegate.Domain.School.ParentBL(_context).GetParentsEmailIDByClassSection(reqDTO.ClassID, null);
                    }
                }
                if (reqDTO.StudentID.HasValue)
                {
                    var emailID = new Eduegate.Domain.School.ParentBL(_context).GetParentEmailIDByStudentID(reqDTO.StudentID);
                    parentEmails.Add(emailID);
                }
            }
            else
            {
                parentEmails.Add(reqDTO.GuardianEmailID);
            }

            var settings = NotificationSetting.GetParentAppSettings();
            string schoolShortName = null;
            foreach (var emailID in parentEmails)
            {
                EmailProcess(emailID, schoolShortName, reqDTO);
            }
        }

        public void EmailProcess(string toEmail, string schoolShortName, MeetingRequestDTO reqDTO)
        {
            var hostDet = new Domain.Setting.SettingBL(null).GetSettingValue<string>("HOST_NAME");

            string defaultMail = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DEFAULT_MAIL_ID");

            var emailTemplate = EmailTemplateMapper.Mapper(_context).GetEmailTemplateDetails(EmailTemplates.MeetingRequestMail.ToString());

            var receiptBody = emailTemplate?.EmailTemplate;
            string emailSubject = emailTemplate?.Subject;

            receiptBody = receiptBody.Replace("{FACULTY_NAME}", reqDTO.Faculty.Value);
            receiptBody = receiptBody.Replace("{MEETING_DATE}", reqDTO.ApprovedDateString);
            receiptBody = receiptBody.Replace("{MEETING_TIME}", reqDTO.ApprovedSlotTime);
            receiptBody = receiptBody.Replace("{MEETING_STATUS}", reqDTO.MeetingRequestStatusName);

            string emailBody = receiptBody;

            if (string.IsNullOrEmpty(emailBody))
            {
                emailBody = @"<br />
                        <p align='left'>
                        Dear Parent/Guardian,<br /></p>
                        A Meeting related changes happened.<br />
                        To view the changes,  please login to your parent portal and check<br /><br />
                        <br/><br/><p style='font-size:0.7rem;'<b>Please Note: </b>do not reply to this email as it is a computer generated email</p>";
            }

            if (string.IsNullOrEmpty(emailSubject))
            {
                emailSubject = "Meeting request update";
            }

            try
            {
                schoolShortName = schoolShortName?.ToLower();
                string mailMessage = new Eduegate.Domain.Notification.EmailNotificationBL(_context).PopulateBody(toEmail, emailBody);

                var mailParameters = new Dictionary<string, string>()
                {
                    { "SCHOOL_SHORT_NAME", schoolShortName},
                };

                if (emailBody != "")
                {
                    if (hostDet.ToLower() == "live")
                    {
                        new Eduegate.Domain.Notification.EmailNotificationBL(_context).SendMail(toEmail, emailSubject, mailMessage, EmailTypes.MeetingRequest, mailParameters);
                    }
                    else
                    {
                        new Eduegate.Domain.Notification.EmailNotificationBL(_context).SendMail(defaultMail, emailSubject, mailMessage, EmailTypes.MeetingRequest, mailParameters);
                    }
                }
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception")
                    ? ex.InnerException?.Message : ex.Message;

                Eduegate.Logger.LogHelper<string>.Fatal($"Meeting request Mailing failed. Error message: {errorMessage}", ex);
            }
        }

    }
}
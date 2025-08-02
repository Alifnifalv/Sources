using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Eduegate.Services.Contracts.SignUp.SignUps;
using System.Globalization;
using Eduegate.Services.Contracts.Enums.Notifications;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Domain.Entity.Lms;
using Eduegate.Domain.Entity.Lms.Models;
using System.Linq;
using System;
using System.Collections.Generic;
using Eduegate.Domain.Mappers.Notification.Notifications.Helpers;
using Eduegate.Domain.Mappers.Notification.Notifications;
using Eduegate.Services.Contracts.Lms.Lms;

namespace Eduegate.Domain.Mappers.Lms.Lms
{
    public class LmsAllocationMapper : DTOEntityDynamicMapper
    {
        public static LmsAllocationMapper Mapper(CallContext context)
        {
            var mapper = new LmsAllocationMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<SignupSlotAllocationMapDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private SignupSlotAllocationMapDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateLmsContext())
            {
                var entity = dbContext.SignupSlotAllocationMaps
                    .Where(x => x.SignupSlotAllocationMapIID == IID)
                    .Include(i => i.School)
                    .Include(i => i.AcademicYear)
                    .Include(i => i.Student)
                    .Include(i => i.Employee)
                    .Include(i => i.Parent)
                    .Include(i => i.SlotMapStatus)
                    .Include(i => i.SignupSlotRemarkMaps)
                    .Include(i => i.SignupSlotMap).ThenInclude(i => i.Signup).ThenInclude(i => i.OrganizerEmployee)
                    .AsNoTracking()
                    .FirstOrDefault();

                return ToDTO(entity);
            }
        }

        private SignupSlotAllocationMapDTO ToDTO(SignupSlotAllocationMap entity)
        {
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");
            var timeFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("TimeFormatWithoutSecond");

            var slotAllocationMapDTO = new SignupSlotAllocationMapDTO()
            {
                SignupSlotAllocationMapIID = entity.SignupSlotAllocationMapIID,
                SignupSlotMapID = entity.SignupSlotMapID,
                StudentID = entity.StudentID,
                SignupName = entity.SignupSlotMap.Signup.SignupName,
                Student = entity.StudentID.HasValue && entity.Student != null ? entity.Student.AdmissionNumber + " - " + entity.Student.FirstName + " " + (string.IsNullOrEmpty(entity.Student.MiddleName) ? "" : entity.Student.MiddleName + " ") + entity.Student.LastName : null,
                ParentID = entity.ParentID,
                Parent = entity.ParentID.HasValue && entity.Parent != null ? entity.Parent.ParentCode + " - " + entity.Parent.FatherFirstName + " " + (string.IsNullOrEmpty(entity.Parent.FatherMiddleName) ? "" : entity.Parent.FatherMiddleName + " ") + entity.Parent.FatherLastName : null,
                EmployeeID = entity.EmployeeID,
                Employee = entity.EmployeeID.HasValue && entity.Employee != null ? entity.Employee.EmployeeCode + " - " + entity.Employee.FirstName + " " + (string.IsNullOrEmpty(entity.Employee.MiddleName) ? "" : entity.Employee.MiddleName + " ") + entity.Employee.LastName : null,
                ParentLoginID = entity.ParentID.HasValue ? entity.Parent?.LoginID : null,
                GuardianEmailID = entity.ParentID.HasValue ? entity.Parent?.GaurdianEmail : null,
                SchoolID = entity.SchoolID,
                School = entity.SchoolID.HasValue ? entity.School?.SchoolName : null,
                AcademicYearID = entity.AcademicYearID,
                AcademicYear = entity.AcademicYearID.HasValue ? entity.AcademicYear?.Description + " (" + entity.AcademicYear?.AcademicYearCode + ")" : null,
                SlotMapStatusID = entity.SlotMapStatusID,
                SlotMapStatus = entity.SlotMapStatusID.HasValue ? entity.SlotMapStatus?.SlotMapStatusName : null,
                SlotDate = entity.SignupSlotMap.SlotDate,
                SlotDateString = entity.SignupSlotMap.SlotDate.HasValue ? entity.SignupSlotMap.SlotDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                StartTime = entity.SignupSlotMap.StartTime,
                StartTimeString = entity.SignupSlotMap.StartTime.HasValue ? DateTime.Today.Add(entity.SignupSlotMap.StartTime.Value).ToString(timeFormat, CultureInfo.InvariantCulture) : null,
                EndTime = entity.SignupSlotMap.EndTime,
                EndTimeString = entity.SignupSlotMap.EndTime.HasValue ? DateTime.Today.Add(entity.SignupSlotMap.EndTime.Value).ToString(timeFormat, CultureInfo.InvariantCulture) : null,
                SlotTimeString = entity.SignupSlotMap.StartTime.HasValue && entity.SignupSlotMap.EndTime.HasValue ? DateTime.Today.Add(entity.SignupSlotMap.StartTime.Value).ToString(timeFormat, CultureInfo.InvariantCulture) + "-" + DateTime.Today.Add(entity.SignupSlotMap.EndTime.Value).ToString(timeFormat, CultureInfo.InvariantCulture) : null,
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedBy = entity.UpdatedBy,
                UpdatedDate = entity.UpdatedDate,
            };

            FillOrganizerEmployee(entity, slotAllocationMapDTO);
            FillSlotRemarkMaps(entity, slotAllocationMapDTO);

            return slotAllocationMapDTO;
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as SignupSlotAllocationMapDTO;
            var slotCancelStatusID = new Domain.Setting.SettingBL(_context).GetSettingValue<byte>("SIGNUP_SLOTMAP_STATUSID_CANCEL", 3);


            using (var dbContext = new dbEduegateLmsContext())
            {
                var entity = new SignupSlotAllocationMap()
                {
                    SignupSlotAllocationMapIID = toDto.SignupSlotAllocationMapIID,
                    SignupSlotMapID = toDto.SignupSlotMapID,
                    StudentID = toDto.StudentID,
                    EmployeeID = toDto.EmployeeID,
                    ParentID = toDto.ParentID,
                    SchoolID = toDto.SchoolID,
                    AcademicYearID = toDto.AcademicYearID,
                    SlotMapStatusID = toDto.SlotMapStatusID,
                    CreatedBy = toDto.SignupSlotAllocationMapIID == 0 ? (int)_context.LoginID : toDto.CreatedBy,
                    UpdatedBy = toDto.SignupSlotAllocationMapIID > 0 ? (int)_context.LoginID : toDto.UpdatedBy,
                    CreatedDate = toDto.SignupSlotAllocationMapIID == 0 ? DateTime.Now : toDto.CreatedDate,
                    UpdatedDate = toDto.SignupSlotAllocationMapIID > 0 ? DateTime.Now : toDto.UpdatedDate,
                };

                if (entity.SignupSlotAllocationMapIID == 0)
                {
                    dbContext.Entry(entity).State = EntityState.Added;
                }
                else
                {
                    dbContext.Entry(entity).State = EntityState.Modified;
                }
                dbContext.SaveChanges();

                string messageType = null;

                if (toDto.SlotMapStatusID == slotCancelStatusID)
                {
                    SendPushNotification(toDto);
                    SendEmailNotification(toDto, messageType);
                }

                return ToDTOString(ToDTO(entity.SignupSlotAllocationMapIID));
            }
        }

        private void FillOrganizerEmployee(SignupSlotAllocationMap entity, SignupSlotAllocationMapDTO slotAllocationMapDTO)
        {
            var signup = entity?.SignupSlotMap?.Signup;
            if (signup != null && signup.OrganizerEmployeeID.HasValue)
            {
                var organizerEmployee = string.Empty;

                if (signup.OrganizerEmployee != null)
                {
                    organizerEmployee = signup.OrganizerEmployee.EmployeeCode + " - " + signup.OrganizerEmployee.FirstName + " " +
                        (!string.IsNullOrEmpty(signup.OrganizerEmployee.MiddleName) ? signup.OrganizerEmployee.MiddleName + " " : "") +
                        signup.OrganizerEmployee.LastName;
                }
                else
                {
                    using (var dbContext = new dbEduegateLmsContext())
                    {
                        var employeeDet = dbContext.Employees.Where(e => e.EmployeeIID == signup.OrganizerEmployeeID).AsNoTracking().FirstOrDefault();

                        organizerEmployee = employeeDet != null ? employeeDet.EmployeeCode + " - " + employeeDet.FirstName + " " +
                        (!string.IsNullOrEmpty(employeeDet.MiddleName) ? employeeDet.MiddleName + " " : "") + employeeDet.LastName : null;
                    }
                }

                slotAllocationMapDTO.OrganizerEmployeeID = signup.OrganizerEmployeeID;
                slotAllocationMapDTO.OrganizerEmployeeName = organizerEmployee;
            }
        }

        private void FillSlotRemarkMaps(SignupSlotAllocationMap entity, SignupSlotAllocationMapDTO slotAllocationMapDTO)
        {
            var slotRemarkMap = entity.SignupSlotRemarkMaps?.FirstOrDefault();
            if (slotRemarkMap != null)
            {
                slotAllocationMapDTO.SignupSlotRemarkMap = new SignupSlotRemarkMapDTO()
                {
                    SignupSlotRemarkMapIID = slotRemarkMap.SignupSlotRemarkMapIID,
                    SignupSlotAllocationMapID = slotRemarkMap.SignupSlotAllocationMapID,
                    SignupSlotMapID = slotRemarkMap.SignupSlotMapID,
                    SignupID = slotRemarkMap.SignupID,
                    TeacherRemarks = slotRemarkMap.TeacherRemarks,
                    ParentRemarks = slotRemarkMap.ParentRemarks,
                    CreatedBy = slotRemarkMap.CreatedBy,
                    CreatedDate = slotRemarkMap.CreatedDate,
                    UpdatedBy = slotRemarkMap.UpdatedBy,
                    UpdatedDate = slotRemarkMap.UpdatedDate,
                };
            }
            else
            {
                slotAllocationMapDTO.SignupSlotRemarkMap = new SignupSlotRemarkMapDTO()
                {
                    SignupSlotRemarkMapIID = 0,
                    SignupSlotAllocationMapID = entity.SignupSlotAllocationMapIID,
                    SignupSlotMapID = entity.SignupSlotMapID,
                    SignupID = entity.SignupSlotMap?.SignupID,
                    TeacherRemarks = null,
                    ParentRemarks = null,
                    CreatedBy = null,
                    CreatedDate = (DateTime?)null,
                    UpdatedBy = null,
                    UpdatedDate = (DateTime?)null,
                };
            }
        }

        private void SendEmailNotification(SignupSlotAllocationMapDTO admDTO, string messageType)
        {
            var parentEmails = new List<string>();

            if (string.IsNullOrEmpty(admDTO.GuardianEmailID))
            {

            }
            else
            {
                parentEmails.Add(admDTO.GuardianEmailID);
            }

            var settings = NotificationSetting.GetParentAppSettings();
            string schoolShortName = null;
            foreach (var emailID in parentEmails)
            {
                EmailProcess(emailID, schoolShortName, admDTO);
            }
        }

        private void SendPushNotification(SignupSlotAllocationMapDTO admDTO)
        {
            var loginIDs = new List<long?>();
            var message = "Scheduled meeting is canceled";
            var title = "Meeting Canceled";
            var settings = NotificationSetting.GetParentAppSettings();
            var slotCancelStatusID = new Domain.Setting.SettingBL(_context).GetSettingValue<byte>("SIGNUP_SLOTMAP_STATUSID_CANCEL", 3);



            if (admDTO.SlotMapStatusID == slotCancelStatusID)
            {
                message = $"Your appointment for the Parent-Teacher Meeting with {admDTO.OrganizerEmployeeName} on {admDTO.SlotDateString} at {admDTO.SlotTimeString}  has been canceled.  Thank you!";
            }


            long toLoginID = Convert.ToInt32(admDTO.ParentLoginID);
            long fromLoginID = _context?.LoginID != null ? (long)_context.LoginID : 2;

            PushNotificationMapper.SendAndSavePushNotification(toLoginID, fromLoginID, message, title, settings);
        }

        public void EmailProcess(string toEmail, string schoolShortName, SignupSlotAllocationMapDTO admDTO)
        {
            var slotMapData = new SlotMapStatus();
            using (var dbContext = new dbEduegateLmsContext())
            {
                slotMapData = dbContext.SlotMapStatuses
               .Where(m => m.SlotMapStatusID == admDTO.SlotMapStatusID)
               .AsNoTracking().FirstOrDefault();
            }

            var hostDet = new Domain.Setting.SettingBL(null).GetSettingValue<string>("HOST_NAME");

            string defaultMail = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DEFAULT_MAIL_ID");

            var emailTemplate = EmailTemplateMapper.Mapper(_context).GetEmailTemplateDetails(EmailTemplates.AdminListingMail.ToString());

            var receiptBody = emailTemplate?.EmailTemplate;
            string emailSubject = emailTemplate?.Subject;

            receiptBody = receiptBody.Replace("{SLOT_DATE}", admDTO.SlotDateString);
            receiptBody = receiptBody.Replace("{FACULTY_NAME}", admDTO.OrganizerEmployeeName);
            receiptBody = receiptBody.Replace("{SLOT_STATUS}", slotMapData.SlotMapStatusName);
            receiptBody = receiptBody.Replace("{SLOT_TIME}", admDTO.SlotTimeString);

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
                emailSubject = "Meeting  update";
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
                        new Eduegate.Domain.Notification.EmailNotificationBL(_context).SendMail(toEmail, emailSubject, mailMessage, EmailTypes.MeetingUpdate, mailParameters);
                    }
                    else
                    {
                        new Eduegate.Domain.Notification.EmailNotificationBL(_context).SendMail(defaultMail, emailSubject, mailMessage, EmailTypes.MeetingUpdate, mailParameters);
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

        //public List<LmsSlotAllocationMapDTO> GetParentAllotedMeetings(long? parentID)
        //{
        //    var allocationDTOs = new List<LmsSlotAllocationMapDTO>();

        //    using (var dbContext = new dbEduegateLmsContext())
        //    {
        //        var assignedSlotMapStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<byte>("SIGNUP_SLOTMAP_STATUSID_ASSIGNED", 2);

        //        var entities = dbContext.SignupSlotAllocationMaps
        //            .Where(x => x.ParentID == parentID && x.SlotMapStatusID == assignedSlotMapStatusID)
        //            .Include(i => i.School)
        //            .Include(i => i.AcademicYear)
        //            .Include(i => i.Student)
        //            .Include(i => i.Employee)
        //            .Include(i => i.Parent)
        //            .Include(i => i.SlotMapStatus)
        //            .Include(i => i.SignupSlotRemarkMaps)
        //            .Include(i => i.SignupSlotMap).ThenInclude(i => i.Signup).ThenInclude(i => i.OrganizerEmployee)
        //            .Include(i => i.SignupSlotMap).ThenInclude(i => i.Signup).ThenInclude(i => i.SignupGroup)
        //            .AsNoTracking()
        //            .OrderByDescending(o => o.SignupSlotAllocationMapIID)
        //            .ToList();

        //        foreach (var entity in entities)
        //        {
        //            var allocationDTO = ToDTO(entity);

        //            allocationDTO.SignupGroupTitle = entity?.SignupSlotMap?.Signup?.SignupGroup?.GroupTitle;
        //            allocationDTO.SignupGroupDescription = entity?.SignupSlotMap?.Signup?.SignupGroup?.GroupDescription;

        //            allocationDTOs.Add( allocationDTO);
        //        }
        //    }

        //    return allocationDTOs;
        //}

    }
}
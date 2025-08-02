using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Students;
using Eduegate.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Eduegate.Services.Contracts.Notifications;
using Eduegate.Services.Contracts.School.Attendences;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Eduegate.Domain.Mappers.Notification;
using Eduegate.Domain.Mappers.Notification.Helpers;

namespace Eduegate.Domain.Mappers.School.Students
{
    public class StudentLeaveApplicationMapper : DTOEntityDynamicMapper
    {
        public static StudentLeaveApplicationMapper Mapper(CallContext context)
        {
            var mapper = new StudentLeaveApplicationMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<StudentLeaveApplicationDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        public StudentLeaveApplicationDTO GetLeaveApplication(long IID)
        {
            return ToDTO(IID);
        }

        public void DeleteLeaveApplication(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.StudentLeaveApplications.Where(a => a.StudentLeaveApplicationIID == IID).AsNoTracking().FirstOrDefault();
                if (entity != null)
                {
                    dbContext.StudentLeaveApplications.Remove(entity);
                    dbContext.SaveChanges();
                }
            }
        }

        public List<StudentLeaveApplicationDTO> GetStudentLeaveApplication(long studentId)
        {
            List<StudentLeaveApplicationDTO> studentLeaveDTOList = new List<StudentLeaveApplicationDTO>();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var applicationPrefix = new Domain.Setting.SettingBL(null).GetSettingValue<string>("PREFIX_LEAVE_APPL_NO");

                var studentLeaveDTO = dbContext.StudentLeaveApplications.Where(s => s.StudentID == studentId)
                    .Include(i => i.LeaveStatus)
                    .Include(i => i.Class)
                    .OrderByDescending(o => o.StudentLeaveApplicationIID)
                    .AsNoTracking()
                    .ToList();

                studentLeaveDTOList = studentLeaveDTO.Select(studentLeaveApplicationGroup => new StudentLeaveApplicationDTO()
                {
                    StudentLeaveApplicationIID = studentLeaveApplicationGroup.StudentLeaveApplicationIID,
                    StudentID = studentLeaveApplicationGroup.StudentID,
                    ClassID = studentLeaveApplicationGroup.ClassID,
                    Remarks = studentLeaveApplicationGroup.Remarks,
                    FromSessionID = studentLeaveApplicationGroup.FromSessionID,
                    ToSessionID = studentLeaveApplicationGroup.ToSessionID,
                    LeaveStatusID = studentLeaveApplicationGroup.LeaveStatusID,
                    LeaveStatusDescription = studentLeaveApplicationGroup.LeaveStatus?.StatusName,
                    FromDate = studentLeaveApplicationGroup.FromDate.HasValue ? studentLeaveApplicationGroup.FromDate : DateTime.Now,
                    ToDate = studentLeaveApplicationGroup.ToDate.HasValue ? studentLeaveApplicationGroup.ToDate : DateTime.Now,
                    Reason = studentLeaveApplicationGroup.Reason,
                    LeaveAppNumber = applicationPrefix + studentLeaveApplicationGroup.StudentLeaveApplicationIID,
                    ClassName = studentLeaveApplicationGroup.Class?.ClassDescription,
                    CreatedDate = studentLeaveApplicationGroup.CreatedDate.HasValue ? studentLeaveApplicationGroup.CreatedDate : DateTime.Now,
                }).ToList();
            }

            return studentLeaveDTOList;
        }

        public List<StudentLeaveApplicationDTO> StudentDetail(long studentId)
        {
            List<StudentLeaveApplicationDTO> LeaveDTO = new List<StudentLeaveApplicationDTO>();
            using (var dbContext = new dbEduegateSchoolContext())
            {
                LeaveDTO = (from studlv in dbContext.Students

                            where (studlv.StudentIID == studentId)
                            orderby studlv.StudentIID descending
                            group studlv by new
                            {
                                studlv.StudentIID,
                                studlv.ClassID,
                                studlv.Class,
                            } into LeaveApplicationGroup
                            select new StudentLeaveApplicationDTO()
                            {
                                StudentID = LeaveApplicationGroup.Key.StudentIID,
                                ClassID = LeaveApplicationGroup.Key.ClassID,
                                ClassName = LeaveApplicationGroup.Key.Class.ClassDescription,
                            }).AsNoTracking().ToList();
            }
            return LeaveDTO;
        }

        private StudentLeaveApplicationDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.StudentLeaveApplications.Where(x => x.StudentLeaveApplicationIID == IID)
                    .Include(x => x.Class)
                    .Include(x => x.LeaveStatus)
                    .Include(x => x.Student).ThenInclude(i => i.Class)
                    .Include(x => x.Student).ThenInclude(x => x.Section)
                    .AsNoTracking()
                    .FirstOrDefault();

                return ToDTO(entity);
            }
        }

        private StudentLeaveApplicationDTO ToDTO(StudentLeaveApplication entity)
        {
            var applicationPrefix = new Domain.Setting.SettingBL(null).GetSettingValue<string>("PREFIX_LEAVE_APPL_NO");
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

            return new StudentLeaveApplicationDTO()
            {
                StudentLeaveApplicationIID = entity.StudentLeaveApplicationIID,
                StudentID = entity.StudentID,
                ClassID = entity.ClassID,
                ClassName = entity.Class == null ? null : entity.Class.ClassDescription,
                StudentName = entity.Student == null ? null : entity.Student.FirstName + " " + entity.Student.MiddleName + " " + entity.Student.LastName,
                SectionID = entity.Student.Section.SectionID,
                Section = entity.Student.Section.SectionName,
                AdmissionNumber = entity.Student.AdmissionNumber,
                Reason = entity.Reason,
                FromDate = entity.FromDate,
                ToDate = entity.ToDate,
                FromDateString = entity.FromDate.HasValue ? entity.FromDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                ToDateString = entity.ToDate.HasValue ? entity.ToDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
                LeaveStatusID = entity.LeaveStatusID,
                LeaveStatusDescription = entity.LeaveStatusID.HasValue ? entity.LeaveStatus.StatusName : null,
                CreatedBy = entity.CreatedBy,
                UpdatedBy = entity.UpdatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdatedDate,
                AcademicYearID = entity.AcademicYearID,
                SchoolID = entity.SchoolID,
                Remarks = entity.Remarks,
                LeaveAppNumber = applicationPrefix + entity.StudentLeaveApplicationIID,
                CreatedDateString = entity.CreatedDate.HasValue ? entity.CreatedDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null,
            };
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as StudentLeaveApplicationDTO;

            if (toDto.Reason == null || toDto.Reason == "")
            {
                throw new Exception("Please enter reason !");
            }

            if (Convert.ToDateTime(toDto.FromDate) > Convert.ToDateTime(toDto.ToDate))
            {
                throw new Exception("Please ensure the selected dates are accurate, with the From date not exceeding the To date. !");
            }

            using (var dbContext = new dbEduegateSchoolContext())
            {
                //check Leave already applied on this dates
                for (DateTime aDate = toDto.FromDate.Value.Date; aDate.Date <= toDto.ToDate.Value.Date; aDate = aDate.AddDays(1))
                {
                    var oldData = dbContext.StudentLeaveApplications
                        .Where(a => a.StudentID == toDto.StudentID && aDate >= a.FromDate && aDate <= a.ToDate)
                        .Include(x => x.Student)
                        .AsNoTracking()
                        .FirstOrDefault();

                    if (oldData != null && toDto.StudentLeaveApplicationIID == 0)
                    {
                        throw new Exception("Leave is already applied for " + oldData.Student.AdmissionNumber + " in this date " + aDate.ToString("dd/MM/yyyy") + " please check !");
                    }
                }

                var studentData = dbContext.Students.Where(s => s.StudentIID == toDto.StudentID)
                    .Include(x => x.Parent)
                    .Include(x => x.Class)
                    .Include(x => x.Section)
                    .AsNoTracking()
                    .FirstOrDefault();

                var entity = new StudentLeaveApplication()
                {
                    StudentLeaveApplicationIID = toDto.StudentLeaveApplicationIID,
                    StudentID = toDto.StudentID,
                    ClassID = studentData.ClassID,
                    FromDate = toDto.FromDate,
                    ToDate = toDto.ToDate,
                    FromSessionID = toDto.FromSessionID,
                    ToSessionID = toDto.ToSessionID,
                    Reason = toDto.Reason,
                    LeaveStatusID = toDto.LeaveStatusID,
                    SchoolID = _context.SchoolID != null ? (byte)_context.SchoolID : studentData.SchoolID,
                    AcademicYearID = toDto.AcademicYearID != null ? toDto.AcademicYearID : studentData.AcademicYearID,
                    CreatedBy = toDto.StudentLeaveApplicationIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                    UpdatedBy = toDto.StudentLeaveApplicationIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                    CreatedDate = toDto.StudentLeaveApplicationIID == 0 ? DateTime.Now : dto.CreatedDate,
                    UpdatedDate = toDto.StudentLeaveApplicationIID > 0 ? DateTime.Now : dto.UpdatedDate,
                    Remarks = toDto.Remarks,
                };

                if (entity.StudentLeaveApplicationIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    dbContext.SaveChanges();

                    var settingValue = dbContext.Settings
                        .AsNoTracking()
                        .FirstOrDefault(s => s.SettingCode == "STUDENT_LEAVE_APPROVAL_WORKFLOW_ID");

                    Workflows.Helpers.WorkflowGeneratorHelper.GenerateWorkflows(long.Parse(settingValue.SettingValue), entity.StudentLeaveApplicationIID);
                }
                else
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    dbContext.SaveChanges();
                }

                UpdateAttendanceStatus(entity);
                SaveAlertsNotification(entity, studentData);

                return ToDTOString(ToDTO(entity.StudentLeaveApplicationIID));
            }
        }

        public void SaveAlertsNotification(StudentLeaveApplication entity, Student studentDetail)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var dto = new NotificationAlertsDTO();

                var approvesettingID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("LEAVE_APPROVED_STATUS_ID");
                var approvedStatusID = byte.Parse(approvesettingID);

                var rejectsettingID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("LEAVE_REJECT_STATUS_ID");
                var rejectStatusID = byte.Parse(rejectsettingID);

                var alertStatus = new Domain.Setting.SettingBL(null).GetSettingValue<string>("ALERTSTATUS_ID");

                var alertType = new Domain.Setting.SettingBL(null).GetSettingValue<string>("ALERTTYPE_ID");

                var screenreferenceID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("STUDENT_LEAVEAPPLICATION_SCREENID");

                var superAdminID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("SUPER_ADMIN_LOGIN_ID");
                var screenID = long.Parse(screenreferenceID);

                var leaveStatus = dbContext.LeaveStatuses.Where(l => l.LeaveStatusID == entity.LeaveStatusID)
                    .AsNoTracking()
                    .FirstOrDefault();

                //Push notification code is not added
                //Case 1 : Notification send to parent when leave approved or reject -- toInbox
                var message = "";
                var title = "";
                var settings = new Dictionary<string, string>();

                List<long?> toLoginLists = new List<long?>();
                if (entity.LeaveStatusID == approvedStatusID || entity.LeaveStatusID == rejectStatusID)
                {
                    var parentLoginID = studentDetail.Parent?.LoginID;
                    toLoginLists.Add(parentLoginID);
                    message = studentDetail.AdmissionNumber + " - " + studentDetail.FirstName + " " + studentDetail.MiddleName + " " + studentDetail.LastName + "'s leave Application : LV_" + entity.StudentLeaveApplicationIID + " is " + leaveStatus.StatusName;
                    title = "Leave marked as " + leaveStatus.StatusName;
                    settings = NotificationSetting.GetParentAppSettings();
                }
                else //notification send to class teacher and IT cordinators
                {
                    message = "Leave applied from student : " + studentDetail.Class?.ClassDescription + ' ' + studentDetail.Section?.SectionName + " - " +
                            studentDetail.AdmissionNumber + " - " + studentDetail.FirstName + " " + studentDetail.MiddleName + " " + studentDetail.LastName;
                    title = "Leave applied for " + studentDetail.AdmissionNumber;
                    settings = NotificationSetting.GetEmployeeAppSettings();

                    //ITcordinators
                    var cordinatorsDesigID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("IT_CORDINATORS_DESIG_ID");
                    var designationID = int.Parse(cordinatorsDesigID);

                    var employees = dbContext.Employees
                        .Where(y => y.DesignationID == designationID && y.IsActive == true && y.BranchID == studentDetail.SchoolID)
                        .AsNoTracking().ToList();

                    toLoginLists = employees.Select(x => x.LoginID).ToList();

                    var getclassTeacherID = dbContext.ClassClassTeacherMaps.Where(y => y.ClassID == studentDetail.ClassID && y.SectionID == studentDetail.SectionID && y.AcademicYearID == entity.AcademicYearID)
                        .Include(y => y.Employee1)
                        .AsNoTracking()
                        .FirstOrDefault();

                    var classTeacherLoginID = getclassTeacherID != null ? getclassTeacherID.Employee1?.LoginID : long.Parse(superAdminID);

                    toLoginLists.Add(classTeacherLoginID);
                }

                if (toLoginLists.Count > 0)
                {
                    foreach (var toLogin in toLoginLists)
                    {
                        var notification = new NotificationAlert()
                        {
                            AlertStatusID = int.Parse(alertStatus),
                            AlertTypeID = int.Parse(alertType),
                            FromLoginID = entity.UpdatedBy,
                            ToLoginID = toLogin,
                            Message = message,
                            ReferenceID = entity.StudentLeaveApplicationIID,
                            NotificationDate = DateTime.Now,
                            CreatedDate = DateTime.Now,
                            CreatedBy = entity.CreatedBy,
                            ReferenceScreenID = long.Parse(screenreferenceID),
                            IsITCordinator = false,
                        };

                        PushNotificationMapper.SendPushNotification(toLogin ?? 0, message, title, settings, string.Empty);

                        dbContext.Entry(notification).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    }
                    dbContext.SaveChanges();

                }
            }
        }

        public void UpdateAttendanceStatus(StudentLeaveApplication entity)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var dto = new StudentAttendenceDTO();
                var studentDetail = dbContext.Students.Where(x => x.StudentIID == entity.StudentID)
                    .AsNoTracking()
                    .FirstOrDefault();

                var leaveAppliedID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("ATTENDANCE_STATUS_ID_LEAVE_APPLIED");
                var absentExcusedID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("ATTENDANCE_STATUS_ID_ABSENT_EXCUSED");
                var leaveApprovedStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<string>("LEAVE_APPROVED_STATUS_ID");

                var leaveAppliedStatusID = byte.Parse(leaveAppliedID);
                var absentExcusedStatusID = byte.Parse(absentExcusedID);
                var approvedStatusID = byte.Parse(leaveApprovedStatusID);

                for (DateTime aDate = entity.FromDate.Value.Date; aDate.Date <= entity.ToDate.Value.Date; aDate = aDate.AddDays(1))
                {
                    var previousAttendance = dbContext.StudentAttendences.Where(a => a.StudentID == entity.StudentID && a.AttendenceDate.Value.Date == aDate.Date).AsNoTracking().FirstOrDefault();

                    if (previousAttendance != null)
                    {
                        previousAttendance.UpdatedBy = entity.UpdatedBy;
                        previousAttendance.UpdatedDate = DateTime.Now;

                        if (entity.LeaveStatusID == approvedStatusID)
                        {
                            previousAttendance.PresentStatusID = absentExcusedStatusID;
                        }
                        else if (entity.LeaveStatusID == leaveAppliedStatusID)
                        {
                            previousAttendance.PresentStatusID = leaveAppliedStatusID;
                        }

                        dbContext.Entry(previousAttendance).State = EntityState.Modified;
                    }
                    else
                    {
                        var studAttendance = new StudentAttendence()
                        {
                            StudentAttendenceIID = previousAttendance != null ? previousAttendance.StudentAttendenceIID : 0,
                            StudentID = entity.StudentID,
                            AttendenceDate = aDate,
                            PresentStatusID = leaveAppliedStatusID,
                            StartTime = null,
                            EndTime = null,
                            ClassID = studentDetail.ClassID,
                            SectionID = studentDetail.SectionID,
                            Reason = entity.Reason,
                            CreatedBy = entity.CreatedBy,
                            CreatedDate = entity.CreatedDate,
                            UpdatedBy = entity.UpdatedBy,
                            UpdatedDate = entity.UpdatedDate,
                            SchoolID = studentDetail.SchoolID == null ? (byte)_context.SchoolID : studentDetail.SchoolID,
                            AcademicYearID = studentDetail.AcademicYearID == null ? (int)_context.AcademicYearID : studentDetail.AcademicYearID,
                            AttendenceReasonID = null,
                        };

                        dbContext.Entry(studAttendance).State = EntityState.Added;
                    }
                    dbContext.SaveChanges();
                }                
            }
        }

        public StudentLeaveApplicationDTO GetStudentLeaveCountByStudentID(long studentID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var attendences = new StudentLeaveApplicationDTO();

                var applicationSubmittedData = dbContext.StudentLeaveApplications.Where(a => a.StudentID == studentID && a.LeaveStatusID == 1).AsNoTracking().ToList();
                var applicationApprovedData = dbContext.StudentLeaveApplications.Where(a => a.StudentID == studentID && a.LeaveStatusID == 2).AsNoTracking().ToList();
                var applicationRejectedData = dbContext.StudentLeaveApplications.Where(a => a.StudentID == studentID && a.LeaveStatusID == 3).AsNoTracking().ToList();

                attendences.ApplicationSubmittedCount = applicationSubmittedData.Count > 0 ? applicationSubmittedData.Count : 0;
                attendences.ApplicationApprovedCount = applicationApprovedData.Count > 0 ? applicationApprovedData.Count : 0;
                attendences.ApplicationRejectedCount = applicationRejectedData.Count > 0 ? applicationRejectedData.Count : 0;

                return attendences;
            }
        }

        public string SubmitStudentLeaveApplication(StudentLeaveApplicationDTO leaveData)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var studentData = dbContext.Students.Where(s => s.StudentIID == leaveData.StudentID)
                        .AsNoTracking()
                        .FirstOrDefault();

                var entity = new StudentLeaveApplication()
                {
                    StudentLeaveApplicationIID = leaveData.StudentLeaveApplicationIID,
                    ClassID = studentData.ClassID ?? 0,
                    StudentID = leaveData.StudentID,
                    FromSessionID = leaveData.FromSessionID ?? 0,
                    ToSessionID = leaveData.ToSessionID ?? 0,
                    LeaveStatusID = leaveData.LeaveStatusID,
                    Reason = leaveData.Reason,
                    FromDate = leaveData.FromDate,
                    ToDate = leaveData.ToDate,
                    Remarks = leaveData.Remarks,
                    SchoolID = studentData.SchoolID,
                    AcademicYearID = studentData.AcademicYearID,
                    CreatedBy = leaveData.StudentLeaveApplicationIID == 0 ? (int)_context.LoginID : leaveData.CreatedBy,
                    UpdatedBy = leaveData.StudentLeaveApplicationIID > 0 ? (int)_context.LoginID : leaveData.UpdatedBy,
                    CreatedDate = leaveData.StudentLeaveApplicationIID == 0 ? DateTime.Now : leaveData.CreatedDate,
                    UpdatedDate = leaveData.StudentLeaveApplicationIID > 0 ? DateTime.Now : leaveData.UpdatedDate,
                };

                if (entity.StudentLeaveApplicationIID == 0)
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    dbContext.SaveChanges();

                    UpdateAttendanceStatus(entity);
                    SaveAlertsNotification(entity, studentData);
                }
                else
                {
                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    dbContext.SaveChanges();
                }

                Workflows.Helpers.WorkflowGeneratorHelper.GenerateWorkflows(1, entity.StudentLeaveApplicationIID);
            }
            return "Saved successfully!";
        }

    }
}
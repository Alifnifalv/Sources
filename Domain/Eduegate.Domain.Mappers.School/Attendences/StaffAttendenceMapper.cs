using Newtonsoft.Json;
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Attendences;
using Eduegate.Framework;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Eduegate.Domain.Entity.HR;
using Eduegate.Domain.Entity.HR.Payroll;
using System.Globalization;
using Eduegate.Domain.Mappers.Notification.Notifications;
using Eduegate.Domain.Mappers.Notification.Notifications.Helpers;

namespace Eduegate.Domain.Mappers.School.Attendences
{
    public class StaffAttendenceMapper : DTOEntityDynamicMapper
    {
        public static StaffAttendenceMapper Mapper(CallContext context)
        {
            var mapper = new StaffAttendenceMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<StaffAttendenceDTO>(entity);
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        private StaffAttendenceDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.StaffAttendences.Where(x => x.StaffAttendenceIID == IID)
                    .Include(x => x.Employee)
                    .Include(x => x.StaffPresentStatus)
                    .Include(x => x.AttendenceReason)
                    .AsNoTracking()
                    .FirstOrDefault();

                return ToDTO(entity);
            }
        }

        private StaffAttendenceDTO ToDTO(StaffAttendence entity)
        {
            return new StaffAttendenceDTO()
            {
                StaffAttendenceIID = entity.StaffAttendenceIID,
                EmployeeID = entity.EmployeeID,
                EmployeeName = entity.Employee != null ? entity.Employee.EmployeeCode + " - " + entity.Employee.FirstName + " " + entity.Employee.MiddleName + " " + entity.Employee.LastName : null,
                PresentStatusTitle = entity.PresentStatusID.HasValue ? entity.StaffPresentStatus.StatusTitle : null,
                AttendenceDate = entity.AttendenceDate,
                PresentStatusID = entity.PresentStatusID,
                StartTime = entity.StartTime,
                EndTime = entity.EndTime,
                Reason = entity.Reason,
                AttendenceReasonID = entity.AttendenceReasonID,
                CreatedBy = entity.CreatedBy,
                UpdatedBy = entity.UpdatedBy,
                CreatedDate = entity.CreatedDate,
                UpdatedDate = entity.UpdateDate,
                //TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
            };
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as StaffAttendenceDTO;

            if (!toDto.EmployeeID.HasValue)
            {
                throw new Exception("Please select employee");
            }

            //convert the dto to entity and pass to the repository.
            var entity = new StaffAttendence()
            {
                StaffAttendenceIID = toDto.StaffAttendenceIID,
                AttendenceDate = toDto.AttendenceDate,
                EmployeeID = toDto.EmployeeID,
                PresentStatusID = toDto.PresentStatusID,
                StartTime = toDto.StartTime,
                EndTime = toDto.EndTime,
                SchoolID = toDto.SchoolID.HasValue ? toDto.SchoolID : Convert.ToByte(_context.SchoolID),
                AcademicYearID = toDto.AcademicYearID.HasValue ? toDto.AcademicYearID : _context.AcademicYearID,
                AttendenceReasonID = toDto.AttendenceReasonID,
                Reason = toDto.Reason,
                //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
                if (entity.StaffAttendenceIID == 0)
                {
                    var attendenceDetails = dbContext.StaffAttendences.Where(x => x.EmployeeID == entity.EmployeeID && (x.AttendenceDate.Value.Day == entity.AttendenceDate.Value.Day && x.AttendenceDate.Value.Month == entity.AttendenceDate.Value.Month && x.AttendenceDate.Value.Year == entity.AttendenceDate.Value.Year)).AsNoTracking().FirstOrDefault();

                    if (attendenceDetails != null)
                    {
                        attendenceDetails.PresentStatusID = entity.PresentStatusID;
                        attendenceDetails.AttendenceReasonID = entity.AttendenceReasonID;
                        attendenceDetails.Reason = entity.Reason;
                        attendenceDetails.UpdatedBy = Convert.ToInt32(_context.LoginID);
                        attendenceDetails.UpdateDate = DateTime.Now;

                        if (attendenceDetails.SchoolID != entity.SchoolID)
                        {
                            attendenceDetails.SchoolID = entity.SchoolID;
                        }

                        dbContext.Entry(attendenceDetails).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                        entity.StaffAttendenceIID = attendenceDetails.StaffAttendenceIID;
                    }
                    else
                    {
                        entity.CreatedBy = Convert.ToInt32(_context.LoginID);
                        entity.CreatedDate = DateTime.Now;

                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    }
                }
                else
                {
                    entity.UpdatedBy = Convert.ToInt32(_context.LoginID);
                    entity.UpdateDate = DateTime.Now;

                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                //dbContext.StaffAttendences.Add(entity);
                dbContext.SaveChanges();

                byte? presentStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<byte?>("STAFFSTATUSID_PRESENT", 0, 1);

                if (entity.PresentStatusID == presentStatusID)
                {
                    var employeeData = dbContext.Employees.Where(e => e.EmployeeIID == entity.EmployeeID).AsNoTracking().FirstOrDefault();

                    var settings = NotificationSetting.GetEmployeeAppSettings();
                    var title = "Today's Attendance Marked";
                    var message = "Hi " + employeeData.FirstName + " " + employeeData?.MiddleName + " " + employeeData?.LastName + " your attendance is marked today :" + DateTime.Now;
                    PushNotificationMapper.SendAndSavePushNotification((long)(employeeData?.LoginID), (long)_context.LoginID, message, title, settings);
                }
            }

            return ToDTOString(ToDTO(entity.StaffAttendenceIID));
        }

        public void MarkStaffAttendanceByEmployeeID(long? employeeID, long? branchID, long loginID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var currentDateWithTime = DateTime.Now;
                var todayDate = currentDateWithTime.Date;

                var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateTimeFormatWithAnnotation");
                var currentDateString = currentDateWithTime.ToString(dateFormat, CultureInfo.InvariantCulture);

                var savedMessage = string.Empty;

                var presentStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<byte>("STAFFSTATUSID_PRESENT", 1);

                var unmarkStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<byte>("STAFFSTATUSID_UNMARKED", 7);

                var currentAcademicYearStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<int>("CURRENT_ACADEMIC_YEAR_STATUSID", 2);

                var defaultTaskID = new Domain.Setting.SettingBL(null).GetSettingValue<long>("DEFAULT_TASKID", 1);

                var academicYearData = branchID.HasValue ? dbContext.AcademicYears.Where(x => x.SchoolID == branchID && x.AcademicYearStatusID == currentAcademicYearStatusID).AsNoTracking().FirstOrDefault() : null;

                var staffAttendanceData = dbContext.StaffAttendences.Where(a => a.EmployeeID == employeeID && (a.AttendenceDate.Value.Day == todayDate.Day && a.AttendenceDate.Value.Month == todayDate.Month && a.AttendenceDate.Value.Year == todayDate.Year)).AsNoTracking().FirstOrDefault();

                string employeeName = null;

                try
                {
                    savedMessage = null;
                    if (staffAttendanceData == null)
                    {
                        var entity = new StaffAttendence()
                        {
                            StaffAttendenceIID = 0,
                            AttendenceDate = todayDate,
                            EmployeeID = employeeID,
                            PresentStatusID = presentStatusID,
                            StartTime = currentDateWithTime.TimeOfDay,
                            EndTime = currentDateWithTime.TimeOfDay,
                            SchoolID = branchID.HasValue ? Convert.ToByte(branchID) : (byte?)null,
                            AcademicYearID = academicYearData != null ? academicYearData.AcademicYearID : (int?)null,
                            CreatedBy = (int)_context.LoginID,
                            CreatedDate = currentDateWithTime,
                        };

                        //dbContext.StaffAttendences.Add(entity);

                        if (entity.StaffAttendenceIID == 0)
                        {
                            dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        }

                        dbContext.SaveChanges();

                        savedMessage = "Attendance marked successfully";
                        var employee = dbContext.Employees.Where(a => a.EmployeeIID == entity.EmployeeID).AsNoTracking().FirstOrDefault();
                        employeeName = employee?.FirstName + " " + employee?.MiddleName + " " + employee?.LastName;
                    }
                    else
                    {
                        if (staffAttendanceData.PresentStatusID == unmarkStatusID)
                        {
                            staffAttendanceData.PresentStatusID = presentStatusID;
                            staffAttendanceData.UpdatedBy = (int)_context.LoginID;
                            staffAttendanceData.UpdateDate = currentDateWithTime;

                            dbContext.Entry(staffAttendanceData).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                            dbContext.SaveChanges();
                            savedMessage = "Attendance updated successfully";
                        }
                    }

                    if (!string.IsNullOrEmpty(savedMessage))
                    {
                        var settings = NotificationSetting.GetEmployeeAppSettings();
                        var title = "Today's Attendance Marked";
                        var message = "Hi " + employeeName + " your attendance is marked today :" + currentDateString;
                        PushNotificationMapper.SendAndSavePushNotification(loginID, loginID, message, title, settings);
                    }

                }
                catch (Exception ex)
                {
                    var errorMessage = ex.Message;
                    savedMessage = null;
                }
                #region Inserting into  EmployeeTimeSheets

                using (var dbHRContext = new dbEduegateHRContext())
                {
                    var entities = dbHRContext.EmployeeTimeSheets.Where(x =>
                    x.EmployeeID == employeeID && x.TimesheetDate == currentDateWithTime.Date).AsNoTracking().FirstOrDefault();

                    if (entities == null)
                    {
                        var entity = new EmployeeTimeSheet()
                        {
                            EmployeeID = employeeID.Value,
                            TimesheetDate = currentDateWithTime,
                            TaskID = defaultTaskID,
                            CreatedBy = (int)_context.LoginID,
                            CreatedDate = currentDateWithTime,

                        };

                        dbHRContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        dbHRContext.SaveChanges();
                    }
                }
                #endregion Inserting into  EmployeeTimeSheets
            }
        }

        public List<StaffAttendenceDTO> GetStaffAttendanceByMonthYear(int month, int year)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var attendences = new List<StaffAttendenceDTO>();
                var entities = dbContext.StaffAttendences.Where(a => a.AttendenceDate.Value.Month == (month + 1) && a.AttendenceDate.Value.Year == year)
                    .Include(x => x.Employee)
                    .Include(x => x.StaffPresentStatus)
                    .Include(x => x.AttendenceReason)
                    .AsNoTracking()
                    .ToList();

                foreach (var attendence in entities)
                {
                    attendences.Add(ToDTO(attendence));
                }

                return attendences;
            }
        }

        public List<PresentStatusDTO> GetStaffPresentStatuses()
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var presentStatuses = new List<PresentStatusDTO>();

                var entities = dbContext.StaffPresentStatuses.Where(a => a.StatusTitle != null).AsNoTracking().ToList();

                foreach (var status in entities)
                {
                    presentStatuses.Add(new PresentStatusDTO()
                    {
                        PresentStatusID = status.StaffPresentStatusID,
                        StatusTitle = status.StatusTitle,
                        StatusDescription = status.StatusDescription
                    });
                }

                return presentStatuses;
            }
        }

        public StaffAttendenceDTO GetStaffAttendence(long staffID, DateTime date)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.StaffAttendences.Where(a => a.EmployeeID == staffID && (a.AttendenceDate.Value.Day == date.Day && a.AttendenceDate.Value.Month == date.Month && a.AttendenceDate.Value.Year == date.Year))
                    .Include(x => x.Employee)
                    .Include(x => x.StaffPresentStatus)
                    .Include(x => x.AttendenceReason)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (entity != null)
                {
                    return ToDTO(entity);
                }
                else
                {
                    return null;
                }
            }
        }

        public string SaveStaffAttendance(BaseMasterDTO dto)
        {
            var toDto = dto as StaffAttendenceDTO;

            if (!toDto.EmployeeID.HasValue)
            {
                return "0#Please select a student";
            }

            //convert the dto to entity and pass to the repository.
            var entity = new StaffAttendence()
            {
                StaffAttendenceIID = toDto.StaffAttendenceIID,
                EmployeeID = toDto.EmployeeID,
                AttendenceDate = toDto.AttendenceDate,
                PresentStatusID = toDto.PresentStatusID,
                StartTime = toDto.StartTime,
                EndTime = toDto.EndTime,
                Reason = toDto.Reason,
                AttendenceReasonID = toDto.AttendenceReasonID,
                SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                AcademicYearID = toDto.AcademicYearID == null ? (int)_context.AcademicYearID : toDto.AcademicYearID,
                CreatedBy = toDto.StaffAttendenceIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.StaffAttendenceIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.StaffAttendenceIID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdateDate = toDto.StaffAttendenceIID > 0 ? DateTime.Now : dto.UpdatedDate,
                //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var attendenceDetails = dbContext.StaffAttendences.Where(x => x.EmployeeID == entity.EmployeeID && (x.AttendenceDate.Value.Day == entity.AttendenceDate.Value.Day && x.AttendenceDate.Value.Month == entity.AttendenceDate.Value.Month && x.AttendenceDate.Value.Year == entity.AttendenceDate.Value.Year))
                    .AsNoTracking()
                    .FirstOrDefault();

                if (attendenceDetails != null)
                {
                    attendenceDetails.PresentStatusID = entity.PresentStatusID;
                    attendenceDetails.AttendenceReasonID = entity.AttendenceReasonID;
                    attendenceDetails.Reason = entity.Reason;
                    attendenceDetails.UpdatedBy = Convert.ToInt32(_context.LoginID);
                    attendenceDetails.UpdateDate = DateTime.Now;

                    if (attendenceDetails.SchoolID != entity.SchoolID)
                    {
                        attendenceDetails.SchoolID = entity.SchoolID;
                    }

                    dbContext.Entry(attendenceDetails).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                else
                {
                    if (entity.StaffAttendenceIID == 0)
                    {
                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    }
                    else
                    {
                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }
                }
                dbContext.SaveChanges();
            }

            return "1#Saved Successfully";
        }

        public List<StaffAttendenceDTO> GetStaffAttendenceByYearMonthEmployeeID(int month, int year)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var attendences = new List<StaffAttendenceDTO>();
                try
                {
                    var empDet = dbContext.Employees.Where(x => x.LoginID == _context.LoginID).AsNoTracking().FirstOrDefault();
                    var employeeID = _context.EmployeeID.HasValue ? _context.EmployeeID : empDet?.EmployeeIID;

                    var entities = dbContext.StaffAttendences.Where(x => x.AttendenceDate.Value.Month == (month + 1) && x.AttendenceDate.Value.Year == year && x.EmployeeID == employeeID).OrderBy(b => b.AttendenceDate)
                        .Include(x => x.Employee)
                        .Include(x => x.StaffPresentStatus)
                        .Include(x => x.AttendenceReason)
                        .AsNoTracking().ToList();

                    foreach (var attendence in entities)
                    {
                        attendences.Add(ToDTO(attendence));
                    }
                }
                catch (Exception ex)
                {
                    var data = ex.Message;
                }
                return attendences;
            }
        }

        public StaffAttendenceDTO GetStaffAttendenceCountByEmployeeID(int month, int year, long employeeID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var attendences = new StaffAttendenceDTO();

                var presentSatusID = new Domain.Setting.SettingBL(null).GetSettingValue<byte>("STAFFSTATUSID_PRESENT", 1);
                var absentSatusID = new Domain.Setting.SettingBL(null).GetSettingValue<byte>("STAFFSTATUSID_ABSENT", 2);

                var staffAttendances = dbContext.StaffAttendences
                    .Where(a => a.AttendenceDate.Value.Month == month && a.AttendenceDate.Value.Year == year && a.EmployeeID == employeeID)
                    .AsNoTracking().ToList();

                var staffPresentData = staffAttendances.Where(p => p.PresentStatusID == presentSatusID).ToList();
                var staffAbsentData = staffAttendances.Where(a => a.PresentStatusID == absentSatusID).ToList();

                attendences.StaffPresentCount = staffPresentData != null ? staffPresentData.Count : 0;
                attendences.StaffAbsentCount = staffAbsentData != null ? staffAbsentData.Count : 0;

                return attendences;
            }
        }

        public StaffAttendenceDTO GetTodayStaffAttendanceByLoginID()
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var attendences = new StaffAttendenceDTO();
                var currentDate = DateTime.Now;
                var loginID = _context.LoginID;

                var attendanceData = dbContext.StaffAttendences
                    .AsNoTracking()
                    .Include(x => x.StaffPresentStatus)
                    .FirstOrDefault(x => x.Employee.LoginID == loginID &&
                    x.AttendenceDate.Value.Day == currentDate.Day && x.AttendenceDate.Value.Month == currentDate.Month && x.AttendenceDate.Value.Year == currentDate.Year);

                if (attendanceData != null)
                {
                    attendences.AttendenceDate = attendanceData.AttendenceDate;
                    attendences.PresentStatus = attendanceData?.StaffPresentStatus?.StatusDescription;
                }

                return attendences;
            }
        }

    }
}
using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Eduegate.Domain.Mappers.Notification;
using Eduegate.Domain.Mappers.Notification.Helpers;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Frameworks.DynamicEntityMapper.Mappers;
using Eduegate.Services.Contracts.School.Attendences;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Globalization;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Domain.Entity;
using System.Data;
using System.Data.SqlClient;

namespace Eduegate.Domain.Mappers.School.Attendences
{
    public class StudentAttendenceMapper : DTOEntityDynamicMapper
    {
        public static StudentAttendenceMapper Mapper(CallContext context)
        {
            var mapper = new StudentAttendenceMapper();
            mapper._context = context;
            return mapper;
        }

        public override BaseMasterDTO ToDTO(string entity)
        {
            return JsonConvert.DeserializeObject<StudentAttendenceDTO>(entity);
        }

        public List<StudentAttendenceDTO> ToDTO(List<StudentAttendence> entities)
        {
            var attendences = new List<StudentAttendenceDTO>();

            foreach (var entity in entities)
            {
                attendences.Add(ToDTO(entity));
            }

            return attendences;
        }

        public override string ToDTOString(BaseMasterDTO dto)
        {
            return dto == null ? null : JsonConvert.SerializeObject(dto);
        }

        public override string GetEntity(long IID)
        {
            return ToDTOString(ToDTO(IID));
        }

        public List<StudentAttendenceDTO> GetStudentAttendence(long studentID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entities = dbContext.StudentAttendences.Where(a => a.StudentID == studentID)
                    .Include(i => i.Student)
                    .Include(i => i.PresentStatus)
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .Include(i => i.AcademicYear)
                    .Include(i => i.AttendenceReason)
                    .AsNoTracking()
                    .ToList();

                if (entities != null && entities.Count > 0)
                {
                    return ToDTO(entities);
                }
                else
                {
                    return null;
                }
            }
        }

        public StudentAttendenceDTO GetStudentAttendence(long studentID, DateTime date)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.StudentAttendences.Where(a => a.AttendenceDate == date && a.StudentID == studentID)
                    .Include(i => i.Student)
                    .Include(i => i.PresentStatus)
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .Include(i => i.AcademicYear)
                    .Include(i => i.AttendenceReason)
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

        private StudentAttendenceDTO ToDTO(long IID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var entity = dbContext.StudentAttendences.Where(a => a.StudentAttendenceIID == IID)
                    .Include(i => i.Student)
                    .Include(i => i.PresentStatus)
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .Include(i => i.AcademicYear)
                    .Include(i => i.AttendenceReason)
                    .AsNoTracking()
                    .FirstOrDefault();

                return ToDTO(entity);
            }
        }

        private StudentAttendenceDTO ToDTO(StudentAttendence entity)
        {
            return new StudentAttendenceDTO()
            {
                StudentAttendenceIID = entity.StudentAttendenceIID,
                StudentID = entity.StudentID,
                StudentName = entity.Student != null ? entity.Student.FirstName + " " + entity.Student.MiddleName + " " + entity.Student.LastName : null,
                AttendenceDate = entity.AttendenceDate,
                PresentStatusID = entity.PresentStatusID,
                PresentStatus = entity.PresentStatusID.HasValue ? entity.PresentStatus.StatusDescription : null,
                PresentStatusTitle = entity.PresentStatusID.HasValue ? entity.PresentStatus.StatusTitle : null,
                AdmissionDate = entity.StudentID.HasValue ? entity.Student.AdmissionDate : null,
                FeeStartDate = entity.StudentID.HasValue ? entity.Student.FeeStartDate : null,
                StartTime = entity.StartTime,
                EndTime = entity.EndTime,
                ClassID = entity.ClassID,
                ClassName = entity.Class != null ? entity.Class.ClassDescription : null,
                SectionID = entity.SectionID,
                SectionName = entity.Section != null ? entity.Section.SectionName : null,
                AcademicYearID = entity.AcademicYearID,
                SchoolID = entity.SchoolID,
                //TimeStamps = entity.TimeStamps == null ? null : Convert.ToBase64String(entity.TimeStamps),
                Reason = entity.Reason,
                AttendenceReasonID = entity.AttendenceReasonID
            };
        }

        public override string SaveEntity(BaseMasterDTO dto)
        {
            var toDto = dto as StudentAttendenceDTO;

            if (toDto.ClassID == null || toDto.ClassID == 0)
            {
                throw new Exception("Please select any Class");
            }

            if (toDto.SectionID == null || toDto.SectionID == 0)
            {
                throw new Exception("Please select any Section");
            }

            if (toDto.StudentID == null || toDto.StudentID == 0)
            {
                throw new Exception("Please select a Student");
            }

            //if (toDto.PresentStatusID == 1 && toDto.AttendenceReasonID == 0 || toDto.PresentStatusID == 1 && toDto.AttendenceReasonID == null) 
            //{
            //    throw new Exception("Please select absense reason");
            //}
            //if (toDto.PresentStatusID == 1 && toDto.Reason == null || toDto.PresentStatusID == 1 && toDto.Reason == "")
            //{
            //    throw new Exception("Please fill absense reason note");
            //}
            //if (toDto.PresentStatusID == 4 && toDto.AttendenceReasonID == 0 || toDto.PresentStatusID == 4 && toDto.AttendenceReasonID == null)
            //{
            //    throw new Exception("Please select late reason");
            //}
            //if (toDto.PresentStatusID == 4 && toDto.Reason == null || toDto.PresentStatusID == 4 && toDto.Reason == "")
            //{
            //    throw new Exception("Please fill late reason note");
            //}

            //convert the dto to entity and pass to the repository.
            var entity = new StudentAttendence()
            {
                StudentAttendenceIID = toDto.StudentAttendenceIID,
                StudentID = toDto.StudentID,
                AttendenceDate = toDto.AttendenceDate,
                PresentStatusID = toDto.PresentStatusID,
                StartTime = toDto.StartTime,
                EndTime = toDto.EndTime,
                ClassID = toDto.ClassID,
                SectionID = toDto.SectionID,
                Reason = toDto.Reason,
                SchoolID = toDto.SchoolID == null ? (byte)_context.SchoolID : toDto.SchoolID,
                AcademicYearID = toDto.AcademicYearID == null ? (int)_context.AcademicYearID : toDto.AcademicYearID,
                AttendenceReasonID = toDto.AttendenceReasonID,
                //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
                dbContext.StudentAttendences.Add(entity);

                if (entity.StudentAttendenceIID == 0)
                {
                    var attendenceDetails = dbContext.StudentAttendences.Where(x => x.StudentID == entity.StudentID && x.AttendenceDate == entity.AttendenceDate).AsNoTracking().FirstOrDefault();

                    if (attendenceDetails != null)
                    {
                        attendenceDetails.PresentStatusID = entity.PresentStatusID;
                        attendenceDetails.AttendenceReasonID = entity.AttendenceReasonID;
                        attendenceDetails.Reason = entity.Reason;
                        attendenceDetails.UpdatedBy = Convert.ToInt32(_context.LoginID);
                        attendenceDetails.UpdatedDate = DateTime.Now;

                        dbContext.Entry(attendenceDetails).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                        entity.StudentAttendenceIID = attendenceDetails.StudentAttendenceIID;
                    }
                    else
                    {
                        entity.CreatedBy = Convert.ToInt32(_context.LoginID);
                        entity.CreatedDate = DateTime.Now;

                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        //entity = repository.Insert(entity);
                    }
                }
                else
                {
                    entity.UpdatedBy = Convert.ToInt32(_context.LoginID);
                    entity.UpdatedDate = DateTime.Now;

                    dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                    //entity = repository.Update(entity);
                }
                dbContext.SaveChanges();
            }

            return ToDTOString(ToDTO(entity.StudentAttendenceIID));
        }

        public string SaveStudentAttendence(BaseMasterDTO dto)
        {
            var toDto = dto as StudentAttendenceDTO;

            if (toDto.StudentID == null || toDto.StudentID == 0)
            {
                return "0#Please select a student";
            }

            if (toDto.ClassID == null)
            {
                using (var dbContext = new dbEduegateSchoolContext())
                {
                    var student = dbContext.Students.Where(s => s.StudentIID == toDto.StudentID).AsNoTracking().FirstOrDefault();

                    toDto.ClassID = student.ClassID;
                    toDto.SectionID = student.SectionID;
                    toDto.ClassName = student.Class.ClassDescription;
                    toDto.CreatedBy = student.CreatedBy;
                }
            }
            //convert the dto to entity and pass to the repository.
            var entity = new StudentAttendence()
            {
                StudentAttendenceIID = toDto.StudentAttendenceIID,
                StudentID = toDto.StudentID,
                AttendenceDate = toDto.AttendenceDate,
                PresentStatusID = toDto.PresentStatusID,
                StartTime = toDto.StartTime,
                EndTime = toDto.EndTime,
                ClassID = toDto.ClassID,
                SectionID = toDto.SectionID,
                Reason = toDto.Reason,
                AttendenceReasonID = toDto.AttendenceReasonID,
                SchoolID = toDto.SchoolID == null ? _context.SchoolID.HasValue ? (byte?)_context.SchoolID : null : toDto.SchoolID,
                AcademicYearID = toDto.AcademicYearID == null ? _context.AcademicYearID : toDto.AcademicYearID,
                CreatedBy = toDto.StudentAttendenceIID == 0 ? (int)_context.LoginID : dto.CreatedBy,
                UpdatedBy = toDto.StudentAttendenceIID > 0 ? (int)_context.LoginID : dto.UpdatedBy,
                CreatedDate = toDto.StudentAttendenceIID == 0 ? DateTime.Now : dto.CreatedDate,
                UpdatedDate = toDto.StudentAttendenceIID > 0 ? DateTime.Now : dto.UpdatedDate,
                //TimeStamps = toDto.TimeStamps == null ? null : Convert.FromBase64String(toDto.TimeStamps),
            };

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var attendenceDetails = dbContext.StudentAttendences.Where(x => x.StudentID == entity.StudentID && x.AttendenceDate == entity.AttendenceDate).AsNoTracking().FirstOrDefault();

                if (attendenceDetails != null)
                {
                    attendenceDetails.PresentStatusID = entity.PresentStatusID;
                    attendenceDetails.AttendenceReasonID = entity.AttendenceReasonID;
                    attendenceDetails.Reason = entity.Reason;
                    attendenceDetails.UpdatedBy = Convert.ToInt32(_context.LoginID);
                    attendenceDetails.UpdatedDate = DateTime.Now;

                    if (attendenceDetails.ClassID != entity.ClassID)
                    {
                        attendenceDetails.ClassID = entity.ClassID;
                    }

                    if (attendenceDetails.SectionID != entity.SectionID)
                    {
                        attendenceDetails.SectionID = entity.SectionID;
                    }

                    if (attendenceDetails.SchoolID != entity.SchoolID)
                    {
                        attendenceDetails.SchoolID = entity.SchoolID;
                    }

                    dbContext.Entry(attendenceDetails).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                else
                {
                    if (entity.StudentAttendenceIID == 0)
                    {
                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    }
                    else
                    {
                        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }
                }
                dbContext.SaveChanges();

                var lateStatusID = new Domain.Setting.SettingBL(null).GetSettingValue<byte>("STUDENT_ATTENDANCE_STATUS_ID_LATE", 4);

                if (entity != null && entity.PresentStatusID == lateStatusID && attendenceDetails == null)
                {
                    SendLateNotification(entity);
                }
            }

            return "1#Saved Successfully";
        }

        //Send Late notification to parents & staffs for single student wise
        public bool SendLateNotification(StudentAttendence attend)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var attendance = dbContext.StudentAttendences.Where(a => a.StudentAttendenceIID == attend.StudentAttendenceIID)
                    .Include(i => i.PresentStatus)
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .Include(i => i.Student).ThenInclude(i => i.Parent)
                    .Include(i => i.Student).ThenInclude(i => i.Class)
                    .Include(i => i.Student).ThenInclude(i => i.Section)
                    .Include(i => i.Student).ThenInclude(i => i.School)
                    .AsNoTracking().FirstOrDefault();

                if (attendance != null)
                {
                    var classCode = attendance.Student?.Class?.Code?.ToLower();

                    if (string.IsNullOrEmpty(classCode))
                    {
                        var classID = attendance.Student?.ClassID;
                        if (classID.HasValue)
                        {
                            var data = new Eduegate.Domain.Setting.SettingBL(_context).GetClassDetailByClassID(classID.Value);

                            classCode = data?.Code?.ToLower();
                        }
                    }

                    var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

                    var todayDate = DateTime.Now;

                    if (attendance.AttendenceDate.Value.Day == todayDate.Day && attendance.AttendenceDate.Value.Month == todayDate.Month && attendance.AttendenceDate.Value.Year == todayDate.Year)
                    {
                        #region Notifications send to Parent
                        #region mail notification
                        var studFullName = attendance.Student?.AdmissionNumber + " - " + attendance.Class?.ClassDescription + "/" + attendance.Section?.SectionName + " : " + attendance.Student?.FirstName + " " + attendance.Student?.MiddleName + " " + attendance.Student?.LastName;
                        var toMailID = attendance.Student?.Parent?.GaurdianEmail != null ? attendance.Student?.Parent?.GaurdianEmail : attendance.Student?.Parent?.FatherEmailID;

                        if (toMailID != null)
                        {
                            //Send Mail Notification
                            String emailDetails = "";
                            String emailSub = "";

                            emailSub = "Student Recorded as " + attendance.PresentStatus?.StatusDescription.ToUpper();
                            var reportStatus = " is late in the class on ";
                            emailDetails = @" <br/> <h3> Dear Parent,</h3>
                                                <p>Your child " + studFullName + @" of "
                                                + attendance.Student?.Class?.ClassDescription + " " + attendance.Student?.Section?.SectionName + reportStatus + todayDate.ToString(dateFormat, CultureInfo.InvariantCulture) +
                                                @"</p><p> Remarks : "+ attendance.Reason+
                                                @"</p><p>Please reach out to the school for any queries.</p><br/>
                                                <h4>Thank You</h4>
                                                " + attendance.Student?.School?.SchoolName + @"
                                                <br/>";

                            string mailMessage = new Eduegate.Domain.Notification.EmailNotificationBL(_context).PopulateBody(toMailID, emailDetails);

                            var hostDet = new Domain.Setting.SettingBL(null).GetSettingValue<string>("HOST_NAME");

                            string defaultMail = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DEFAULT_MAIL_ID");

                            var mailParameters = new Dictionary<string, string>()
                            {
                                { "CLASS_CODE", classCode},
                            };

                            if (emailDetails != "")
                            {
                                if (hostDet.ToLower() == "live")
                                {
                                    new Eduegate.Domain.Notification.EmailNotificationBL(_context).SendMail(toMailID, emailSub, mailMessage, EmailTypes.Attendance, mailParameters);
                                }
                                else
                                {
                                    new Eduegate.Domain.Notification.EmailNotificationBL(_context).SendMail(defaultMail, emailSub, mailMessage, EmailTypes.Attendance, mailParameters);
                                }
                            }
                        }
                        #endregion

                        #region push notification
                        long fromLoginID = _context?.LoginID != null ? (long)_context.LoginID : 2;
                        var settings = NotificationSetting.GetParentAppSettings();
                        var title = "Attendance : " + todayDate.ToString(dateFormat, CultureInfo.InvariantCulture);
                        var message = studFullName + " is " + attendance.PresentStatus?.StatusDescription.ToUpper() + " today. Remarks :" + attendance.Reason;
                        long toLoginID = (long)attendance.Student.Parent?.LoginID;

                        if (toLoginID != 0)
                        {
                            PushNotificationMapper.SendAndSavePushNotification(toLoginID, fromLoginID, message, title, settings);
                        }
                        #endregion

                        #endregion

                        #region push notification sendTo >> RouteDriver,TransportCordinators,TransportSupervisor

                        var driverSheduleDat = dbContext.DriverScheduleLogs.Where(d => d.ScheduleLogType == "PICK-IN" &&
                        d.SheduleDate.Value.Day == todayDate.Day && d.SheduleDate.Value.Month == todayDate.Month && d.SheduleDate.Value.Year == todayDate.Year && d.StudentID == attendance.StudentID)
                            .Include(i => i.Routes1)
                            .AsNoTracking().FirstOrDefault();

                        if (driverSheduleDat != null)
                        {
                            string designationIdsString = new Domain.Setting.SettingBL(null).GetSettingValue<string>("Student_Late_Notifications_To_Designations");
                            
                            List<int?> designationIds = designationIdsString
                                .Split(',')
                                .Select(x => int.TryParse(x, out int result) ? (int?)result : null)
                                .ToList();

                            var sendToEmp = dbContext.Employees.Where(e => (e.BranchID == _context.SchoolID && designationIds.Contains(e.DesignationID)) || (e.AssignVehicleMaps.Any(v => v.IsActive == true && v.RouteID == driverSheduleDat.RouteID))).AsNoTracking().ToList();

                            var empsettings = NotificationSetting.GetEmployeeAppSettings();
                            var emptitle = "Student Late Attendance Marked";
                            var empmessage = studFullName+ " using Bus No.:" + driverSheduleDat.Routes1?.RouteCode + " is " + attendance.PresentStatus?.StatusDescription.ToUpper() + " today ( " + todayDate.ToString(dateFormat, CultureInfo.InvariantCulture) + " ). Remarks: " + attendance.Reason;

                            foreach (var sendTo in sendToEmp)
                            {
                                if (sendTo.LoginID.HasValue)
                                {
                                    PushNotificationMapper.SendAndSavePushNotification((long)sendTo.LoginID, fromLoginID, empmessage, emptitle, empsettings);
                                }
                            }
                        }

                        #endregion 
                    }
                }
            }
            return true;
        }

        public List<StudentAttendenceDTO> GetStudentAttendenceByYearMonth(int month, int year, int classId, int sectionId)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var attendences = new List<StudentAttendenceDTO>();
                
                var entities = dbContext.StudentAttendences
                    .Where(a => a.AttendenceDate.Value.Month == (month + 1) && a.AttendenceDate.Value.Year == year && a.ClassID == classId && a.SectionID == sectionId)
                    .Include(i => i.Student)
                    .Include(i => i.PresentStatus)
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .Include(i => i.AcademicYear)
                    .Include(i => i.AttendenceReason)
                    .AsNoTracking()
                    .ToList();

                foreach (var attendence in entities)
                {
                    attendences.Add(ToDTO(attendence));
                }

                return attendences;
            }
        }

        public List<StudentAttendenceDTO> GetStudentAttendenceByYearMonthStudentId(int month, int year, long studentId)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var attendences = new List<StudentAttendenceDTO>();
                
                var entities = dbContext.StudentAttendences
                    .Where(a => a.AttendenceDate.Value.Month == (month + 1) && a.AttendenceDate.Value.Year == year && a.StudentID == studentId)
                    .Include(i => i.Student)
                    .Include(i => i.PresentStatus)
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .Include(i => i.AcademicYear)
                    .Include(i => i.AttendenceReason)
                    .AsNoTracking()
                    .ToList();

                foreach (var attendence in entities)
                {
                    attendences.Add(ToDTO(attendence));
                }

                return attendences;
            }
        }

        public List<StudentAttendenceDTO> GetStudentAttendenceByYearMonthClassSection(int month, int year, int classId, int sectionId)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var attendences = new List<StudentAttendenceDTO>();
                
                var entities = dbContext.StudentAttendences
                    .Where(a => a.AttendenceDate.Value.Month == (month + 1) && a.AttendenceDate.Value.Year == year && a.ClassID == classId && a.SectionID == sectionId)
                    .Include(i => i.Student)
                    .Include(i => i.PresentStatus)
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .Include(i => i.AcademicYear)
                    .Include(i => i.AttendenceReason)
                    .AsNoTracking()
                    .ToList();

                foreach (var attendence in entities)
                {
                    attendences.Add(ToDTO(attendence));
                }

                return attendences;
            }
        }

        public List<StudentAttendenceDTO> GetStudentAttendenceByDayClassSection(int month, int year, int classId, int sectionId,int day)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var attendences = new List<StudentAttendenceDTO>();
                
                var entities = dbContext.StudentAttendences
                    .Where(a => a.AttendenceDate.Value.Day == day && a.AttendenceDate.Value.Month == (month + 1) && a.AttendenceDate.Value.Year == year && a.ClassID == classId && a.SectionID == sectionId)
                    .Include(i => i.Student)
                    .Include(i => i.PresentStatus)
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .Include(i => i.AcademicYear)
                    .Include(i => i.AttendenceReason)
                    .AsNoTracking()
                    .ToList();

                foreach (var attendence in entities)
                {
                    attendences.Add(ToDTO(attendence));
                }

                return attendences;
            }
        }

        public StudentAttendenceDTO GetStudentAttendenceCountByStudentID(int month, int year, long studentID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var attendences = new StudentAttendenceDTO();

                var presentSatusID = new Domain.Setting.SettingBL(null).GetSettingValue<byte>("ATTENDANCE_STATUS_ID_PRESENT", 6);
                var absentSatusID = new Domain.Setting.SettingBL(null).GetSettingValue<byte>("ATTENDANCE_STATUS_ID_ABSENT", 1);
                var absentExcusedSatusID = new Domain.Setting.SettingBL(null).GetSettingValue<byte>("ATTENDANCE_STATUS_ID_ABSENT_EXCUSED", 2);

                var studentAttendances = dbContext.StudentAttendences.Where(a => a.AttendenceDate.Value.Month == (month) && a.AttendenceDate.Value.Year == year && a.StudentID == studentID).AsNoTracking().ToList();

                var studentPresentData = studentAttendances.Where(p => p.PresentStatusID == presentSatusID).OrderBy(b => b.AttendenceDate).ToList();
                var studentAbsentData = studentAttendances.Where(a => a.PresentStatusID == absentSatusID || a.PresentStatusID == absentExcusedSatusID).OrderBy(b => b.AttendenceDate).ToList();

                attendences.StudentPresentCount = studentPresentData.Count > 0 ? studentPresentData.Count : 0;
                attendences.StudentAbsentCount = studentAbsentData.Count > 0 ? studentAbsentData.Count : 0;

                return attendences;
            }
        }

        public List<PresentStatusDTO> GetPresentStatuses()
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                var presentStatuses = new List<PresentStatusDTO>();

                var entities = dbContext.PresentStatuses.Where(a => a.StatusTitle != null)
                    .AsNoTracking()
                    .ToList();

                foreach (var status in entities)
                {
                    presentStatuses.Add(new PresentStatusDTO()
                    {
                        PresentStatusID = status.PresentStatusID,
                        StatusTitle = status.StatusTitle,
                        StatusDescription = status.StatusDescription
                    });
                }

                return presentStatuses;
            }
        }

        public string SendTodayAttendancePushNotification(int classId, int sectionId)
        {
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var presentID = new Domain.Setting.SettingBL(null).GetSettingValue<byte>("ATTENDANCE_STATUS_ID_PRESENT", 6);
                var absentID = new Domain.Setting.SettingBL(null).GetSettingValue<byte>("ATTENDANCE_STATUS_ID_ABSENT", 1);

                var principalDesigID = new Domain.Setting.SettingBL(null).GetSettingValue<int>("EMP_PRINCIPAL_DESIGNATION_ID", 25);
                var adminManDesigID = new Domain.Setting.SettingBL(null).GetSettingValue<int>("EMP_ADMINMANAGER_DESIGNATION_ID", 5);

                var todayDate = DateTime.Now;

                var todayAttendanceDatas = dbContext.StudentAttendences
                    .Where(x => x.ClassID == classId && x.SectionID == sectionId && x.AttendenceDate.Value.Day == todayDate.Day && x.AttendenceDate.Value.Month == todayDate.Month && x.AttendenceDate.Value.Year == todayDate.Year && x.PresentStatusID == presentID
                    || x.ClassID == classId && x.SectionID == sectionId && x.AttendenceDate.Value.Day == todayDate.Day && x.AttendenceDate.Value.Month == todayDate.Month && x.AttendenceDate.Value.Year == todayDate.Year && x.PresentStatusID == absentID)
                    .Include(i => i.Student)
                    .Include(i => i.PresentStatus)
                    .AsNoTracking().ToList();

                if (todayAttendanceDatas.Count <= 0)
                {
                    return "There is no attendance marked today " + todayDate.ToString(dateFormat, CultureInfo.InvariantCulture) + " please mark attendance !";
                }

                foreach (var attend in todayAttendanceDatas)
                {
                    long fromLoginID = _context?.LoginID != null ? (long)_context.LoginID : 2;

                    var studFullName = attend.Student?.AdmissionNumber + " - " + attend.Student?.FirstName + " " + attend.Student?.MiddleName + " " + attend.Student?.LastName;

                    var schoolINdata = dbContext.DriverScheduleLogs
                        .Where(d => d.StudentID == attend.StudentID && d.SheduleDate.Value.Day == todayDate.Day && d.SheduleDate.Value.Month == todayDate.Month && d.SheduleDate.Value.Year == todayDate.Year)
                        .AsNoTracking().ToList();

                    #region attendance sending to parent
                    //Case 1 : Absent notification send to parent when (student not in bus and not in class during attendance time)
                    //Case 2 : Present notification send to parent when (student present in bus and class)

                    var pickIN = schoolINdata?.FirstOrDefault(x => x.ScheduleLogType == "PICK-IN");
                    var pickOUT = schoolINdata?.FirstOrDefault(x => x.ScheduleLogType == "PICK-OUT");
                    var dropIN = schoolINdata?.FirstOrDefault(x => x.ScheduleLogType == "DROP-IN");
                    var dropOUT = schoolINdata?.FirstOrDefault(x => x.ScheduleLogType == "DROP-OUT");

                    var case1 = schoolINdata == null ? true : pickOUT.Status == "O" && attend.PresentStatusID == presentID; //Student out from bus and teacher marked present in class or not using transport
                    var case2 = schoolINdata == null ? true : pickIN.Status == "A" && pickOUT.Status == "A" && attend.PresentStatusID == absentID; //student not IN/OUT in pickup or not using transport and teacher marked as absent

                    if (case1 == true || case2 == true)
                    {
                        var settings = NotificationSetting.GetParentAppSettings();
                        var title = "Attendance : " + todayDate.ToString(dateFormat, CultureInfo.InvariantCulture);
                        var message = studFullName + " is " + attend.PresentStatus?.StatusDescription.ToUpper() + " today";
                        long toLoginID = (long)attend.Student.Parent.LoginID;

                        // check today's notification already send to same loginID
                        var checkOldNotifications = dbContext.NotificationAlerts
                            .Where(c => c.Message == message && c.NotificationDate.Value.Day == todayDate.Day && c.NotificationDate.Value.Month == todayDate.Month && c.NotificationDate.Value.Year == todayDate.Year)
                            .AsNoTracking().FirstOrDefault();

                        if (checkOldNotifications == null)
                        {
                            PushNotificationMapper.SendAndSavePushNotification(toLoginID, fromLoginID, message, title, settings);
                        }
                    }
                    #endregion

                    var getClassTeacher = dbContext.ClassClassTeacherMaps
                        .Where(c => c.ClassID == attend.ClassID && c.SectionID == attend.SectionID && c.AcademicYearID == attend.AcademicYearID)
                        .Include(i => i.Employee1)
                        .AsNoTracking().FirstOrDefault();

                    var studTransportCheck = dbContext.StudentRouteStopMaps
                        .Where(s => s.StudentID == attend.StudentID && s.IsActive == true && todayDate.Date >= s.DateFrom)
                        .AsNoTracking().FirstOrDefault();

                    var employees = dbContext.Employees
                        .Include(i => i.Designation)
                        .Where(d => d.Designation.IsTransportNotification == true)
                        .AsNoTracking().ToList();

                    //Sending push notification cases:
                    var case3 = schoolINdata == null ? true : pickIN.Status == "A" && pickOUT.Status == "A" && attend.PresentStatusID == presentID;
                    var case4 = schoolINdata != null && pickIN.Status == "I" && pickOUT.Status == "A" && attend.PresentStatusID == absentID;
                    var case5 = schoolINdata != null && pickIN.Status == "I" && pickOUT.Status == "O" && attend.PresentStatusID == absentID;

                    #region case 3 : private/parent direct drop into school and student present in class during attendance time for Class teacher and designation who's enabled for istransport notification enabled designations
                    if (case3 == true)
                    {
                        var settings = NotificationSetting.GetEmployeeAppSettings();
                        var title = "Attendance : " + todayDate.ToString(dateFormat, CultureInfo.InvariantCulture);
                        var message = studFullName + " is " + attend.PresentStatus?.StatusDescription.ToUpper() + " today & not used school transportation";

                        if (getClassTeacher != null)
                        {
                            long toLoginID = (long)getClassTeacher.Employee1.LoginID;
                            PushNotificationMapper.SendAndSavePushNotification(toLoginID, fromLoginID, message, title, settings);
                        }

                        foreach (var emp in employees)
                        {
                            if (emp.DesignationID != principalDesigID || emp.DesignationID != adminManDesigID)
                            {
                                long toLoginID = (long)emp.LoginID;
                                PushNotificationMapper.SendAndSavePushNotification(toLoginID, fromLoginID, message, title, settings);
                            }
                        }
                    }
                    #endregion

                    #region case 4 and case 5 Emergency alerts
                    //case 4 : when student IN in bus and not OUT in school and Absent in class during attendance time
                    //case 5 : when student IN in bus and OUT from bus,but not present in class during attendance time

                    if (case4 == true || case5 == true)
                    {
                        var settings = NotificationSetting.GetEmployeeAppSettings();
                        var title = "Alert !!! Please check!  Attendance : " + todayDate.ToString(dateFormat, CultureInfo.InvariantCulture);
                        var message = studFullName + " is " + attend.PresentStatus?.StatusDescription.ToUpper() + " today.but, was in school bus !!!";

                        if (getClassTeacher != null)
                        {
                            long toLoginID = (long)getClassTeacher.Employee1.LoginID;
                            PushNotificationMapper.SendAndSavePushNotification(toLoginID, fromLoginID, message, title, settings);
                        }

                        foreach (var emp in employees)
                        {
                            long toLoginID = (long)emp.LoginID;
                            PushNotificationMapper.SendAndSavePushNotification(toLoginID, fromLoginID, message, title, settings);
                        }
                    }
                    #endregion

                }
            }
            return "Notification Sent Succesfully !";
        }


        #region Present/Absent Push and Mail Notification send to Parents -- using foreach for all students
        public string SendAttendanceNotificationsToParents(int classId, int sectionId)
        {
            MutualRepository mutualRepository = new MutualRepository();
            var dateFormat = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DateFormat");

            using (var dbContext = new dbEduegateSchoolContext())
            {
                var presentID = new Domain.Setting.SettingBL(null).GetSettingValue<byte>("ATTENDANCE_STATUS_ID_PRESENT", 6);
                var absentID = new Domain.Setting.SettingBL(null).GetSettingValue<byte>("ATTENDANCE_STATUS_ID_ABSENT", 1);

                var todayDate = DateTime.Now;

                var todayAttendanceDatas = dbContext.StudentAttendences.Where(x => x.ClassID == classId && x.SectionID == sectionId && x.AttendenceDate.Value.Day == todayDate.Day && x.AttendenceDate.Value.Month == todayDate.Month && x.AttendenceDate.Value.Year == todayDate.Year && x.PresentStatusID == presentID
                    || x.ClassID == classId && x.SectionID == sectionId && x.AttendenceDate.Value.Day == todayDate.Day && x.AttendenceDate.Value.Month == todayDate.Month && x.AttendenceDate.Value.Year == todayDate.Year && x.PresentStatusID == absentID)
                    .Include(x => x.PresentStatus)
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .Include(i => i.Student).ThenInclude(i => i.Parent)
                    .Include(i => i.Student).ThenInclude(i => i.Class)
                    .Include(i => i.Student).ThenInclude(i => i.Section)
                    .Include(i => i.Student).ThenInclude(i => i.School)
                    .AsNoTracking().ToList();

                if (todayAttendanceDatas.Count <= 0)
                {
                    return "There is no attendance marked today " + todayDate.ToString(dateFormat, CultureInfo.InvariantCulture) + " please mark attendance !";
                }


                foreach (var attend in todayAttendanceDatas)
                {
                    var classCode = attend.Student?.Class?.Code?.ToLower();

                    if(string.IsNullOrEmpty(classCode))
                    {
                        var classID = attend.Student?.ClassID;
                        if (classID.HasValue)
                        {
                            var data = new Eduegate.Domain.Setting.SettingBL(_context).GetClassDetailByClassID(classID.Value);
                            
                            classCode = data?.Code?.ToLower();
                        }
                    }

                    if (attend.PresentStatusID == presentID || attend.PresentStatusID == absentID)
                    {
                        long fromLoginID = _context?.LoginID != null ? (long)_context.LoginID : 2;

                        var studFullName = attend.Student?.AdmissionNumber + " - " + attend.Class?.ClassDescription + "/" + attend.Section?.SectionName + " : " + attend.Student?.FirstName + " " + attend.Student?.MiddleName + " " + attend.Student?.LastName;


                        var settings = NotificationSetting.GetParentAppSettings();
                        var title = "Attendance : " + todayDate.ToString(dateFormat, CultureInfo.InvariantCulture);
                        var message = studFullName + " is " + attend.PresentStatus?.StatusDescription.ToUpper() + " today";
                        long toLoginID = (long)attend.Student.Parent?.LoginID;

                        // check today's notification already send to same loginID
                        var checkOldNotifications = dbContext.NotificationAlerts.FirstOrDefault(c => c.Message == message && c.NotificationDate.Value.Day == todayDate.Day && c.NotificationDate.Value.Month == todayDate.Month && c.NotificationDate.Value.Year == todayDate.Year);

                        var toMailID = attend.Student?.Parent?.GaurdianEmail != null ? attend.Student?.Parent?.GaurdianEmail : attend.Student?.Parent?.FatherEmailID ;
                        
                        if (checkOldNotifications == null && toMailID != null)
                        {
                            //Send Mail Notification
                            String emailDetails = "";
                            String emailSub = "";

                            emailSub = "Student Recorded as " + attend.PresentStatus?.StatusDescription.ToUpper();

                            var reportStatus = attend.PresentStatusID == presentID ? " is PRESENT in the class on " : " has not reported to the school on ";

                            emailDetails = @" <br/> <h3> Dear Parent,</h3>
                                                <p>Your child "+ studFullName + @" of "
                                                + attend.Student?.Class?.ClassDescription +" "+ attend.Student?.Section?.SectionName + reportStatus + todayDate.ToString(dateFormat, CultureInfo.InvariantCulture) +
                                                @".Please reach out to the school for any queries.</p><br/>
                                                <h4>Thank You</h4>
                                                "+ attend.Student?.School?.SchoolName + @"
                                                <br/>";

                            string mailMessage = new Eduegate.Domain.Notification.EmailNotificationBL(_context).PopulateBody(toMailID, emailDetails);

                            var hostDet = new Domain.Setting.SettingBL(null).GetSettingValue<string>("HOST_NAME");

                            string defaultMail = new Domain.Setting.SettingBL(null).GetSettingValue<string>("DEFAULT_MAIL_ID");

                            var mailParameters = new Dictionary<string, string>()
                            {
                                { "CLASS_CODE", classCode},
                            };

                            if (emailDetails != "")
                            {
                                if (hostDet.ToLower() == "live")
                                {
                                    new Eduegate.Domain.Notification.EmailNotificationBL(_context).SendMail(toMailID, emailSub, mailMessage, EmailTypes.Attendance, mailParameters);
                                }
                                else
                                {
                                    new Eduegate.Domain.Notification.EmailNotificationBL(_context).SendMail(defaultMail, emailSub, mailMessage, EmailTypes.Attendance, mailParameters);
                                }
                            }
                        }

                        if (checkOldNotifications == null && toLoginID != 0)
                        {
                            PushNotificationMapper.SendAndSavePushNotification(toLoginID, fromLoginID, message, title, settings);
                        }
                    }
                }
            }

            return "Notification Sent Succesfully !";
        }
        #endregion


        public List<StudentAttendenceDTO> GetStudentClassWiseAttendance(long filter, int schoolID, long loginID, int? academicYearID)
        {
            var attendanceList = new List<StudentAttendenceDTO>();

            using (var dbContext = new dbEduegateERPContext())
            {
                SqlConnectionStringBuilder _sBuilder = new SqlConnectionStringBuilder(Infrastructure.ConfigHelper.GetDefaultConnectionString());
                _sBuilder.ConnectTimeout = 30; // Set timeout
                using (SqlConnection conn = new SqlConnection(_sBuilder.ConnectionString))
                {
                    try
                    {
                        conn.Open();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    using (SqlCommand sqlCommand = new SqlCommand("[schools].[SPS_StudentClassWiseAttendanceChart]", conn))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;

                        // Adding parameters
                        sqlCommand.Parameters.Add(new SqlParameter("@Filter", SqlDbType.VarChar) { Value = filter  });
                        sqlCommand.Parameters.Add(new SqlParameter("@SchoolID", SqlDbType.Int) { Value = schoolID });
                        sqlCommand.Parameters.Add(new SqlParameter("@LoginID", SqlDbType.Int) { Value = loginID });
                        sqlCommand.Parameters.Add(new SqlParameter("@AcademicYearID", SqlDbType.Int) { Value = academicYearID });

                        SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                        DataSet dataSet = new DataSet();
                        adapter.Fill(dataSet);

                        if (dataSet.Tables.Count > 0)
                        {
                            DataTable dataTable = dataSet.Tables[0];
                            foreach (DataRow row in dataTable.Rows)
                            {
                                attendanceList.Add(new StudentAttendenceDTO()
                                {
                                    ClassName = (string)row["Class"],
                                    AttendancePercentage = (decimal)row["Attendance"]
                                });
                            }
                        }
                    }
                }
            }

            return attendanceList;
        }


    }
}
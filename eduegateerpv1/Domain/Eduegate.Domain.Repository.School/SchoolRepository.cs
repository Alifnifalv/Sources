using Eduegate.Domain.Entity.School.Models;
using Eduegate.Domain.Entity.School.Models.School;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eduegate.Domain.Repository.School
{
    public class SchoolRepository
    {
        public Student GetStudentDetail(string qatarID, string admissionNumber)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                return dbContext.Students
                    .Where(x => x.AdmissionNumber == admissionNumber)
                    .Include(i => i.School)
                    .Include(i => i.Class)
                    .Include(i => i.Class1)
                    .Include(i => i.Section)
                    .Include(i => i.Parent)
                    .Include(i => i.StudentPassportDetails)
                    .AsNoTracking()
                    .FirstOrDefault();
            }
        }

        public List<Schools> GetSchools()
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                return dbContext.School
                    .Include(i => i.Students).ThenInclude(i => i.AcademicYear)
                    .Include(i => i.AcademicYears)
                    .Include(i => i.Sections)
                    .AsNoTracking()
                    .ToList();
            }
        }

        public AcademicYear GetAcademicYearDataByID(int? academicYearID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                return dbContext.AcademicYears
                    .Where(a => a.AcademicYearID == academicYearID)
                    .AsNoTracking()
                    .FirstOrDefault();
            }
        }

        public List<ClassClassTeacherMap> GetClassClassTeacherMapsByLogin(long? loginID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                return dbContext.ClassClassTeacherMaps
                    .Where(x => x.Employee1.LoginID == loginID)
                    .AsNoTracking()
                    .ToList();
            }
        }

        public Employee GetEmployeeUserDataByLoginID(long? loginID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                return dbContext.Employees
                    .Where(a => a.LoginID == loginID)
                    .Include(i => i.Departments1)
                    .Include(i => i.Designation)
                    .AsNoTracking()
                    .FirstOrDefault();
            }
        }

        public StaffAttendence GetStaffAttendancesByID(long? employeeID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                return dbContext.StaffAttendences
                    .Where(x => x.EmployeeID == employeeID)
                    .Include(i => i.StaffPresentStatus)
                    .OrderByDescending(o => o.StaffAttendenceIID)
                    .AsNoTracking()
                    .FirstOrDefault();
            }
        }

        public List<NotificationAlert> GetNotificationsByLoginAndDate(long? loginID, DateTime? date)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                return dbContext.NotificationAlerts
                    .Where(a => a.ToLoginID == loginID && a.NotificationDate.Value >= date.Value)
                    .AsNoTracking()
                    .ToList();
            }
        }

        public List<TimeTableAllocation> GetTimeTableAllocations (long? loginID, int weekDayID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                return dbContext.TimeTableAllocations
                    .Where(a => a.Employee.LoginID == loginID && a.WeekDayID == weekDayID)
                    .Include(i => i.Class)
                    .Include(i => i.Section)
                    .Include(i => i.Subject)
                    .Include(i => i.ClassTiming)
                    .AsNoTracking()
                    .ToList();
            }
        }

        public List<long> GetStudentByParentLoginID(long loginID)
        {
            using (var dbContext = new dbEduegateSchoolContext())
            {
                return dbContext.Students.Where(s => s.Parent != null && s.Parent.LoginID == loginID && s.IsActive == true)
                    .Select(x => x.StudentIID)
                    .ToList();
            }
        }
    }
}

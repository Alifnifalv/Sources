using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.School.Models
{
    [Table("TimeTableExtension", Schema = "schools")]
    public partial class TimeTableExtension
    {
        public TimeTableExtension()
        {
            TimeTableExtClassTimings = new HashSet<TimeTableExtClassTiming>();
            TimeTableExtClasses = new HashSet<TimeTableExtClass>();
            TimeTableExtSections = new HashSet<TimeTableExtSection>();
            TimeTableExtSubjects = new HashSet<TimeTableExtSubject>();
            TimeTableExtTeachers = new HashSet<TimeTableExtTeacher>();
            TimeTableExtWeekDays = new HashSet<TimeTableExtWeekDay>();
        }

        [Key]
        public long TimeTableExtIID { get; set; }

        public string TimeTableExtName { get; set; }

        public string TimeTableExtRemarks { get; set; }

        public int TimeTableID { get; set; }

        public byte? SubjectTypeID { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        public long? TimeTableExtParentID { get; set; }

        public int? MinPeriodCountDay { get; set; }

        public int? MaxPeriodCountDay { get; set; }

        public int? PeriodCountWeek { get; set; }

        public bool? IsPeriodContinues { get; set; }

        public bool? IsActive { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual Schools School { get; set; }

        public virtual SubjectType SubjectType { get; set; }

        public virtual TimeTable TimeTable { get; set; }
        
        public virtual ICollection<TimeTableExtClassTiming> TimeTableExtClassTimings { get; set; }
        
        public virtual ICollection<TimeTableExtClass> TimeTableExtClasses { get; set; }
        
        public virtual ICollection<TimeTableExtSection> TimeTableExtSections { get; set; }
        
        public virtual ICollection<TimeTableExtSubject> TimeTableExtSubjects { get; set; }
        
        public virtual ICollection<TimeTableExtTeacher> TimeTableExtTeachers { get; set; }
        
        public virtual ICollection<TimeTableExtWeekDay> TimeTableExtWeekDays { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
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
        [StringLength(500)]
        public string TimeTableExtName { get; set; }
        [StringLength(1000)]
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
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("TimeTableExtensions")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("TimeTableExtensions")]
        public virtual School School { get; set; }
        [ForeignKey("SubjectTypeID")]
        [InverseProperty("TimeTableExtensions")]
        public virtual SubjectType SubjectType { get; set; }
        [ForeignKey("TimeTableID")]
        [InverseProperty("TimeTableExtensions")]
        public virtual TimeTable TimeTable { get; set; }
        [InverseProperty("TimeTableExt")]
        public virtual ICollection<TimeTableExtClassTiming> TimeTableExtClassTimings { get; set; }
        [InverseProperty("TimeTableExt")]
        public virtual ICollection<TimeTableExtClass> TimeTableExtClasses { get; set; }
        [InverseProperty("TimeTableExt")]
        public virtual ICollection<TimeTableExtSection> TimeTableExtSections { get; set; }
        [InverseProperty("TimeTableExt")]
        public virtual ICollection<TimeTableExtSubject> TimeTableExtSubjects { get; set; }
        [InverseProperty("TimeTableExt")]
        public virtual ICollection<TimeTableExtTeacher> TimeTableExtTeachers { get; set; }
        [InverseProperty("TimeTableExt")]
        public virtual ICollection<TimeTableExtWeekDay> TimeTableExtWeekDays { get; set; }
    }
}

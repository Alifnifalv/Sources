using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.School.Models
{
    [Table("SubjectUnits", Schema = "schools")]
    public partial class SubjectUnit
    {
        public SubjectUnit()
        {
            InverseParentSubjectUnit = new HashSet<SubjectUnit>();
            LessonPlans = new HashSet<LessonPlan>();
        }

        [Key]
        public long UnitIID { get; set; }

        public long? ParentSubjectUnitID { get; set; }

        public long? ChapterID { get; set; }

        public int? AcademicYearID { get; set; }

        public byte? SchoolID { get; set; }

        public int? ClassID { get; set; }

        public int? SectionID { get; set; }

        public int? SubjectID { get; set; }

        public string UnitTitle { get; set; }

        public string Description { get; set; }

        public long? TotalPeriods { get; set; }

        public long? TotalHours { get; set; }
 
        public DateTime? CreatedDate { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual Class Class { get; set; }

        public virtual Schools School { get; set; }

        public virtual Section Section { get; set; }

        public virtual Subject Subject { get; set; }

        public virtual Chapter Chapter { get; set; }

        public virtual ICollection<LessonPlan> LessonPlans { get; set; }

        public virtual SubjectUnit ParentSubjectUnit { get; set; }

        public virtual ICollection<SubjectUnit> InverseParentSubjectUnit { get; set; }

    }
}
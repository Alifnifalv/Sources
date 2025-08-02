using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
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
        [StringLength(255)]
        public string UnitTitle { get; set; }
        public string Description { get; set; }
        public long? TotalPeriods { get; set; }
        public long? TotalHours { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("SubjectUnits")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("ChapterID")]
        [InverseProperty("SubjectUnits")]
        public virtual Chapter Chapter { get; set; }
        [ForeignKey("ClassID")]
        [InverseProperty("SubjectUnits")]
        public virtual Class Class { get; set; }
        [ForeignKey("ParentSubjectUnitID")]
        [InverseProperty("InverseParentSubjectUnit")]
        public virtual SubjectUnit ParentSubjectUnit { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("SubjectUnits")]
        public virtual School School { get; set; }
        [ForeignKey("SectionID")]
        [InverseProperty("SubjectUnits")]
        public virtual Section Section { get; set; }
        [ForeignKey("SubjectID")]
        [InverseProperty("SubjectUnits")]
        public virtual Subject Subject { get; set; }
        [InverseProperty("ParentSubjectUnit")]
        public virtual ICollection<SubjectUnit> InverseParentSubjectUnit { get; set; }
        [InverseProperty("Unit")]
        public virtual ICollection<LessonPlan> LessonPlans { get; set; }
    }
}

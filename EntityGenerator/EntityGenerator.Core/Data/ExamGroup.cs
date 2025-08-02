using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ExamGroups", Schema = "schools")]
    public partial class ExamGroup
    {
        public ExamGroup()
        {
            Exams = new HashSet<Exam>();
            HealthEntries = new HashSet<HealthEntry>();
            MarkRegisters = new HashSet<MarkRegister>();
            ProgressReports = new HashSet<ProgressReport>();
            RemarksEntries = new HashSet<RemarksEntry>();
        }

        [Key]
        public int ExamGroupID { get; set; }
        [StringLength(100)]
        public string ExamGroupName { get; set; }
        public bool? IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FromDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ToDate { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("ExamGroups")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("ExamGroups")]
        public virtual School School { get; set; }
        [InverseProperty("ExamGroup")]
        public virtual ICollection<Exam> Exams { get; set; }
        [InverseProperty("ExamGroup")]
        public virtual ICollection<HealthEntry> HealthEntries { get; set; }
        [InverseProperty("ExamGroup")]
        public virtual ICollection<MarkRegister> MarkRegisters { get; set; }
        [InverseProperty("ExamGroup")]
        public virtual ICollection<ProgressReport> ProgressReports { get; set; }
        [InverseProperty("ExamGroup")]
        public virtual ICollection<RemarksEntry> RemarksEntries { get; set; }
    }
}

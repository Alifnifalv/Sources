using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("HealthEntries", Schema = "schools")]
    public partial class HealthEntry
    {
        public HealthEntry()
        {
            HealthEntryStudentMaps = new HashSet<HealthEntryStudentMap>();
        }

        [Key]
        public long HealthEntryIID { get; set; }
        public int? ClassID { get; set; }
        public int? SectionID { get; set; }
        public long? TeacherID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        public int? ExamGroupID { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("HealthEntries")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("ClassID")]
        [InverseProperty("HealthEntries")]
        public virtual Class Class { get; set; }
        [ForeignKey("ExamGroupID")]
        [InverseProperty("HealthEntries")]
        public virtual ExamGroup ExamGroup { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("HealthEntries")]
        public virtual School School { get; set; }
        [ForeignKey("SectionID")]
        [InverseProperty("HealthEntries")]
        public virtual Section Section { get; set; }
        [ForeignKey("TeacherID")]
        [InverseProperty("HealthEntries")]
        public virtual Employee Teacher { get; set; }
        [InverseProperty("HealthEntry")]
        public virtual ICollection<HealthEntryStudentMap> HealthEntryStudentMaps { get; set; }
    }
}

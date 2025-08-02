using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("RemarksEntries", Schema = "schools")]
    public partial class RemarksEntry
    {
        public RemarksEntry()
        {
            RemarksEntryStudentMaps = new HashSet<RemarksEntryStudentMap>();
        }

        [Key]
        public long RemarksEntryIID { get; set; }
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
        [InverseProperty("RemarksEntries")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("ClassID")]
        [InverseProperty("RemarksEntries")]
        public virtual Class Class { get; set; }
        [ForeignKey("ExamGroupID")]
        [InverseProperty("RemarksEntries")]
        public virtual ExamGroup ExamGroup { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("RemarksEntries")]
        public virtual School School { get; set; }
        [ForeignKey("SectionID")]
        [InverseProperty("RemarksEntries")]
        public virtual Section Section { get; set; }
        [ForeignKey("TeacherID")]
        [InverseProperty("RemarksEntries")]
        public virtual Employee Teacher { get; set; }
        [InverseProperty("RemarksEntry")]
        public virtual ICollection<RemarksEntryStudentMap> RemarksEntryStudentMaps { get; set; }
    }
}

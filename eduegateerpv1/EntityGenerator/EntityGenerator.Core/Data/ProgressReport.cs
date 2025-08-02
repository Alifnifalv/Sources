using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ProgressReports", Schema = "schools")]
    public partial class ProgressReport
    {
        [Key]
        public long ProgressReportIID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public long? StudentId { get; set; }
        public int? ClassID { get; set; }
        public int? SectionID { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        public long? ReportContentID { get; set; }
        public byte? PublishStatusID { get; set; }
        public long? ExamID { get; set; }
        public int? ExamGroupID { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("ProgressReports")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("ClassID")]
        [InverseProperty("ProgressReports")]
        public virtual Class Class { get; set; }
        [ForeignKey("ExamID")]
        [InverseProperty("ProgressReports")]
        public virtual Exam Exam { get; set; }
        [ForeignKey("ExamGroupID")]
        [InverseProperty("ProgressReports")]
        public virtual ExamGroup ExamGroup { get; set; }
        [ForeignKey("PublishStatusID")]
        [InverseProperty("ProgressReports")]
        public virtual ProgressReportPublishStatus PublishStatus { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("ProgressReports")]
        public virtual School School { get; set; }
        [ForeignKey("SectionID")]
        [InverseProperty("ProgressReports")]
        public virtual Section Section { get; set; }
        [ForeignKey("StudentId")]
        [InverseProperty("ProgressReports")]
        public virtual Student Student { get; set; }
    }
}

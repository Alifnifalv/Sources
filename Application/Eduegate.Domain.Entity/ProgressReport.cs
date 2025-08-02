namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.ProgressReports")]
    public partial class ProgressReport
    {
        [Key]
        public long ProgressReportIID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
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

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual Class Class { get; set; }

        public virtual ExamGroup ExamGroup { get; set; }

        public virtual Exam Exam { get; set; }

        public virtual ProgressReportPublishStatus ProgressReportPublishStatus { get; set; }

        public virtual School School { get; set; }

        public virtual Section Section { get; set; }

        public virtual Student Student { get; set; }
    }
}

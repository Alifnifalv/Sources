namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.ExamSubjectMaps")]
    public partial class ExamSubjectMap
    {
        [Key]
        public long ExamSubjectMapIID { get; set; }

        public long? ExamID { get; set; }

        public int? SubjectID { get; set; }

        public DateTime? ExamDate { get; set; }

        public decimal? MinimumMarks { get; set; }

        public decimal? MaximumMarks { get; set; }

        public TimeSpan? StartTime { get; set; }

        public TimeSpan? EndTime { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public int? MarkGradeID { get; set; }

        public decimal? ConversionFactor { get; set; }

        public virtual Exam Exam { get; set; }

        public virtual MarkGrade MarkGrade { get; set; }

        public virtual Subject Subject { get; set; }
    }
}

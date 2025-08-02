using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ExamSubjectMaps", Schema = "schools")]
    [Index("ExamID", Name = "IDX_ExamSubjectMaps_ExamID_")]
    [Index("ExamID", Name = "IDX_ExamSubjectMaps_ExamID_ConversionFactor")]
    [Index("ExamID", Name = "IDX_ExamSubjectMaps_ExamID_SubjectID")]
    [Index("ExamID", "SubjectID", Name = "IDX_ExamSubjectMaps_ExamID__SubjectID_")]
    [Index("ExamID", "SubjectID", Name = "IDX_ExamSubjectMaps_ExamID__SubjectID_ConversionFactor")]
    [Index("ExamID", "SubjectID", Name = "IDX_ExamSubjectMaps_ExamID__SubjectID_MaximumMarks")]
    public partial class ExamSubjectMap
    {
        [Key]
        public long ExamSubjectMapIID { get; set; }
        public long? ExamID { get; set; }
        public int? SubjectID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ExamDate { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? MinimumMarks { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? MaximumMarks { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public int? MarkGradeID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? ConversionFactor { get; set; }

        [ForeignKey("ExamID")]
        [InverseProperty("ExamSubjectMaps")]
        public virtual Exam Exam { get; set; }
        [ForeignKey("MarkGradeID")]
        [InverseProperty("ExamSubjectMaps")]
        public virtual MarkGrade MarkGrade { get; set; }
        [ForeignKey("SubjectID")]
        [InverseProperty("ExamSubjectMaps")]
        public virtual Subject Subject { get; set; }
    }
}

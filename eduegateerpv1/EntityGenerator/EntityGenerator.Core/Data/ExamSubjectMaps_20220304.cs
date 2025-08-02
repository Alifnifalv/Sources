using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class ExamSubjectMaps_20220304
    {
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
    }
}

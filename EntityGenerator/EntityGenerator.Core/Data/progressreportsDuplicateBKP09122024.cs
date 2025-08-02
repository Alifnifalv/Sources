using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("progressreportsDuplicateBKP09122024", Schema = "schools")]
    public partial class progressreportsDuplicateBKP09122024
    {
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
    }
}

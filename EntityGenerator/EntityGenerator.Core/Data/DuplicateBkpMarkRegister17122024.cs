using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("DuplicateBkpMarkRegister17122024", Schema = "schools")]
    public partial class DuplicateBkpMarkRegister17122024
    {
        public long MarkRegisterIID { get; set; }
        public long? ExamID { get; set; }
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
        public byte? MarkEntryStatusID { get; set; }
        public int? ExamGroupID { get; set; }
        public byte? PresentStatusID { get; set; }
        public int? SubjectID { get; set; }
        [Column(TypeName = "decimal(10, 3)")]
        public decimal? Mark { get; set; }
        public bool? IsAbsent { get; set; }
        public long? MarksGradeMapID { get; set; }
        public bool? IsPassed { get; set; }
        public byte? MapMarkEntryStatusID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? ConversionFactor { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? TotalMark { get; set; }
    }
}

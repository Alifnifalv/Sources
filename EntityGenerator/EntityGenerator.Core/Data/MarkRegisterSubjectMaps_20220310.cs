using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class MarkRegisterSubjectMaps_20220310
    {
        public long MarkRegisterSubjectMapIID { get; set; }
        public long? MarkRegisterID { get; set; }
        public int? SubjectID { get; set; }
        [Column(TypeName = "decimal(10, 3)")]
        public decimal? Mark { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public bool? IsAbsent { get; set; }
        public long? MarksGradeMapID { get; set; }
        public bool? IsPassed { get; set; }
        public byte? MarkEntryStatusID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? ConversionFactor { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? TotalMark { get; set; }
    }
}

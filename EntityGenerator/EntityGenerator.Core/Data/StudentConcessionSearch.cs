using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class StudentConcessionSearch
    {
        public long StudentFeeConcessionIID { get; set; }
        public long? StudentID { get; set; }
        [StringLength(123)]
        public string AcademicYear { get; set; }
        [StringLength(553)]
        public string Student { get; set; }
        [StringLength(50)]
        public string Fee { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
        public int? AcademicYearID { get; set; }
        public int? FeeMasterID { get; set; }
        public int? FeePeriodID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? DueAmount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? PercentageAmount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? ConcessionAmount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? NetAmount { get; set; }
    }
}

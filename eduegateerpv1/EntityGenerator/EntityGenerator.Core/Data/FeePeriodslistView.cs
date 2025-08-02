using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class FeePeriodslistView
    {
        public int FeePeriodID { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
        public int? AcademicYearId { get; set; }
        [StringLength(126)]
        public string AcademicYear { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(200)]
        public string CreatedUserName { get; set; }
        [StringLength(200)]
        public string UpdatedUserName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime PeriodFrom { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime PeriodTo { get; set; }
        public int? NumberOfPeriods { get; set; }
        public byte? SchoolID { get; set; }
    }
}

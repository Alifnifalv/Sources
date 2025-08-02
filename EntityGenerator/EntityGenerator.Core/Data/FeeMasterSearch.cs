using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class FeeMasterSearch
    {
        public int FeeMasterID { get; set; }
        public int? FeeTypeID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DueDate { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
        public byte? FeeCycleID { get; set; }
        public int? DueInDays { get; set; }
        public long? LedgerAccountID { get; set; }
        public long? TaxLedgerAccountID { get; set; }
        [Column(TypeName = "decimal(4, 2)")]
        public decimal? TaxPercentage { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
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
        public byte[] TimeStamps { get; set; }
        [StringLength(100)]
        public string TypeName { get; set; }
        [StringLength(50)]
        public string FeeCycle { get; set; }
        [Required]
        [StringLength(8)]
        [Unicode(false)]
        public string IsActive { get; set; }
        [Required]
        [StringLength(5)]
        [Unicode(false)]
        public string RowCategory { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class FineMasterSearch
    {
        public int FineMasterID { get; set; }
        [Required]
        [StringLength(20)]
        public string FineCode { get; set; }
        [Required]
        [StringLength(100)]
        public string FineName { get; set; }
        public short? FeeFineTypeID { get; set; }
        public long? LedgerAccountID { get; set; }
        [Column(TypeName = "numeric(18, 4)")]
        public decimal? Amount { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? AcademicYearID { get; set; }
        public byte? SchoolID { get; set; }
        [StringLength(200)]
        public string CreatedUserName { get; set; }
        [StringLength(200)]
        public string UpdatedUserName { get; set; }
        public byte[] TimeStamps { get; set; }
        [StringLength(50)]
        public string FeeFineType { get; set; }
        [StringLength(500)]
        public string LedgerAccount { get; set; }
    }
}

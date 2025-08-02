using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class LSProvisionSearchView
    {
        public long EmployeeLSProvisionHeadIID { get; set; }
        [StringLength(50)]
        public string EmployeeCode { get; set; }
        [Required]
        [StringLength(501)]
        public string EmployeeName { get; set; }
        public long? EmployeeID { get; set; }
        [Column(TypeName = "date")]
        public DateTime? EntryDate { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? LeaveSalaryAmount { get; set; }
        [Column(TypeName = "date")]
        public DateTime? LastLeaveSalaryDate { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? BasicSalary { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? NoofLeaveSalaryDays { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? OpeningAmount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Balance { get; set; }
    }
}

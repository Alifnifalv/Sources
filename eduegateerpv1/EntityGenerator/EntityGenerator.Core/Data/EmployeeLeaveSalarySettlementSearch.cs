using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class EmployeeLeaveSalarySettlementSearch
    {
        public long? EmployeeSalarySettlementIID { get; set; }
        [Column(TypeName = "date")]
        public DateTime? EmployeeSettlementDate { get; set; }
        [StringLength(30)]
        public string Month { get; set; }
        [StringLength(50)]
        public string EmployeeCode { get; set; }
        [Required]
        [StringLength(501)]
        public string EmployeeName { get; set; }
        public long? EmployeeID { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? TotalAmount { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? Earnings { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? Deductions { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? WorkingDays { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? LeaveSalaryDays { get; set; }
    }
}

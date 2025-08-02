using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class SalarySlipSearch
    {
        public long? SalarySlipIID { get; set; }
        [Column(TypeName = "date")]
        public DateTime? SlipDate { get; set; }
        [StringLength(30)]
        public string Month { get; set; }
        public int? YEAR { get; set; }
        [StringLength(50)]
        public string EmployeeCode { get; set; }
        [Required]
        [StringLength(501)]
        public string EmployeeName { get; set; }
        public long? EmployeeID { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? Amount { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? Earnings { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? Deductions { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        public decimal? WorkingDays { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        public decimal? LOPDays { get; set; }
        [Column(TypeName = "decimal(38, 2)")]
        public decimal? NormalHours { get; set; }
        [Column(TypeName = "decimal(38, 2)")]
        public decimal? OThrs { get; set; }
    }
}

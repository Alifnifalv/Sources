using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class SalarySlipReviewView
    {
        public long? SalarySlipIID { get; set; }
        [Column(TypeName = "date")]
        public DateTime? SlipDate { get; set; }
        [StringLength(30)]
        public string Month { get; set; }
        public long? EmployeeID { get; set; }
        [StringLength(50)]
        public string EmployeeCode { get; set; }
        [StringLength(502)]
        public string EmployeeName { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string EmployeeWorkEmail { get; set; }
        public long? DepartmentID { get; set; }
        public long? BranchID { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal Amount { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal Earnings { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal Deductions { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        public decimal WorkingDays { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        public decimal LOPDays { get; set; }
        [Column(TypeName = "decimal(38, 2)")]
        public decimal NormalHours { get; set; }
        [Column(TypeName = "decimal(38, 2)")]
        public decimal OTHours { get; set; }
        [StringLength(4000)]
        public string Description { get; set; }
        public string Notes { get; set; }
        public byte? SalarySlipStatusID { get; set; }
        [StringLength(50)]
        public string SalarySlipStatus { get; set; }
        public bool? IsVerified { get; set; }
        public long? ReportContentID { get; set; }
    }
}

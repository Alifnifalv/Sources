using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class SalaryFileReportView
    {
        public long? SalarySlipIID { get; set; }
        public long? BranchID { get; set; }
        public long? SponsorID { get; set; }
        [StringLength(50)]
        public string SchoolName { get; set; }
        [Column(TypeName = "date")]
        public DateTime? SlipDate { get; set; }
        [StringLength(30)]
        public string SlipMonth { get; set; }
        public int? SlipYear { get; set; }
        public long? EmployeeID { get; set; }
        [StringLength(50)]
        public string EmployeeCode { get; set; }
        [StringLength(502)]
        public string EmployeeName { get; set; }
        [StringLength(50)]
        public string Designation { get; set; }
        [StringLength(50)]
        public string Department { get; set; }
        [StringLength(500)]
        public string Sponsor { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal TotalAmount { get; set; }
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
        public decimal NormalOTHrs { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        public decimal ArrearDays { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal BasicPayAmount { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal VaccationSalary { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal OtherAllowanceAmount { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal HRAAmount { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal OTAmount { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal ArrearAmount { get; set; }
        public int SpecialOTHrs { get; set; }
        public int TransAllAmount { get; set; }
        public int ESBAmount { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal BankAmount { get; set; }
        public int CashAmount { get; set; }
    }
}

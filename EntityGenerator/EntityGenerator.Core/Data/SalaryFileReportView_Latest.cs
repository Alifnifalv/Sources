using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class SalaryFileReportView_Latest
    {
        public long SalarySlipIID { get; set; }
        [Column(TypeName = "date")]
        public DateTime? SlipDate { get; set; }
        [StringLength(30)]
        public string SlipDateMonth { get; set; }
        public int? MonthID { get; set; }
        public int? YearID { get; set; }
        public long? EmployeeID { get; set; }
        [StringLength(50)]
        public string EmployeeCode { get; set; }
        [StringLength(502)]
        public string EmployeeName { get; set; }
        [StringLength(50)]
        public string DesignationName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateOfJoining { get; set; }
        [StringLength(50)]
        public string DepartmentName { get; set; }
        [StringLength(500)]
        public string Sponsor { get; set; }
        public int? SalaryComponentID { get; set; }
        [StringLength(500)]
        public string ComponentDescription { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        public decimal? NoOfHours { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        public decimal? NoOfDays { get; set; }
        public int IsEarnings { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal Amount { get; set; }
        public long? BranchID { get; set; }
        public long? SponsorID { get; set; }
        [StringLength(50)]
        public string SchoolName { get; set; }
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
        public int SpecialOTHrs { get; set; }
        public int TransAllAmount { get; set; }
        public int ESBAmount { get; set; }
        public int CashAmount { get; set; }
    }
}

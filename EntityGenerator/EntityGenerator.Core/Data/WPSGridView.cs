using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class WPSGridView
    {
        public byte? SchoolID { get; set; }
        [StringLength(50)]
        public string SchoolName { get; set; }
        [StringLength(500)]
        public string SponsorName { get; set; }
        public byte? EmpSchoolID { get; set; }
        [StringLength(50)]
        public string EmpSchool { get; set; }
        [StringLength(50)]
        public string Department { get; set; }
        public long? N_Sponcer { get; set; }
        public long? EmployeeID { get; set; }
        public long EmployeeBankIID { get; set; }
        [Required]
        [StringLength(50)]
        public string EmployeeCode { get; set; }
        [StringLength(502)]
        public string EmployeeName { get; set; }
        public long? SponsorID { get; set; }
        [Column(TypeName = "date")]
        public DateTime? SlipDate { get; set; }
        [StringLength(30)]
        public string SlipMonth { get; set; }
        public int? SlipYear { get; set; }
        [Required]
        [StringLength(1)]
        [Unicode(false)]
        public string SalaryFrequency { get; set; }
        [StringLength(20)]
        public string EmployeeQID { get; set; }
        [StringLength(20)]
        public string VisaNo { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string Emp_BankShortName { get; set; }
        [StringLength(50)]
        public string BankName { get; set; }
        [StringLength(50)]
        public string Emp_IBAN { get; set; }
        [StringLength(20)]
        public string Emp_AccountNo { get; set; }
        [Column(TypeName = "decimal(5, 0)")]
        public decimal WorkingDays { get; set; }
        [StringLength(30)]
        public string NetSalary { get; set; }
        [StringLength(30)]
        public string BasicSalary { get; set; }
        [StringLength(30)]
        public string ExtraIncome { get; set; }
        [StringLength(30)]
        public string SalaryDeductions { get; set; }
        [Column(TypeName = "decimal(38, 2)")]
        public decimal ExtraHours { get; set; }
        [Required]
        [StringLength(14)]
        [Unicode(false)]
        public string Entitlement { get; set; }
        [Required]
        [StringLength(9)]
        [Unicode(false)]
        public string Comments { get; set; }
        [StringLength(555)]
        public string EmployeeSearch { get; set; }
    }
}

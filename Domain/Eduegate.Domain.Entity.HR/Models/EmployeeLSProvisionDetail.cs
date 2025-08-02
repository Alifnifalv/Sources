using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Eduegate.Domain.Entity.HR.Models;
namespace Eduegate.Domain.Entity.HR
{
    [Table("EmployeeLSProvisionDetails", Schema = "payroll")]
    public partial class EmployeeLSProvisionDetail
    {
        [Key]
        public long EmployeeLSProvisionDetailIID { get; set; }
        public long EmployeeLSProvisionHeadID { get; set; }
        public long? EmployeeID { get; set; }
       
        [Column(TypeName = "datetime")]
        public DateTime? LastLeaveSalaryDate { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? BasicSalary { get; set; }

        [Column(TypeName = "decimal(18, 3)")]
        public decimal? OpeningAmount { get; set; }

        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Balance { get; set; }     

        [Column(TypeName = "decimal(18, 3)")]
        public decimal? NoofDaysWorked { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? LeaveSalaryAmount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? NoofLeaveSalaryDays { get; set; }

        [ForeignKey("EmployeeID")]
        [InverseProperty("EmployeeLSProvisionDetails")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("EmployeeLSProvisionHeadID")]
        [InverseProperty("EmployeeLSProvisionDetails")]
        public virtual EmployeeLSProvisionHead EmployeeLSProvisionHead { get; set; }
    }
}

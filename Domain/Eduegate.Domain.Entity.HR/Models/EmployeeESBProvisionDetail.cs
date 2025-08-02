using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Eduegate.Domain.Entity.HR.Models;
namespace Eduegate.Domain.Entity.HR
{
    [Table("EmployeeESBProvisionDetails", Schema = "payroll")]
    public partial class EmployeeESBProvisionDetail
    {
        [Key]
        public long EmployeeESBProvisionDetailIID { get; set; }
        public long EmployeeESBProvisionHeadID { get; set; }
        public long? EmployeeID { get; set; }
       
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? ESBAmount { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastLeaveSalaryDate { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? BasicSalary { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? NoofESBDays { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? NoofDaysWorked { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? OpeningAmount { get; set; }

        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Balance { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("EmployeeID")]
        [InverseProperty("EmployeeESBProvisionDetails")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("EmployeeESBProvisionHeadID")]
        [InverseProperty("EmployeeESBProvisionDetails")]
        public virtual EmployeeESBProvisionHead EmployeeESBProvisionHead { get; set; }
    }
}

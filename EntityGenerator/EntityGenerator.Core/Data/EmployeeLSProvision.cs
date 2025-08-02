using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EmployeeLSProvisions", Schema = "payroll")]
    public partial class EmployeeLSProvision
    {
        [Key]
        public long EmployeeLSProvisionIID { get; set; }
        public long? EmployeeID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EntryDate { get; set; }
        [StringLength(25)]
        public string EntryNumber { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? LeaveSalaryAmount { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastLeaveSalaryDate { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? BasicSalary { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? NoofLeaveSalaryDays { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? NoofDaysWorked { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("EmployeeID")]
        [InverseProperty("EmployeeLSProvisions")]
        public virtual Employee Employee { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EmployeeSalaries", Schema = "payroll")]
    public partial class EmployeeSalary
    {
        [Key]
        public long EmployeeSalaryIID { get; set; }
        public int? CompanyID { get; set; }
        public long? EmployeeID { get; set; }
        public int? SalaryComponentID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("CompanyID")]
        [InverseProperty("EmployeeSalaries")]
        public virtual Company Company { get; set; }
        [ForeignKey("EmployeeID")]
        [InverseProperty("EmployeeSalaries")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("SalaryComponentID")]
        [InverseProperty("EmployeeSalaries")]
        public virtual SalaryComponent SalaryComponent { get; set; }
    }
}

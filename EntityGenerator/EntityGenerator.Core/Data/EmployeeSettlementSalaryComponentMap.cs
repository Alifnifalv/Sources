using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EmployeeSettlementSalaryComponentMaps", Schema = "payroll")]
    public partial class EmployeeSettlementSalaryComponentMap
    {
        [Key]
        public long EmployeeSettlementSalaryComponentMapIID { get; set; }
        public long? EmployeeSalarySettlementID { get; set; }
        public int? SalaryComponentID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        [StringLength(1000)]
        public string Formula { get; set; }

        [ForeignKey("EmployeeSalarySettlementID")]
        [InverseProperty("EmployeeSettlementSalaryComponentMaps")]
        public virtual EmployeeSalarySettlement EmployeeSalarySettlement { get; set; }
        [ForeignKey("SalaryComponentID")]
        [InverseProperty("EmployeeSettlementSalaryComponentMaps")]
        public virtual SalaryComponent SalaryComponent { get; set; }
    }
}

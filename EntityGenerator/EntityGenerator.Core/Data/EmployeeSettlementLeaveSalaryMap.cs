using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EmployeeSettlementLeaveSalaryMaps", Schema = "payroll")]
    public partial class EmployeeSettlementLeaveSalaryMap
    {
        [Key]
        public long EmployeeSettlementLeaveSalaryMapIID { get; set; }
        public long? EmployeeSalarySettlementID { get; set; }
        public int? SalaryComponentID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        [StringLength(1000)]
        public string Formula { get; set; }

        [ForeignKey("EmployeeSalarySettlementID")]
        [InverseProperty("EmployeeSettlementLeaveSalaryMaps")]
        public virtual EmployeeSalarySettlement EmployeeSalarySettlement { get; set; }
        [ForeignKey("SalaryComponentID")]
        [InverseProperty("EmployeeSettlementLeaveSalaryMaps")]
        public virtual SalaryComponent SalaryComponent { get; set; }
    }
}

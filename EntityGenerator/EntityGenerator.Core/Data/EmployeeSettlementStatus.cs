using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EmployeeSettlementStatuses", Schema = "payroll")]
    public partial class EmployeeSettlementStatus
    {
        public EmployeeSettlementStatus()
        {
            EmployeeSalarySettlements = new HashSet<EmployeeSalarySettlement>();
        }

        [Key]
        public byte EmployeeSettlementStatusID { get; set; }
        [StringLength(50)]
        public string EmployeeSettlementName { get; set; }

        [InverseProperty("EmployeeSettlementStatus")]
        public virtual ICollection<EmployeeSalarySettlement> EmployeeSalarySettlements { get; set; }
    }
}

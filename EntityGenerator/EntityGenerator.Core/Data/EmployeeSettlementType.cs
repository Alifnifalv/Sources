using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EmployeeSettlementTypes", Schema = "payroll")]
    public partial class EmployeeSettlementType
    {
        public EmployeeSettlementType()
        {
            EmployeeSalarySettlements = new HashSet<EmployeeSalarySettlement>();
        }

        [Key]
        public byte EmployeeSettlementTypeID { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [InverseProperty("EmployeeSettlementType")]
        public virtual ICollection<EmployeeSalarySettlement> EmployeeSalarySettlements { get; set; }
    }
}

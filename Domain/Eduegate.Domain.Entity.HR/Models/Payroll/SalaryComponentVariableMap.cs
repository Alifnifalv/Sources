using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.HR.Payroll
{
    [Table("SalaryComponentVariableMaps", Schema = "payroll")]
    public partial class SalaryComponentVariableMap
    {
        [Key]
        public long SalaryComponentVariableMapIID { get; set; }
        public long? SalaryStructureComponentMapID { get; set; }
        public int? SalaryComponentID { get; set; }
        [StringLength(100)]
        public string VariableKey { get; set; }
        [StringLength(100)]
        public string VariableValue { get; set; }

        public virtual SalaryStructureComponentMap SalaryStructureComponentMap { get; set; }
        public virtual SalaryComponent SalaryComponent { get; set; }
    }
}

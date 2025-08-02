using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("SalaryComponentVariableMaps", Schema = "payroll")]
    public partial class SalaryComponentVariableMap
    {
        [Key]
        public long SalaryComponentVariableMapIID { get; set; }
        public int? SalaryComponentID { get; set; }
        [StringLength(100)]
        public string VariableKey { get; set; }
        [StringLength(100)]
        public string VariableValue { get; set; }
        public long? SalaryStructureComponentMapID { get; set; }

        [ForeignKey("SalaryComponentID")]
        [InverseProperty("SalaryComponentVariableMaps")]
        public virtual SalaryComponent SalaryComponent { get; set; }
        [ForeignKey("SalaryStructureComponentMapID")]
        [InverseProperty("SalaryComponentVariableMaps")]
        public virtual SalaryStructureComponentMap SalaryStructureComponentMap { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.HR.Payroll
{
    [Table("EmployeeSalaryStructureVariableMaps", Schema = "payroll")]
    public partial class EmployeeSalaryStructureVariableMap
    {
        [Key]
        public long EmployeeSalaryStructureVariableMapIID { get; set; }
        public long? EmployeeSalaryStructureComponentMapID { get; set; }
        public int? SalaryComponentID { get; set; }
        [StringLength(100)]
        public string VariableKey { get; set; }
        [StringLength(100)]
        public string VariableValue { get; set; }      
        public virtual EmployeeSalaryStructureComponentMap EmployeeSalaryStructureComponentMap { get; set; }      
        public virtual SalaryComponent SalaryComponent { get; set; }
    }
}

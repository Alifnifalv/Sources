namespace Eduegate.Domain.Entity.HR.Payroll
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("EmployeeSalaryStructureComponentMaps", Schema = "payroll")]
    public partial class EmployeeSalaryStructureComponentMap
    {
        public EmployeeSalaryStructureComponentMap()
        {
            EmployeePromotionSalaryComponentMaps = new HashSet<EmployeePromotionSalaryComponentMap>();
            EmployeeSalaryStructureVariableMaps = new HashSet<EmployeeSalaryStructureVariableMap>();
        }

        [Key]
        public long EmployeeSalaryStructureComponentMapIID { get; set; }

        public long? EmployeeSalaryStructureID { get; set; }

        public int? SalaryComponentID { get; set; }

        public decimal? Amount { get; set; }

        [StringLength(1000)]
        public string Formula { get; set; }

        public virtual EmployeeSalaryStructure EmployeeSalaryStructure { get; set; }

        public virtual SalaryComponent SalaryComponent { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeePromotionSalaryComponentMap> EmployeePromotionSalaryComponentMaps { get; set; }
        public virtual ICollection<EmployeeSalaryStructureVariableMap> EmployeeSalaryStructureVariableMaps { get; set; }
    }
}

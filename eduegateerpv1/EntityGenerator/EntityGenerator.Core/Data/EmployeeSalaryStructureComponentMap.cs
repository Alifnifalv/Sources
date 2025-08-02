using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EmployeeSalaryStructureComponentMaps", Schema = "payroll")]
    public partial class EmployeeSalaryStructureComponentMap
    {
        public EmployeeSalaryStructureComponentMap()
        {
            EmployeePromotionSalaryComponentMaps = new HashSet<EmployeePromotionSalaryComponentMap>();
        }

        [Key]
        public long EmployeeSalaryStructureComponentMapIID { get; set; }
        public long? EmployeeSalaryStructureID { get; set; }
        public int? SalaryComponentID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        [StringLength(1000)]
        public string Formula { get; set; }

        [ForeignKey("EmployeeSalaryStructureID")]
        [InverseProperty("EmployeeSalaryStructureComponentMaps")]
        public virtual EmployeeSalaryStructure EmployeeSalaryStructure { get; set; }
        [ForeignKey("SalaryComponentID")]
        [InverseProperty("EmployeeSalaryStructureComponentMaps")]
        public virtual SalaryComponent SalaryComponent { get; set; }
        [InverseProperty("EmployeeSalaryStructureComponentMap")]
        public virtual ICollection<EmployeePromotionSalaryComponentMap> EmployeePromotionSalaryComponentMaps { get; set; }
    }
}

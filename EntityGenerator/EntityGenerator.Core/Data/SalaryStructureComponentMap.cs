using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("SalaryStructureComponentMaps", Schema = "payroll")]
    public partial class SalaryStructureComponentMap
    {
        public SalaryStructureComponentMap()
        {
            SalaryComponentVariableMaps = new HashSet<SalaryComponentVariableMap>();
        }

        [Key]
        public long SalaryStructureComponentMapIID { get; set; }
        public int? SalaryComponentID { get; set; }
        public long? SalaryStructureID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        [StringLength(1000)]
        public string Formula { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? MinAmount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? MaxAmount { get; set; }

        [ForeignKey("SalaryComponentID")]
        [InverseProperty("SalaryStructureComponentMaps")]
        public virtual SalaryComponent SalaryComponent { get; set; }
        [ForeignKey("SalaryStructureID")]
        [InverseProperty("SalaryStructureComponentMaps")]
        public virtual SalaryStructure SalaryStructure { get; set; }
        [InverseProperty("SalaryStructureComponentMap")]
        public virtual ICollection<SalaryComponentVariableMap> SalaryComponentVariableMaps { get; set; }
    }
}

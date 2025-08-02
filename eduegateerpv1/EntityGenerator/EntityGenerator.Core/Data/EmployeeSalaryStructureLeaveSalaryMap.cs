using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EmployeeSalaryStructureLeaveSalaryMaps", Schema = "payroll")]
    public partial class EmployeeSalaryStructureLeaveSalaryMap
    {
        [Key]
        public long EmployeeSalaryStructureLeaveSalaryMapIID { get; set; }
        public long? EmployeeSalaryStructureID { get; set; }
        public int? SalaryComponentID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        [StringLength(1000)]
        public string Formula { get; set; }

        [ForeignKey("EmployeeSalaryStructureID")]
        [InverseProperty("EmployeeSalaryStructureLeaveSalaryMaps")]
        public virtual EmployeeSalaryStructure EmployeeSalaryStructure { get; set; }
        [ForeignKey("SalaryComponentID")]
        [InverseProperty("EmployeeSalaryStructureLeaveSalaryMaps")]
        public virtual SalaryComponent SalaryComponent { get; set; }
    }
}

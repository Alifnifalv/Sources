using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EmployeePromotionSalaryComponentMaps", Schema = "payroll")]
    public partial class EmployeePromotionSalaryComponentMap
    {
        [Key]
        public long EmployeePromotionSalaryComponentMapIID { get; set; }
        public long? EmployeePromotionID { get; set; }
        public int? SalaryComponentID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        [StringLength(1000)]
        public string Formula { get; set; }
        public long? EmployeeSalaryStructureComponentMapID { get; set; }

        [ForeignKey("EmployeePromotionID")]
        [InverseProperty("EmployeePromotionSalaryComponentMaps")]
        public virtual EmployeePromotion EmployeePromotion { get; set; }
        [ForeignKey("EmployeeSalaryStructureComponentMapID")]
        [InverseProperty("EmployeePromotionSalaryComponentMaps")]
        public virtual EmployeeSalaryStructureComponentMap EmployeeSalaryStructureComponentMap { get; set; }
        [ForeignKey("SalaryComponentID")]
        [InverseProperty("EmployeePromotionSalaryComponentMaps")]
        public virtual SalaryComponent SalaryComponent { get; set; }
    }
}

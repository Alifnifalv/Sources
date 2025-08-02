namespace Eduegate.Domain.Entity.HR
{
    using Eduegate.Domain.Entity.HR.Models;
    using Eduegate.Domain.Entity.HR.Payroll;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("EmployeePromotionSalaryComponentMaps", Schema = "payroll")]
    public partial class EmployeePromotionSalaryComponentMap
    {
        [Key]
        public long EmployeePromotionSalaryComponentMapIID { get; set; }

        public long? EmployeeSalaryStructureComponentMapID { get; set; }

        public long? EmployeePromotionID { get; set; }

        public int? SalaryComponentID { get; set; }

        public decimal? Amount { get; set; }

        [StringLength(1000)]
        public string Formula { get; set; }
             

        public virtual EmployeePromotion EmployeePromotion { get; set; }

        public virtual EmployeeSalaryStructureComponentMap EmployeeSalaryStructureComponentMap { get; set; }

        public virtual SalaryComponent SalaryComponent { get; set; }
    }
}

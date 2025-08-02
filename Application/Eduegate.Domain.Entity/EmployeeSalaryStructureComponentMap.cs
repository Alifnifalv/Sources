namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("payroll.EmployeeSalaryStructureComponentMaps")]
    public partial class EmployeeSalaryStructureComponentMap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EmployeeSalaryStructureComponentMap()
        {
            EmployeePromotionSalaryComponentMaps = new HashSet<EmployeePromotionSalaryComponentMap>();
            EmployeePromotionSalaryComponentMaps1 = new HashSet<EmployeePromotionSalaryComponentMap>();
        }

        [Key]
        public long EmployeeSalaryStructureComponentMapIID { get; set; }

        public long? EmployeeSalaryStructureID { get; set; }

        public int? SalaryComponentID { get; set; }

        public decimal? Amount { get; set; }

        [StringLength(1000)]
        public string Formula { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeePromotionSalaryComponentMap> EmployeePromotionSalaryComponentMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeePromotionSalaryComponentMap> EmployeePromotionSalaryComponentMaps1 { get; set; }

        public virtual EmployeeSalaryStructure EmployeeSalaryStructure { get; set; }

        public virtual SalaryComponent SalaryComponent { get; set; }
    }
}

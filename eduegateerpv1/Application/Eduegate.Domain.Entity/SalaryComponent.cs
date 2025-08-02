namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("payroll.SalaryComponents")]
    public partial class SalaryComponent
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SalaryComponent()
        {
            EmployeePromotions = new HashSet<EmployeePromotion>();
            EmployeePromotionSalaryComponentMaps = new HashSet<EmployeePromotionSalaryComponentMap>();
            EmployeeSalaries = new HashSet<EmployeeSalary>();
            EmployeeSalaryStructureComponentMaps = new HashSet<EmployeeSalaryStructureComponentMap>();
            EmployeeSalaryStructureLeaveSalaryMaps = new HashSet<EmployeeSalaryStructureLeaveSalaryMap>();
            EmployeeSalaryStructures = new HashSet<EmployeeSalaryStructure>();
            SalaryComponentRelationMaps = new HashSet<SalaryComponentRelationMap>();
            SalaryComponentRelationMaps1 = new HashSet<SalaryComponentRelationMap>();
            SalarySlips = new HashSet<SalarySlip>();
            SalaryStructures = new HashSet<SalaryStructure>();
            SalaryStructureComponentMaps = new HashSet<SalaryStructureComponentMap>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SalaryComponentID { get; set; }

        public byte? ComponentTypeID { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(50)]
        public string Abbreviation { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public byte? SalaryComponentGroupID { get; set; }

        public int? ReportHeadGroupID { get; set; }

        public int? NoOfDaysApplicable { get; set; }

        public virtual ReportHeadGroup ReportHeadGroup { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeePromotion> EmployeePromotions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeePromotionSalaryComponentMap> EmployeePromotionSalaryComponentMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeSalary> EmployeeSalaries { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeSalaryStructureComponentMap> EmployeeSalaryStructureComponentMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeSalaryStructureLeaveSalaryMap> EmployeeSalaryStructureLeaveSalaryMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeSalaryStructure> EmployeeSalaryStructures { get; set; }

        public virtual SalaryComponentGroup SalaryComponentGroup { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalaryComponentRelationMap> SalaryComponentRelationMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalaryComponentRelationMap> SalaryComponentRelationMaps1 { get; set; }

        public virtual SalaryComponentType SalaryComponentType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalarySlip> SalarySlips { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalaryStructure> SalaryStructures { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalaryStructureComponentMap> SalaryStructureComponentMaps { get; set; }
    }
}

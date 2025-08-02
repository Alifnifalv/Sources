namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("payroll.SalaryStructure")]
    public partial class SalaryStructure
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SalaryStructure()
        {
            EmployeePromotions = new HashSet<EmployeePromotion>();
            EmployeePromotions1 = new HashSet<EmployeePromotion>();
            EmployeePromotions2 = new HashSet<EmployeePromotion>();
            EmployeeSalaryStructures = new HashSet<EmployeeSalaryStructure>();
            SalaryStructureComponentMaps = new HashSet<SalaryStructureComponentMap>();
        }

        public long SalaryStructureID { get; set; }

        [StringLength(500)]
        public string StructureName { get; set; }

        public bool? IsActive { get; set; }

        public byte? PayrollFrequencyID { get; set; }

        public bool? IsSalaryBasedOnTimeSheet { get; set; }

        public int? TimeSheetSalaryComponentID { get; set; }

        public decimal? TimeSheetHourRate { get; set; }

        public decimal? TimeSheetLeaveEncashmentPerDay { get; set; }

        public decimal? TimeSheetMaximumBenefits { get; set; }

        public int? PaymentModeID { get; set; }

        public long? AccountID { get; set; }

        public virtual Account Account { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeePromotion> EmployeePromotions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeePromotion> EmployeePromotions1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeePromotion> EmployeePromotions2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeSalaryStructure> EmployeeSalaryStructures { get; set; }

        public virtual PayrollFrequency PayrollFrequency { get; set; }

        public virtual SalaryComponent SalaryComponent { get; set; }

        public virtual SalaryPaymentMode SalaryPaymentMode { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalaryStructureComponentMap> SalaryStructureComponentMaps { get; set; }
    }
}

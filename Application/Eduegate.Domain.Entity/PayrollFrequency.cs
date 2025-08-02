namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("payroll.PayrollFrequencies")]
    public partial class PayrollFrequency
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PayrollFrequency()
        {
            EmployeePromotions = new HashSet<EmployeePromotion>();
            EmployeeSalaryStructures = new HashSet<EmployeeSalaryStructure>();
            SalaryStructures = new HashSet<SalaryStructure>();
        }

        public byte PayrollFrequencyID { get; set; }

        [StringLength(50)]
        public string FrequencyName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeePromotion> EmployeePromotions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeSalaryStructure> EmployeeSalaryStructures { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalaryStructure> SalaryStructures { get; set; }
    }
}

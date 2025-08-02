namespace Eduegate.Domain.Entity.HR.Payroll
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SalaryComponentGroup", Schema = "payroll")]
    public partial class SalaryComponentGroup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SalaryComponentGroup()
        {
            SalaryComponents = new HashSet<SalaryComponent>();
        }
        [Key]
        public byte SalaryComponentGroupID { get; set; }

        [StringLength(50)]
        public string SalaryComponentGroupName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalaryComponent> SalaryComponents { get; set; }
    }
}

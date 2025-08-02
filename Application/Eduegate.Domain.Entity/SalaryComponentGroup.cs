namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("payroll.SalaryComponentGroup")]
    public partial class SalaryComponentGroup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SalaryComponentGroup()
        {
            SalaryComponents = new HashSet<SalaryComponent>();
        }

        public byte SalaryComponentGroupID { get; set; }

        [StringLength(50)]
        public string SalaryComponentGroupName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalaryComponent> SalaryComponents { get; set; }
    }
}

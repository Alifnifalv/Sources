namespace Eduegate.Domain.Entity.HR.Payroll
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ReportHeadGroups", Schema = "mutual")]
    public partial class ReportHeadGroup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ReportHeadGroup()
        {
            SalaryComponents = new HashSet<SalaryComponent>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ReportHeadGroupID { get; set; }

        [StringLength(100)]
        public string ReportHeadGroupName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalaryComponent> SalaryComponents { get; set; }
    }
}

namespace Eduegate.Domain.Entity.HR.Payroll
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SalarySlipStatuses", Schema = "payroll")]
    public partial class SalarySlipStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SalarySlipStatus()
        {
            SalarySlips = new HashSet<SalarySlip>();
        }
        [Key]
        public byte SalarySlipStatusID { get; set; }

        [StringLength(50)]
        public string StatusName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalarySlip> SalarySlips { get; set; }

    }
}

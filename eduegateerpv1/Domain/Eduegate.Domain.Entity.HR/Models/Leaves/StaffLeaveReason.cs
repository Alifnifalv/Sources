namespace Eduegate.Domain.Entity.HR.Leaves
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("StaffLeaveReasons", Schema = "payroll")]
    public partial class StaffLeaveReason
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StaffLeaveReason()
        {
            LeaveApplications = new HashSet<LeaveApplication>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StaffLeaveReasonIID { get; set; }

        [StringLength(500)]
        public string Reason { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LeaveApplication> LeaveApplications { get; set; }
    }
}

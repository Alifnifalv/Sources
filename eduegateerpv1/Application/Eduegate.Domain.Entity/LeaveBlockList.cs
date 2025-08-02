namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("payroll.LeaveBlockLists")]
    public partial class LeaveBlockList
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LeaveBlockList()
        {
            LeaveBlockListApprovers = new HashSet<LeaveBlockListApprover>();
            LeaveBlockListEntries = new HashSet<LeaveBlockListEntry>();
        }

        [Key]
        public long LeaveBlockListIID { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public long? DepartmentID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LeaveBlockListApprover> LeaveBlockListApprovers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LeaveBlockListEntry> LeaveBlockListEntries { get; set; }
    }
}

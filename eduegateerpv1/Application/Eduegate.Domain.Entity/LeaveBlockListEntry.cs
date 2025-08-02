namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("payroll.LeaveBlockListEntries")]
    public partial class LeaveBlockListEntry
    {
        [Key]
        public long LeaveBlockListEntryIID { get; set; }

        public long? LeaveBlockListID { get; set; }

        public DateTime? BlockDate { get; set; }

        [StringLength(500)]
        public string Reason { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual LeaveBlockList LeaveBlockList { get; set; }
    }
}

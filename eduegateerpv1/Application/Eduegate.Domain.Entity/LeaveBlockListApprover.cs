namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("payroll.LeaveBlockListApprovers")]
    public partial class LeaveBlockListApprover
    {
        [Key]
        public long LeaveBlockListApproverIID { get; set; }

        public long? LeaveBlockListID { get; set; }

        public long? EmployeeID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual LeaveBlockList LeaveBlockList { get; set; }
    }
}

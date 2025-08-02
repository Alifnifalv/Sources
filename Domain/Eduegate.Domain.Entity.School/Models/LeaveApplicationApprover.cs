namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("LeaveApplicationApprovers", Schema = "payroll")]
    public partial class LeaveApplicationApprover
    {
        [Key]
        public long LeaveApplicationApproverIID { get; set; }

        public long? LeaveApplicationID { get; set; }

        public long? EmployeeID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual LeaveApplication LeaveApplication { get; set; }
    }
}
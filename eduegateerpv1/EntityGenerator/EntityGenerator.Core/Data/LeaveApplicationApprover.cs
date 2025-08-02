using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("LeaveApplicationApprovers", Schema = "payroll")]
    public partial class LeaveApplicationApprover
    {
        [Key]
        public long LeaveApplicationApproverIID { get; set; }
        public long? LeaveApplicationID { get; set; }
        public long? EmployeeID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("EmployeeID")]
        [InverseProperty("LeaveApplicationApprovers")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("LeaveApplicationID")]
        [InverseProperty("LeaveApplicationApprovers")]
        public virtual LeaveApplication LeaveApplication { get; set; }
    }
}

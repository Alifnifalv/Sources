using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("LeaveBlockListApprovers", Schema = "payroll")]
    public partial class LeaveBlockListApprover
    {
        [Key]
        public long LeaveBlockListApproverIID { get; set; }
        public long? LeaveBlockListID { get; set; }
        public long? EmployeeID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("EmployeeID")]
        [InverseProperty("LeaveBlockListApprovers")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("LeaveBlockListID")]
        [InverseProperty("LeaveBlockListApprovers")]
        public virtual LeaveBlockList LeaveBlockList { get; set; }
    }
}

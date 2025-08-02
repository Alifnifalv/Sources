using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EmployeeLeaveAllocations", Schema = "payroll")]
    public partial class EmployeeLeaveAllocation
    {
        [Key]
        public long LeaveAllocationIID { get; set; }
        public int? LeaveTypeID { get; set; }
        public long? EmployeeID { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
        public double? AllocatedLeaves { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("EmployeeID")]
        [InverseProperty("EmployeeLeaveAllocations")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("LeaveTypeID")]
        [InverseProperty("EmployeeLeaveAllocations")]
        public virtual LeaveType LeaveType { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("LeaveAllocations", Schema = "payroll")]
    public partial class LeaveAllocation
    {
        public long LeaveAllocationIID { get; set; }
        public int? LeaveTypeID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateFrom { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateTo { get; set; }
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
        public int? LeaveGroupID { get; set; }

        [ForeignKey("LeaveGroupID")]
        public virtual LeaveGroup LeaveGroup { get; set; }
        [ForeignKey("LeaveTypeID")]
        public virtual LeaveType LeaveType { get; set; }
    }
}

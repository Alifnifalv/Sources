using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EmployeePromotionLeaveAllocations", Schema = "payroll")]
    public partial class EmployeePromotionLeaveAllocation
    {
        [Key]
        public long EmployeePromotionLeaveAllocationIID { get; set; }
        public long? EmployeePromotionID { get; set; }
        public int? LeaveTypeID { get; set; }
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

        [ForeignKey("EmployeePromotionID")]
        [InverseProperty("EmployeePromotionLeaveAllocations")]
        public virtual EmployeePromotion EmployeePromotion { get; set; }
        [ForeignKey("LeaveTypeID")]
        [InverseProperty("EmployeePromotionLeaveAllocations")]
        public virtual LeaveType LeaveType { get; set; }
    }
}

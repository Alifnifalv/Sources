namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("payroll.EmployeePromotionLeaveAllocations")]
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

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public virtual EmployeePromotion EmployeePromotion { get; set; }

        public virtual LeaveType LeaveType { get; set; }
    }
}

namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("payroll.LeaveAllocations")]
    public partial class LeaveAllocation
    {
        [Key]
        public long LeaveAllocationIID { get; set; }

        public int? LeaveTypeID { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

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

        public int? LeaveGroupID { get; set; }

        public virtual LeaveGroup LeaveGroup { get; set; }

        public virtual LeaveType LeaveType { get; set; }
    }
}

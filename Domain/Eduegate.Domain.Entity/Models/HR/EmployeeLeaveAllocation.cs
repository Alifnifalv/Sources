using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
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

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual LeaveType LeaveType { get; set; }

    }
}
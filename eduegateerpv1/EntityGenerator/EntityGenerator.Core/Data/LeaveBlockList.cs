using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("LeaveBlockLists", Schema = "payroll")]
    public partial class LeaveBlockList
    {
        public LeaveBlockList()
        {
            LeaveBlockListApprovers = new HashSet<LeaveBlockListApprover>();
            LeaveBlockListEntries = new HashSet<LeaveBlockListEntry>();
        }

        [Key]
        public long LeaveBlockListIID { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public long? DepartmentID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [InverseProperty("LeaveBlockList")]
        public virtual ICollection<LeaveBlockListApprover> LeaveBlockListApprovers { get; set; }
        [InverseProperty("LeaveBlockList")]
        public virtual ICollection<LeaveBlockListEntry> LeaveBlockListEntries { get; set; }
    }
}

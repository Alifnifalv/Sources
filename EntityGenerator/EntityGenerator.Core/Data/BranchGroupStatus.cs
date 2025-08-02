using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("BranchGroupStatuses", Schema = "mutual")]
    public partial class BranchGroupStatus
    {
        public BranchGroupStatus()
        {
            BranchGroups = new HashSet<BranchGroup>();
        }

        [Key]
        public byte BranchGroupStatusID { get; set; }
        [StringLength(50)]
        public string StatusName { get; set; }

        [InverseProperty("Status")]
        public virtual ICollection<BranchGroup> BranchGroups { get; set; }
    }
}

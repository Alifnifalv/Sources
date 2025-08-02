using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class BranchGroupStatus
    {
        public BranchGroupStatus()
        {
            this.BranchGroups = new List<BranchGroup>();
        }

        public byte BranchGroupStatusID { get; set; }
        public string StatusName { get; set; }
        public virtual ICollection<BranchGroup> BranchGroups { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class BranchStatus
    {
        public BranchStatus()
        {
            this.Branches = new List<Branch>();
        }

        public byte BranchStatusID { get; set; }
        public string StatusName { get; set; }
        public virtual ICollection<Branch> Branches { get; set; }
    }
}

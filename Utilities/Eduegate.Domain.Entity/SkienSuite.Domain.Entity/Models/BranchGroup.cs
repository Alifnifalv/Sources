using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class BranchGroup
    {
        public BranchGroup()
        {
            this.Branches = new List<Branch>();
        }

        public long BranchGroupIID { get; set; }
        public string GroupName { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public Nullable<byte> StatusID { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public virtual ICollection<Branch> Branches { get; set; }
        public virtual BranchGroupStatus BranchGroupStatus { get; set; }
    }
}

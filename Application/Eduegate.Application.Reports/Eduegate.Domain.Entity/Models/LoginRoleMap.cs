using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class LoginRoleMap
    {
        public long LoginRoleMapIID { get; set; }
        public Nullable<long> LoginID { get; set; }
        public Nullable<int> RoleID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual Login Login { get; set; }
        public virtual Role Role { get; set; }
    }
}

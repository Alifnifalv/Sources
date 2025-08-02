using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class Role
    {
        public Role()
        {
            this.LoginRoleMaps = new List<LoginRoleMap>();
            this.RoleCultureDatas = new List<RoleCultureData>();
            this.RolePermissionMaps = new List<RolePermissionMap>();
            this.TransactionHeads = new List<TransactionHead>();
        }

        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public virtual ICollection<LoginRoleMap> LoginRoleMaps { get; set; }
        public virtual ICollection<RoleCultureData> RoleCultureDatas { get; set; }
        public virtual ICollection<RolePermissionMap> RolePermissionMaps { get; set; }
        public virtual ICollection<TransactionHead> TransactionHeads { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class Permission
    {
        public Permission()
        {
            this.PermissionCultureDatas = new List<PermissionCultureData>();
            this.RolePermissionMaps = new List<RolePermissionMap>();
        }

        public int PermissionID { get; set; }
        public string PermissionName { get; set; }
        public virtual ICollection<PermissionCultureData> PermissionCultureDatas { get; set; }
        public virtual ICollection<RolePermissionMap> RolePermissionMaps { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("Permissions", Schema = "admin")]
    public partial class Permission
    {
        public Permission()
        {
            this.PermissionCultureDatas = new List<PermissionCultureData>();
            this.RolePermissionMaps = new List<RolePermissionMap>();
        }

        [Key]
        public int PermissionID { get; set; }
        public string PermissionName { get; set; }
        public virtual ICollection<PermissionCultureData> PermissionCultureDatas { get; set; }
        public virtual ICollection<RolePermissionMap> RolePermissionMaps { get; set; }
    }
}

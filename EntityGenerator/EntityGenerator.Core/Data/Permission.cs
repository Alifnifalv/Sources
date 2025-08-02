using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Permissions", Schema = "admin")]
    public partial class Permission
    {
        public Permission()
        {
            PermissionCultureDatas = new HashSet<PermissionCultureData>();
            RolePermissionMaps = new HashSet<RolePermissionMap>();
        }

        [Key]
        public int PermissionID { get; set; }
        [StringLength(50)]
        public string PermissionName { get; set; }

        [InverseProperty("Permission")]
        public virtual ICollection<PermissionCultureData> PermissionCultureDatas { get; set; }
        [InverseProperty("Permission")]
        public virtual ICollection<RolePermissionMap> RolePermissionMaps { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Roles", Schema = "admin")]
    public partial class Role
    {
        public Role()
        {
            LoginRoleMaps = new HashSet<LoginRoleMap>();
            RoleCultureDatas = new HashSet<RoleCultureData>();
            RolePermissionMaps = new HashSet<RolePermissionMap>();
            TransactionHeads = new HashSet<TransactionHead>();
        }

        [Key]
        public int RoleID { get; set; }
        [StringLength(50)]
        public string RoleName { get; set; }

        [InverseProperty("Role")]
        public virtual ICollection<LoginRoleMap> LoginRoleMaps { get; set; }
        [InverseProperty("Role")]
        public virtual ICollection<RoleCultureData> RoleCultureDatas { get; set; }
        [InverseProperty("Role")]
        public virtual ICollection<RolePermissionMap> RolePermissionMaps { get; set; }
        [InverseProperty("TransactionRoleNavigation")]
        public virtual ICollection<TransactionHead> TransactionHeads { get; set; }
    }
}

using EntityGenerator.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("Roles", Schema = "admin")]
    public partial class Role
    {
        public Role()
        {
            this.LoginRoleMaps = new List<LoginRoleMap>();
            this.RoleCultureDatas = new List<RoleCultureData>();
            this.RolePermissionMaps = new List<RolePermissionMap>();
            RecruitmentLogins = new HashSet<RecruitmentLogin>();
        }

        [Key]
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public virtual ICollection<LoginRoleMap> LoginRoleMaps { get; set; }
        public virtual ICollection<RoleCultureData> RoleCultureDatas { get; set; }
        public virtual ICollection<RolePermissionMap> RolePermissionMaps { get; set; }
        public virtual ICollection<TransactionHead> TransactionHeads { get; set; }
        public virtual ICollection<RecruitmentLogin> RecruitmentLogins { get; set; }
    }
}

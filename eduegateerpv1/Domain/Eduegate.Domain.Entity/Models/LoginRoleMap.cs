using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("LoginRoleMaps", Schema = "admin")]
    public partial class LoginRoleMap
    {
        [Key]
        public long LoginRoleMapIID { get; set; }
        public Nullable<long> LoginID { get; set; }
        public Nullable<int> RoleID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }
        public virtual Login Login { get; set; }
        public virtual Role Role { get; set; }
    }
}

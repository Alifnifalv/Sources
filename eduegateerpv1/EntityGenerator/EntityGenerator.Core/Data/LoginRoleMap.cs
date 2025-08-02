using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("LoginRoleMaps", Schema = "admin")]
    [Index("LoginID", Name = "IDX_LoginRoleMaps_LoginID_")]
    public partial class LoginRoleMap
    {
        [Key]
        public long LoginRoleMapIID { get; set; }
        public long? LoginID { get; set; }
        public int? RoleID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("LoginID")]
        [InverseProperty("LoginRoleMaps")]
        public virtual Login Login { get; set; }
        [ForeignKey("RoleID")]
        [InverseProperty("LoginRoleMaps")]
        public virtual Role Role { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("RolePermissionMaps", Schema = "admin")]
    public partial class RolePermissionMap
    {
        [Key]
        public long RolePermissionMapIID { get; set; }
        public int? RoleID { get; set; }
        public int? PermissionID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("PermissionID")]
        [InverseProperty("RolePermissionMaps")]
        public virtual Permission Permission { get; set; }
        [ForeignKey("RoleID")]
        [InverseProperty("RolePermissionMaps")]
        public virtual Role Role { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("PermissionCultureDatas", Schema = "admin")]
    public partial class PermissionCultureData
    {
        [Key]
        public int PermissionID { get; set; }
        [Key]
        public byte CultureID { get; set; }
        [StringLength(50)]
        public string PermissionName { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("CultureID")]
        [InverseProperty("PermissionCultureDatas")]
        public virtual Culture Culture { get; set; }
        [ForeignKey("PermissionID")]
        [InverseProperty("PermissionCultureDatas")]
        public virtual Permission Permission { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("RoleCultureDatas", Schema = "admin")]
    public partial class RoleCultureData
    {
        [Key]
        public int RoleID { get; set; }
        [Key]
        public byte CultureID { get; set; }
        [StringLength(50)]
        public string RoleName { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("CultureID")]
        [InverseProperty("RoleCultureDatas")]
        public virtual Culture Culture { get; set; }
        [ForeignKey("RoleID")]
        [InverseProperty("RoleCultureDatas")]
        public virtual Role Role { get; set; }
    }
}

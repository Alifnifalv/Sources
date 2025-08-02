using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("UserViewColumnMaps", Schema = "setting")]
    public partial class UserViewColumnMap
    {
        [Key]
        public long UserViewColumnMapIID { get; set; }
        public long? UserViewID { get; set; }
        public long? ViewColumnID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("UserViewID")]
        [InverseProperty("UserViewColumnMaps")]
        public virtual UserView UserView { get; set; }
    }
}

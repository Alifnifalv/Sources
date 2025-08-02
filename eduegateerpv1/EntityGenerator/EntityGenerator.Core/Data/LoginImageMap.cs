using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("LoginImageMaps", Schema = "admin")]
    public partial class LoginImageMap
    {
        [Key]
        public long LoginImageMapIID { get; set; }
        public long? LoginID { get; set; }
        public long? ContentImageID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("LoginID")]
        [InverseProperty("LoginImageMaps")]
        public virtual Login Login { get; set; }
    }
}

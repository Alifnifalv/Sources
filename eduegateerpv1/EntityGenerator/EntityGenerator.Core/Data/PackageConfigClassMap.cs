using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("PackageConfigClassMaps", Schema = "schools")]
    public partial class PackageConfigClassMap
    {
        [Key]
        public long PackageConfigClassMapIID { get; set; }
        public int? ClassID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public long? PackageConfigID { get; set; }
        [Required]
        public bool? IsActive { get; set; }

        [ForeignKey("ClassID")]
        [InverseProperty("PackageConfigClassMaps")]
        public virtual Class Class { get; set; }
        [ForeignKey("PackageConfigID")]
        [InverseProperty("PackageConfigClassMaps")]
        public virtual PackageConfig PackageConfig { get; set; }
    }
}

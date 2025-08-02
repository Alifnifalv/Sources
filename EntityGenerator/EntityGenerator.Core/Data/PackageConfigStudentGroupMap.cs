using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("PackageConfigStudentGroupMaps", Schema = "schools")]
    public partial class PackageConfigStudentGroupMap
    {
        [Key]
        public long PackageConfigStudentGroupMapIID { get; set; }
        public int? StudentGroupID { get; set; }
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

        [ForeignKey("PackageConfigID")]
        [InverseProperty("PackageConfigStudentGroupMaps")]
        public virtual PackageConfig PackageConfig { get; set; }
        [ForeignKey("StudentGroupID")]
        [InverseProperty("PackageConfigStudentGroupMaps")]
        public virtual StudentGroup StudentGroup { get; set; }
    }
}

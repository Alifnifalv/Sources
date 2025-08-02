using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("PackageConfigStudentMaps", Schema = "schools")]
    public partial class PackageConfigStudentMap
    {
        [Key]
        public long PackageConfigStudentMapIID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public long? StudentID { get; set; }
        public long? PackageConfigID { get; set; }
        public bool? IsActive { get; set; }

        [ForeignKey("PackageConfigID")]
        [InverseProperty("PackageConfigStudentMaps")]
        public virtual PackageConfig PackageConfig { get; set; }
        [ForeignKey("StudentID")]
        [InverseProperty("PackageConfigStudentMaps")]
        public virtual Student Student { get; set; }
    }
}

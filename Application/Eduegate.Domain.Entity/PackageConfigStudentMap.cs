namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.PackageConfigStudentMaps")]
    public partial class PackageConfigStudentMap
    {
        [Key]
        public long PackageConfigStudentMapIID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public long? StudentID { get; set; }

        public long? PackageConfigID { get; set; }

        public bool? IsActive { get; set; }

        public virtual PackageConfig PackageConfig { get; set; }

        public virtual Student Student { get; set; }
    }
}

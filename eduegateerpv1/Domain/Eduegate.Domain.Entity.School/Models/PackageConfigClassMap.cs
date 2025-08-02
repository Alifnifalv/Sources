namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("PackageConfigClassMaps", Schema = "schools")]
   
    public partial class PackageConfigClassMap
    {
        [Key]
        public long PackageConfigClassMapIID { get; set; }

        public int? ClassID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public long? PackageConfigID { get; set; }

        public bool IsActive { get; set; }

        public virtual Class Class { get; set; }

        public virtual PackageConfig PackageConfig { get; set; }
    }
}

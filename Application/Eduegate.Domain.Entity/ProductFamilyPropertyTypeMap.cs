namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("catalog.ProductFamilyPropertyTypeMaps")]
    public partial class ProductFamilyPropertyTypeMap
    {
        [Key]
        public long ProductFamilyPropertyTypeMapIID { get; set; }

        public long? ProductFamilyID { get; set; }

        public byte? PropertyTypeID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public virtual ProductFamily ProductFamily { get; set; }
    }
}

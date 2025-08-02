namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("catalog.ProductFamilyPropertyMaps")]
    public partial class ProductFamilyPropertyMap
    {
        [Key]
        public long ProductFamilyPropertyMapIID { get; set; }

        public long? ProductFamilyID { get; set; }

        public long? ProductPropertyID { get; set; }

        [StringLength(50)]
        public string Value { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public virtual ProductFamily ProductFamily { get; set; }

        public virtual Property Property { get; set; }
    }
}

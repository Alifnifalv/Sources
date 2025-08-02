namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("catalog.ProductVideoMaps")]
    public partial class ProductVideoMap
    {
        [Key]
        public long ProductVideoMapIID { get; set; }

        public long? ProductID { get; set; }

        public long? ProductSKUMapID { get; set; }

        [StringLength(500)]
        public string VideoFile { get; set; }

        public byte? Sequence { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public virtual Product Product { get; set; }

        public virtual ProductSKUMap ProductSKUMap { get; set; }
    }
}

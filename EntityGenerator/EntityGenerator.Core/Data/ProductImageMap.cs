using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ProductImageMaps", Schema = "catalog")]
    [Index("ProductID", Name = "IDX_ProductImageMaps_ProductID_")]
    [Index("ProductID", Name = "IDX_ProductImageMaps_ProductID_ProductSKUMapID__ProductImageTypeID__ImageFile__Sequence__CreatedBy_")]
    [Index("ProductSKUMapID", "ProductImageTypeID", "Sequence", Name = "IDX_ProductImageMaps_ProductSKUMapID__ProductImageTypeID__Sequence_ImageFile")]
    public partial class ProductImageMap
    {
        [Key]
        public long ProductImageMapIID { get; set; }
        public long? ProductID { get; set; }
        public long? ProductSKUMapID { get; set; }
        public long? ProductImageTypeID { get; set; }
        [StringLength(500)]
        public string ImageFile { get; set; }
        public byte? Sequence { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        [StringLength(500)]
        public string ImageFileReference { get; set; }

        [ForeignKey("ProductID")]
        [InverseProperty("ProductImageMaps")]
        public virtual Product Product { get; set; }
        [ForeignKey("ProductSKUMapID")]
        [InverseProperty("ProductImageMaps")]
        public virtual ProductSKUMap ProductSKUMap { get; set; }
    }
}

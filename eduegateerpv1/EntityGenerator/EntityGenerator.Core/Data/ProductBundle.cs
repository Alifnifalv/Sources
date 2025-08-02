using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ProductBundles", Schema = "catalog")]
    [Index("FromProductID", Name = "IDX_ProductBundles_FromProductID_")]
    [Index("ToProductSKUMapID", Name = "IDX_ProductBundles_ToProductSKUMapID_")]
    public partial class ProductBundle
    {
        [Key]
        public long BundleIID { get; set; }
        public long? FromProductID { get; set; }
        public long? ToProductID { get; set; }
        public long? FromProductSKUMapID { get; set; }
        public long? ToProductSKUMapID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? Quantity { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? SellingPrice { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? CostPrice { get; set; }
        [StringLength(250)]
        public string Remarks { get; set; }

        [ForeignKey("FromProductID")]
        [InverseProperty("ProductBundleFromProducts")]
        public virtual Product FromProduct { get; set; }
        [ForeignKey("FromProductSKUMapID")]
        [InverseProperty("ProductBundleFromProductSKUMaps")]
        public virtual ProductSKUMap FromProductSKUMap { get; set; }
        [ForeignKey("ToProductID")]
        [InverseProperty("ProductBundleToProducts")]
        public virtual Product ToProduct { get; set; }
        [ForeignKey("ToProductSKUMapID")]
        [InverseProperty("ProductBundleToProductSKUMaps")]
        public virtual ProductSKUMap ToProductSKUMap { get; set; }
    }
}

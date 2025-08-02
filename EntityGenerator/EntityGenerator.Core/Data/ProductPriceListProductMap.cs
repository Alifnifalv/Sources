using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ProductPriceListProductMaps", Schema = "catalog")]
    public partial class ProductPriceListProductMap
    {
        public ProductPriceListProductMap()
        {
            ProductPriceListProductQuantityMaps = new HashSet<ProductPriceListProductQuantityMap>();
        }

        [Key]
        public long ProductPriceListProductMapIID { get; set; }
        public int? CompanyID { get; set; }
        public long? ProductPriceListID { get; set; }
        public long? ProductID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Price { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? DiscountPrice { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? DiscountPercentage { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Cost { get; set; }

        [ForeignKey("CompanyID")]
        [InverseProperty("ProductPriceListProductMaps")]
        public virtual Company Company { get; set; }
        [ForeignKey("ProductID")]
        [InverseProperty("ProductPriceListProductMaps")]
        public virtual Product Product { get; set; }
        [ForeignKey("ProductPriceListID")]
        [InverseProperty("ProductPriceListProductMaps")]
        public virtual ProductPriceList ProductPriceList { get; set; }
        [InverseProperty("ProductPriceListProductMap")]
        public virtual ICollection<ProductPriceListProductQuantityMap> ProductPriceListProductQuantityMaps { get; set; }
    }
}

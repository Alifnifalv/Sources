namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("catalog.ProductPriceListProductMaps")]
    public partial class ProductPriceListProductMap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductPriceListProductMap()
        {
            ProductPriceListProductQuantityMaps = new HashSet<ProductPriceListProductQuantityMap>();
        }

        [Key]
        public long ProductPriceListProductMapIID { get; set; }

        public int? CompanyID { get; set; }

        public long? ProductPriceListID { get; set; }

        public long? ProductID { get; set; }

        public decimal? Price { get; set; }

        public decimal? DiscountPrice { get; set; }

        public decimal? DiscountPercentage { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public decimal? Cost { get; set; }

        public virtual Company Company { get; set; }

        public virtual ProductPriceList ProductPriceList { get; set; }

        public virtual Product Product { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductPriceListProductQuantityMap> ProductPriceListProductQuantityMaps { get; set; }
    }
}

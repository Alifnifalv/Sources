namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("catalog.ProductPriceListTypes")]
    public partial class ProductPriceListType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductPriceListType()
        {
            ProductPriceLists = new HashSet<ProductPriceList>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short ProductPriceListTypeID { get; set; }

        [StringLength(50)]
        public string PriceListTypeName { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductPriceList> ProductPriceLists { get; set; }
    }
}

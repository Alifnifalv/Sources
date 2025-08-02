namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("catalog.ProductOptions")]
    public partial class ProductOption
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductOption()
        {
            ProductOptionCultureDatas = new HashSet<ProductOptionCultureData>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductOptionID { get; set; }

        [StringLength(100)]
        public string ProductOptionName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductOptionCultureData> ProductOptionCultureDatas { get; set; }
    }
}

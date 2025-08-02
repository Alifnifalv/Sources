using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ProductStatus", Schema = "catalog")]
    public partial class ProductStatu
    {
        public ProductStatu()
        {
            ProductSKUMaps = new HashSet<ProductSKUMap>();
            Products = new HashSet<Product>();
        }

        [Key]
        public byte ProductStatusID { get; set; }
        [StringLength(50)]
        public string StatusName { get; set; }

        [InverseProperty("Status")]
        public virtual ICollection<ProductSKUMap> ProductSKUMaps { get; set; }
        [InverseProperty("Status")]
        public virtual ICollection<Product> Products { get; set; }
    }
}

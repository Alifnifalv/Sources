using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ProductPriceListLevels", Schema = "catalog")]
    public partial class ProductPriceListLevel
    {
        public ProductPriceListLevel()
        {
            ProductPriceLists = new HashSet<ProductPriceList>();
        }

        [Key]
        public short ProductPriceListLevelID { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Description { get; set; }

        [InverseProperty("ProductPriceListLevel")]
        public virtual ICollection<ProductPriceList> ProductPriceLists { get; set; }
    }
}

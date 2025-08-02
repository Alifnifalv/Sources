using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ProductPriceListTypes", Schema = "catalog")]
    public partial class ProductPriceListType
    {
        public ProductPriceListType()
        {
            ProductPriceLists = new HashSet<ProductPriceList>();
        }

        [Key]
        public short ProductPriceListTypeID { get; set; }
        [StringLength(50)]
        public string PriceListTypeName { get; set; }
        [StringLength(100)]
        public string Description { get; set; }

        [InverseProperty("ProductPriceListType")]
        public virtual ICollection<ProductPriceList> ProductPriceLists { get; set; }
    }
}

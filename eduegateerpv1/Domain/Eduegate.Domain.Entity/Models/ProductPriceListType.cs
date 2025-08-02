using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    [Table("ProductPriceListTypes", Schema = "catalog")]
    public partial class ProductPriceListType
    {
        public ProductPriceListType()
        {
            this.ProductPriceLists = new List<ProductPriceList>();
        }

        [Key]
        public short ProductPriceListTypeID { get; set; }
        public string PriceListTypeName { get; set; }
        public string Description { get; set; }
        public virtual ICollection<ProductPriceList> ProductPriceLists { get; set; }
    }
}

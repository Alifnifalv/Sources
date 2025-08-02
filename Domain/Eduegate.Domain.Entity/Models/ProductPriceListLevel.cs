using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    [Table("ProductPriceListLevels", Schema = "catalog")]
    public partial class ProductPriceListLevel
    {
        public ProductPriceListLevel()
        {
            this.ProductPriceLists = new List<ProductPriceList>();
        }

        [Key]
        public short ProductPriceListLevelID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<ProductPriceList> ProductPriceLists { get; set; }
    }
}

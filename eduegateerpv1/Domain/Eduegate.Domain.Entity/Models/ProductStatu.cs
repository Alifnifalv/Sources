using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Security.Secured;

namespace Eduegate.Domain.Entity.Models
{
    [Table("ProductStatus", Schema = "catalog")]
    public partial class ProductStatu : ISecuredEntity
    {
        public ProductStatu()
        {
            this.Products = new List<Product>();
            this.ProductSKUMaps = new List<ProductSKUMap>();
        }

        [Key]
        public byte ProductStatusID { get; set; }
        public string StatusName { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<ProductSKUMap> ProductSKUMaps { get; set; }

        public long GetIID()
        {
            return ProductStatusID;
        }
    }
}

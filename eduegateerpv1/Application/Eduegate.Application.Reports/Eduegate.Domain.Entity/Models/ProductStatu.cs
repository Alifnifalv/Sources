using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Security.Secured;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductStatu : ISecuredEntity
    {
        public ProductStatu()
        {
            this.Products = new List<Product>();
            this.ProductSKUMaps = new List<ProductSKUMap>();
        }

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

using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductType
    {
        public ProductType()
        {
            this.ProductTypeDeliveryTypeMaps = new List<ProductTypeDeliveryTypeMap>();
        }

        public int ProductTypeID { get; set; }
        public string ProductTypeName { get; set; }
        public virtual ICollection<ProductTypeDeliveryTypeMap> ProductTypeDeliveryTypeMaps { get; set; }
    }
}

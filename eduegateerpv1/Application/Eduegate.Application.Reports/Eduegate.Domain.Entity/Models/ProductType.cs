using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductType
    {
        public int ProductTypeID { get; set; }
        public string ProductTypeName { get; set; }
        public virtual ICollection<ProductTypeDeliveryTypeMap> ProductTypeDeliveryTypeMaps { get; set; }
    }
}

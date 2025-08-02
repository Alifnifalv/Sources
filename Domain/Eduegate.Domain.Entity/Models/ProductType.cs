using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("ProductTypes", Schema = "catalog")]
    public partial class ProductType
    {
        [Key]
        public int ProductTypeID { get; set; }
        public string ProductTypeName { get; set; }
        public virtual ICollection<ProductTypeDeliveryTypeMap> ProductTypeDeliveryTypeMaps { get; set; }
    }
}

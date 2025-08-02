using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ProductTypes", Schema = "catalog")]
    public partial class ProductType
    {
        public ProductType()
        {
            ProductTypeDeliveryTypeMaps = new HashSet<ProductTypeDeliveryTypeMap>();
        }

        [Key]
        public int ProductTypeID { get; set; }
        [Required]
        [StringLength(50)]
        public string ProductTypeName { get; set; }

        [InverseProperty("ProductType")]
        public virtual ICollection<ProductTypeDeliveryTypeMap> ProductTypeDeliveryTypeMaps { get; set; }
    }
}

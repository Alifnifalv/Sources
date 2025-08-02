using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("DeliveryOrderTypes", Schema = "inventory")]
    public partial class DeliveryOrderType
    {
        public DeliveryOrderType()
        {
            ShoppingCart1 = new HashSet<ShoppingCart1>();
        }

        [Key]
        public byte DeliveryOrderTypeID { get; set; }
        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        [InverseProperty("DeliveryOrderType")]
        public virtual ICollection<ShoppingCart1> ShoppingCart1 { get; set; }
    }
}

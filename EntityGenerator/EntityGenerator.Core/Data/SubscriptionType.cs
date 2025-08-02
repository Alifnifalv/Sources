using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("SubscriptionTypes", Schema = "inventory")]
    public partial class SubscriptionType
    {
        public SubscriptionType()
        {
            ShoppingCart1 = new HashSet<ShoppingCart1>();
        }

        [Key]
        public short SubscriptionTypeID { get; set; }
        [StringLength(100)]
        public string SubscriptionName { get; set; }

        [InverseProperty("SubscriptionType")]
        public virtual ICollection<ShoppingCart1> ShoppingCart1 { get; set; }
    }
}

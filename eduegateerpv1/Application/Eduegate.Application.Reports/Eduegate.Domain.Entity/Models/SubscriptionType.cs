using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Eduegate.Domain.Entity.Models
{
    [Table("inventory.SubscriptionTypes")]
    public partial class SubscriptionType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SubscriptionType()
        {
            ShoppingCarts = new HashSet<ShoppingCart>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short SubscriptionTypeID { get; set; }

        [StringLength(100)]
        public string SubscriptionName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
    }
}

namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("inventory.SubscriptionTypes")]
    public partial class SubscriptionType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SubscriptionType()
        {
            ShoppingCarts = new HashSet<ShoppingCart1>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short SubscriptionTypeID { get; set; }

        [StringLength(100)]
        public string SubscriptionName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShoppingCart1> ShoppingCarts { get; set; }
    }
}

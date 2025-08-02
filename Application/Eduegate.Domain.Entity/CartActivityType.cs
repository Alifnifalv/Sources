namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("inventory.CartActivityTypes")]
    public partial class CartActivityType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CartActivityType()
        {
            ShoppingCartActivityLogs = new HashSet<ShoppingCartActivityLog>();
        }

        public byte CartActivityTypeID { get; set; }

        [StringLength(50)]
        public string ActivityTypeName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShoppingCartActivityLog> ShoppingCartActivityLogs { get; set; }
    }
}

namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("inventory.CartActivityStatuses")]
    public partial class CartActivityStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CartActivityStatus()
        {
            ShoppingCartActivityLogs = new HashSet<ShoppingCartActivityLog>();
        }

        public byte CartActivityStatusID { get; set; }

        [StringLength(50)]
        public string ActivtyStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShoppingCartActivityLog> ShoppingCartActivityLogs { get; set; }
    }
}

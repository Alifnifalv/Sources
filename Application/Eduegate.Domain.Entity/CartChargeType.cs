namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("inventory.CartChargeTypes")]
    public partial class CartChargeType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CartChargeType()
        {
            CartCharges = new HashSet<CartCharge>();
            ShoppingCartChargeMaps = new HashSet<ShoppingCartChargeMap>();
            TransactionHeadChargeMaps = new HashSet<TransactionHeadChargeMap>();
        }

        public byte CartChargeTypeID { get; set; }

        [StringLength(100)]
        public string ChargeTypeName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CartCharge> CartCharges { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShoppingCartChargeMap> ShoppingCartChargeMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransactionHeadChargeMap> TransactionHeadChargeMaps { get; set; }
    }
}

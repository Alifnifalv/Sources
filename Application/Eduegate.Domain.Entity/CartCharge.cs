namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("inventory.CartCharges")]
    public partial class CartCharge
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CartCharge()
        {
            ShoppingCartChargeMaps = new HashSet<ShoppingCartChargeMap>();
            TransactionHeadChargeMaps = new HashSet<TransactionHeadChargeMap>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CartChargeID { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public byte? CartChargeTypeID { get; set; }

        public bool? IsDeduction { get; set; }

        public decimal? Percentage { get; set; }

        public decimal? Amount { get; set; }

        public virtual CartChargeType CartChargeType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShoppingCartChargeMap> ShoppingCartChargeMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransactionHeadChargeMap> TransactionHeadChargeMaps { get; set; }
    }
}

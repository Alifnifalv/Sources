namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("inventory.ShoppingCartItems")]
    public partial class ShoppingCartItem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ShoppingCartItem()
        {
            ShoppingCartActivityLogs = new HashSet<ShoppingCartActivityLog>();
        }

        public long ShoppingCartItemID { get; set; }

        public long? ShoppingCartID { get; set; }

        public long? ProductSKUMapID { get; set; }

        public decimal? Quantity { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? DeliveryTypeID { get; set; }

        public int? DeliveryDays { get; set; }

        public decimal? NetWeight { get; set; }

        public long? BranchID { get; set; }

        public decimal? CostPrice { get; set; }

        public decimal? ProductPrice { get; set; }

        public decimal? ProductDiscountPrice { get; set; }

        public int? DisplayRange { get; set; }

        public bool? IsDeleted { get; set; }

        public long? ParentCartItemID { get; set; }

        public int? ProductOptionID { get; set; }

        public long? TransactionDetailID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShoppingCartActivityLog> ShoppingCartActivityLogs { get; set; }

        public virtual ShoppingCart1 ShoppingCart { get; set; }
    }
}

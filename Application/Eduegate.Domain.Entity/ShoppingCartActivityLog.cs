namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("inventory.ShoppingCartActivityLogs")]
    public partial class ShoppingCartActivityLog
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ShoppingCartActivityLog()
        {
            ShoppingCartActivityLogCultreDatas = new HashSet<ShoppingCartActivityLogCultreData>();
        }

        [Key]
        public long ShoppingCartActivityLogIID { get; set; }

        public long? ShoppingCartID { get; set; }

        public long? ShoppingCartItemID { get; set; }

        public byte CartActivityTypeID { get; set; }

        [StringLength(2000)]
        public string Message { get; set; }

        public byte? CartActivityStatusID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ExpiredDateTime { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public virtual CartActivityStatus CartActivityStatus { get; set; }

        public virtual CartActivityType CartActivityType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShoppingCartActivityLogCultreData> ShoppingCartActivityLogCultreDatas { get; set; }

        public virtual ShoppingCartItem ShoppingCartItem { get; set; }

        public virtual ShoppingCart1 ShoppingCart { get; set; }
    }
}

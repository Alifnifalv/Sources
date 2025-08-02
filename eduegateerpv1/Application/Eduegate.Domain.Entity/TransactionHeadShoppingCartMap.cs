namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("inventory.TransactionHeadShoppingCartMaps")]
    public partial class TransactionHeadShoppingCartMap
    {
        [Key]
        public long TransactionHeadShoppingCartMapIID { get; set; }

        public long? TransactionHeadID { get; set; }

        public long? ShoppingCartID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public DateTime? CreatedBy { get; set; }

        public DateTime? UpdatedBy { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public byte? StatusID { get; set; }

        public decimal? DeliveryCharge { get; set; }

        public virtual ShoppingCart1 ShoppingCart { get; set; }

        public virtual TransactionHead TransactionHead { get; set; }

        public virtual Status Status { get; set; }
    }
}

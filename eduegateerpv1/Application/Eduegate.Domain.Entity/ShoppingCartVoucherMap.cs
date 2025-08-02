namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("inventory.ShoppingCartVoucherMaps")]
    public partial class ShoppingCartVoucherMap
    {
        [Key]
        public long ShoppingCartVoucherMapIID { get; set; }

        public long? ShoppingCartID { get; set; }

        public long? VoucherID { get; set; }

        public decimal? Amount { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public byte? StatusID { get; set; }

        public virtual ShoppingCart1 ShoppingCart { get; set; }

        public virtual Voucher Voucher { get; set; }

        public virtual Status Status { get; set; }
    }
}

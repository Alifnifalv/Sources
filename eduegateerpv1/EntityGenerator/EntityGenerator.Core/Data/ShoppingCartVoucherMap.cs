using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("ShoppingCartVoucherMaps", Schema = "inventory")]
    public partial class ShoppingCartVoucherMap
    {
        public long ShoppingCartVoucherMapIID { get; set; }
        public long? ShoppingCartID { get; set; }
        public long? VoucherID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public byte[] TimeStamps { get; set; }
        public byte? StatusID { get; set; }

        [ForeignKey("ShoppingCartID")]
        public virtual ShoppingCart1 ShoppingCart { get; set; }
        [ForeignKey("StatusID")]
        public virtual Status Status { get; set; }
        [ForeignKey("VoucherID")]
        public virtual Voucher Voucher { get; set; }
    }
}

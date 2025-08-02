using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TransactionHeadShoppingCartMaps", Schema = "inventory")]
    public partial class TransactionHeadShoppingCartMap
    {
        [Key]
        public long TransactionHeadShoppingCartMapIID { get; set; }
        public long? TransactionHeadID { get; set; }
        public long? ShoppingCartID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedBy { get; set; }
        public byte[] TimeStamps { get; set; }
        public byte? StatusID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? DeliveryCharge { get; set; }

        [ForeignKey("ShoppingCartID")]
        [InverseProperty("TransactionHeadShoppingCartMaps")]
        public virtual ShoppingCart1 ShoppingCart { get; set; }
        [ForeignKey("StatusID")]
        [InverseProperty("TransactionHeadShoppingCartMaps")]
        public virtual Status Status { get; set; }
        [ForeignKey("TransactionHeadID")]
        [InverseProperty("TransactionHeadShoppingCartMaps")]
        public virtual TransactionHead TransactionHead { get; set; }
    }
}

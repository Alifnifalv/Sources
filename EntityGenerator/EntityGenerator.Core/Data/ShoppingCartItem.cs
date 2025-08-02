using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ShoppingCartItems", Schema = "inventory")]
    [Index("ProductSKUMapID", Name = "IDX_ShoppingCartItems_ProductSKUMapID_")]
    [Index("ShoppingCartID", Name = "IDX_ShoppingCartItems_ShoppingCartID_")]
    [Index("ShoppingCartID", Name = "IDX_ShoppingCartItems_ShoppingCartID_ProductSKUMapID")]
    [Index("ShoppingCartID", Name = "IDX_ShoppingCartItems_ShoppingCartID_ProductSKUMapID__Quantity__CreatedDate__UpdatedDate__DeliveryT")]
    public partial class ShoppingCartItem
    {
        public ShoppingCartItem()
        {
            ShoppingCartActivityLogs = new HashSet<ShoppingCartActivityLog>();
        }

        [Key]
        public long ShoppingCartItemID { get; set; }
        public long? ShoppingCartID { get; set; }
        public long? ProductSKUMapID { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal? Quantity { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? DeliveryTypeID { get; set; }
        public int? DeliveryDays { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? NetWeight { get; set; }
        public long? BranchID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? CostPrice { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? ProductPrice { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? ProductDiscountPrice { get; set; }
        public int? DisplayRange { get; set; }
        public bool? IsDeleted { get; set; }
        public long? ParentCartItemID { get; set; }
        public int? ProductOptionID { get; set; }
        public long? TransactionDetailID { get; set; }

        [ForeignKey("ShoppingCartID")]
        [InverseProperty("ShoppingCartItems")]
        public virtual ShoppingCart1 ShoppingCart { get; set; }
        [InverseProperty("ShoppingCartItem")]
        public virtual ICollection<ShoppingCartActivityLog> ShoppingCartActivityLogs { get; set; }
    }
}

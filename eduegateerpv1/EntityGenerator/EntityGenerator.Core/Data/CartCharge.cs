using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CartCharges", Schema = "inventory")]
    public partial class CartCharge
    {
        public CartCharge()
        {
            ShoppingCartChargeMaps = new HashSet<ShoppingCartChargeMap>();
            TransactionHeadChargeMaps = new HashSet<TransactionHeadChargeMap>();
        }

        [Key]
        public int CartChargeID { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public byte? CartChargeTypeID { get; set; }
        public bool? IsDeduction { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Percentage { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }

        [ForeignKey("CartChargeTypeID")]
        [InverseProperty("CartCharges")]
        public virtual CartChargeType CartChargeType { get; set; }
        [InverseProperty("CartCharge")]
        public virtual ICollection<ShoppingCartChargeMap> ShoppingCartChargeMaps { get; set; }
        [InverseProperty("CartCharge")]
        public virtual ICollection<TransactionHeadChargeMap> TransactionHeadChargeMaps { get; set; }
    }
}

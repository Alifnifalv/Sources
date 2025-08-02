using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CartChargeTypes", Schema = "inventory")]
    public partial class CartChargeType
    {
        public CartChargeType()
        {
            CartCharges = new HashSet<CartCharge>();
            ShoppingCartChargeMaps = new HashSet<ShoppingCartChargeMap>();
            TransactionHeadChargeMaps = new HashSet<TransactionHeadChargeMap>();
        }

        [Key]
        public byte CartChargeTypeID { get; set; }
        [StringLength(100)]
        public string ChargeTypeName { get; set; }

        [InverseProperty("CartChargeType")]
        public virtual ICollection<CartCharge> CartCharges { get; set; }
        [InverseProperty("CartChargeType")]
        public virtual ICollection<ShoppingCartChargeMap> ShoppingCartChargeMaps { get; set; }
        [InverseProperty("CartChargeType")]
        public virtual ICollection<TransactionHeadChargeMap> TransactionHeadChargeMaps { get; set; }
    }
}

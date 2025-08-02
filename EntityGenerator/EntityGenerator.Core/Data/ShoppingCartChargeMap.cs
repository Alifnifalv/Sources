using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ShoppingCartChargeMaps", Schema = "inventory")]
    public partial class ShoppingCartChargeMap
    {
        [Key]
        public long ShoppingCartChargeMapIID { get; set; }
        public long? ShoppingCartID { get; set; }
        public int? CartChargeID { get; set; }
        public bool? IsDeduction { get; set; }
        public byte? CartChargeTypeID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Percentage { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        [StringLength(500)]
        public string Description { get; set; }

        [ForeignKey("CartChargeID")]
        [InverseProperty("ShoppingCartChargeMaps")]
        public virtual CartCharge CartCharge { get; set; }
        [ForeignKey("CartChargeTypeID")]
        [InverseProperty("ShoppingCartChargeMaps")]
        public virtual CartChargeType CartChargeType { get; set; }
        [ForeignKey("ShoppingCartID")]
        [InverseProperty("ShoppingCartChargeMaps")]
        public virtual ShoppingCart1 ShoppingCart { get; set; }
    }
}

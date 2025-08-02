namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("inventory.ShoppingCartChargeMaps")]
    public partial class ShoppingCartChargeMap
    {
        [Key]
        public long ShoppingCartChargeMapIID { get; set; }

        public long? ShoppingCartID { get; set; }

        public int? CartChargeID { get; set; }

        public bool? IsDeduction { get; set; }

        public byte? CartChargeTypeID { get; set; }

        public decimal? Percentage { get; set; }

        public decimal? Amount { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public virtual CartCharge CartCharge { get; set; }

        public virtual CartChargeType CartChargeType { get; set; }

        public virtual ShoppingCart1 ShoppingCart { get; set; }
    }
}

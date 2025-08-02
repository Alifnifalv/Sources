namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("inventory.ShoppingCartActivitySKUMaps")]
    public partial class ShoppingCartActivitySKUMap
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ShoppingCartActivityLogID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ProductSKUMapID { get; set; }

        [StringLength(1000)]
        public string Notes { get; set; }

        public byte? ActivityActionStatusID { get; set; }

        public decimal? Quantity { get; set; }
    }
}

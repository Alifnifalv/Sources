namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("inventory.ShoppingCartActivityLogCultreDatas")]
    public partial class ShoppingCartActivityLogCultreData
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ShoppingCartActivityLogID { get; set; }

        [Key]
        [Column(Order = 1)]
        public byte CultreID { get; set; }

        [StringLength(2000)]
        public string Message { get; set; }

        public virtual Culture Culture { get; set; }

        public virtual ShoppingCartActivityLog ShoppingCartActivityLog { get; set; }
    }
}

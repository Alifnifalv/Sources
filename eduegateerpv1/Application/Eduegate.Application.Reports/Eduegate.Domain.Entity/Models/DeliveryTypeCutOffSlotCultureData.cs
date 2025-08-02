namespace Eduegate.Domain.Entity
{
    using Eduegate.Domain.Entity.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("inventory.DeliveryTypeCutOffSlotCultureDatas")]
    public partial class DeliveryTypeCutOffSlotCultureData
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DeliveryTypeCutOffSlotID { get; set; }

        [Key]
        [Column(Order = 1)]
        public byte CultureID { get; set; }

        public string WarningMessage { get; set; }

        public string TooltipMessage { get; set; }

        public virtual Culture Culture { get; set; }

        public virtual DeliveryTypeCutOffSlot DeliveryTypeCutOffSlot { get; set; }
    }
}

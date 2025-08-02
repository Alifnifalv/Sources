namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("inventory.DeliveryTypeTimeSlotMapsCulture")]
    public partial class DeliveryTypeTimeSlotMapsCulture
    {
        [Key]
        [Column(Order = 0)]
        public byte CultureID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long DeliveryTypeTimeSlotMapID { get; set; }

        [StringLength(100)]
        public string CutOffDisplayText { get; set; }

        public virtual DeliveryTypeTimeSlotMap DeliveryTypeTimeSlotMap { get; set; }

        public virtual Culture Culture { get; set; }
    }
}

namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("inventory.DeliveryTimeSlotBranchMaps")]
    public partial class DeliveryTimeSlotBranchMap
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long DeliveryTimeSlotBranchMapID { get; set; }

        public int? DeliveryTimeSlotID { get; set; }

        public long? BranchID { get; set; }

        public int? NoOfCutOffOrder { get; set; }

        public byte? CutOffHour { get; set; }

        public virtual Branch Branch { get; set; }

        public virtual DeliveryTimeSlot DeliveryTimeSlot { get; set; }
    }
}

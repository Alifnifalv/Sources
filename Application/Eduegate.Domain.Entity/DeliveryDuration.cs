namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("inventory.DeliveryDuration")]
    public partial class DeliveryDuration
    {
        public byte DeliveryDurationID { get; set; }

        [StringLength(50)]
        public string DurationName { get; set; }
    }
}

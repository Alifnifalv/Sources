namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("orders.OrderRoleTracking")]
    public partial class OrderRoleTracking
    {
        [Key]
        public long OrderTrackingIID { get; set; }

        public long TransactionHeadID { get; set; }

        [StringLength(150)]
        public string DevicePlatform { get; set; }

        [StringLength(150)]
        public string DeviceVersion { get; set; }

        public virtual TransactionHead TransactionHead { get; set; }
    }
}

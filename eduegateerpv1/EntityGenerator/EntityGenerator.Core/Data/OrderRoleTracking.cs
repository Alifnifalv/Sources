using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("OrderRoleTracking", Schema = "orders")]
    public partial class OrderRoleTracking
    {
        [Key]
        public long OrderTrackingIID { get; set; }
        public long TransactionHeadID { get; set; }
        [StringLength(150)]
        public string DevicePlatform { get; set; }
        [StringLength(150)]
        public string DeviceVersion { get; set; }

        [ForeignKey("TransactionHeadID")]
        [InverseProperty("OrderRoleTrackings")]
        public virtual TransactionHead TransactionHead { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class OrderTracking
    {
        [Key]
        public long OrderTrackingIID { get; set; }
        public Nullable<long> OrderID { get; set; }
        public Nullable<byte> StatusID { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> StatusDate { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public virtual TransactionHead TransactionHead { get; set; }
        public virtual TransactionStatus TransactionStatus { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("NotificationsProcessing", Schema = "notification")]
    public partial class NotificationsProcessing
    {
        [Key]
        public long NotificationProcessingIID { get; set; }
        public int NotificationTypeID { get; set; }
        public long NotificationQueueID { get; set; }
        public Nullable<bool> IsReprocess { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        //public byte[] TimeStamps { get; set; }
        public virtual NotificationType NotificationType { get; set; }
    }
}

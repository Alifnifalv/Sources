using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("NotificationQueueParentMaps", Schema = "notification")]
    public class NotificationQueueParentMap
    {
        [Key]
        public long NotificationQueueParentMapIID { get; set; }
        public long NotificationQueueID { get; set; }
        public long ParentNotificationQueueID { get; set; }
        public Nullable<bool> IsNotificationSuccess { get; set; }
    }
}

namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("notification.NotificationQueueParentMaps")]
    public partial class NotificationQueueParentMap
    {
        [Key]
        public long NotificationQueueParentMapIID { get; set; }

        public long NotificationQueueID { get; set; }

        public long ParentNotificationQueueID { get; set; }

        public bool? IsNotificationSuccess { get; set; }
    }
}

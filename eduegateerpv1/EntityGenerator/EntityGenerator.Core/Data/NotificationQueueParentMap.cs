using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("NotificationQueueParentMaps", Schema = "notification")]
    public partial class NotificationQueueParentMap
    {
        [Key]
        public long NotificationQueueParentMapIID { get; set; }
        public long NotificationQueueID { get; set; }
        public long ParentNotificationQueueID { get; set; }
        public bool? IsNotificationSuccess { get; set; }
    }
}

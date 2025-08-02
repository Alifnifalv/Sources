using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("NotificationStatuses", Schema = "notification")]
    public partial class NotificationStatus
    {
        public NotificationStatus()
        {
            NotificationLogs = new HashSet<NotificationLog>();
        }

        [Key]
        public int NotificationStatusID { get; set; }
        [StringLength(50)]
        public string Description { get; set; }

        [InverseProperty("NotificationStatus")]
        public virtual ICollection<NotificationLog> NotificationLogs { get; set; }
    }
}

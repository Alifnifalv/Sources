using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("NotificationStatuses", Schema = "notification")]
    public partial class NotificationStatus
    {
        [Key]
        public int NotificationStatusID { get; set; }
        public string Description { get; set; }
    }
}

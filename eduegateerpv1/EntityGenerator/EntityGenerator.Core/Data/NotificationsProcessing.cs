using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("NotificationsProcessing", Schema = "notification")]
    public partial class NotificationsProcessing
    {
        [Key]
        public long NotificationProcessingIID { get; set; }
        public int NotificationTypeID { get; set; }
        public long NotificationQueueID { get; set; }
        public bool? IsReprocess { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("NotificationTypeID")]
        [InverseProperty("NotificationsProcessings")]
        public virtual NotificationType NotificationType { get; set; }
    }
}

namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("notification.NotificationsProcessing")]
    public partial class NotificationsProcessing
    {
        [Key]
        public long NotificationProcessingIID { get; set; }

        public int NotificationTypeID { get; set; }

        public long NotificationQueueID { get; set; }

        public bool? IsReprocess { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public long? CreatedBy { get; set; }

        public long? UpdatedBy { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public virtual NotificationType NotificationType { get; set; }
    }
}

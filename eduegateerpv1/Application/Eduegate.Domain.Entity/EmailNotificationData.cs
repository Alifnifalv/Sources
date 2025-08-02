namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("notification.EmailNotificationData")]
    public partial class EmailNotificationData
    {
        [Key]
        public long EmailMetaDataIID { get; set; }

        public long NotificationQueueID { get; set; }

        public int EmailNotificationTypeID { get; set; }

        public string EmailData { get; set; }

        [StringLength(50)]
        public string TemplateName { get; set; }

        [StringLength(500)]
        public string Subject { get; set; }

        [StringLength(1000)]
        public string FromEmailID { get; set; }

        [StringLength(1000)]
        public string ToEmailID { get; set; }

        public string SerializedJsonParameters { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public long? UpdatedBy { get; set; }

        public long? CreatedBy { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public virtual EmailNotificationType EmailNotificationType { get; set; }
    }
}

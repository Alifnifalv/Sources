namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("notification.SMSNotificationDatas")]
    public partial class SMSNotificationData
    {
        [Key]
        [Column(Order = 0)]
        public long SMSNotificationDataIID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long NotificationQueueID { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SMSNotificationTypeID { get; set; }

        public string SMSContent { get; set; }

        [StringLength(50)]
        public string TemplateName { get; set; }

        [StringLength(500)]
        public string Subject { get; set; }

        [StringLength(1000)]
        public string FromMobileNumber { get; set; }

        [StringLength(1000)]
        public string ToMobileNumber { get; set; }

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

        public virtual SMSNotificationType SMSNotificationType { get; set; }
    }
}

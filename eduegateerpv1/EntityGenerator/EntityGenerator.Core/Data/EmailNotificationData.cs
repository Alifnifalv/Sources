using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EmailNotificationData", Schema = "notification")]
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
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public long? CreatedBy { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("EmailNotificationTypeID")]
        [InverseProperty("EmailNotificationDatas")]
        public virtual EmailNotificationType EmailNotificationType { get; set; }
    }
}

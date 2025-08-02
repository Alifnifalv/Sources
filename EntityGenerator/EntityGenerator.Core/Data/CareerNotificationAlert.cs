using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("CareerNotificationAlerts", Schema = "notification")]
    public partial class CareerNotificationAlert
    {
        public long NotificationAlertIID { get; set; }
        [StringLength(1000)]
        public string Message { get; set; }
        public long? FromLoginID { get; set; }
        public long? ToLoginID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? NotificationDate { get; set; }
        public int? AlertStatusID { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public int? AlertTypeID { get; set; }
        public long? ReferenceID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ExpiryDate { get; set; }
        public long? ReferenceScreenID { get; set; }
        public bool? IsITCordinator { get; set; }
    }
}

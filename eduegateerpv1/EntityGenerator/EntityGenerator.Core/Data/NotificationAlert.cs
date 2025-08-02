using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("NotificationAlerts", Schema = "notification")]
    [Index("ToLoginID", "AlertStatusID", Name = "IDX_NotificationAlerts_ToLoginID__AlertStatusID_Message__FromLoginID__NotificationDate__CreatedBy__")]
    public partial class NotificationAlert
    {
        [Key]
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

        [ForeignKey("AlertStatusID")]
        [InverseProperty("NotificationAlerts")]
        public virtual AlertStatus AlertStatus { get; set; }
        [ForeignKey("AlertTypeID")]
        [InverseProperty("NotificationAlerts")]
        public virtual AlertType AlertType { get; set; }
        [ForeignKey("FromLoginID")]
        [InverseProperty("NotificationAlertFromLogins")]
        public virtual Login FromLogin { get; set; }
        [ForeignKey("ToLoginID")]
        [InverseProperty("NotificationAlertToLogins")]
        public virtual Login ToLogin { get; set; }
    }
}

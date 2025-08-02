namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("notification.NotificationAlerts")]
    public partial class NotificationAlert
    {
        [Key]
        public long NotificationAlertIID { get; set; }

        [StringLength(1000)]
        public string Message { get; set; }

        public long? FromLoginID { get; set; }

        public long? ToLoginID { get; set; }

        public DateTime? NotificationDate { get; set; }

        public int? AlertStatusID { get; set; }

        public long? CreatedBy { get; set; }

        public long? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public int? AlertTypeID { get; set; }

        public long? ReferenceID { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public long? ReferenceScreenID { get; set; }

        public bool? IsITCordinator { get; set; }

        public virtual Login Login { get; set; }

        public virtual Login Login1 { get; set; }

        public virtual AlertStatus AlertStatus { get; set; }

        public virtual AlertType AlertType { get; set; }
    }
}

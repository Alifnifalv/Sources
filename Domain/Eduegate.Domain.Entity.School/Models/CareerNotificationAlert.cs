namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("CareerNotificationAlerts", Schema = "notification")]
    public partial class CareerNotificationAlert
    {
        [Key]
        public long NotificationAlertIID { get; set; }

        public string Message { get; set; }

        public long? FromLoginID { get; set; }

        public long? ToLoginID { get; set; }

        public DateTime? NotificationDate { get; set; }

        public int? AlertStatusID { get; set; }

        public long? CreatedBy { get; set; }

        public long? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? AlertTypeID { get; set; }

        public long? ReferenceID { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public long? ReferenceScreenID { get; set; }

        public bool? IsITCordinator { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("NotificationAlerts", Schema = "notification")]
    public partial class NotificationAlert
    {
        [Key]
        public long NotificationAlertIID { get; set; }
        public string Message { get; set; }
        public Nullable<long> FromLoginID { get; set; }
        public Nullable<long> ToLoginID { get; set; }
        public Nullable<System.DateTime> NotificationDate { get; set; }
        public Nullable<int> AlertStatusID { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public Nullable<int> AlertTypeID { get; set; }
        public Nullable<long> ReferenceID { get; set; }
        public long? ReferenceScreenID { get; set; }
        public bool? IsITCordinator { get; set; }
        public virtual Login Login { get; set; }
        public virtual Login Login1 { get; set; }
        public virtual AlertStatus AlertStatus { get; set; }
    }
}

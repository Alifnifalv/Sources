using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class NotificationAlert
    {
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
        public byte[] TimeStamps { get; set; }
        public Nullable<int> AlertTypeID { get; set; }
        public Nullable<long> ReferenceID { get; set; }
        public virtual Login Login { get; set; }
        public virtual Login Login1 { get; set; }
        public virtual AlertStatus AlertStatus { get; set; }
    }
}

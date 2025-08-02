using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductNotification
    {
        [Key]
        public int NotifyID { get; set; }
        public long RefProductID { get; set; }
        public string EmailID { get; set; }
        public Nullable<System.DateTime> NotifyOn { get; set; }
        public Nullable<bool> NotifyEmail { get; set; }
        public string SessionID { get; set; }
        public string IPAddress { get; set; }
        public string IPCountry { get; set; }
        public Nullable<System.DateTime> NotifyEmailOn { get; set; }
        public Nullable<int> NotifyEmailBy { get; set; }
    }
}

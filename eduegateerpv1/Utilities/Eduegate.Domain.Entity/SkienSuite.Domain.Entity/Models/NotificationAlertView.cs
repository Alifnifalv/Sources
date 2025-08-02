using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class NotificationAlertView
    {
        public long NotificationAlertIID { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> FromLoginID { get; set; }
        public Nullable<long> ToLoginID { get; set; }
        public string actionrequired { get; set; }
        public string Viewdocuments { get; set; }
    }
}

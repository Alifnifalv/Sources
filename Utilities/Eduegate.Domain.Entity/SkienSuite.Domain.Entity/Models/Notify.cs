using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class Notify
    {
        public long NotifyIID { get; set; }
        public Nullable<long> ProductSKUMapID { get; set; }
        public string EmailID { get; set; }
        public Nullable<int> SiteID { get; set; }
        public Nullable<bool> IsEmailSend { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public virtual ProductSKUMap ProductSKUMap { get; set; }
        public virtual Site Site { get; set; }
    }
}

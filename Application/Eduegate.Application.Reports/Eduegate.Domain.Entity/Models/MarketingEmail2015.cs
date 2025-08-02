using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class MarketingEmail2015
    {
        public long RowID { get; set; }
        public Nullable<long> CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailID { get; set; }
        public Nullable<int> Points { get; set; }
        public Nullable<bool> EmailSent { get; set; }
        public Nullable<System.DateTime> EmailSentOn { get; set; }
        public Nullable<bool> EmailOpened { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class MarketingEmail2015_v1
    {
        public short MarketingEmailv1ID { get; set; }
        public string PmName { get; set; }
        public Nullable<int> PmMobile { get; set; }
        public string PmEmail { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailID { get; set; }
        public Nullable<int> CategorizationPoints { get; set; }
        public Nullable<int> TotalLoyaltyPoints { get; set; }
        public string Telephone { get; set; }
        public bool EmailSent { get; set; }
    }
}

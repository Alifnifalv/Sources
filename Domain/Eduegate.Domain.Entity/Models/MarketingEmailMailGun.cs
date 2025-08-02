using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class MarketingEmailMailGun
    {
        [Key]
        public int MarketingEmailMailGunID { get; set; }
        public int RefCustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }
        public string EmailID { get; set; }
        public string CampaignName { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public bool SendEmail { get; set; }
        public Nullable<System.DateTime> SendEmailOn { get; set; }
        public bool Active { get; set; }
    }
}

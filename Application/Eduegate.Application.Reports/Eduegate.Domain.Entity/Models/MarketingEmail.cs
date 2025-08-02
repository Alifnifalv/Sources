using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class MarketingEmail
    {
        public int MarketingEmaillID { get; set; }
        public string Subject { get; set; }
        public System.DateTime EmailDate { get; set; }
        public string LogoLink { get; set; }
        public byte TextCount { get; set; }
        public byte BannerCount { get; set; }
        public string RefEmail { get; set; }
        public long CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public bool EmailSent { get; set; }
        public Nullable<short> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    }
}

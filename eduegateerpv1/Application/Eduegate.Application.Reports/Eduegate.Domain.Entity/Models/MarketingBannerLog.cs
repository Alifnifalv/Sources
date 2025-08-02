using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class MarketingBannerLog
    {
        public int MarketingBannerLogID { get; set; }
        public int RefMarketingBannerDetailsID { get; set; }
        public string RequestStatus { get; set; }
        public short CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
    }
}

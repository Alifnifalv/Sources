using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class MarketingEmailLog
    {
        public long RowID { get; set; }
        public string EmailID { get; set; }
        public string CampaignName { get; set; }
        public System.DateTime OpenedOn { get; set; }
    }
}

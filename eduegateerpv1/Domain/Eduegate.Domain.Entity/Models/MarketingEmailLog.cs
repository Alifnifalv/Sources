using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class MarketingEmailLog
    {
        [Key]
        public long RowID { get; set; }
        public string EmailID { get; set; }
        public string CampaignName { get; set; }
        public System.DateTime OpenedOn { get; set; }
    }
}

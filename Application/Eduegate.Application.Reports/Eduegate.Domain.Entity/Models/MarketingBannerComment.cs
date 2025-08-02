using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class MarketingBannerComment
    {
        public int MarketingBannerCommentID { get; set; }
        public int RefMarketingBannerMasterID { get; set; }
        public short RefUserID { get; set; }
        public string BannerComments { get; set; }
        public System.DateTime UpdatedOn { get; set; }
    }
}

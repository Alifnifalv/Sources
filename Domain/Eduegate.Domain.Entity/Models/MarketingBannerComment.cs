using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class MarketingBannerComment
    {
        [Key]
        public int MarketingBannerCommentID { get; set; }
        public int RefMarketingBannerMasterID { get; set; }
        public short RefUserID { get; set; }
        public string BannerComments { get; set; }
        public System.DateTime UpdatedOn { get; set; }
    }
}

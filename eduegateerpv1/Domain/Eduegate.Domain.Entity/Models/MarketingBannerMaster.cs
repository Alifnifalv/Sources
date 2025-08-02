using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class MarketingBannerMaster
    {
        [Key]
        public int MarketingBannerMasterID { get; set; }
        public string BannerRequestType { get; set; }
        public byte RefBannerClassificationID { get; set; }
        public Nullable<int> ProductID { get; set; }
        public string BannerLink { get; set; }
        public string BannerDetails { get; set; }
        public string RequestStatus { get; set; }
        public short CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<System.DateTime> BannerSubmitTill { get; set; }
        public Nullable<short> RefDesignerID { get; set; }
        public byte Position { get; set; }
    }
}

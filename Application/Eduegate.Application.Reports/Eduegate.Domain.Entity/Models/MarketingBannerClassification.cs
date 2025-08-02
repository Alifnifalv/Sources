using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class MarketingBannerClassification
    {
        public byte BannerClassificationID { get; set; }
        public string ClassificationType { get; set; }
        public bool Active { get; set; }
    }
}

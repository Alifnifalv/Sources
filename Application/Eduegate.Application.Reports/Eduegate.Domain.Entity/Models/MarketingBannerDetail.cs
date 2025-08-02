using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class MarketingBannerDetail
    {
        public int MarketingBannerDetailsID { get; set; }
        public int RefMarketingBannerMasterID { get; set; }
        public byte RefMarketingBannerConfigID { get; set; }
        public string BannerImageName { get; set; }
        public Nullable<short> RefDesignerID { get; set; }
        public Nullable<short> UDFValue1 { get; set; }
        public Nullable<short> UDFValue2 { get; set; }
        public Nullable<short> UDFValue3 { get; set; }
    }
}

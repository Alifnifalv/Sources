using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class MarketingEmaillDetail
    {
        public int MarketingEmaillDetailsID { get; set; }
        public int RefMarketingEmaillID { get; set; }
        public string LinkUrl { get; set; }
        public string LinkText { get; set; }
        public string LinkImage { get; set; }
        public string LinkType { get; set; }
        public byte Position { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public string ProductID { get; set; }
    }
}

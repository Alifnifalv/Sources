using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class MarketingBannerConfig
    {
        [Key]
        public byte MarketingBannerConfigID { get; set; }
        public string FormatText { get; set; }
        public short MaxWidth { get; set; }
        public short MaxHeight { get; set; }
        public short MaxSizeKB { get; set; }
        public bool Active { get; set; }
        public bool EnableStats { get; set; }
        public string UDF1 { get; set; }
        public string UDF2 { get; set; }
        public string UDF3 { get; set; }
    }
}

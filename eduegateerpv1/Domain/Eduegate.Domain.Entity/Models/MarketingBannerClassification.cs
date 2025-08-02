using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class MarketingBannerClassification
    {
        [Key]
        public byte BannerClassificationID { get; set; }
        public string ClassificationType { get; set; }
        public bool Active { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductPromotion
    {
        public int PromotionID { get; set; }
        public string PromotionType { get; set; }
        public long RefProductID { get; set; }
        public long PromoProductID { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductPromotion
    {
        [Key]
        public int PromotionID { get; set; }
        public string PromotionType { get; set; }
        public long RefProductID { get; set; }
        public long PromoProductID { get; set; }
    }
}

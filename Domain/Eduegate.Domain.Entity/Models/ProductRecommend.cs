using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductRecommend
    {
        [Key]
        public int ProductRecommendID { get; set; }
        public int Ref1ProductID { get; set; }
        public int Ref2ProductID { get; set; }
        public virtual ProductMaster ProductMaster { get; set; }
        public virtual ProductMaster ProductMaster1 { get; set; }
    }
}

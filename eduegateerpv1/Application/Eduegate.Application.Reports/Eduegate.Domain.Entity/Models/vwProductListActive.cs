using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class vwProductListActive
    {
        public int ProductID { get; set; }
        public int BrandID { get; set; }
        public string BrandCode { get; set; }
        public string ProductCategoryAll { get; set; }
        public short BrandPosition { get; set; }
    }
}

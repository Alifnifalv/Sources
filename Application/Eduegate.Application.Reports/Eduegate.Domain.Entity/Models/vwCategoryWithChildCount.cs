using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class vwCategoryWithChildCount
    {
        public int CategoryID { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryNameEn { get; set; }
        public string CategoryNameAr { get; set; }
    }
}

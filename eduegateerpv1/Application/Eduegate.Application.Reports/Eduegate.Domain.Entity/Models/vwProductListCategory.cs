using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class vwProductListCategory
    {
        public int RefProductCategoryProductID { get; set; }
        public int CategoryID { get; set; }
        public string RefCategoryBreadcrumbs { get; set; }
    }
}

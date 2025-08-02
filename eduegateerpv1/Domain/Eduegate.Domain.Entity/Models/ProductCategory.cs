using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductCategory
    {
        [Key]
        public int ProductCategoryID { get; set; }
        public int RefProductCategoryProductID { get; set; }
        public int RefProductCategoryCategoryID { get; set; }
        public string RefCategoryBreadcrumbs { get; set; }
        public virtual CategoryMaster CategoryMaster { get; set; }
        public virtual ProductMaster ProductMaster { get; set; }
    }
}

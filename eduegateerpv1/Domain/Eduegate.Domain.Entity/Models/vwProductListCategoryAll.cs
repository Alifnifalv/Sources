using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class vwProductListCategoryAll
    {
        [Key]
        public int RefProductCategoryProductID { get; set; }
        public int CategoryID { get; set; }
        public string RefCategoryBreadcrumbs { get; set; }
    }
}

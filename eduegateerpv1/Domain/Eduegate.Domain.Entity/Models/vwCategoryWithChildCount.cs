using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class vwCategoryWithChildCount
    {
        [Key]
        public int CategoryID { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryNameEn { get; set; }
        public string CategoryNameAr { get; set; }
    }
}

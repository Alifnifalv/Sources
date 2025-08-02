using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class CategorySortDetail
    {
        [Key]
        public int CategorySortDetailsID { get; set; }
        public short RefCategoryID { get; set; }
        public byte RefCategorySortID { get; set; }
        public bool Active { get; set; }
        public short UpdatedBy { get; set; }
        public System.DateTime UpdatedOn { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class CategoryColumn
    {
        public CategoryColumn()
        {
            this.CategoryFilters = new List<CategoryFilter>();
            this.ProductDetails = new List<ProductDetail>();
        }

        [Key]
        public int CategoryColumnID { get; set; }
        public int RefCategoryID { get; set; }
        public int RefColumnID { get; set; }
        public virtual CategoryMaster CategoryMaster { get; set; }
        public virtual ColumnMaster ColumnMaster { get; set; }
        public virtual ICollection<CategoryFilter> CategoryFilters { get; set; }
        public virtual ICollection<ProductDetail> ProductDetails { get; set; }
    }
}

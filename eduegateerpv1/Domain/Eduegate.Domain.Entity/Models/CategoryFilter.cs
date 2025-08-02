using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class CategoryFilter
    {
        [Key]
        public int FilterID { get; set; }
        public int RefCategoryColumnID { get; set; }
        public byte Position { get; set; }
        public virtual CategoryColumn CategoryColumn { get; set; }
    }
}

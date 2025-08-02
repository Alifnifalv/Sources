using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class CategoryFilter
    {
        public int FilterID { get; set; }
        public int RefCategoryColumnID { get; set; }
        public byte Position { get; set; }
        public virtual CategoryColumn CategoryColumn { get; set; }
    }
}

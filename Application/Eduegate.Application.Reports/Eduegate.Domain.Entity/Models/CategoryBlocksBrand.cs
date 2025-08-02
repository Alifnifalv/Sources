using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class CategoryBlocksBrand
    {
        public int RowID { get; set; }
        public int RefBlockID { get; set; }
        public int RefBrandID { get; set; }
    }
}

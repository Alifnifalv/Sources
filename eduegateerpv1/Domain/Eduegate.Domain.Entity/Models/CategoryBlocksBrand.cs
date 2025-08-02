using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class CategoryBlocksBrand
    {
        [Key]
        public int RowID { get; set; }
        public int RefBlockID { get; set; }
        public int RefBrandID { get; set; }
    }
}

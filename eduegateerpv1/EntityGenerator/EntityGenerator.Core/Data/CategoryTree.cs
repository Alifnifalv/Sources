using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class CategoryTree
    {
        public long? CategoryIID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string CategoryCode { get; set; }
        public long? ParentCategoryID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string ParentCategoryCode { get; set; }
    }
}

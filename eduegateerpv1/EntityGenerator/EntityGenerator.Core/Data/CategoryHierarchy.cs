using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class CategoryHierarchy
    {
        public long? CategoryIID { get; set; }
        [StringLength(100)]
        public string CategoryName { get; set; }
        public long? ParentCategoryID { get; set; }
        [Unicode(false)]
        public string CategorySearch { get; set; }
        [Unicode(false)]
        public string path { get; set; }
        [Column(TypeName = "numeric(38, 18)")]
        public decimal? x { get; set; }
    }
}

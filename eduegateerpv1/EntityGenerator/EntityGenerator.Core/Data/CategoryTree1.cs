using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CategoryTree", Schema = "offlineindex")]
    public partial class CategoryTree1
    {
        [Key]
        public long CategoryIID { get; set; }
        [StringLength(100)]
        public string CategoryName { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string CategoryCode { get; set; }
        public long? ParentCategoryID { get; set; }
        [Unicode(false)]
        public string CategorySearch { get; set; }
        [Unicode(false)]
        public string CategoryIDSearch { get; set; }
        [Unicode(false)]
        public string path { get; set; }
        [Column(TypeName = "numeric(38, 18)")]
        public decimal? x { get; set; }
    }
}

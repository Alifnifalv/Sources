using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("CategoryRelation", Schema = "offlineindex")]
    public partial class CategoryRelation
    {
        public long? CategoryID { get; set; }
        public long? ParentCategoryID { get; set; }
    }
}

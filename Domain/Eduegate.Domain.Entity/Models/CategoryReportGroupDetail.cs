using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class CategoryReportGroupDetail
    {
        [Key]
        public int RowID { get; set; }
        public int RefGroupID { get; set; }
        public int RefCategoryID { get; set; }
    }
}

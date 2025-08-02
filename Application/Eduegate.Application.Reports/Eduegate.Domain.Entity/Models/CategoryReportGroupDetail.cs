using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class CategoryReportGroupDetail
    {
        public int RowID { get; set; }
        public int RefGroupID { get; set; }
        public int RefCategoryID { get; set; }
    }
}

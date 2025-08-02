using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    [Keyless]
    public partial class vwCategoryColumnFilter
    {
        public string ColumnCode { get; set; }
        public string ColumnNameEn { get; set; }
        public string ColumnType { get; set; }
        public string DataType { get; set; }
        public string DisplayType { get; set; }
        public string FilterType { get; set; }
        public byte Position { get; set; }
        public int RefCategoryID { get; set; }
        public int RefColumnID { get; set; }
        public int CategoryColumnID { get; set; }
        public string ColumnNameAr { get; set; }
    }
}

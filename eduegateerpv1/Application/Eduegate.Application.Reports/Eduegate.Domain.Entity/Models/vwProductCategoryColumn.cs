using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class vwProductCategoryColumn
    {
        public int CategoryColumnID { get; set; }
        public int RefCategoryID { get; set; }
        public int RefColumnID { get; set; }
        public int ProductDetailsID { get; set; }
        public int RefProductID { get; set; }
        public int RefCategoryColumnID { get; set; }
        public string CategoryColumnValue { get; set; }
        public Nullable<int> OrderSeq { get; set; }
        public Nullable<double> CategoryColumnValueRange { get; set; }
        public string CategoryColumnValueAr { get; set; }
        public Nullable<double> CategoryColumnValueRangeAr { get; set; }
    }
}

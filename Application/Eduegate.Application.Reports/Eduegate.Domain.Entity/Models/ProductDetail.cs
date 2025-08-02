using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductDetail
    {
        public int ProductDetailsID { get; set; }
        public int RefProductID { get; set; }
        public int RefCategoryColumnID { get; set; }
        public string CategoryColumnValue { get; set; }
        public Nullable<double> CategoryColumnValueRange { get; set; }
        public Nullable<int> OrderSeq { get; set; }
        public string GroupName { get; set; }
        public string CategoryColumnValueAr { get; set; }
        public Nullable<double> CategoryColumnValueRangeAr { get; set; }
        public virtual CategoryColumn CategoryColumn { get; set; }
        public virtual ProductMaster ProductMaster { get; set; }
    }
}

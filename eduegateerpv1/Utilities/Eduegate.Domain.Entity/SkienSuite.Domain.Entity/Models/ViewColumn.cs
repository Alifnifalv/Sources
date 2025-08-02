using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ViewColumn
    {
        public long ViewColumnID { get; set; }
        public Nullable<long> ViewID { get; set; }
        public string ColumnName { get; set; }
        public string DataType { get; set; }
        public string PhysicalColumnName { get; set; }
        public Nullable<bool> IsDefault { get; set; }
        public Nullable<bool> IsVisible { get; set; }
        public Nullable<bool> IsSortable { get; set; }
        public Nullable<bool> IsQuickSearchable { get; set; }
        public Nullable<int> SortOrder { get; set; }
        public Nullable<bool> IsExpression { get; set; }
        public string Expression { get; set; }
        public string FilterValue { get; set; }
        public virtual View View { get; set; }
    }
}

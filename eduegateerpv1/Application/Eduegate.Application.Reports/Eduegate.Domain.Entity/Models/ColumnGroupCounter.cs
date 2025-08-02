using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ColumnGroupCounter
    {
        public int ColumnGroupCounterID { get; set; }
        public Nullable<int> RefColumnID { get; set; }
        public Nullable<int> ColumnGroupCount { get; set; }
    }
}

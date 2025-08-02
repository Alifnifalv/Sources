using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ColumnGroupCounter
    {
        [Key]
        public int ColumnGroupCounterID { get; set; }
        public Nullable<int> RefColumnID { get; set; }
        public Nullable<int> ColumnGroupCount { get; set; }
    }
}

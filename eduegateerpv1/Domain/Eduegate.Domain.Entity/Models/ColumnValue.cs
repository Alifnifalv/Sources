using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ColumnValue
    {
        [Key]
        public int ColumnValuesID { get; set; }
        public int RefColumnID { get; set; }
        public string DisplayValue { get; set; }
        public Nullable<short> DisplayPosition { get; set; }
        public string DisplayValueAr { get; set; }
        public virtual ColumnMaster ColumnMaster { get; set; }
    }
}

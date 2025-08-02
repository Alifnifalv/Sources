using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ColumnMaster
    {
        public ColumnMaster()
        {
            this.CategoryColumns = new List<CategoryColumn>();
            this.ColumnValues = new List<ColumnValue>();
        }

        [Key]
        public int ColumnID { get; set; }
        public string ColumnCode { get; set; }
        public string ColumnNameEn { get; set; }
        public string ColumnType { get; set; }
        public string DataType { get; set; }
        public string DisplayType { get; set; }
        public string DisplayValue { get; set; }
        public string FilterType { get; set; }
        public string ColumnNameAr { get; set; }
        public string DisplayValueAr { get; set; }
        public virtual ICollection<CategoryColumn> CategoryColumns { get; set; }
        public virtual ICollection<ColumnValue> ColumnValues { get; set; }
    }
}

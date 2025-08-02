using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class vwProductAttribute
    {
        [Key]
        public int ColumnID { get; set; }
        public string ColumnCode { get; set; }
        public string ColumnNameEn { get; set; }
        public string ColumnType { get; set; }
        public string DataType { get; set; }
        public string DisplayType { get; set; }
        public string DisplayValue { get; set; }
        public int RefProductCategoryProductID { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("NewBookExcelData", Schema = "schools")]
    public partial class NewBookExcelData
    {
        [Key]
        public long NewBookExcelDataIID { get; set; }
        public string TitleOfBooks { get; set; }
        [StringLength(30)]
        public string Class { get; set; }
        public long? Quantity { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Rate { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? CostPrice { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? SellingPrice { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? SalesAmount { get; set; }
        public bool? AvailableStatus { get; set; }
    }
}

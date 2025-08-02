namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.ProductExcelExport")]
    public partial class ProductExcelExport
    {
        [Key]
        public long ProductDataIID { get; set; }

        [StringLength(100)]
        public string Category { get; set; }

        [StringLength(100)]
        public string ProductName { get; set; }

        public decimal? Quantity { get; set; }

        public decimal? CostPrice { get; set; }

        public decimal? SellingPrice { get; set; }

        [StringLength(100)]
        public string Campus { get; set; }
    }
}

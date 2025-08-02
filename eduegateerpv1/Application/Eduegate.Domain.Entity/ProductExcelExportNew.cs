namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.ProductExcelExportNew")]
    public partial class ProductExcelExportNew
    {
        [Key]
        public long ProductDataIID { get; set; }

        [StringLength(100)]
        public string Category { get; set; }

        [StringLength(100)]
        public string ProductName { get; set; }

        public decimal? Quantity { get; set; }

        [StringLength(100)]
        public string Campus { get; set; }

        public int? IsPreviouslyAdded { get; set; }
    }
}

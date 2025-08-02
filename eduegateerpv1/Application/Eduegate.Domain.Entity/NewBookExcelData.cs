namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.NewBookExcelData")]
    public partial class NewBookExcelData
    {
        [Key]
        public long NewBookExcelDataIID { get; set; }

        public string TitleOfBooks { get; set; }

        [StringLength(30)]
        public string Class { get; set; }

        public long? Quantity { get; set; }

        public decimal? Rate { get; set; }

        public decimal? Amount { get; set; }

        public decimal? CostPrice { get; set; }

        public decimal? SellingPrice { get; set; }

        public decimal? SalesAmount { get; set; }

        public bool? AvailableStatus { get; set; }
    }
}

namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.FeeDiscounts")]
    public partial class FeeDiscount
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FeeDiscountID { get; set; }

        [StringLength(20)]
        public string DiscountCode { get; set; }

        public decimal? DiscountPercentage { get; set; }

        public decimal? Amount { get; set; }

        [StringLength(100)]
        public string Description { get; set; }
    }
}

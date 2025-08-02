namespace  Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("FeeDiscounts", Schema = "schools")]
    public partial class FeeDiscount
    {
        [Key]
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

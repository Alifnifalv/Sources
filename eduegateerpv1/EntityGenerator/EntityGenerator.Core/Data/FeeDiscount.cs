using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("FeeDiscounts", Schema = "schools")]
    public partial class FeeDiscount
    {
        [Key]
        public int FeeDiscountID { get; set; }
        [StringLength(20)]
        public string DiscountCode { get; set; }
        [Column(TypeName = "decimal(3, 2)")]
        public decimal? DiscountPercentage { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        [StringLength(100)]
        public string Description { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ServicePricings", Schema = "saloon")]
    public partial class ServicePricing
    {
        [Key]
        public long ServicePricingIID { get; set; }
        public long ServiceID { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Duration { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Price { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? DiscountPrice { get; set; }
        [StringLength(500)]
        public string Caption { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("ServiceID")]
        [InverseProperty("ServicePricings")]
        public virtual Service Service { get; set; }
    }
}

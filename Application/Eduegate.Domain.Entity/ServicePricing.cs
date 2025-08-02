namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("saloon.ServicePricings")]
    public partial class ServicePricing
    {
        [Key]
        public long ServicePricingIID { get; set; }

        public long ServiceID { get; set; }

        public decimal? Duration { get; set; }

        public decimal? Price { get; set; }

        public decimal? DiscountPrice { get; set; }

        [StringLength(500)]
        public string Caption { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public virtual Service Service { get; set; }
    }
}

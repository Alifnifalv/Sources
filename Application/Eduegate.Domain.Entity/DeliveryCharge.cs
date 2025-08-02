namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("inventory.DeliveryCharges")]
    public partial class DeliveryCharge
    {
        [Key]
        public long DeliveryChargeIID { get; set; }

        public int? ServiceProviderID { get; set; }

        public int? FromCountryID { get; set; }

        public int? ToCountryID { get; set; }

        public int? DeliveryTypeID { get; set; }

        public decimal? CartStartRange { get; set; }

        public decimal? CartEndRange { get; set; }

        public decimal? WeightStartRange { get; set; }

        public decimal? WeightEndRange { get; set; }

        [Column("DeliveryCharge")]
        public decimal? DeliveryCharge1 { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public long? CountryGroupID { get; set; }

        public decimal? WeightRangeDivisor { get; set; }

        public decimal? WeightChargeDivisor { get; set; }

        public decimal? MinimumCap { get; set; }

        public decimal? DistanceStartRange { get; set; }

        public decimal? DistanceEndRange { get; set; }

        public long? BranchID { get; set; }

        public virtual ServiceProviderCountryGroup ServiceProviderCountryGroup { get; set; }

        public virtual ServiceProvider ServiceProvider { get; set; }

        public virtual Country Country { get; set; }

        public virtual Country Country1 { get; set; }

        public virtual DeliveryCharge DeliveryCharges1 { get; set; }

        public virtual DeliveryCharge DeliveryCharge2 { get; set; }

        public virtual DeliveryTypes1 DeliveryTypes1 { get; set; }
    }
}

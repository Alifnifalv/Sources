using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("DeliveryCharges", Schema = "inventory")]
    public partial class DeliveryCharge
    {
        [Key]
        public long DeliveryChargeIID { get; set; }
        public int? ServiceProviderID { get; set; }
        public int? FromCountryID { get; set; }
        public int? ToCountryID { get; set; }
        public int? DeliveryTypeID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? CartStartRange { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? CartEndRange { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? WeightStartRange { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? WeightEndRange { get; set; }
        [Column("DeliveryCharge", TypeName = "decimal(18, 3)")]
        public decimal? DeliveryCharge1 { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public long? CountryGroupID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? WeightRangeDivisor { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? WeightChargeDivisor { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? MinimumCap { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? DistanceStartRange { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? DistanceEndRange { get; set; }
        public long? BranchID { get; set; }

        [ForeignKey("CountryGroupID")]
        [InverseProperty("DeliveryCharges")]
        public virtual ServiceProviderCountryGroup CountryGroup { get; set; }
        [ForeignKey("DeliveryTypeID")]
        [InverseProperty("DeliveryCharges")]
        public virtual DeliveryType1 DeliveryType { get; set; }
        [ForeignKey("FromCountryID")]
        [InverseProperty("DeliveryChargeFromCountries")]
        public virtual Country FromCountry { get; set; }
        [ForeignKey("ServiceProviderID")]
        [InverseProperty("DeliveryCharges")]
        public virtual ServiceProvider ServiceProvider { get; set; }
        [ForeignKey("ToCountryID")]
        [InverseProperty("DeliveryChargeToCountries")]
        public virtual Country ToCountry { get; set; }
    }
}

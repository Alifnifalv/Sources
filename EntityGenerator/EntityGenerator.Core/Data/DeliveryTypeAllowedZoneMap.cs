using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("DeliveryTypeAllowedZoneMaps", Schema = "inventory")]
    public partial class DeliveryTypeAllowedZoneMap
    {
        [Key]
        public long ZoneDeliveryTypeMapIID { get; set; }
        public int? DeliveryTypeID { get; set; }
        public int CountryID { get; set; }
        public short? ZoneID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? DeliveryCharge { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? DeliveryChargePercentage { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? CartTotalFrom { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? CartTotalTo { get; set; }
        public bool? IsSelected { get; set; }

        [ForeignKey("DeliveryTypeID")]
        [InverseProperty("DeliveryTypeAllowedZoneMaps")]
        public virtual DeliveryType1 DeliveryType { get; set; }
        [ForeignKey("ZoneID")]
        [InverseProperty("DeliveryTypeAllowedZoneMaps")]
        public virtual Zone Zone { get; set; }
    }
}

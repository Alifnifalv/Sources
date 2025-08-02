namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("inventory.DeliveryTypeAllowedZoneMaps")]
    public partial class DeliveryTypeAllowedZoneMap
    {
        [Key]
        public long ZoneDeliveryTypeMapIID { get; set; }

        public int? DeliveryTypeID { get; set; }

        public int CountryID { get; set; }

        public short? ZoneID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public decimal? DeliveryCharge { get; set; }

        public decimal? DeliveryChargePercentage { get; set; }

        public decimal? CartTotalFrom { get; set; }

        public decimal? CartTotalTo { get; set; }

        public bool? IsSelected { get; set; }

        public virtual DeliveryTypes1 DeliveryTypes1 { get; set; }

        public virtual Zone Zone { get; set; }
    }
}

namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("inventory.ProductDeliveryTypeMaps")]
    public partial class ProductDeliveryTypeMap
    {
        [Key]
        public long ProductDeliveryTypeMapIID { get; set; }

        public int? CompanyID { get; set; }

        public int? DeliveryTypeID { get; set; }

        public long? ProductID { get; set; }

        public long? ProductSKUMapID { get; set; }

        public decimal? DeliveryCharge { get; set; }

        public decimal? DeliveryChargePercentage { get; set; }

        public byte? DeliveryDays { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public bool? IsSelected { get; set; }

        public decimal? CartTotalFrom { get; set; }

        public decimal? CartTotalTo { get; set; }

        public int? DisplayRange { get; set; }

        public long? BranchID { get; set; }

        public virtual Product Product { get; set; }

        public virtual ProductSKUMap ProductSKUMap { get; set; }

        public virtual DeliveryTypes1 DeliveryTypes1 { get; set; }

        public virtual Branch Branch { get; set; }

        public virtual Company Company { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CustomerGroupDeliveryTypeMaps", Schema = "inventory")]
    public partial class CustomerGroupDeliveryTypeMap
    {
        [Key]
        public long CustomerGroupDeliveryTypeMapIID { get; set; }
        public int? DeliveryTypeID { get; set; }
        public long? CustomerGroupID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? CartTotalFrom { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? CartTotalTo { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? DeliveryCharge { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? DeliveryChargePercentage { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public bool? IsSelected { get; set; }

        [ForeignKey("CustomerGroupID")]
        [InverseProperty("CustomerGroupDeliveryTypeMaps")]
        public virtual CustomerGroup CustomerGroup { get; set; }
        [ForeignKey("DeliveryTypeID")]
        [InverseProperty("CustomerGroupDeliveryTypeMaps")]
        public virtual DeliveryType1 DeliveryType { get; set; }
    }
}

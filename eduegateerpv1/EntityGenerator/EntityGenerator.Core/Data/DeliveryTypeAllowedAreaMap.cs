using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("DeliveryTypeAllowedAreaMaps", Schema = "inventory")]
    public partial class DeliveryTypeAllowedAreaMap
    {
        [Key]
        public long AreaDeliveryTypeMapIID { get; set; }
        public int? DeliveryTypeID { get; set; }
        public int? AreaID { get; set; }
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

        [ForeignKey("AreaID")]
        [InverseProperty("DeliveryTypeAllowedAreaMaps")]
        public virtual Area Area { get; set; }
        [ForeignKey("DeliveryTypeID")]
        [InverseProperty("DeliveryTypeAllowedAreaMaps")]
        public virtual DeliveryType1 DeliveryType { get; set; }
    }
}

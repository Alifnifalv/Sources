using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ProductDeliveryTypeMaps", Schema = "inventory")]
    public partial class ProductDeliveryTypeMap
    {
        [Key]
        public long ProductDeliveryTypeMapIID { get; set; }
        public int? CompanyID { get; set; }
        public int? DeliveryTypeID { get; set; }
        public long? ProductID { get; set; }
        public long? ProductSKUMapID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? DeliveryCharge { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? DeliveryChargePercentage { get; set; }
        public byte? DeliveryDays { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public bool? IsSelected { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? CartTotalFrom { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? CartTotalTo { get; set; }
        public int? DisplayRange { get; set; }
        public long? BranchID { get; set; }

        [ForeignKey("BranchID")]
        [InverseProperty("ProductDeliveryTypeMaps")]
        public virtual Branch Branch { get; set; }
        [ForeignKey("CompanyID")]
        [InverseProperty("ProductDeliveryTypeMaps")]
        public virtual Company Company { get; set; }
        [ForeignKey("DeliveryTypeID")]
        [InverseProperty("ProductDeliveryTypeMaps")]
        public virtual DeliveryType1 DeliveryType { get; set; }
        [ForeignKey("ProductID")]
        [InverseProperty("ProductDeliveryTypeMaps")]
        public virtual Product Product { get; set; }
        [ForeignKey("ProductSKUMapID")]
        [InverseProperty("ProductDeliveryTypeMaps")]
        public virtual ProductSKUMap ProductSKUMap { get; set; }
    }
}

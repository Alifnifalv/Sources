using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ProductTypeDeliveryTypeMaps", Schema = "inventory")]
    public partial class ProductTypeDeliveryTypeMap
    {
        [Key]
        public long ProductTypeDeliveryTypeMapIID { get; set; }
        public int? ProductTypeID { get; set; }
        public int? DeliveryTypeID { get; set; }
        public int? CompanyID { get; set; }
        public int? Sequence { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("CompanyID")]
        [InverseProperty("ProductTypeDeliveryTypeMaps")]
        public virtual Company Company { get; set; }
        [ForeignKey("DeliveryTypeID")]
        [InverseProperty("ProductTypeDeliveryTypeMaps")]
        public virtual DeliveryType1 DeliveryType { get; set; }
        [ForeignKey("ProductTypeID")]
        [InverseProperty("ProductTypeDeliveryTypeMaps")]
        public virtual ProductType ProductType { get; set; }
    }
}

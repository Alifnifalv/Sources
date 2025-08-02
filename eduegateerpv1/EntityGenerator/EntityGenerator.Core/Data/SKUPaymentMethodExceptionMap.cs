using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("SKUPaymentMethodExceptionMaps", Schema = "inventory")]
    public partial class SKUPaymentMethodExceptionMap
    {
        [Key]
        public int SKUPaymentMethodExceptionMapIID { get; set; }
        public long? SKUID { get; set; }
        public short? PaymentMethodID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? DeliveryTypeID { get; set; }
        public int? SiteID { get; set; }
        public int? CompanyID { get; set; }
        public int? AreaID { get; set; }

        [ForeignKey("PaymentMethodID")]
        [InverseProperty("SKUPaymentMethodExceptionMaps")]
        public virtual PaymentMethod PaymentMethod { get; set; }
        [ForeignKey("SKUID")]
        [InverseProperty("SKUPaymentMethodExceptionMaps")]
        public virtual ProductSKUMap SKU { get; set; }
    }
}

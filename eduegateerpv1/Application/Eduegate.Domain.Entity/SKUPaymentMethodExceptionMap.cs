namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("inventory.SKUPaymentMethodExceptionMaps")]
    public partial class SKUPaymentMethodExceptionMap
    {
        [Key]
        public int SKUPaymentMethodExceptionMapIID { get; set; }

        public long? SKUID { get; set; }

        public short? PaymentMethodID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? DeliveryTypeID { get; set; }

        public int? SiteID { get; set; }

        public int? CompanyID { get; set; }

        public int? AreaID { get; set; }

        public virtual ProductSKUMap ProductSKUMap { get; set; }

        public virtual PaymentMethod PaymentMethod { get; set; }
    }
}

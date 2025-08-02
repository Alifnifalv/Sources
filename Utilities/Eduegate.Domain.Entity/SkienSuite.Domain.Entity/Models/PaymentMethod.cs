using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class PaymentMethod
    {
        public PaymentMethod()
        {
            this.SKUPaymentMethodExceptionMaps = new List<SKUPaymentMethodExceptionMap>();
            this.EntityTypePaymentMethodMaps = new List<EntityTypePaymentMethodMap>();
            this.PaymentMethodSiteMaps = new List<PaymentMethodSiteMap>();
            this.PaymentGroups = new List<PaymentGroup>();
        }

        public short PaymentMethodID { get; set; }
        public string PaymentMethod1 { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        public virtual ICollection<SKUPaymentMethodExceptionMap> SKUPaymentMethodExceptionMaps { get; set; }
        public virtual ICollection<EntityTypePaymentMethodMap> EntityTypePaymentMethodMaps { get; set; }
        public virtual ICollection<PaymentMethodSiteMap> PaymentMethodSiteMaps { get; set; }
        public virtual ICollection<PaymentGroup> PaymentGroups { get; set; }
    }
}

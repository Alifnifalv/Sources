using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("PaymentMethods", Schema = "mutual")]
    public partial class PaymentMethod
    {
        public PaymentMethod()
        {
            EntityTypePaymentMethodMaps = new HashSet<EntityTypePaymentMethodMap>();
            PaymentExceptionByZoneDeliveries = new HashSet<PaymentExceptionByZoneDelivery>();
            PaymentMethodCultureDatas = new HashSet<PaymentMethodCultureData>();
            PaymentMethodSiteMaps = new HashSet<PaymentMethodSiteMap>();
            SKUPaymentMethodExceptionMaps = new HashSet<SKUPaymentMethodExceptionMap>();
            PaymentGroups = new HashSet<PaymentGroup>();
        }

        [Key]
        public short PaymentMethodID { get; set; }
        [Column("PaymentMethod")]
        [StringLength(50)]
        public string PaymentMethod1 { get; set; }
        [StringLength(100)]
        public string Description { get; set; }
        [StringLength(50)]
        public string ImageName { get; set; }
        public bool? IsVirtual { get; set; }
        public bool? IsActive { get; set; }
        public bool? ShowIfZero { get; set; }

        [InverseProperty("PaymentMethod")]
        public virtual ICollection<EntityTypePaymentMethodMap> EntityTypePaymentMethodMaps { get; set; }
        [InverseProperty("PaymentMethod")]
        public virtual ICollection<PaymentExceptionByZoneDelivery> PaymentExceptionByZoneDeliveries { get; set; }
        [InverseProperty("PaymentMethodNavigation")]
        public virtual ICollection<PaymentMethodCultureData> PaymentMethodCultureDatas { get; set; }
        [InverseProperty("PaymentMethod")]
        public virtual ICollection<PaymentMethodSiteMap> PaymentMethodSiteMaps { get; set; }
        [InverseProperty("PaymentMethod")]
        public virtual ICollection<SKUPaymentMethodExceptionMap> SKUPaymentMethodExceptionMaps { get; set; }

        [ForeignKey("PaymentMethodID")]
        [InverseProperty("PaymentMethods")]
        public virtual ICollection<PaymentGroup> PaymentGroups { get; set; }
    }
}

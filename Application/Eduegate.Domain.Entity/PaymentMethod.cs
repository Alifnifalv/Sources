namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mutual.PaymentMethods")]
    public partial class PaymentMethod
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PaymentMethod()
        {
            SKUPaymentMethodExceptionMaps = new HashSet<SKUPaymentMethodExceptionMap>();
            EntityTypePaymentMethodMaps = new HashSet<EntityTypePaymentMethodMap>();
            PaymentMethodCultureDatas = new HashSet<PaymentMethodCultureData>();
            PaymentExceptionByZoneDeliveries = new HashSet<PaymentExceptionByZoneDelivery>();
            PaymentMethodSiteMaps = new HashSet<PaymentMethodSiteMap>();
            PaymentGroups = new HashSet<PaymentGroup>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SKUPaymentMethodExceptionMap> SKUPaymentMethodExceptionMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EntityTypePaymentMethodMap> EntityTypePaymentMethodMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PaymentMethodCultureData> PaymentMethodCultureDatas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PaymentExceptionByZoneDelivery> PaymentExceptionByZoneDeliveries { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PaymentMethodSiteMap> PaymentMethodSiteMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PaymentGroup> PaymentGroups { get; set; }
    }
}

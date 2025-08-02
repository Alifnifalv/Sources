namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mutual.EntityTypeEntitlements")]
    public partial class EntityTypeEntitlement
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EntityTypeEntitlement()
        {
            CustomerAccountMaps = new HashSet<CustomerAccountMap>();
            SupplierAccountMaps = new HashSet<SupplierAccountMap>();
            ProductPriceListCustomerMaps = new HashSet<ProductPriceListCustomerMap>();
            TransactionHeads = new HashSet<TransactionHead>();
            TransactionHeadEntitlementMaps = new HashSet<TransactionHeadEntitlementMap>();
            EntitlementMaps = new HashSet<EntitlementMap>();
        }

        [Key]
        public byte EntitlementID { get; set; }

        [StringLength(50)]
        public string EntitlementName { get; set; }

        public int? EntityTypeID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerAccountMap> CustomerAccountMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SupplierAccountMap> SupplierAccountMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductPriceListCustomerMap> ProductPriceListCustomerMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransactionHead> TransactionHeads { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransactionHeadEntitlementMap> TransactionHeadEntitlementMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EntitlementMap> EntitlementMaps { get; set; }

        public virtual EntityType EntityType { get; set; }
    }
}

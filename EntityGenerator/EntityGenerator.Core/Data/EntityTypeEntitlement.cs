using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EntityTypeEntitlements", Schema = "mutual")]
    public partial class EntityTypeEntitlement
    {
        public EntityTypeEntitlement()
        {
            CustomerAccountMaps = new HashSet<CustomerAccountMap>();
            EntitlementMaps = new HashSet<EntitlementMap>();
            ProductPriceListCustomerMaps = new HashSet<ProductPriceListCustomerMap>();
            SupplierAccountMaps = new HashSet<SupplierAccountMap>();
            TransactionHeadEntitlementMaps = new HashSet<TransactionHeadEntitlementMap>();
            TransactionHeads = new HashSet<TransactionHead>();
        }

        [Key]
        public byte EntitlementID { get; set; }
        [StringLength(50)]
        public string EntitlementName { get; set; }
        public int? EntityTypeID { get; set; }

        [ForeignKey("EntityTypeID")]
        [InverseProperty("EntityTypeEntitlements")]
        public virtual EntityType EntityType { get; set; }
        [InverseProperty("Entitlement")]
        public virtual ICollection<CustomerAccountMap> CustomerAccountMaps { get; set; }
        [InverseProperty("Entitlement")]
        public virtual ICollection<EntitlementMap> EntitlementMaps { get; set; }
        [InverseProperty("Entitlement")]
        public virtual ICollection<ProductPriceListCustomerMap> ProductPriceListCustomerMaps { get; set; }
        [InverseProperty("Entitlement")]
        public virtual ICollection<SupplierAccountMap> SupplierAccountMaps { get; set; }
        [InverseProperty("Entitlement")]
        public virtual ICollection<TransactionHeadEntitlementMap> TransactionHeadEntitlementMaps { get; set; }
        [InverseProperty("Entitlement")]
        public virtual ICollection<TransactionHead> TransactionHeads { get; set; }
    }
}

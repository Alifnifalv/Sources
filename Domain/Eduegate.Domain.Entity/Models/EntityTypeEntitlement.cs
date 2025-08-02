using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("EntityTypeEntitlements", Schema = "mutual")]
    public partial class EntityTypeEntitlement
    {
        public EntityTypeEntitlement()
        {
            this.CustomerAccountMaps = new List<CustomerAccountMap>();
            this.SupplierAccountMaps = new List<SupplierAccountMap>();
            this.ProductPriceListCustomerMaps = new List<ProductPriceListCustomerMap>();
            this.TransactionHeads = new List<TransactionHead>();
            this.TransactionHeadEntitlementMaps = new List<TransactionHeadEntitlementMap>();
            this.EntitlementMaps = new List<EntitlementMap>();
        }

        [Key]
        public byte EntitlementID { get; set; }
        public string EntitlementName { get; set; }
        public Nullable<int> EntityTypeID { get; set; }
        public virtual ICollection<CustomerAccountMap> CustomerAccountMaps { get; set; }
        public virtual ICollection<SupplierAccountMap> SupplierAccountMaps { get; set; }
        public virtual ICollection<ProductPriceListCustomerMap> ProductPriceListCustomerMaps { get; set; }
        public virtual ICollection<TransactionHead> TransactionHeads { get; set; }
        public virtual ICollection<TransactionHeadEntitlementMap> TransactionHeadEntitlementMaps { get; set; }
        public virtual ICollection<EntitlementMap> EntitlementMaps { get; set; }
        public virtual EntityType EntityType { get; set; }
    }
}

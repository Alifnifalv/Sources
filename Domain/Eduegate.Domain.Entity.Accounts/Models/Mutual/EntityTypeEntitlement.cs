using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eduegate.Domain.Entity.Accounts.Models.Accounts;

namespace Eduegate.Domain.Entity.Accounts.Models.Mutual
{
    [Table("EntityTypeEntitlements", Schema = "mutual")]
    public partial class EntityTypeEntitlement
    {
        public EntityTypeEntitlement()
        {
            CustomerAccountMaps = new HashSet<CustomerAccountMap>();
            EntitlementMaps = new HashSet<EntitlementMap>();
            SupplierAccountMaps = new HashSet<SupplierAccountMap>();
        }

        [Key]
        public byte EntitlementID { get; set; }

        [StringLength(50)]
        public string EntitlementName { get; set; }

        public int? EntityTypeID { get; set; }

        //public virtual EntityType EntityType { get; set; }

        public virtual ICollection<CustomerAccountMap> CustomerAccountMaps { get; set; }

        public virtual ICollection<EntitlementMap> EntitlementMaps { get; set; }

        public virtual ICollection<SupplierAccountMap> SupplierAccountMaps { get; set; }
    }
}
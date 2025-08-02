namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("inventory.TransactionHeadEntitlementMap")]
    public partial class TransactionHeadEntitlementMap
    {
        [Key]
        public long TransactionHeadEntitlementMapIID { get; set; }

        public long TransactionHeadID { get; set; }

        public byte EntitlementID { get; set; }

        public decimal? Amount { get; set; }

        [StringLength(25)]
        public string ReferenceNo { get; set; }

        public virtual TransactionHead TransactionHead { get; set; }

        public virtual EntityTypeEntitlement EntityTypeEntitlement { get; set; }
    }
}

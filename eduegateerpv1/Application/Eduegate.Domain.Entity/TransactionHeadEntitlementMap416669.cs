namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("inventory.TransactionHeadEntitlementMap416669")]
    public partial class TransactionHeadEntitlementMap416669
    {
        [Key]
        [Column(Order = 0)]
        public long TransactionHeadEntitlementMapIID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long TransactionHeadID { get; set; }

        [Key]
        [Column(Order = 2)]
        public byte EntitlementID { get; set; }

        public decimal? Amount { get; set; }

        [StringLength(25)]
        public string ReferenceNo { get; set; }
    }
}

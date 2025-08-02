namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("inventory.TransactionHeadPointsMap")]
    public partial class TransactionHeadPointsMap
    {
        [Key]
        public long TransactionHeadPointsMapIID { get; set; }

        public long TransactionHeadID { get; set; }

        public long LoyaltyPoints { get; set; }

        public long CategorizationPoints { get; set; }

        public virtual TransactionHead TransactionHead { get; set; }
    }
}

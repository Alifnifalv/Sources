namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("account.Accounts_SubLedger_Relation")]
    public partial class Accounts_SubLedger_Relation
    {
        [Key]
        public long SL_Rln_ID { get; set; }

        public long AccountID { get; set; }

        public long SL_AccountID { get; set; }

        public virtual Accounts_SubLedger Accounts_SubLedger { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Accounts_SubLedger_Relation", Schema = "account")]
    [Index("AccountID", Name = "IDX_Accounts_SubLedger_Relation_AccountID_SL_AccountID")]
    [Index("SL_AccountID", "AccountID", Name = "IX_Accounts_SubLedger_Relation", IsUnique = true)]
    public partial class Accounts_SubLedger_Relation
    {
        [Key]
        public long SL_Rln_ID { get; set; }
        public long AccountID { get; set; }
        public long SL_AccountID { get; set; }

        [ForeignKey("SL_AccountID")]
        [InverseProperty("Accounts_SubLedger_Relation")]
        public virtual Accounts_SubLedger SL_Account { get; set; }
    }
}

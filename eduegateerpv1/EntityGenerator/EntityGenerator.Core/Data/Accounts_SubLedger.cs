using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Accounts_SubLedger", Schema = "account")]
    [Index("SL_AccountCode", "SL_Alias", Name = "IDX_Accounts_SubLedger_SL_AccountCode__SL_Alias_")]
    [Index("SL_Alias", Name = "idx_Accounts_SubLedgerSL_Alias")]
    public partial class Accounts_SubLedger
    {
        public Accounts_SubLedger()
        {
            AccountTransactionDetails = new HashSet<AccountTransactionDetail>();
            Accounts_SubLedger_Relation = new HashSet<Accounts_SubLedger_Relation>();
        }

        [Key]
        public long SL_AccountID { get; set; }
        [StringLength(50)]
        public string SL_AccountCode { get; set; }
        [StringLength(100)]
        public string SL_AccountName { get; set; }
        [StringLength(100)]
        public string SL_Alias { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public bool? IsHidden { get; set; }
        public bool? AllowUserDelete { get; set; }
        public bool? AllowUserEdit { get; set; }
        public bool? AllowUserRename { get; set; }

        [InverseProperty("SubLedger")]
        public virtual ICollection<AccountTransactionDetail> AccountTransactionDetails { get; set; }
        [InverseProperty("SL_Account")]
        public virtual ICollection<Accounts_SubLedger_Relation> Accounts_SubLedger_Relation { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("account.Accounts_SubLedger")]
    public partial class Accounts_SubLedger
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Accounts_SubLedger()
        {
            Accounts_SubLedger_Relation = new HashSet<Accounts_SubLedger_Relation>();
            AccountTransactionDetails = new HashSet<AccountTransactionDetail>();
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

        public DateTime? CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public bool? IsHidden { get; set; }

        public bool? AllowUserDelete { get; set; }

        public bool? AllowUserEdit { get; set; }

        public bool? AllowUserRename { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Accounts_SubLedger_Relation> Accounts_SubLedger_Relation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccountTransactionDetail> AccountTransactionDetails { get; set; }
    }
}

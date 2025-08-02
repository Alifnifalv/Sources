using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.School.Models
{
    [Table("Accounts_SubLedger_Relation", Schema = "account")]
    public partial class Accounts_SubLedger_Relation
    {
        [Key]
        public long SL_Rln_ID { get; set; }

        public long AccountID { get; set; }

        public long SL_AccountID { get; set; }

        public virtual Accounts_SubLedger Accounts_SubLedger { get; set; }
    }
}

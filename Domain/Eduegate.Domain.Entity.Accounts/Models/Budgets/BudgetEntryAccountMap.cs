using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eduegate.Domain.Entity.Accounts.Models.Accounts;

namespace Eduegate.Domain.Entity.Accounts.Models.Budgets
{
    [Table("BudgetEntryAccountMaps", Schema = "budget")]
    public partial class BudgetEntryAccountMap
    {
        [Key]
        public long BudgetEntryAccountMapIID { get; set; }

        public long? BudgetEntryID { get; set; }

        public int? GroupID { get; set; }

        public long? AccountID { get; set; }

        public int? CostCenterID { get; set; }

        [StringLength(500)]
        public string Remarks { get; set; }

        public virtual Account Account { get; set; }
    }
}

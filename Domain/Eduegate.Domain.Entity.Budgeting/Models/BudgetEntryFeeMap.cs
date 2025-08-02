using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Budgeting.Models
{
    [Table("BudgetEntryFeeMaps", Schema = "budget")]
    public partial class BudgetEntryFeeMap
    {
        [Key]
        public long BudgetEntryFeeMapIID { get; set; }

        public long? BudgetEntryID { get; set; }

        public int? FeeMasterID { get; set; }

        public virtual BudgetEntry BudgetEntry { get; set; }

        public virtual FeeMaster FeeMaster { get; set; }
    }
}
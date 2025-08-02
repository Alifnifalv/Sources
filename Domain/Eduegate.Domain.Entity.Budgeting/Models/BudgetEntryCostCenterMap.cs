using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Budgeting.Models
{
    [Table("BudgetEntryCostCenterMaps", Schema = "budget")]
    public partial class BudgetEntryCostCenterMap
    {
        [Key]
        public long BudgetEntryCostCenterMapIID { get; set; }

        public long? BudgetEntryID { get; set; }

        public int? CostCenterID { get; set; }

        public virtual BudgetEntry BudgetEntry { get; set; }

        public virtual CostCenter CostCenter { get; set; }
    }
}
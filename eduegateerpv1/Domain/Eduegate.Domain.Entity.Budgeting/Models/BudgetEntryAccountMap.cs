using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Budgeting.Models
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

        public virtual Account Account { get; set; }

        public virtual BudgetEntry BudgetEntry { get; set; }

        public virtual CostCenter CostCenter { get; set; }

        public virtual Group Group { get; set; }
        [StringLength(500)]
        public string Remarks { get; set; }
    }
}
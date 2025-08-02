using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Budgeting.Models
{
    [Table("BudgetSuggestions", Schema = "budget")]
    public partial class BudgetSuggestion
    {
        public BudgetSuggestion()
        {
            BudgetEntries = new HashSet<BudgetEntry>();
        }

        [Key]
        public byte BudgetSuggestionID { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public virtual ICollection<BudgetEntry> BudgetEntries { get; set; }
    }
}
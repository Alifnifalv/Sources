using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
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

        [InverseProperty("BudgetSuggestion")]
        public virtual ICollection<BudgetEntry> BudgetEntries { get; set; }
    }
}

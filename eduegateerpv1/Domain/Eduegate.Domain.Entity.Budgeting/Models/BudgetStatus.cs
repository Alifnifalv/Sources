using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Budgeting.Models
{
    [Table("BudgetStatuses", Schema = "budget")]
    public partial class BudgetStatus
    {
        public BudgetStatus()
        {
            Budget1 = new HashSet<Budget1>();
        }

        [Key]
        public byte BudgetStatusID { get; set; }

        [StringLength(50)]
        public string StatusName { get; set; }

        public virtual ICollection<Budget1> Budget1 { get; set; }
    }
}
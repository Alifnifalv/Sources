using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Accounts.Models.Inventory
{
    [Table("TaxStatuses", Schema = "inventory")]
    public partial class TaxStatus
    {
        public TaxStatus()
        {
            Taxes = new HashSet<Tax>();
        }

        [Key]
        public int TaxStatusID { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        public virtual ICollection<Tax> Taxes { get; set; }
    }
}
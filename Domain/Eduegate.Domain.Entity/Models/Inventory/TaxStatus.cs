using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models.Inventory
{
    [Table("TaxStatuses", Schema = "inventory")]
    public partial class TaxStatus
    {
        public TaxStatus()
        {
            this.Taxes = new List<Tax>();
        }

        [Key]
        public int TaxStatusID { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Tax> Taxes { get; set; }
    }
}

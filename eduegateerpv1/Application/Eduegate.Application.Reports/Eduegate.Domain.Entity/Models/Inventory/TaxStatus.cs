using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models.Inventory
{
    public partial class TaxStatus
    {
        public TaxStatus()
        {
            this.Taxes = new List<Tax>();
        }

        public int TaxStatusID { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Tax> Taxes { get; set; }
    }
}

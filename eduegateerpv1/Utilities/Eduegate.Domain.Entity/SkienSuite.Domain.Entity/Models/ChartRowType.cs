using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ChartRowType
    {
        public ChartRowType()
        {
            this.ChartOfAccountMaps = new List<ChartOfAccountMap>();
        }

        public int ChartRowTypeID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ChartOfAccountMap> ChartOfAccountMaps { get; set; }
    }
}

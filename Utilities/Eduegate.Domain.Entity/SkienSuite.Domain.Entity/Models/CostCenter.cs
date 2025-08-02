using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class CostCenter
    {
        public CostCenter()
        {
            this.AccountTransactions = new List<AccountTransaction>();
        }

        public int CostCenterID { get; set; }
        public string CostCenterName { get; set; }
        public virtual ICollection<AccountTransaction> AccountTransactions { get; set; }
    }
}

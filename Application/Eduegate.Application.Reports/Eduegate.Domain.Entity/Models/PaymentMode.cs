using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    public partial class PaymentMode
    {
        public PaymentMode()
        {
            this.AccountTransactionHeads = new List<AccountTransactionHead>();
        }

        public int PaymentModeID { get; set; }
        public string PaymentModeName { get; set; }
        public long? AccountId { get; set; }
        public virtual ICollection<AccountTransactionHead> AccountTransactionHeads { get; set; }
    }
}

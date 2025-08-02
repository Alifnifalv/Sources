using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.ValueObjects
{
    public class AccountTransactionAmountDetail
    {
        public long AccountID { get; set; }
        public decimal TotalAmount { get; set; }
    }
}

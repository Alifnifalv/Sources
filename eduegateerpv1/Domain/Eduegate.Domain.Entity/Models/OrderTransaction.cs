using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class OrderTransaction
    {
        [Key]
        public long TransactionID { get; set; }
        public long RefTransactionOrderID { get; set; }
        public string TransactionType { get; set; }
        public System.DateTime TransactionDate { get; set; }
        public decimal TransactionAmount { get; set; }
        public virtual OrderMaster OrderMaster { get; set; }
    }
}

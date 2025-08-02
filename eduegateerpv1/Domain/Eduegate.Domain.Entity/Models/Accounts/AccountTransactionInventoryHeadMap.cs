using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class AccountTransactionInventoryHeadMap
    {
        [Key]
        public long AccountTransactionInventoryHeadMapIID { get; set; }
        public long TransactionHeadID { get; set; }
        public long AccountTransactionID { get; set; }
        public virtual TransactionHead TransactionHead { get; set; }
        public virtual AccountTransaction AccountTransaction { get; set; }
    }
}

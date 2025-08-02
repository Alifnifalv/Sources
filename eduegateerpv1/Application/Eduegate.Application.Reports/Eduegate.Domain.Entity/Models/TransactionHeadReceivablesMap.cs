using Eduegate.Domain.Entity.Models.Inventory;
using Eduegate.Domain.Entity.Models.Workflows;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("inventory.TransactionHeadReceivablesMaps")]
    public partial class TransactionHeadReceivablesMap
    {
        [Key]
        public long TransactionHeadReceivablesMapIID { get; set; }

        public long ReceivableID { get; set; }

        public long HeadID { get; set; }

        public virtual Receivable Receivable { get; set; }

        public virtual TransactionHead TransactionHead { get; set; }
    }
}

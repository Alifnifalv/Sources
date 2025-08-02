using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("TransactionHeadPayablesMaps", Schema = "inventory")]
    public partial class TransactionHeadPayablesMap
    {
        [Key]
        public long TransactionHeadPayablesMapIID { get; set; }
        public Nullable<long> PayableID { get; set; }
        public Nullable<long> HeadID { get; set; }
        public virtual Payable Payable { get; set; }
        public virtual TransactionHead TransactionHead { get; set; }
    }
}

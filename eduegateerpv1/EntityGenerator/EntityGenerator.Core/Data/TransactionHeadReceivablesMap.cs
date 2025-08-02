using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TransactionHeadReceivablesMaps", Schema = "inventory")]
    public partial class TransactionHeadReceivablesMap
    {
        [Key]
        public long TransactionHeadReceivablesMapIID { get; set; }
        public long ReceivableID { get; set; }
        public long HeadID { get; set; }

        [ForeignKey("HeadID")]
        [InverseProperty("TransactionHeadReceivablesMaps")]
        public virtual TransactionHead Head { get; set; }
        [ForeignKey("ReceivableID")]
        [InverseProperty("TransactionHeadReceivablesMaps")]
        public virtual Receivable Receivable { get; set; }
    }
}

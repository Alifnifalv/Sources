using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.Models
{
    [Table("AccountTransactionReceivablesMaps", Schema = "account")]
    public partial class AccountTransactionReceivablesMap
    {
        [Key]
        public long AccountTransactionReceivablesMapIID { get; set; }
        public long? ReceivableID { get; set; }
        public long? AccountTransactionHeadID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }

        [ForeignKey("AccountTransactionHeadID")]
        [InverseProperty("AccountTransactionReceivablesMaps")]
        public virtual AccountTransactionHead AccountTransactionHead { get; set; }
        [ForeignKey("ReceivableID")]
        [InverseProperty("AccountTransactionReceivablesMaps")]
        public virtual Receivable Receivable { get; set; }
    }
}

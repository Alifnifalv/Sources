using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class IntlPoOrderDetailsCancelledLog
    {
        [Key]
        public int IntlPoOrderDetailsCancelledLogID { get; set; }
        public int RefIntlPoOrderDetailsID { get; set; }
        public byte RefIntlPoBankAccountID { get; set; }
        public short QtyCancelled { get; set; }
        public decimal CancelledTotalUSD { get; set; }
        public short UpdatedBy { get; set; }
        public System.DateTime UpdatedOn { get; set; }
        public virtual IntlPoBankAccount IntlPoBankAccount { get; set; }
        public virtual IntlPoOrderDetail IntlPoOrderDetail { get; set; }
    }
}

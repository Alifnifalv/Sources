using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class IntlPoOrderMasterPayment
    {
        [Key]
        public int IntlPoOrderMasterPaymentID { get; set; }
        public int RefIntlPoOrderMasterID { get; set; }
        public byte RefIntlPoBankAccountID { get; set; }
        public decimal AmountPaidUSD { get; set; }
        public string TransType { get; set; }
        public bool TransActive { get; set; }
        public System.DateTime UpdatedOn { get; set; }
        public short UpdatedBy { get; set; }
        public virtual IntlPoBankAccount IntlPoBankAccount { get; set; }
        public virtual IntlPoOrderMaster IntlPoOrderMaster { get; set; }
    }
}

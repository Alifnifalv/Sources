using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class IntlPoOrderMasterPaymentAdditional
    {
        public int IntlPoOrderMasterPaymentAdditionalID { get; set; }
        public int RefIntlPoOrderMasterID { get; set; }
        public string PaymentType { get; set; }
        public string PaymentText { get; set; }
        public decimal PaymentAmountUSD { get; set; }
        public short CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public bool PaymentStatus { get; set; }
        public virtual IntlPoOrderMaster IntlPoOrderMaster { get; set; }
    }
}

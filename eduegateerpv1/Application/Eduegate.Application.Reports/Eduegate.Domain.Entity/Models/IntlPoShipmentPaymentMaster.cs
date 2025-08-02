using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class IntlPoShipmentPaymentMaster
    {
        public int IntlPoShipmentPaymentMasterID { get; set; }
        public int RefIntlPoShipmentMasterID { get; set; }
        public byte IntlPoShipmentPaymentTypeID { get; set; }
        public string TransType { get; set; }
        public string PaymentText { get; set; }
        public byte RefIntlPoBankAccountID { get; set; }
        public decimal AmountPaidUSD { get; set; }
        public bool Status { get; set; }
        public System.DateTime UpdatedOn { get; set; }
        public short UpdateBy { get; set; }
        public virtual IntlPoBankAccount IntlPoBankAccount { get; set; }
        public virtual IntlPoShipmentMaster IntlPoShipmentMaster { get; set; }
    }
}

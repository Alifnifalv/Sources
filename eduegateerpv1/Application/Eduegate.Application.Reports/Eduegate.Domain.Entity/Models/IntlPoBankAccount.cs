using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class IntlPoBankAccount
    {
        public IntlPoBankAccount()
        {
            this.IntlPoOrderDetailsCancelledLogs = new List<IntlPoOrderDetailsCancelledLog>();
            this.IntlPoOrderMasterPayments = new List<IntlPoOrderMasterPayment>();
            this.IntlPoShipmentPaymentMasters = new List<IntlPoShipmentPaymentMaster>();
        }

        public byte IntlPoBankAccountID { get; set; }
        public string BankAccount { get; set; }
        public bool BankAccountActive { get; set; }
        public virtual ICollection<IntlPoOrderDetailsCancelledLog> IntlPoOrderDetailsCancelledLogs { get; set; }
        public virtual ICollection<IntlPoOrderMasterPayment> IntlPoOrderMasterPayments { get; set; }
        public virtual ICollection<IntlPoShipmentPaymentMaster> IntlPoShipmentPaymentMasters { get; set; }
    }
}

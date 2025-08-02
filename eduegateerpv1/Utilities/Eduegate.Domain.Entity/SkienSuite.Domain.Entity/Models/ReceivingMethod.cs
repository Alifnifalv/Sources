using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ReceivingMethod
    {
        public ReceivingMethod()
        {
            this.Suppliers = new List<Supplier>();
            this.TransactionHeads = new List<TransactionHead>();
        }

        public int ReceivingMethodID { get; set; }
        public string ReceivingMethodName { get; set; }
        public virtual ICollection<Supplier> Suppliers { get; set; }
        public virtual ICollection<TransactionHead> TransactionHeads { get; set; }
    }
}

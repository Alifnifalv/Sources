using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ReturnMethod
    {
        public ReturnMethod()
        {
            this.Suppliers = new List<Supplier>();
            this.TransactionHeads = new List<TransactionHead>();
        }

        public int ReturnMethodID { get; set; }
        public string ReturnMethodName { get; set; }
        public virtual ICollection<Supplier> Suppliers { get; set; }
        public virtual ICollection<TransactionHead> TransactionHeads { get; set; }
    }
}

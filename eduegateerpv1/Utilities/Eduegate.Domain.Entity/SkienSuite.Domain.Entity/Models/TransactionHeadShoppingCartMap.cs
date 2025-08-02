using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class TransactionHeadShoppingCartMap
    {
        public long TransactionHeadShoppingCartMapIID { get; set; }
        public Nullable<long> TransactionHeadID { get; set; }
        public Nullable<long> ShoppingCartID { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<System.DateTime> CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedBy { get; set; }
        public byte[] TimeStamps { get; set; }
        public Nullable<byte> StatusID { get; set; }
        public Nullable<decimal> DeliveryCharge { get; set; }
        public virtual ShoppingCart1 ShoppingCart { get; set; }
        public virtual TransactionHead TransactionHead { get; set; }
        public virtual Status Status { get; set; }
    }
}

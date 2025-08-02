using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ShoppingCartVoucherMap
    {
        public long ShoppingCartVoucherMapIID { get; set; }
        public Nullable<long> ShoppingCartID { get; set; }
        public Nullable<long> VoucherID { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public byte[] TimeStamps { get; set; }
        public Nullable<byte> StatusID { get; set; }
        public virtual ShoppingCart1 ShoppingCart { get; set; }
        public virtual Status Status { get; set; }
        public virtual Voucher Voucher { get; set; }
    }
}

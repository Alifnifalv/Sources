using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    public class ShoppingCartVoucherMap
    {
        public long ShoppingCartVoucherMapID { get; set; }
        public Nullable<long> ShoppingCartID { get; set; }
        public Nullable<long> VoucherID { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<byte> StatusID { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual ShoppingCart ShoppingCart { get; set; }
        public virtual Voucher Voucher { get; set; }
        //public virtual Status Status { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class VoucherMaster
    {
        public VoucherMaster()
        {
            this.VoucherMasterDetails = new List<VoucherMasterDetail>();
        }

        [Key]
        public long VoucherID { get; set; }
        public string VoucherNo { get; set; }
        public string VoucherPin { get; set; }
        public string VoucherType { get; set; }
        public bool IsSharable { get; set; }
        public long CustomerID { get; set; }
        public int VoucherPoint { get; set; }
        public decimal VoucherAmount { get; set; }
        public decimal CurrentBalance { get; set; }
        public int Validity { get; set; }
        public System.DateTime ValidTillDate { get; set; }
        public System.DateTime GenerateDate { get; set; }
        public long RefOrderID { get; set; }
        public bool IsRedeemed { get; set; }
        public Nullable<long> RefOrderItemID { get; set; }
        public Nullable<byte> VoucherDiscount { get; set; }
        public Nullable<bool> isRefund { get; set; }
        public Nullable<int> isRefundForOrder { get; set; }
        public Nullable<decimal> MinAmount { get; set; }
        public virtual ICollection<VoucherMasterDetail> VoucherMasterDetails { get; set; }
    }
}
